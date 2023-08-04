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
    public GameManager gameManager;

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
                PlaceCoin();
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

    public void PlaceCoin()
    {
        //something check if your turn?

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z * -1));
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        //RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (!hit.collider)
            Debug.Log("Nothing");

        Debug.Log(hit.collider.name);


        if (hit.collider.CompareTag("Press"))
        {
            //Check bounds
            //if (hit.collider.gameObject.GetComponent<Column>().targetLocation.y > 1.5f) return;

            Vector3 spawnPos = hit.collider.gameObject.GetComponent<Column>().spawnLocation;
            Vector3 targetPos = hit.collider.gameObject.GetComponent<Column>().targetLocation;
            Debug.Log(spawnPos + "spawnpos");
            client.CallOnServerObject("Fire", this, spawnPos, targetPos);
            hit.collider.gameObject.GetComponent<Column>().targetLocation = new Vector3(targetPos.x, targetPos.y + 38f, targetPos.z);

        }
    }

    public void Fire(Vector3 pos, Vector3 target)
    {
        // Debug.Log($"Called: {pos} {dir}");
        Debug.Log("Test");
        GameObject obj;
        uint id = NetworkManager.NextNetworkID;
        SpawnCoinMessage msg = new SpawnCoinMessage
        {
            playerID = id,
            objectType = NetworkSpawnObject.COIN,
            spawnPos = pos,
            targetPos = target
        };

        server.SendBroadcast(msg);
        //if (server.networkManager.SpawnWithId(NetworkSpawnObject.COIN, id, out obj))
        //{
        //    obj.GetComponent<NetworkedCoin>().isServer = true;

        //    SpawnMessage msg = new SpawnMessage
        //    {
        //        objectType = NetworkSpawnObject.COIN,
        //        networkId = id,
        //        position = pos,
        //        rotation = dir
        //    };

        //}
    }

    public void UpdateInput(InputUpdate received)
    {
        input.horizontal = received.horizontal;
        input.vertical = received.vertical;
        input.fire = received.fire;
        input.jump = received.jump;
    }
}
