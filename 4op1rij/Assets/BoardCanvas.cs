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
    public void NewCoin(Vector3 pos, uint playerID)
    {
        GameObject newCoin;
        if(networkManager.SpawnWithId(NetworkSpawnObject.COIN, playerID, out newCoin)){
            newCoin.transform.SetParent(parent);
        }

        //GameObject newCoin = Instantiate(coin);
        //newCoin.transform.SetParent(parent);
        //Debug.Log("Board");
        //newCoin.GetComponent<NetworkManager>();
    }
}
