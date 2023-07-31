using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedCoin : NetworkedBehaviour
{
    public bool isLocal = false;
    public bool isServer = false;

    Server server;
    Client client;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        isLocal = gameManager.isLocal;
        isServer = gameManager.isServer;


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
}
