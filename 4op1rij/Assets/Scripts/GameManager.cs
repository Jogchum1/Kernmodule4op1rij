using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isLocal;
    public bool isServer;

    private Client client;
    private Server server;

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
}
