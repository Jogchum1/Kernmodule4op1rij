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
	public Vector3 position;
	public NetworkSpawnObject objectType;

	public override void SerializeObject(ref DataStreamWriter writer)
	{
		// very important to call this first
		base.SerializeObject(ref writer);

		writer.WriteUInt(playerID);
		writer.WriteUInt((uint)objectType);
		
		writer.WriteFloat(position.x);
		writer.WriteFloat(position.y);
		writer.WriteFloat(position.z);
	}

	public override void DeserializeObject(ref DataStreamReader reader)
	{
		// very important to call this first
		base.DeserializeObject(ref reader);

		playerID = reader.ReadUInt();
		objectType = (NetworkSpawnObject)reader.ReadUInt();

		position.x = reader.ReadFloat();
		position.y = reader.ReadFloat();
		position.z = reader.ReadFloat();
	}
}
