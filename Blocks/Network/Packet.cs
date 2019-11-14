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
using System.IO;
using System.Text;

namespace Blocks.Network
{
	/// <summary>
	/// Minecraft Network Packet.
	/// </summary>
	public abstract class Packet : Utils.DataStream
	{
		protected Types.VersionType VersionType;
		
		public Types.VersionType GameVersionType
		{
			get { return VersionType; }
		}
		
		public string AsStringText()
		{
			string result = "";
			
			BeginRead();
			
			byte[] bytes = DataBytes;
			
			foreach(byte b in bytes) result += b.ToString("X2") + " ";
			
			BeginRead();
			
			return "Packet { " + result + "} (string) { " + Encoding.UTF8.GetString(bytes);
		}
	}
}
