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
	public class VarLong
	{
		public readonly MemoryStream CurrentStream = new MemoryStream();
		
		public void SetValue(ulong value)
		{
			while ((value & 0xFFFFFFFFFFFFFF80) != 0)
			{
				CurrentStream.WriteByte((byte) ((value & 0x7F) | 0x80));
				value >>= 7;
			}

			CurrentStream.WriteByte((byte) value);
		}
		
		public void SetValue(long value)
		{
			SetValue((ulong) value);
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
