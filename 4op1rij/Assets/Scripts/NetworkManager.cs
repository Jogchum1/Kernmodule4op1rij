using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private static uint nextNetworkId = 0;
    public static uint NextNetworkID => ++nextNetworkId;

    [SerializeField]
    private NetworkSpawnInfo spawnInfo;
    private Dictionary<uint, GameObject> networkedReferences = new Dictionary<uint, GameObject>();

    public bool GetReference( uint id, out GameObject obj ) {
        obj = null;
        if (networkedReferences.ContainsKey(id)) {
            obj = networkedReferences[id];
            return true;
		}
        return false;
	}

    public bool SpawnWithId( NetworkSpawnObject type, uint id, out GameObject obj ) {
        obj = null;
        Debug.Log("test1");
        if ( networkedReferences.ContainsKey(id)) {
            Debug.Log("test2");
            return false;
		}
        else {
            // assuming this doesn't crash...
            obj = GameObject.Instantiate(spawnInfo.prefabList[2]);
            
            NetworkedBehaviour beh = obj.GetComponent<NetworkedBehaviour>();
            if ( beh == null ) {
                beh = obj.AddComponent<NetworkedBehaviour>();
			}
            beh.networkId = id;

            networkedReferences.Add(id, obj);
            Debug.Log("test3");

            return true;
		}
	}

    public bool DestroyWithId(uint id) {
        if (networkedReferences.ContainsKey(id)) {
            Destroy(networkedReferences[id]);
            networkedReferences.Remove(id);
            return true;
        }
        else {
            return false;
        }
    }
}