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

using Blocks.Utils;
using Blocks.Network;
using Blocks.Network.Bedrock;
using Blocks.Network.Java;

namespace Blocks
{
	/// <summary>
	/// Global Server.
	/// </summary>
	public static class Server
	{
		public const string GLOBAL_VERSION = "v0.1";
		public const string BUILD_SIGNATURE = "build 1";
		
		public static Config Properties;
		
		public static void Start()
		{
			Properties = new Config("server.properties");
			
			InitConfig();
			
			Network.Network.StartListeners();
		}
		
		public static void InitConfig()
		{
			if(!Properties.ExistsProperty("bedrock.port"))
				Properties.SetProperty("bedrock.port", "19132");
			
			if(!Properties.ExistsProperty("javaedition.port"))
				Properties.SetProperty("javaedition.port", "25565");
			
			Properties.Save();
		}
	}
}
