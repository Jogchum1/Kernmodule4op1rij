﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
using Unity.Collections;
using Unity.Networking.Transport.Utilities;
using System;

public delegate void ServerMessageHandler(Server server, NetworkConnection con, MessageHeader header);
public delegate void ClientMessageHandler(Client client, MessageHeader header);

public enum NetworkMessageType
{
    HANDSHAKE,
    HANDSHAKE_RESPONSE,
    CHAT_MESSAGE,
    CHAT_QUIT,
    NETWORK_SPAWN,
    NETWORK_DESTROY,
    NETWORK_UPDATE_POSITION,
    INPUT_UPDATE,                        // uint networkId, InputUpdate (float, float, bool)
    PING,
    PONG,
    RPC_MESSAGE,
    SPAWN_COIN,
    PLAYER_TURN,
    UPDATE_COLUMN,
    END_ROUND
}

public enum MessageType
{
    MESSAGE,
    JOIN,
    QUIT
}

public class PingPong
{
    public float lastSendTime = 0;
    public int status = -1;
    public string name = ""; // because of weird issues...
}

public static class NetworkMessageInfo
{
    public static Dictionary<NetworkMessageType, System.Type> TypeMap = new Dictionary<NetworkMessageType, System.Type> {
            { NetworkMessageType.HANDSHAKE,                 typeof(HandshakeMessage) },
            { NetworkMessageType.HANDSHAKE_RESPONSE,        typeof(HandshakeResponseMessage) },
            { NetworkMessageType.CHAT_MESSAGE,              typeof(ChatMessage) },
            { NetworkMessageType.CHAT_QUIT,                 typeof(ChatQuitMessage) },
            { NetworkMessageType.NETWORK_SPAWN,             typeof(SpawnMessage) },
            { NetworkMessageType.NETWORK_DESTROY,           typeof(DestroyMessage) },
            { NetworkMessageType.NETWORK_UPDATE_POSITION,   typeof(UpdatePositionMessage) },
            { NetworkMessageType.INPUT_UPDATE,              typeof(InputUpdateMessage) },
            { NetworkMessageType.PING,                      typeof(PingMessage) },
            { NetworkMessageType.PONG,                      typeof(PongMessage) },
            { NetworkMessageType.RPC_MESSAGE,               typeof(RPCMessage) },
            { NetworkMessageType.SPAWN_COIN,                typeof(SpawnCoinMessage) },
            { NetworkMessageType.PLAYER_TURN,               typeof(PlayerTurnMessage)},
            { NetworkMessageType.UPDATE_COLUMN,             typeof(UpdateColumnMessage)},
            { NetworkMessageType.END_ROUND,                 typeof(EndRoundMessage)}
        };
}

public class Server : MonoBehaviour
{
    static Dictionary<NetworkMessageType, ServerMessageHandler> networkMessageHandlers = new Dictionary<NetworkMessageType, ServerMessageHandler> {
            { NetworkMessageType.HANDSHAKE,     HandleClientHandshake },
            { NetworkMessageType.CHAT_MESSAGE,  HandleClientMessage },
            { NetworkMessageType.CHAT_QUIT,     HandleClientExit },
            { NetworkMessageType.INPUT_UPDATE,  HandleClientInput },
            { NetworkMessageType.PONG,          HandleClientPong },
            { NetworkMessageType.RPC_MESSAGE,   HandleClientRPC },
            { NetworkMessageType.SPAWN_COIN,    HandleClientCoinSpawn },
            { NetworkMessageType.UPDATE_COLUMN, HandleClientColumnUpdate },
            { NetworkMessageType.END_ROUND,     HandleClientEndRound}
            //{ NetworkMessageType.PLAYER_TURN,   HandleClientTurn}
        };

    

    public NetworkDriver m_Driver;
    public NetworkPipeline m_Pipeline;
    private NativeList<NetworkConnection> m_Connections;

    private Dictionary<NetworkConnection, string> nameList = new Dictionary<NetworkConnection, string>();
    private Dictionary<NetworkConnection, NetworkedPlayer> playerInstances = new Dictionary<NetworkConnection, NetworkedPlayer>();
    private Dictionary<NetworkConnection, PingPong> pongDict = new Dictionary<NetworkConnection, PingPong>();

    public GameManager gameManager;

    public ChatCanvas chat;
    public NetworkManager networkManager;
    public BoardCanvas board;
    public static int playerCount;

    void Start()
    {
        if (!ClientServerSelection.isServer)
        {
            this.enabled = false;
        }

        // Create Driver
        m_Driver = NetworkDriver.Create(new ReliableUtility.Parameters { WindowSize = 32 });
        m_Pipeline = m_Driver.CreatePipeline(typeof(ReliableSequencedPipelineStage));

        // Open listener on server port
        NetworkEndPoint endpoint = NetworkEndPoint.AnyIpv4;
        endpoint.Port = 1511;
        if (m_Driver.Bind(endpoint) != 0)
            Debug.Log("Failed to bind to port 1511");
        else
            m_Driver.Listen();

        m_Connections = new NativeList<NetworkConnection>(64, Allocator.Persistent);
    }

    // Write this immediately after creating the above Start calls, so you don't forget
    //  Or else you well get lingering thread sockets, and will have trouble starting new ones!
    void OnDestroy()
    {
        m_Driver.Dispose();
        m_Connections.Dispose();
    }

    void Update()
    {
        // This is a jobified system, so we need to tell it to handle all its outstanding tasks first
        m_Driver.ScheduleUpdate().Complete();

        // Clean up connections, remove stale ones
        for (int i = 0; i < m_Connections.Length; i++)
        {
            if (!m_Connections[i].IsCreated)
            {
                m_Connections.RemoveAtSwapBack(i);
                // This little trick means we can alter the contents of the list without breaking/skipping instances
                --i;
            }
        }

        // Accept new connections
        NetworkConnection c;
        while ((c = m_Driver.Accept()) != default(NetworkConnection))
        {
            m_Connections.Add(c);
            // Debug.Log("Accepted a connection");
        }

        DataStreamReader stream;
        for (int i = 0; i < m_Connections.Length; i++)
        {
            if (!m_Connections[i].IsCreated)
                continue;

            // Loop through available events
            NetworkEvent.Type cmd;
            while ((cmd = m_Driver.PopEventForConnection(m_Connections[i], out stream)) != NetworkEvent.Type.Empty)
            {
                if (cmd == NetworkEvent.Type.Data)
                {
                    // First UInt is always message type (this is our own first design choice)
                    NetworkMessageType msgType = (NetworkMessageType)stream.ReadUShort();

                    // Create instance and deserialize
                    MessageHeader header = (MessageHeader)System.Activator.CreateInstance(NetworkMessageInfo.TypeMap[msgType]);
                    header.DeserializeObject(ref stream);

                    if (networkMessageHandlers.ContainsKey(msgType))
                    {
                            networkMessageHandlers[msgType].Invoke(this, m_Connections[i], header);
                        //try
                        //{
                        //}
                        //catch
                        //{
                        //    Debug.LogError($"Badly formatted message received: {msgType}");
                        //}
                    }
                    else
                    {
                        Debug.LogWarning($"Unsupported message type received: {msgType}", this);
                    }
                }
            }
        }

        // Ping Pong stuff for timeout disconnects
        for (int i = 0; i < m_Connections.Length; i++)
        {
            if (!m_Connections[i].IsCreated)
                continue;

            if (pongDict.ContainsKey(m_Connections[i]))
            {
                if (Time.time - pongDict[m_Connections[i]].lastSendTime > 5f)
                {
                    pongDict[m_Connections[i]].lastSendTime = Time.time;
                    if (pongDict[m_Connections[i]].status == 0)
                    {
                        // Remove from all the dicts, save name / id for msg

                        // FIXME: for some reason, sometimes this isn't in the list?
                        if (nameList.ContainsKey(m_Connections[i]))
                        {
                            nameList.Remove(m_Connections[i]);
                        }

                        uint destroyId = playerInstances[m_Connections[i]].networkId;
                        networkManager.DestroyWithId(destroyId);
                        playerInstances.Remove(m_Connections[i]);

                        string name = pongDict[m_Connections[i]].name;
                        pongDict.Remove(m_Connections[i]);

                        // Disconnect this player
                        m_Connections[i].Disconnect(m_Driver);

                        // Build messages
                        string msg = $"{name} has been Disconnected (connection timed out)";
                        chat.NewMessage(msg, ChatCanvas.leaveColor);

                        ChatMessage quitMsg = new ChatMessage
                        {
                            message = msg,
                            messageType = MessageType.QUIT
                        };

                        DestroyMessage destroyMsg = new DestroyMessage
                        {
                            networkId = destroyId
                        };

                        // Broadcast
                        SendBroadcast(quitMsg);
                        SendBroadcast(destroyMsg, m_Connections[i]);

                        // Clean up
                        m_Connections[i] = default;
                    }
                    else
                    {
                        pongDict[m_Connections[i]].status -= 1;
                        PingMessage pingMsg = new PingMessage();
                        SendUnicast(m_Connections[i], pingMsg);
                    }
                }
            }
            else if (nameList.ContainsKey(m_Connections[i]))
            { //means they've succesfully handshaked
                PingPong ping = new PingPong();
                ping.lastSendTime = Time.time;
                ping.status = 3;    // 3 retries
                ping.name = nameList[m_Connections[i]];
                pongDict.Add(m_Connections[i], ping);

                PingMessage pingMsg = new PingMessage();
                SendUnicast(m_Connections[i], pingMsg);
            }
        }
    }

    public void SendUnicast(NetworkConnection connection, MessageHeader header, bool realiable = true)
    {
        DataStreamWriter writer;
        int result = m_Driver.BeginSend(realiable ? m_Pipeline : NetworkPipeline.Null, connection, out writer);
        if (result == 0)
        {
            header.SerializeObject(ref writer);
            m_Driver.EndSend(writer);
        }
    }

    public void SendBroadcast(MessageHeader header, NetworkConnection toExclude = default, bool realiable = true)
    {
        for (int i = 0; i < m_Connections.Length; i++)
        {
            if (!m_Connections[i].IsCreated || m_Connections[i] == toExclude)
                continue;

            DataStreamWriter writer;
            int result = m_Driver.BeginSend(realiable ? m_Pipeline : NetworkPipeline.Null, m_Connections[i], out writer);
            if (result == 0)
            {
                header.SerializeObject(ref writer);
                m_Driver.EndSend(writer);
            }
        }
    }

    // Static handler functions
    //  - Client handshake                  (DONE)
    //  - Client chat message               (DONE)
    //  - Client chat exit                  (DONE)
    //  - Input update

    static void HandleClientHandshake(Server serv, NetworkConnection connection, MessageHeader header)
    {
        HandshakeMessage message = header as HandshakeMessage;

        // Add to list
        serv.nameList.Add(connection, message.name);
        string msg = $"{message.name.ToString()} has joined the chat.";
        serv.chat.NewMessage(msg, ChatCanvas.joinColor);

        ChatMessage chatMsg = new ChatMessage
        {
            messageType = MessageType.JOIN,
            message = msg
        };

        // Send all clients the chat message
        serv.SendBroadcast(chatMsg);

        // spawn a non-local, server player
        GameObject player;
        uint networkId = 0;
        if (serv.networkManager.SpawnWithId(NetworkSpawnObject.PLAYER, NetworkManager.NextNetworkID, out player))
        {
            // Get and setup player instance
            NetworkedPlayer playerInstance = player.GetComponent<NetworkedPlayer>();
            Debug.Log("HandleClientHandshake");
            playerInstance.isServer = true;
            playerInstance.isLocal = false;

            GameManager manager = FindObjectOfType<GameManager>();
            manager.isServer = true;
            manager.isLocal = false;
            manager.SetGameManager();
            networkId = playerInstance.networkId;
            playerCount++;
            Debug.Log(playerCount);
            serv.playerInstances.Add(connection, playerInstance);

            if(playerCount == 1)
            {
                PlayerTurnMessage turnMSG = new PlayerTurnMessage
                {
                    playerTurn = true,                    
                };
                serv.SendUnicast(connection, turnMSG);
            }
            if (playerCount == 2)
            {
                PlayerTurnMessage turnMSG = new PlayerTurnMessage
                {
                    playerTurn = false,

                };

                serv.SendUnicast(connection, turnMSG);
            }
            // Send spawn local player back to sender
            HandshakeResponseMessage responseMsg = new HandshakeResponseMessage
            {
                message = $"Welcome {message.name.ToString()}!",
                networkId = playerInstance.networkId
            };


            serv.SendUnicast(connection, responseMsg);

            
        }
        else
        {
            Debug.LogError("Could not spawn player instance");
        }

        // Send all existing players to this player
        foreach (KeyValuePair<NetworkConnection, NetworkedPlayer> pair in serv.playerInstances)
        {
            if (pair.Key == connection) continue;

            SpawnMessage spawnMsg = new SpawnMessage
            {
                networkId = pair.Value.networkId,
                objectType = NetworkSpawnObject.PLAYER
            };

            serv.SendUnicast(connection, spawnMsg);
        }

        // Send creation of this player to all existing players
        if (networkId != 0)
        {
            SpawnMessage spawnMsg = new SpawnMessage
            {
                networkId = networkId,
                objectType = NetworkSpawnObject.PLAYER
            };
            serv.SendBroadcast(spawnMsg, connection);
        }
        else
        {
            Debug.LogError("Invalid network id for broadcasting creation");
        }
    }

    static void HandleClientMessage(Server serv, NetworkConnection connection, MessageHeader header)
    {
        ChatMessage receivedMsg = header as ChatMessage;

        if (serv.nameList.ContainsKey(connection))
        {
            string msg = $"{serv.nameList[connection]}: {receivedMsg.message}";
            serv.chat.NewMessage(msg, ChatCanvas.chatColor);

            receivedMsg.message = msg;

            // forward message to all clients
            serv.SendBroadcast(receivedMsg);
        }
        else
        {
            Debug.LogError($"Received message from unlisted connection: {receivedMsg.message}");
        }
    }

    static void HandleClientExit(Server serv, NetworkConnection connection, MessageHeader header)
    {
        ChatQuitMessage quitMsg = header as ChatQuitMessage;

        if (serv.nameList.ContainsKey(connection))
        {
            string msg = $"{serv.nameList[connection]} has left the chat.";
            serv.chat.NewMessage(msg, ChatCanvas.leaveColor);

            // Clean up
            serv.nameList.Remove(connection);
            // if you join and quit quickly, might not be in this dict yet
            if (serv.pongDict.ContainsKey(connection))
            {
                serv.pongDict.Remove(connection);
            }

            connection.Disconnect(serv.m_Driver);

            uint destroyId = serv.playerInstances[connection].networkId;
            Destroy(serv.playerInstances[connection].gameObject);
            serv.playerInstances.Remove(connection);

            // Build messages
            ChatMessage chatMsg = new ChatMessage
            {
                message = msg,
                messageType = MessageType.QUIT
            };

            DestroyMessage destroyMsg = new DestroyMessage
            {
                networkId = destroyId
            };

            // Send Messages to all other clients
            serv.SendBroadcast(chatMsg, connection);
            serv.SendBroadcast(destroyMsg, connection);
        }
        else
        {
            Debug.LogError("Received exit from unlisted connection");
        }
    }

    static void HandleClientInput(Server serv, NetworkConnection connection, MessageHeader header)
    {
        InputUpdateMessage inputMsg = header as InputUpdateMessage;

        if (serv.playerInstances.ContainsKey(connection))
        {
            if (serv.playerInstances[connection].networkId == inputMsg.networkId)
            {
                serv.playerInstances[connection].UpdateInput(inputMsg.input);
            }
            else
            {
                Debug.LogError("NetworkID Mismatch for Player Input");
            }
        }
        else
        {
            Debug.LogError("Received player input from unlisted connection");
        }
    }

    static void HandleClientPong(Server serv, NetworkConnection connection, MessageHeader header)
    {
        // Debug.Log("PONG");
        serv.pongDict[connection].status = 3;   //reset retry count
    }
    private static void HandleClientRPC(Server serv, NetworkConnection connection, MessageHeader header)
    {
        RPCMessage rpcMsg = header as RPCMessage;
        if (serv.playerInstances.ContainsKey(connection))
        {
            if (serv.playerInstances[connection].networkId == rpcMsg.target)
            {
                serv.playerInstances[connection].Fire(rpcMsg.position, rpcMsg.rotation, rpcMsg.columnNumber);
            }
            else
            {
                Debug.LogError("NetworkID Mismatch for Player Input");
            }
        }
        else
        {
            Debug.LogError("Received player input from unlisted connection");
        }
    }

    private static void HandleClientCoinSpawn(Server serv, NetworkConnection connection, MessageHeader header)
    {
        SpawnCoinMessage receivedCoinMSG = header as SpawnCoinMessage;

        if (serv.nameList.ContainsKey(connection))
        {
            Debug.Log("Spawn coin");
            serv.board.NewCoin(receivedCoinMSG.spawnPos, receivedCoinMSG.targetPos, receivedCoinMSG.playerID, receivedCoinMSG.column);
            serv.gameManager.placedCoin = true;
        }
        
    }

    private static void HandleClientColumnUpdate(Server serv, NetworkConnection con, MessageHeader header)
    {
        UpdateColumnMessage columnMSG = header as UpdateColumnMessage;

        
        foreach (Column col in serv.gameManager.columns)
        {
            if(col.col == columnMSG.columnNumber)
            {
                col.UpdateTargetLocation();
            }
        }
        //Debug.Log("server msg");
    }

    private static void HandleClientEndRound(Server server, NetworkConnection con, MessageHeader header)
    {
        EndRoundMessage roundMSG = header as EndRoundMessage;

        server.gameManager.placedCoin = false;
    }

    //private static void HandleClientTurn(Server serv, NetworkConnection connection, MessageHeader header)
    //{
    //    PlayerTurnMessage turnMSG = header as PlayerTurnMessage;
    //    Debug.Log("turnmsg");

    //    if (serv.playerInstances.ContainsKey(connection))
    //    {
    //        if (serv.playerInstances[connection].networkId == turnMSG.playerID)
    //        {
    //            serv.playerInstances[connection].playerTurn = turnMSG.playerTurn;
    //        }
    //        else
    //        {
    //            Debug.LogError("NetworkID Mismatch for Player Input");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("Received player input from unlisted connection");
    //    }

    //    //serv.SendBroadcast(turnMSG);
    //}

}

