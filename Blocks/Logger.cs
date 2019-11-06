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
	class Logger
	{
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
			DateTime date = DateTime.Now;
			Console.WriteLine("[" + date.ToLongTimeString() + "]" + text);
			using (StreamWriter file = new StreamWriter("server.log", true, Encoding.Default))
			{
				file.WriteLine("[" + date + "]" + text);
			}
		}
	}
}