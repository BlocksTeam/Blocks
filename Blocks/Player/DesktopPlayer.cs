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

using Blocks;
using Blocks.Network;
using Blocks.Network.Java;

using Blocks.Network.Java.Packet;
using Blocks.Utils;

namespace Blocks.Player
{
	/// <summary>
	/// DesktopPlayer: player on Java Edition.
	/// </summary>
	public class DesktopPlayer : Player
	{
		public readonly Session Session;
		public readonly UUID UUID;
		
		internal readonly PacketList Network;
		
		public DesktopPlayer(Session session, UUID uuid)
		{
			Session = session;
			UUID = uuid;
			
			Network = new PacketList(Session.Client);
			
			Name = NameTag = PlayerLabel = session.LoginNickname;
			
			Server.Log("DesktopPlayer {0} joined the game.", Name);
		}
		
		public override void Close(string reason)
		{
			if(string.IsNullOrWhiteSpace(reason)) reason = "Closed.";
			
			Player.DesktopPlayers.Remove(Session.Client);
			Player.DesktopPlayers[Session.Client] = null;
			
			PacketManager.CloseSession(Session);
			PacketManager.Close(Session.Client, reason);
			
			Server.Log("DesktopPlayer {0} left the game.", Name);
		}
		
		public override bool IsOnline()
		{
			return Session.IsActiveConnection;
		}
		
		public void SendPacket(JavaPacket packet, bool needMaking = true)
		{
			Network.SendPacket(packet, needMaking);
		}
		
		
	}
}
