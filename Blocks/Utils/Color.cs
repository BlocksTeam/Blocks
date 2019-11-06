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

namespace Blocks.Utils
{
	/// <summary>
	/// Color Minecraft (16).
	/// </summary>
	public class Color : MetaTag
	{	
		public const char WHITE = 'f';
		public const char BLACK = '0';
		public const char GREEN  = '1';
		public const char BLUE = '2';
		public const char AQUA = '3';
		public const char BRICK = '4';
		public const char PURPLE = '5';
		public const char ORANGE = '6';
		public const char SMOKE = '7';
		public const char GRAY = '8';
		public const char OCEAN = '9';
		public const char LIME = 'a';
		public const char SKY = 'b';
		public const char RED = 'c';
		public const char PINK = 'd';
		public const char YELLOW = 'e';
		
		public const char _SIGNATURE = '§';
		
		public readonly char Code;
		
		public Color()
		{
			Code = WHITE;
		}
		
		public Color(char code)
		{
			Code = code;
		}
		
		public static Color Create(char c)
	    {
			return new Color(c);
	    }
		
		public string Format
		{
			get { return string.Format("{0}{1}", _SIGNATURE, Code); }
		}
		
		public override string ToString()
		{
			return Format;
		}
	}
	
	public static class Colors
	{
		public static Color White { get { return new Color(Color.WHITE); } }
		public static Color Black { get { return new Color(Color.BLACK); } }
		public static Color Green { get { return new Color(Color.GREEN); } }
		public static Color Blue { get { return new Color(Color.BLUE); } }
		public static Color Aqua { get { return new Color(Color.AQUA); } }
		public static Color Brick { get { return new Color(Color.BRICK); } }
		public static Color Purple { get { return new Color(Color.PURPLE); } }
		public static Color Orange { get { return new Color(Color.ORANGE); } }
		public static Color Smoke { get { return new Color(Color.SMOKE); } }
		public static Color Gray { get { return new Color(Color.GRAY); } }
		public static Color Ocean { get { return new Color(Color.OCEAN); } }
		public static Color Lime { get { return new Color(Color.LIME); } }
		public static Color Sky { get { return new Color(Color.SKY); } }
		public static Color Red { get { return new Color(Color.RED); } }
		public static Color Pink { get { return new Color(Color.PINK); } }
		public static Color Yellow { get { return new Color(Color.YELLOW); } }
	}
}
