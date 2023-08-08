using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;

public class SpawnCoinMessage : MessageHeader
{
	public override NetworkMessageType Type
	{
		get
		{
			return NetworkMessageType.SPAWN_COIN;
		}
	}

	public uint playerID;
	public uint column;
	public Vector3 spawnPos;
	public Vector3 targetPos;
	public NetworkSpawnObject objectType;

	public override void SerializeObject(ref DataStreamWriter writer)
	{
		// very important to call this first
		base.SerializeObject(ref writer);

		writer.WriteUInt(playerID);
		writer.WriteUInt(column);
		writer.WriteUInt((uint)objectType);
		
		writer.WriteFloat(spawnPos.x);
		writer.WriteFloat(spawnPos.y);
		writer.WriteFloat(spawnPos.z);
		
		writer.WriteFloat(targetPos.x);
		writer.WriteFloat(targetPos.y);
		writer.WriteFloat(targetPos.z);
	}

	public override void DeserializeObject(ref DataStreamReader reader)
	{
		// very important to call this first
		base.DeserializeObject(ref reader);

		playerID = reader.ReadUInt();
		column = reader.ReadUInt();
		objectType = (NetworkSpawnObject)reader.ReadUInt();

		spawnPos.x = reader.ReadFloat();
		spawnPos.y = reader.ReadFloat();
		spawnPos.z = reader.ReadFloat();
		
		targetPos.x = reader.ReadFloat();
		targetPos.y = reader.ReadFloat();
		targetPos.z = reader.ReadFloat();
	}
}
