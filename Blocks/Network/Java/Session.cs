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
using System.Net.Sockets;

namespace Blocks.Network.Java
{
	/// <summary>
	/// Network Session: creating before player login.
	/// </summary>
	public class Session
	{
		public readonly int Identifier;
		
		public readonly Socket Client;
		
		public readonly string SubmittedAddress;
		public readonly int SubmittedPort;
		
		public readonly int Protocol;
		
		public byte[] Token = { 0x00 };
		
		public string LoginNickname;
		
		public Session(int identifier, Socket client, string adr, int port, int protocol = Network.JE_PROTOCOL)
		{
			Identifier = identifier;
			Client = client;
			SubmittedAddress = adr;
			SubmittedPort = port;
			Protocol = protocol;
			
			Logger.Debug("Created session on " + Address + ", protocol = " + Protocol);
		}
		
		public bool IsActiveConnection
		{
			get 
			{
				return Network.TCPL.ActiveConnections.Contains(Client);
			}
		}
		
		public string Address
		{
			get { return Client.RemoteEndPoint.ToString(); }
		}
	}
}
