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
        for (int row = 0; row < heightOfBoard; row++)
        {
            if(boardState[column, row] == 0)
            {
                if (player1)
                {
                    boardState[column, row] = 1;
                }
                else
                {
                    boardState[column, row] = 2;
                }
                Debug.Log("Piece is being spawned at {" + column + ", " + row + "}");
                return true;
            }
        }
        Debug.Log(column + "is full");
        return false;
    }

    
}
