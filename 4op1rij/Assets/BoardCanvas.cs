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
    public Board board;
    private RectTransform rect;
    public Canvas canvas;
    private void Start()
    {
        rect = GetComponent<RectTransform>();
    }
    public void NewCoin(Vector3 spawnPos, Vector3 targetPos, uint playerID)
    {
        GameObject newCoin;
        if(networkManager.SpawnWithId(NetworkSpawnObject.COIN, playerID, out newCoin)){
            newCoin.transform.SetParent(board.transform, false);
            
            newCoin.GetComponent<MoveCoin>().spawnPos = spawnPos;
            newCoin.GetComponent<MoveCoin>().targetPosition = targetPos;
        }

        //GameObject newCoin = Instantiate(coin);
        //newCoin.transform.SetParent(parent);
        //Debug.Log("Board");
        //newCoin.GetComponent<NetworkManager>();
    }
}
