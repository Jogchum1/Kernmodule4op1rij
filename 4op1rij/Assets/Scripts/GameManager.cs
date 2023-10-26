using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
using UnityEngine.UI;
public class GameManager : NetworkedBehaviour
{
    public bool isLocal;
    public bool isServer;
    public Text winText;


    private Client client;
    private Server server;

    public BoardCanvas board;
    public bool playerTurn = false;
    public bool gameOver = false;
    public bool placedCoin = false;

    public Column[] columns;
    public HandleScore scoreHandler;
    public void SetGameManager()
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

                EndRoundMessage roundMSG = new EndRoundMessage
                {
                    hasWon = this.gameOver,
                };

                Debug.Log("server send end round message");
                server.SendBroadcast(roundMSG);
                this.placedCoin = false;
            }

        }
        
        
    }

    public void CheckResult(int playerNumber)
    {
        if (board.DidWin(playerNumber))
        {
            Debug.Log(playerNumber + " WON!");
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

    public void EndGame(int player)
    {
        this.winText.gameObject.SetActive(true);
        foreach (Column col in columns)
        {
            col.gameObject.SetActive(false);
        }

        if (player == 1)
        {
            winText.text = "Player red won!";
            if(this.playerTurn == false)
            {
                if(isLocal)
                    this.scoreHandler.StartScoreInsert(0);
            }
            else
            {
                if (isLocal)
                    this.scoreHandler.StartScoreInsert(1);
            }
        }
        if(player == 2)
        {
            winText.text = "Player green won!";
            if (this.playerTurn == false)
            {
                if (isLocal)
                    this.scoreHandler.StartScoreInsert(0);
            }
            else
            {
                if (isLocal)
                    this.scoreHandler.StartScoreInsert(1);
            }
        }

    }

}
   
