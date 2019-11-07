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
using System.Text;

namespace Blocks.Network.Java
{
	/// <summary>
	/// PacketManager: control with JE packets.
	/// </summary>
	public static class PacketManager
	{
		public const byte STATUS_PACKET_PING = 0x00;
		public const byte STATUS_PACKET_PONG = 0x01;
		
		public const byte PACKET_SEND_PACKET = 0x01;
		public const byte PACKET_OPEN_SESSION = 0x02;
		public const byte PACKET_CLOSE_SESSION = 0x03;
		public const byte PACKET_ENABLE_ENCRYPTION = 0x04;
		public const byte PACKET_SET_COMPRESSION = 0x05;
		public const byte PACKET_SHUTDOWN = 0xfe;
		public const byte PACKET_EMERGENCY_SHUTDOWN = 0xff;
		
		public static string MOTD = "Blocks Minecraft Server";
		
		public static void Execute(JavaPacket packet, TcpClient connection)
		{
			if(packet.DataBytesCount > 0)
			{
				if(packet.ReadByte() == 0xFE && packet.ReadByte() == 0x01)
				{
					//MC|PingHost code
					JavaPacket pack = new JavaPacket();
					pack.WriteByte(0xFF);
					Network.TCPL.SendPacket(connection, pack);
				}
				else
				{
					packet.BeginRead();
					
					int size = packet.ReadVarInt();
					int packetId = packet.ReadVarInt();
					
					switch(packetId)
					{
						case 0x00:
							int protocol = packet.ReadVarInt();

							Logger.Debug("New request " + connection.Client.RemoteEndPoint.ToString() + ", protocol = " + protocol);

							JavaPacket pack = new JavaPacket();
							pack.WriteVarInt(0x00);
							pack.WriteStringArray(ServerInfoJSON());
							Network.TCPL.SendPacket(connection, pack);
						break;
					}
				}
			}
			else connection.Close();
		}
		
		public static string ServerInfoJSON()
		{
			string sver = "1.14.4";
			string protocol = "47";
			string maxPlayers = "100";
			string online = "0";
			
			return 
				"{\"version\":{\"name\":\"" + sver + "\",\"protocol\":" + protocol + "}," +
				"\"players\":{\"max\":" + maxPlayers + ",\"online\":" + online + ",\"sample\":[]}," +
				"\"description\":{\"text\":\"" + MOTD + "\"}}";
		}
	}	
}
