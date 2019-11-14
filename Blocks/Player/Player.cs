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

using Blocks.Utils;

namespace Blocks.Player
{
	/// <summary>
	/// Abstract Class of Game Player.
	/// </summary>
	public abstract class Player : Position
	{
		public static Dictionary<Socket, DesktopPlayer> DesktopPlayers = new Dictionary<Socket, DesktopPlayer>();
		public static Dictionary<Socket, BedrockPlayer> BedrockPlayers = new Dictionary<Socket, BedrockPlayer>();
		
		public enum DeviceType
		{
			Desktop,
			Windows10Edition,
			PocketEdition,
			Other
		}
		
		public DeviceType CurrentDeviceType;
		
		public readonly List<string> Permissions = new List<string>();
		
		public string Name
		{
			get;
			protected set;
		}
		
		public override string ToString()
		{
			return Name;
		}
		
		public string PlayerLabel, NameTag;
		
		public abstract void Close(string reason);
		
		public abstract bool IsOnline();
		
		public void Friend(Player player)
		{
			//TODO Implementation
		}
		
		public void Unfriend(Player player)
		{
			//TODO Implementation
		}
		
		public void IsFriend(Player player)
		{
			//TODO Implementation
		}
		
		public static bool IsNewPlayer
		{
			get;
			protected set;
		}
		
		public const string GROUP_OPERATOR = "server: operator";
		public const string GROUP_BANNED = "server: banned";
		public const string GROUP_BANNEDIP = "server: ipbanned";
		public const string GROUP_CHAT_PREFIX = "server: prefix";
		public const string GROUP_NAMETAG = "server: nametag";
		public const string GROUP_FRIEND = "server: friend";
		public const string GROUP_LAST_ADDRESS = "server: ip";
		public const string GROUP_POSITION_X = "pos: x";
		public const string GROUP_POSITION_Y = "pos: y";
		public const string GROUP_POSITION_Z = "pos: z";
		public const string GROUP_WORLD = "pos: level";
	}
}
