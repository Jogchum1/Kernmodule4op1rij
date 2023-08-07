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

    public bool playerTurn = true;
    public BoardCanvas board;
   

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
        board = FindObjectOfType<BoardCanvas>();
    }

    private void Update()
    {

    }

    public void SpawnCoin()
    {

    }

   
}
   
