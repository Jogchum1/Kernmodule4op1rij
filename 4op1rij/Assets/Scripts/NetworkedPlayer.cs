using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct InputUpdate
{
    public float horizontal, vertical;
    public bool fire, jump;

    public InputUpdate(float h, float v, bool f, bool j)
    {
        horizontal = h;
        vertical = v;
        fire = f;
        jump = j;
    }
}

public class NetworkedPlayer : NetworkedBehaviour
{
    public bool isLocal = false;
    public bool isServer = false;
    public GameObject coin;

    public Camera camera;

    float refireTimer = 0;
    bool canJump = true;
    float yVel = 0;
    InputUpdate input;

    Client client;
    Server server;

    private void Start()
    {
        if (isLocal)
        {
            camera = Camera.main;
            client = FindObjectOfType<Client>();
        }
        if (isServer)
        {
            server = FindObjectOfType<Server>();
        }
    }

    private void Update()
    {
        if (isLocal)
        {
            InputUpdate update = new InputUpdate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), Input.GetMouseButtonDown(0), Input.GetKey(KeyCode.Space));

            // Send input update to server
            InputUpdateMessage inputMsg = new InputUpdateMessage
            {
                input = update,
                networkId = this.networkId
            };
            client.SendPackedMessage(inputMsg);

            if (Input.GetMouseButtonDown(0))
            {
                //Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
                //Vector3 dir = Vector3.forward;

                //GameObject obj;
                //uint id = NetworkManager.NextNetworkID;
                //Debug.Log(id + " is id");
                //if (client.networkManager.SpawnWithId(NetworkSpawnObject.COIN, id, out obj))
                //{
                //    obj.GetComponent<NetworkedBullet>().isServer = false;

                //    SpawnMessage msg = new SpawnMessage
                //    {
                //        objectType = NetworkSpawnObject.COIN,
                //        networkId = id,
                //        position = pos,
                //        rotation = dir
                //    };
                //    client.SendPackedMessage(msg);
                //}


                client.CallOnServerObject("Fire", this, transform.position, transform.forward);

                // Code for this client function (note the use of the params keyword)
                /* 
                public void CallOnServerObject( string function, NetworkedBehaviour target, params object[] data ) {
                    RPCMessage rpc = new RPCMessage {
                        target = target,
                        methodName = function,
                        data = data
                    };

                    SendPackedMessage(rpc);
                }
                */

                //client.CallOnServerObject("Fire", this, transform.position, transform.forward);

            }
        }

        if (isServer)
        {
            refireTimer -= Time.deltaTime;
            if (transform.position.y > 0)
            {
                yVel -= Time.deltaTime * 9.81f;
            }
            else
            {
                yVel = 0;
                Vector3 p = transform.position;
                p.y = 0;
                transform.position = p;
                canJump = true;
            }

            // handle input state
            if (canJump && input.jump)
            {
                yVel = 10;
                canJump = false;
            }

            // TODO: Handle input.fire
            //		- TODO: Include object position & rotation when spawning

            transform.Translate(input.horizontal * Time.deltaTime * 5, yVel * Time.deltaTime, input.vertical * Time.deltaTime * 5);

            // TODO: Send position update to all clients (maybe not every frame!)
            if (Time.frameCount % 3 == 0)
            { // assuming 60fps, so 20fps motion updates
              // We could consider sending this over a non-reliable pipeline
                UpdatePositionMessage posMsg = new UpdatePositionMessage
                {
                    networkId = this.networkId,
                    position = transform.position,
                    rotation = transform.eulerAngles
                };

                server.SendBroadcast(posMsg);
            }
        }
    }
    public void Fire(Vector3 pos, Vector3 dir)
    {
        // Debug.Log($"Called: {pos} {dir}");

        GameObject obj;
        uint id = NetworkManager.NextNetworkID;
        if (server.networkManager.SpawnWithId(NetworkSpawnObject.COIN, id, out obj))
        {
            obj.GetComponent<NetworkedBullet>().isServer = true;

            SpawnMessage msg = new SpawnMessage
            {
                objectType = NetworkSpawnObject.COIN,
                networkId = id,
                position = pos,
                rotation = dir
            };

            server.SendBroadcast(msg);
        }
    }

    public void UpdateInput(InputUpdate received)
    {
        input.horizontal = received.horizontal;
        input.vertical = received.vertical;
        input.fire = received.fire;
        input.jump = received.jump;
    }
}
