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

namespace Blocks.Utils
{
	/// <summary>
	/// Simple class of Server Info.
	/// </summary>
	public class ServerInfo
	{
		public int Online
		{
			get { return Server.Online; }
		}
		
		public int Slots
		{
			get { return Server.MaxOnline; }
		}
		
		public string JEMotd
		{
			get { return Server.Properties.GetProperty("javaedition.motd"); }
		}
		
		public string BEMotd
		{
			get { return Server.Properties.GetProperty("bedrockedition.motd"); }
		}
		
		public int JEProtocol
		{
			get { return Network.Network.JE_PROTOCOL; }
		}
		
		public string JEVersion
		{
			get { return Network.Network.JE_VERSION; }
		}
		
		public string JsonView1()
		{
			string d = "<!#version#:<!#name#:#BS#,#protocol#:{0}!>,";
			
			d += "#players#:<!#max#:{1},#online#:{2}!>,";
			d += "#description#:<!#text#:#{3}#!>!>";
			
			return ToSimpleJSON(d, JEProtocol, Slots, Online, JEMotd);
		}
		
		public static string ToSimpleJSON(string format, params object[] objects)
		{
			string json = string.Format(format, objects);
			
			json = json.Replace("<!", "{");
			json = json.Replace("!>", "}");
			json = json.Replace("#", "\"");
			
			return json;
		}
	}
}
