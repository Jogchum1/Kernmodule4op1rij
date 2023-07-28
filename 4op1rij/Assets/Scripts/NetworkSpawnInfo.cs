using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NetworkSpawnObject
{
	PLAYER = 0,
	COIN = 1,
	BOARD = 2
}

[CreateAssetMenu(menuName = "My Assets/NetworkSpawnInfo")]
public class NetworkSpawnInfo : ScriptableObject
{
	// TODO: A serializableDictionary would help here...
	public List<GameObject> prefabList = new List<GameObject>();
}
