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

namespace Blocks
{
	/// <summary>
	/// Server logger.
	/// </summary>
	class Logger
	{

		private static readonly StreamWriter File = new StreamWriter("server.log", true, Encoding.UTF8);

		public static void Info(string text)
		{
			Log("[Info] " + text);
		}

		public static void Error(string text)
		{
			Log("[Error] " + text);
		}

		public static void Debug(string text)
		{
			Log("[Debug] " + text);
		}

		private static void Log(string text)
		{
			Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "]" + text);
			File.WriteLine("[" + DateTime.Now.ToLongTimeString() + "]" + text);
		}
	}
}