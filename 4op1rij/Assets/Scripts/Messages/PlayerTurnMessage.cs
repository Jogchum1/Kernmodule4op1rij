using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;
using System;

public class PlayerTurnMessage : MessageHeader
{
	public override NetworkMessageType Type
	{
		get
		{
			return NetworkMessageType.PLAYER_TURN;
		}
	}

	public bool playerTurn;
	public override void SerializeObject(ref DataStreamWriter writer)
	{
		// very important to call this first
		base.SerializeObject(ref writer);

		byte pt = Convert.ToByte(playerTurn);
		writer.WriteByte(pt);

	}

	public override void DeserializeObject(ref DataStreamReader reader)
	{
		// very important to call this first
		base.DeserializeObject(ref reader);

		bool pt = Convert.ToBoolean(reader.ReadByte());
		playerTurn = pt;

	}
}
