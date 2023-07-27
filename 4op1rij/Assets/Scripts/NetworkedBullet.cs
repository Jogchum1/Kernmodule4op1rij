using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedBullet : NetworkedBehaviour
{
    // TODO: Make sure this is true when creating this object on the server

    float lifeTime = 5f;

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
    // Update is called once per frame
    void Update()
    {
        // just fly in fired direction
        transform.Translate(transform.forward * Time.deltaTime * 10);

        if (isServer) {
            // (optional) TODO: raycast and handle collision with player objects?
            //  if hit
            //      TODO: Destroy bullet, broadcast destroy, (optional) send HitMessage (need to make it first!)


            // TODO: Decrement lifeTime
            // if < 0
            //      TODO: Broadcast Destroy

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
}
