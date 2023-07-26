using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Networking.Transport;



    public class RPCMessage : MessageHeader
    {
        public override NetworkMessageType Type
        {
            get{
                return NetworkMessageType.RPC_MESSAGE;
            }
        }

		public string function;
		public uint target;
		public object[] data;
		public override void SerializeObject(ref DataStreamWriter writer)
		{
			// very important to call this first
			base.SerializeObject(ref writer);

			writer.WriteUInt((uint)target);
			writer.WriteFixedString128(function);
			
			

		}

		public override void DeserializeObject(ref DataStreamReader reader)
		{
			// very important to call this first
			base.DeserializeObject(ref reader);

			target = reader.ReadUInt();
			function = reader.ReadFixedString128().ToString();
		}
    }



