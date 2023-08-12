using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.UI;

    
public class BoardCanvas : MonoBehaviour
{
    public GameObject coin;
    public Transform parent;
    public NetworkManager networkManager;
    public GameManager gameManager;
    public Board board;
    private RectTransform rect;
    public Canvas canvas;

    public int heightOfBoard = 6;
    public int lenghtOfBoard = 7;

    private bool player1 = true;
    public bool serverFirst = false;

    int[,] boardState; //0 is empty, 1 is player 1, 2 is player 2

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        boardState = new int[lenghtOfBoard, heightOfBoard];
    }
    public void NewCoin(Vector3 spawnPos, Vector3 targetPos, uint playerID, uint column)
    {
        if (UpdateBoardState((int)column))
        {
            GameObject newCoin;
            if(networkManager.SpawnWithId(NetworkSpawnObject.COIN, playerID, out newCoin)){
                newCoin.transform.SetParent(board.transform, false);
                Debug.Log(playerID);
                newCoin.GetComponent<MoveCoin>().spawnPos = spawnPos;
                newCoin.GetComponent<MoveCoin>().targetPosition = targetPos;
                if (player1)
                {
                    newCoin.GetComponent<Image>().color = Color.red;
                    player1 = false;
                }
                else
                {
                    newCoin.GetComponent<Image>().color = Color.green;
                    player1 = true;
                }

                foreach (Column col in gameManager.columns)
                {
                    if(col.col == column)
                    {
                        //col.targetLocation = targetPos;
                        col.targetLocation = new Vector3(targetPos.x, targetPos.y + 38f, targetPos.z);
                    }
                }
            }
        }
    }

    bool UpdateBoardState(int column)
    {
        if (gameManager.isLocal)
        {
            Debug.Log(column);
            for (int row = 0; row < heightOfBoard; row++)
            {
                if (boardState[column, row] == 0)
                {
                    if (player1)
                    {
                        boardState[column, row] = 1;
                        if (DidWin(1))
                        {
                            Debug.Log("Player 1 won client");
                            gameManager.gameOver = true;
                            gameManager.EndGame(1);
                        }
                    }
                    else
                    {
                        boardState[column, row] = 2;
                        if (DidWin(2))
                        {
                            Debug.Log("Player 2 won client");
                            gameManager.gameOver = true;
                            gameManager.EndGame(2);
                        }

                    }
                    Debug.Log("Piece is being spawned at {" + column + ", " + row + "}");
                    return true;
                }
            }
            Debug.Log(column + "is full");
            return false;
        }
        else if(gameManager.isServer)
        {
            for (int row = 0; row < heightOfBoard; row++)
            {
                if (boardState[column, row] == 0)
                {
                    if (serverFirst == false)
                    {
                        if (player1)
                        {
                            boardState[column, row] = 1;
                            serverFirst = true;
                            if (DidWin(1))
                            {
                                Debug.Log("Player 1 won server");
                                gameManager.gameOver = true;
                                gameManager.EndGame(1);
                            }
                        }
                        else
                        {
                            boardState[column, row] = 2;
                            serverFirst = true;
                            if (DidWin(2))
                            {
                                Debug.Log("Player 2 won server");
                                gameManager.gameOver = true;
                                gameManager.EndGame(2);

                            }
                        }
                        Debug.Log("Piece is being spawned at {" + column + ", " + row + "}");
                        return true;
                    }
                    else
                    {
                        Debug.Log("server first");
                        serverFirst = false;
                        return true;
                    }
                }
            }
            Debug.Log(column + "is full");
            return false;
        }
        else
        {
            Debug.Log("wtf");
            return false;
        }
    }

    public bool DidWin(int playerNumber)
    {
        //Horizontal
        for (int x = 0; x < lenghtOfBoard - 3; x++)
        {
            for (int y = 0; y < heightOfBoard; y++)
            {
                if (boardState[x, y] == playerNumber && boardState[x + 1, y] == playerNumber && boardState[x + 2, y] == playerNumber && boardState[x + 3, y] == playerNumber)
                {
                    return true;
                }
            }
        }
        //vertical
        for (int x = 0; x < lenghtOfBoard; x++)
        {
            for (int y = 0; y < heightOfBoard - 3; y++)
            {
                if (boardState[x, y] == playerNumber && boardState[x, y + 1] == playerNumber && boardState[x, y + 2] == playerNumber && boardState[x, y + 3] == playerNumber)
                {
                    return true;
                }
            }
        }
        //y=x line
        for (int x = 0; x < lenghtOfBoard - 3; x++)
        {
            for (int y = 0; y < heightOfBoard - 3; y++)
            {
                if (boardState[x, y] == playerNumber && boardState[x + 1, y + 1] == playerNumber && boardState[x + 2, y + 2] == playerNumber && boardState[x + 3, y + 3] == playerNumber)
                {
                    return true;
                }
            }
        }
        //y=-x line
        for (int x = 0; x < lenghtOfBoard - 3; x++)
        {
            for (int y = 0; y < heightOfBoard - 3; y++)
            {
                if (boardState[x, y + 3] == playerNumber && boardState[x + 1, y + 2] == playerNumber && boardState[x + 2, y + 1] == playerNumber && boardState[x + 3, y] == playerNumber)
                {
                    return true;
                }
            }
        }

        return false;
    }

    
}
