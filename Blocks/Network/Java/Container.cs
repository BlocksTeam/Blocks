/*
	╔══╗─     ╔╗───     ╔═══╗     ╔═══╗     ╔╗╔═╗     ╔═══╗
	║╔╗║─     ║║───     ║╔═╗║     ║╔═╗║     ║║║╔╝     ║╔═╗║
	║╚╝╚╗     ║║───     ║║─║║     ║║─╚╝     ║╚╝╝─     ║╚══╗
	║╔═╗║     ║║─╔╗     ║║─║║     ║║─╔╗     ║╔╗║─     ╚══╗║
	║╚═╝║     ║╚═╝║     ║╚═╝║     ║╚═╝║     ║║║╚╗     ║╚═╝║
	╚═══╝     ╚═══╝     ╚═══╝     ╚═══╝     ╚╝╚═╝     ╚═══╝
	
	* https://github.com/blocksteam/Blocks
	* Contact Us: blocksteamcore@gmail.com
 */
using System;
using System.Collections.Generic;

namespace Blocks.Network.Java
{
	/// <summary>
	/// Container split one stream to packets.
	/// </summary>
	public class Container : Utils.DataStream
	{
		public int Length
		{
			protected set;
			get;
		}
		
		public Container(byte[] rawData)
		{
			Length = rawData.Length;
			
			WriteBytes(rawData);
		}
		
		public virtual JavaPacket[] ToPackets()
		{
			Queue<JavaPacket> packets = new Queue<JavaPacket>();
			
			if(DataBytesCount > 0)
			{
				BeginRead();
				
				int byteIndex = 0;
				
				while(byteIndex < Length - 1)
				{
					int packetSize = ReadVarInt();
						
					if(packetSize < 1)
					{
						JavaPacket packet = new JavaPacket();
						
						packet.ReadBytes((int) (BaseStream.Length - BaseStream.Position - (packetSize*-1)));
						
						packets.Enqueue(packet);
						
						break;
					}
					else
					{
						packets.Enqueue(new JavaPacket(ReadBytes(packetSize)));
							
						byteIndex += packetSize;
					}
				}
			}
			
			return packets.ToArray();
		}
		
		public void Push(JavaPacket packet)
		{
			Length += packet.DataBytesCount32;
			
			packet.BeginRead();
			
			WriteBytes(packet.DataBytes);
		}
	}
}
