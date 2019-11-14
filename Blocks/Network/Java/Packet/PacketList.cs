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
using System.Net.Sockets;

namespace Blocks.Network.Java.Packet
{
	/// <summary>
	/// PacketList: control player's packets.
	/// </summary>
	public class PacketList
	{
		readonly Socket Connection;
		
		public PacketList(Socket connection)
		{
			Connection = connection;
		}
		
		public void SendPacket(JavaPacket packet, bool needMaking)
		{
			if(needMaking) packet = JavaPacket.MakePacket(packet);
			
			Network.TCPL.SendPacket(Connection, packet);
		}
		
		public void HandleRawPacket(JavaPacket packet)
		{
			Server.Log(packet.AsStringText());
		}
	}
}
