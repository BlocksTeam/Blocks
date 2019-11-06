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
using System.Linq;

namespace Blocks.Utils
{
	/// <summary>
	/// UUID As Object.
	/// </summary>
	public class UUID
	{
		ulong PartA;
		ulong PartB;

		public UUID(byte[] dataRFC4122)
		{
			PartA = BitConverter.ToUInt64(dataRFC4122.Skip(0).Take(8).Reverse().ToArray(), 0);
			PartB = BitConverter.ToUInt64(dataRFC4122.Skip(8).Take(8).Reverse().ToArray(), 0);
		}

		public UUID(string uuid)
		{
			uuid = uuid.Replace("-", "");
			
			byte[] bytes = ToArray(uuid);
			
			PartA = BitConverter.ToUInt64(bytes.Skip(0).Take(8).ToArray(), 0);
			PartB = BitConverter.ToUInt64(bytes.Skip(8).Take(8).ToArray(), 0);
		}

		public byte[] GetBytes()
		{
			byte[] bytes = new byte[0];
			
			return bytes.Concat(BitConverter.GetBytes(PartA).Reverse())
				.Concat(BitConverter.GetBytes(PartB).Reverse())
				.ToArray();
		}
		
		public override string ToString()
		{
			byte[] bytes = GetBytes();

			string hex = string.Join("", bytes.Select(b => b.ToString("x2")));

			return 
				hex.Substring(0, 8) + "-" +
				hex.Substring(8, 4) + "-" + 
				hex.Substring(12, 4) + "-" + 
				hex.Substring(16, 4) + "-" + 
				hex.Substring(20, 12);
		}
		
		public static byte[] ToArray(string hex)
		{
			return Enumerable.Range(0, hex.Length)
				.Where(x => x % 2 == 0)
				.Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
				.ToArray();
		}
	}
}
