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

using Blocks.Utils;
using Blocks.Level.Entities;

namespace Blocks.Level
{
	/// <summary>
	/// MinecraftWorld: level container.
	/// </summary>
	public class MinecraftWorld
	{
		public enum LevelType
		{
			Anvil,
			Custom
		}
		
		public readonly string Name;
		public readonly LevelType Type;
		
		public bool IsLoaded
		{
			protected set;
			get;
		}
		
		public MinecraftWorld(string name, bool autoload = true, LevelType type = LevelType.Anvil)
		{
			Name = name;
			Type = type;
			
			IsLoaded = false;
			
			if(autoload) Load();
		}
		
		public string FileFormat = "mca";
		
		public string Path
		{
			get { return Server.LevelsDirectory + Name + @"\"; }
		}
		
		public string RegionFilesPath
		{
			get { return Path + @"\region\"; }
		}
		
		public void Load()
		{
			if(!Directory.Exists(Path))
			{
				Logger.Error("No level by name '" + Name + "'");
				return;
			}
			
			string nbtFile = Path + "level.dat";
			
			if(!File.Exists(nbtFile))
			{
				Logger.Error("No level.dat in directory '" + Path + "'");
				return;
			}
			
			byte[] levelDatBytes = File.ReadAllBytes(nbtFile);
			
			if(Type == LevelType.Anvil)
			{
				FileFormat = "mca";
				
				if(!Valid)
				{
					Logger.Error("Invalid region files form in '" + Name + "'");
					return;
				}
			}
		}
		
		public void Save()
		{
			if(Type == LevelType.Anvil)
			{
				
			}
		}
		
		public bool Valid
		{
			get
			{
				return (Directory.Exists(RegionFilesPath) && 
				        (Directory.GetFiles(RegionFilesPath, "*." + FileFormat).Length > 0));
			}
		}
	}
}
