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
using Blocks.Level;

namespace Blocks.Utils
{
	public class Position : Vector3
	{
		public MinecraftWorld Level;
		
		public Position(MinecraftWorld level = null)
		{
			Level = level;
		}
		
		public Position(MinecraftWorld level, double x, double y, double z) : base(x, y, z)
		{
			Level = level;
		}
		
		public Position(MinecraftWorld level, int fx, int fy, int fz) : base(fx, fy, fz)
		{
			Level = level;
		}
		
		public string LevelName
		{
			get { return Level.Name; }
		}
	}
}
