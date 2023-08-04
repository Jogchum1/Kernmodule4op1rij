using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;

public class GameManager : NetworkedBehaviour
{
    public bool isLocal;
    public bool isServer;

    private Client client;
    private Server server;

    public bool player1Turn = true;
    public NetworkedPlayer player1test;
    public NetworkedPlayer player2test;

    public void AwakeObject()
    {

        if (isLocal)
        {
            client = FindObjectOfType<Client>();
        }

        if (isServer)
        {
            server = FindObjectOfType<Server>();
        }
    }

    private void Update()
    {

    }

    public void CreateMatch(NetworkedPlayer player1, NetworkedPlayer player2)
    {

    }
}
   
