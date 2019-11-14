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
using System.IO;

using Blocks.Utils;

using Blocks.Player;

using Blocks.Level;
using Blocks.Level.Entities;

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
		
		public static MinecraftWorld DefaultLevel
		{
			private set;
			get;
		}
		
		public static void Start()
		{
			Properties = new Config("server.properties");
			
			InitConfig();
			
			if(!Directory.Exists(PluginsDirectory))
				Directory.CreateDirectory(PluginsDirectory);
			
			if(!Directory.Exists(LevelsDirectory))
				Directory.CreateDirectory(LevelsDirectory);
			
			Entity.InitializeEntities();
			
			DefaultLevel = new MinecraftWorld(Properties.GetProperty("server.level.default"));
			
			Network.Network.StartListeners();
		}
		
		public static void Stop(string reason = "")
		{
			Network.Network.TCPL.Close();
			Network.Network.UDPL.Close();
		}
		
		public static int Online
		{
			get 
			{
				return Player.Player.DesktopPlayers.Values.Count +
					Player.Player.BedrockPlayers.Values.Count;
			}
		}
		
		public static int MaxOnline
		{
			get { return int.Parse(Properties.GetProperty("server.maxonline")); }
		}
		
		public static Player.Player[] GetPlayers()
		{
			List<Player.Player> players = new List<Blocks.Player.Player>();
				
			players.AddRange(Player.Player.DesktopPlayers.Values);
			players.AddRange(Player.Player.BedrockPlayers.Values);
				
			return players.ToArray();
		}
		
		public static void Log(string message, params object[] args)
		{
			Logger.Info(string.Format(message, args));
		}
		
		public static void Crash(Exception ex, bool stop = false)
		{
			Console.WriteLine("<=============== C R A S H ===============>");
			Console.WriteLine("Source: " + ex.Source);
			Console.WriteLine(ex.StackTrace);
			Console.WriteLine("-> " + ex.Message);
			Console.WriteLine("</============== C R A S H ===============>");
		}
		
		public static string LevelsDirectory
		{
			get { return Environment.CurrentDirectory + @"\worlds\"; }
		}
		
		public static string PluginsDirectory
		{
			get { return Environment.CurrentDirectory + @"\plugins\"; }
		}
		
		public static void InitConfig()
		{
			if(!Properties.ExistsProperty("bedrockedition.enabled"))
				Properties.SetProperty("bedrockedition.enabled", Config.SWITCH_ON);
			
			if(!Properties.ExistsProperty("bedrockedition.port"))
				Properties.SetProperty("bedrockedition.port", "19132");
			
			if(!Properties.ExistsProperty("bedrockedition.motd"))
				Properties.SetProperty("bedrockedition.motd", "Welcome to Blocks server");
			
			if(!Properties.ExistsProperty("javaedition.enabled"))
				Properties.SetProperty("javaedition.enabled", Config.SWITCH_ON);
			
			if(!Properties.ExistsProperty("javaedition.port"))
				Properties.SetProperty("javaedition.port", "25565");
			
			if(!Properties.ExistsProperty("javaedition.motd"))
				Properties.SetProperty("javaedition.motd", "Welcome to Blocks server");
			
			if(!Properties.ExistsProperty("server.maxonline"))
				Properties.SetProperty("server.maxonline", "1000");
			
			if(!Properties.ExistsProperty("server.level.default"))
				Properties.SetProperty("server.level.default", "world");
			
			Properties.Save();
		}
	}
}
