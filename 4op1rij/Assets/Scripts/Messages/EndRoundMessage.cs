using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using System;
public class EndRoundMessage : MessageHeader
{
	public override NetworkMessageType Type
	{
		get
		{
			return NetworkMessageType.END_ROUND;
		}
	}

	public uint playerID;
	public bool hasWon;

	public override void SerializeObject(ref DataStreamWriter writer)
	{
		// very important to call this first
		base.SerializeObject(ref writer);

		writer.WriteUInt(playerID);
		byte pt = Convert.ToByte(hasWon);
		writer.WriteByte(pt);


	}

	public override void DeserializeObject(ref DataStreamReader reader)
	{
		// very important to call this first
		base.DeserializeObject(ref reader);

		playerID = reader.ReadUInt();
		bool won = Convert.ToBoolean(reader.ReadByte());
		hasWon = won;


	}
}
