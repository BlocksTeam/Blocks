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
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

using Blocks.Player;
using Blocks.Utils;

using Blocks.Utils.Depends;

namespace Blocks.Network.Java
{
	/// <summary>
	/// PacketManager: control with JE packets.
	/// </summary>
	public static class PacketManager
	{	
		public const byte PACKET_SLP = 0x00;
		public const byte PACKET_PING = 0x01;
		public const byte PACKET_LOGIN_START = 0x00;
		public const byte PACKET_ENCRYPTION = 0x01;
		public const byte PACKET_LOGIN_SUCCESS = 0x02;
		
		public static ServerInfo Info = new ServerInfo();
		
		public static readonly List<Session> Sessions = new List<Session>();
		
		private static int IdentifiersCounter = 0;
		
		private static Random RandomGenerator = new Random();
		
		internal static RSACryptoServiceProvider CryptoServiceProvider;
		internal static RSAParameters ServerKey;
		
		public static void Initialize()
		{
			CryptoServiceProvider = new RSACryptoServiceProvider(1024);
            ServerKey = CryptoServiceProvider.ExportParameters(true);
		}
		
		public static void Execute(JavaPacket packet, Socket connection)
		{
			try
			{
				if(Player.Player.DesktopPlayers.ContainsKey(connection))
				{
					Player.Player.DesktopPlayers[connection].Network.HandleRawPacket(packet);
					
					return;
				}
				
				Session session = GetSession(connection);
				
				if(session == null)
				{
					packet.BeginRead();
						
					int packetId = packet.ReadVarInt();
						
					switch(packetId)
					{
						case PACKET_SLP:
							//-3 day later was born Server List Ping Packet :3
								
							if(packet.DataBytesCount32 > 16) 
							{
								int protocol = packet.ReadVarInt();
								string address = packet.ReadStringArray();
								ushort port = packet.ReadUInt16();
								int state = packet.ReadVarInt();
								
								if(state == 1) //state: 1 - status
								{
									JavaPacket pack = new JavaPacket();
										
									pack.WriteVarInt(PACKET_SLP);
									pack.WriteStringArray(Info.JsonView1());
										
									pack = JavaPacket.MakePacket(pack);
										
									Network.TCPL.SendPacket(connection, pack);
								}
								else //state: 2 - login
								{
									if(IsSupportedProtocol(protocol))
									{	
										Sessions.Add(new Session(IdentifiersCounter, connection, address, port, protocol));
										
										if(IdentifiersCounter == int.MaxValue)
											IdentifiersCounter = 0;
										
										IdentifiersCounter++;
									}
									else Close(connection, "Unsupported client version");
								}
							}
						break;
							
						case PACKET_PING:
							Network.TCPL.SendPacket(connection, JavaPacket.MakePacket(packet), true);
						break;
					}
				}
				else
				{
					//Connection state on session : pre login
					
					packet.BeginRead();
						
					int packetId = packet.ReadVarInt();
					
					switch(packetId)
					{
						case PACKET_LOGIN_START:
							string nickname = packet.ReadStringArray();
							
							if(!string.IsNullOrWhiteSpace(nickname))
							{
								if(IsValidNickname(nickname))
								{
									session.LoginNickname = nickname;
									
									StartGame(connection, session);
								}
								else Close(connection, "Invalid Name");
							}
						break;
						
						case PACKET_ENCRYPTION:
							//Temporarily unavailable
						break;
					}
				}
			}
			catch { /* TODO Network Crash */  }
		}
		
		public static bool HasSession(Session session)
		{
			return Sessions.Contains(session);
		}
		
		public static void CloseSession(Session session)
		{
			if(HasSession(session)) Sessions.Remove(session);
		}
		
		public static Session GetSession(Socket connection)
		{
			foreach(Session s in Sessions.ToArray())
			{
				if(s.Client.RemoteEndPoint.ToString() == connection.RemoteEndPoint.ToString()) return s;
			}
			
			return null;
		}
		
		public static void StartEncryption(Socket connection, Session session)
		{
			//[!] now isn't implemented :(
			byte[] token = new byte[4];
            byte[] key = AsnKeyBuilder.PublicKeyToX509(ServerKey).GetBytes();
            
            RandomNumberGenerator crypto = RandomNumberGenerator.Create();
            crypto.GetBytes(token);
            
            JavaPacket packet = new JavaPacket();
            
            packet.WriteVarInt(PACKET_ENCRYPTION);
            packet.WriteStringArray(" ");
            packet.WriteBytesArray(key);
            packet.WriteBytesArray(token);
            
            session.Token = token;
            
            Network.TCPL.SendPacket(connection, JavaPacket.MakePacket(packet, token.Length + key.Length + 1));
		}
		
		public static void StartGame(Socket connection, Session session)
		{
			//Create Player
			DesktopPlayer player = new DesktopPlayer(session, GenerateUUID());
			
			player.CurrentDeviceType = Player.Player.DeviceType.Desktop;
			
			JavaPacket packet = new JavaPacket();
            
            packet.WriteVarInt(PACKET_LOGIN_SUCCESS);
            packet.WriteStringArray(player.UUID.ToString());
            packet.WriteStringArray(player.Name);
            
            Player.Player.DesktopPlayers[player.Session.Client] = player;
            
            Network.TCPL.SendPacket(connection, JavaPacket.MakePacket(packet));
		}
		
		public static bool IsSupportedProtocol(int protocol)
		{
			foreach(int p in Network.JE_SUPPORTED_PROTOCOLS)
				if(p == protocol) return true;
			return false;
		}
		
		public static bool IsValidNickname(string name)
		{
			if(name.ToLower() == "rcon" || name.ToLower() == "console") return false;
			else return (Regex.Match(name, @"^[a-zA-Z0-9_.]{2,16}$").Success);
		}
		
		public static byte R
		{
			get {  return (byte) RandomGenerator.Next(0, 255); }
		}
		
		public static UUID GenerateUUID()
		{			
			Guid guid = new Guid(new byte[16] { R, R, R, R, R, R, R, R, R, R, R, R, R, R, R, R });
			
			return new UUID(guid.ToByteArray());
		}
		
		public static void Close(Socket connection, string reason = "Closed connection", Encoding encoding = null)
        {
			reason = reason.Replace(' ', (char) 96); //temp fix
			
			JavaPacket packet = new JavaPacket();
			packet.WriteVarInt(0x00);
			packet.WriteStringArray(reason, encoding);
			
			packet = JavaPacket.MakePacket(packet);
			
			Server.Log("Closed connection with {0}", connection.RemoteEndPoint.ToString());
			Server.Log("By reason: " + reason);
			
			Network.TCPL.SendPacket(connection, packet, true);
        }
	}
}
