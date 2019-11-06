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

namespace Blocks.Network
{
	/// <summary>
	/// Binary Write and Read methods.
	/// </summary>
	public class DataStream : BinaryReader
	{	
		private readonly BinaryWriter Writer;
		
		public DataStream() : base(new MemoryStream())
		{
			Writer = new BinaryWriter(BaseStream);
		}
		
		public byte[] DataBytes
		{
			get { return ReadBytes((int) BaseStream.Length); }
		}
		
		public long DataBytesCount
		{
			get { return BaseStream.Length; }
		}
		
		public int DataBytesCount32
		{
			get { return (int) BaseStream.Length; }
		}
		
		public bool IsEmpty
		{
			get { return (BaseStream.Length < 1); }
		}
		
		public void Clean()
		{
			Writer.Flush();
		}
		
		public int WriteBoolean(bool data)
		{
			Writer.Write(data);
			
			return sizeof(bool);
		}
		
		public int WriteByte(byte data)
		{
			Writer.Write(data);
			
			return 1;
		}
		
		public int WriteBytes(byte[] data)
		{
			Writer.Write(data);
			
			return data.Length;
		}
		
		public int WriteSByte(sbyte data)
		{
			Writer.Write(data);
			
			return sizeof(sbyte);
		}
		
		public int WriteChars(char[] data)
		{
			Writer.Write(data);
			
			return sizeof(char) * data.Length;
		}
		
		public int WriteInt(int data)
		{
			Writer.Write(data);
			
			return sizeof(int);
		}
		
		public int WriteLong(long data)
		{
			Writer.Write(data);
			
			return sizeof(long);
		}
		
		public int WriteDouble(double data)
		{
			Writer.Write(data);
			
			return sizeof(double);
		}
		
		public int WriteFloat(float data)
		{
			Writer.Write(data);
			
			return sizeof(float);
		}
		
		public int WriteShort(short data)
		{
			Writer.Write(data);
			
			return sizeof(short);
		}
		
		public int WriteUShort(ushort data)
		{
			Writer.Write(data);

			return sizeof(ushort);
		}
		
		public int WriteUInt(uint data)
		{
			Writer.Write(data);

			return sizeof(uint);
		}
		
		public int WriteULong(ulong data)
		{
			Writer.Write(data);

			return sizeof(ulong);
		}
		
		public void WriteString(string data)
		{
			Writer.Write(data);
		}
		
		public int WriteHex(string hex) 
		{
			if (!string.IsNullOrEmpty(hex))
			{
		        const string str = "0123456789ABCDEF";
		        
		        hex = hex.ToUpper().Replace(" ", "");
		        
		        char[] hexChars = hex.ToCharArray();
		        
		        byte[] d = new byte[hex.Length / 2];
		        
		        for (int i = 0; i < hex.Length / 2; i++) 
		        {
		            int pos = i * 2;
		            
		            d[i] = (byte) (((byte) str.IndexOf(hexChars[pos]) << 4) | ((byte) str.IndexOf(hexChars[pos + 1])));
		        }
		        
		        return WriteBytes(d);
			}
			else return 0;
    	}
		
		public int WriteStream(Stream stream)
		{
			byte[] data = new byte[stream.Length];
			
			stream.Read(data, 0, (int) stream.Length);
			
			return WriteBytes(data);
		}
		
		public int WriteBytesArray(byte[] data)
		{
			WriteUInt((uint) data.Length);
			
			return WriteBytes(data) + sizeof(uint);
		}
		
		public byte[] ReadBytesArray()
		{
			return ReadBytes((int) ReadUInt32());
		}
		
		public void WriteVarInt(VarInt data)
		{
			WriteStream(data.CurrentStream);
		}
		
		public void WriteVarLong(VarLong data)
		{
			WriteStream(data.CurrentStream);
		}
		
		public ulong ReadVarInt(int maxSize = 5)
		{
			VarInt vi = new VarInt();
			
			vi.CurrentStream.Write(DataBytes, 0, DataBytesCount32);
			
			return vi.GetValue(maxSize);
		}
		
		public ulong ReadVarLong(int maxSize = 10)
		{
			VarLong vl = new VarLong();
			
			vl.CurrentStream.Write(DataBytes, 0, DataBytesCount32);
			
			return vl.GetValue(maxSize);
		}
	}
}
