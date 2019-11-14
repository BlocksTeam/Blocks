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

namespace Blocks.Utils.NBT
{
	/// <summary>
	/// Quick way for working with NBT Objects.
	/// </summary>
	public class NBTStream : MemoryStream
	{
		public NBTStream()
		{
			
		}
		
		public NBTStream(byte[] nbtRawData)
		{
			Write(nbtRawData, 0, nbtRawData.Length);
		}
		
		public void BeginRead()
		{
			Seek(0, SeekOrigin.Begin);
		}
		
		public void ToEnd()
		{
			Seek(0, SeekOrigin.End);
		}
		
		public byte[] DataBytes
		{
			get 
			{
				BeginRead();
				
				byte[] data = new byte[Length];
				
				Read(data, 0, (int) Length);
				
				return data;
			}
		}
		
		public bool IsEmpty
		{
			get { return (Length < 1); }
		}
		
		//Writing data
		
		public void WriteTag(byte value)
		{
			WriteTag(new ByteTag(value));
		}
		
		public void WriteTag(ByteTag value)
		{
			
		}
		
		public void WriteTag(int value)
		{
			WriteTag(new IntTag(value));
		}
		
		public void WriteTag(IntTag value)
		{
			
		}
		
		public void WriteTag(double value)
		{
			WriteTag(new DoubleTag(value));
		}
		
		public void WriteTag(DoubleTag value)
		{
			
		}
		
		public void WriteTag(float value)
		{
			WriteTag(new FloatTag(value));
		}
		
		public void WriteTag(FloatTag value)
		{
			
		}
		
		public void WriteTag(long value)
		{
			WriteTag(new LongTag(value));
		}
		
		public void WriteTag(LongTag value)
		{
			
		}
		
		public void WriteTag(string value)
		{
			WriteTag(new StringTag(value));
		}
		
		public void WriteTag(StringTag value)
		{
			
		}
		
		public void WriteTag(CompoundTag value)
		{
			
		}
		
		
		
		//Reading data
		
	}
}
