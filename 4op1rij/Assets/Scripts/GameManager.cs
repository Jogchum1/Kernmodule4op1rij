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

    public BoardCanvas board;

    public Column[] columns;
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
        columns = FindObjectsOfType<Column>();
    }

    private void Update()
    {
        //Debug.Log(playerTurn);
    }

    
    public void Test()
    {
        //playerTurn = !playerTurn;
        Debug.Log("TESSSSSSSSSST");
    }
   
}
   
