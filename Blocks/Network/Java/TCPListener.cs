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
using System.Net;

namespace Blocks.Network.Java
{
	/// <summary>
	/// TCPListener: Java Edition Minecraft Listener.
	/// </summary>
	public class TCPListener : Listener
	{
		public TCPListener()
		{
			ConnectionType = Types.ConnectionType.TCP;
			
			Address = IPAddress.Any;
			Port = int.Parse(Server.Properties.GetProperty("javaedition.port"));
		}
		
		public override void StartListen()
		{
			
		}
		
		public override void Close()
		{
			//TODO (close connections)
		}
	}
}
