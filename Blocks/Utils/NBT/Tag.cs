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

namespace Blocks.Utils.NBT
{
	public abstract class Tag
	{
		public const byte TAG_End = 0;
	    public const byte TAG_Byte = 1;
	    public const byte TAG_Short = 2;
	    public const byte TAG_Int = 3;
	    public const byte TAG_Long = 4;
	    public const byte TAG_Float = 5;
	    public const byte TAG_Double = 6;
	    public const byte TAG_Byte_Array = 7;
	    public const byte TAG_String = 8;
	    public const byte TAG_List = 9;
	    public const byte TAG_Compound = 10;
	    public const byte TAG_Int_Array = 11;
	    
	    public string Name
	    {
	    	protected set;
	    	get;
	    }
	    
		public override string ToString()
		{
			return Name;
		}

	}
}
