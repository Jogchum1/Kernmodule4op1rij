using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;



public class RPCMessage : MessageHeader
{
    public override NetworkMessageType Type
    {
        get
        {
            return NetworkMessageType.RPC_MESSAGE;
        }
    }

    public string function;
    public uint target;
    public uint columnNumber;
    public Vector3 position, rotation;
    public override void SerializeObject(ref DataStreamWriter writer)
    {
        // very important to call this first
        base.SerializeObject(ref writer);

        writer.WriteUInt((uint)target);
        writer.WriteFixedString128(function);
        writer.WriteUInt((uint)columnNumber);
        writer.WriteFloat(position.x);
        writer.WriteFloat(position.y);
        writer.WriteFloat(position.z);
        writer.WriteFloat(rotation.x);
        writer.WriteFloat(rotation.y);
        writer.WriteFloat(rotation.z);

    }

    public override void DeserializeObject(ref DataStreamReader reader)
    {
        // very important to call this first
        base.DeserializeObject(ref reader);

        target = reader.ReadUInt();
        function = reader.ReadFixedString128().ToString();
        columnNumber = reader.ReadUInt();
        position.x = reader.ReadFloat();
        position.y = reader.ReadFloat();
        position.z = reader.ReadFloat();
        rotation.x = reader.ReadFloat();
        rotation.y = reader.ReadFloat();
        rotation.z = reader.ReadFloat();
    }
}



