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
    public bool playerTurn = false;
    public bool gameOver = false;
    public bool placedCoin = false;

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
        CheckResult();
    }

    public void CheckResult() {

        if (isServer)
        {
            if(placedCoin == true)
            {
                //Check result stuff, if 4 op n rij enzo



                EndRoundMessage roundMSG = new EndRoundMessage
                {
                    hasWon = this.gameOver,

                };

                server.SendBroadcast(roundMSG);
            }

        }
    }
    
    public void EndOfRound(EndRoundMessage msg)
    {
        Debug.Log("test of dit wordt gedaan");
        if (msg.hasWon)
        {
            Debug.Log("you've won");
        }
        else
        {
            if (playerTurn)
            {
                playerTurn = false;
            }
            else
            {
                playerTurn = true;
            }
        }

        placedCoin = false;
    }
   
}
   
