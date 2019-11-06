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

using Blocks.Task;

namespace Blocks.Network
{
	/// <summary>
	/// Listener: abstract methods for TCP and UDP Listeners.
	/// </summary>
	public abstract class Listener : AsyncTask
	{ 	
		public Listener() : base("Listener", true)
		{
			
		}
		
		protected override void Run(params object[] args)
		{
			StartListen();
		}
		
		public IPAddress Address;
		public int Port;
		
		public Types.ConnectionType ConnectionType;
		
		public abstract void StartListen();
		public abstract void Close();
		
		
	}
}
