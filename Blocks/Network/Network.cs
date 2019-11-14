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

using Blocks.Network.Bedrock;
using Blocks.Network.Java;
using Blocks.Utils;

namespace Blocks.Network
{
	/// <summary>
	/// Basic Network class.
	/// </summary>
	public static class Network
	{
		public const int JE_PROTOCOL = 498;
		public const string JE_VERSION = "1.14.4";
		
		public static readonly int[] JE_SUPPORTED_PROTOCOLS = { 498 };
		
		internal static UDPListener UDPL;
		internal static TCPListener TCPL;
		
		public static void StartListeners()
		{
			if(Server.Properties.GetProperty("bedrockedition.enabled") == Config.SWITCH_ON)
				UDPL = new UDPListener();
			if(Server.Properties.GetProperty("javaedition.enabled") == Config.SWITCH_ON)
				TCPL = new TCPListener();
		}
	}
}
