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

namespace Blocks.Utils
{
	/// <summary>
	/// VarInt32.
	/// </summary>
	public class VarInt
	{
		public readonly MemoryStream CurrentStream = new MemoryStream();
		
		public void SetValue(uint value)
		{
			while ((value & -128) != 0)
			{
				CurrentStream.Flush();
				
				CurrentStream.WriteByte((byte) ((value & 0x7F) | 0x80));
				
				value >>= 7;
			}

			CurrentStream.WriteByte((byte) value);
		}
		
		public void SetValue(int value)
		{
			SetValue((uint) value);
		}
		
		public ulong GetValue(int maxSize)
		{
			ulong result = 0;
			
			int B0, J = 0;

			do
			{
				B0 = CurrentStream.ReadByte();

				result |= (ulong) (B0 & 0x7f) << J++ * 7;

			} 
			while ((B0 & 0x80) == 0x80);
			
			return result;
		}
	}
}
