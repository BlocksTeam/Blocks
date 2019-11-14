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
using System.Text;

namespace Blocks.Utils
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
		
		public void BeginRead()
		{
			Writer.Seek(0, SeekOrigin.Begin);
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
			BaseStream.Flush();
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
		
		public void WriteBytesArray(byte[] data)
		{
			WriteVarInt(data.Length);
			WriteBytes(data);
		}
		
		public byte[] ReadBytesArray()
		{
			return ReadBytes((int) ReadUInt32());
		}
		
		public int ReadVarInt()
        {
			
	        uint result = 0;
	        int length = 0;
	        
	        try
			{
	            while (true)
	            {
	                byte current = ReadByte();
	                result |= (current & 0x7Fu) << length++ * 7;
	
	                if ((current & 0x80) != 128)
	                    break;
	            }
	            return (int) result;
			}
			catch { return -length; }
        }
		
		public int ReadVarInt(out int length)
        {
            uint result = 0;
            length = 0;
            
            while (true)
            {
                byte current =  ReadByte();
                result |= (current & 0x7Fu) << length++ * 7;

                if ((current & 0x80) != 128)
                    break;
            }
            return (int) result;
        }
		
		public void WriteVarInt(int integer)
		{
			while ((integer & -128) != 0)
			{
				Writer.Write((byte) (integer & 127 | 128));
				integer = (int) (((uint) integer) >> 7);
			}
			Writer.Write((byte) integer);
		}
		
		public void WriteStringArray(string data, Encoding encoding = null)
		{
			if(encoding == null) encoding = Encoding.UTF8;
			
			byte[] stringData = encoding.GetBytes(data);
			WriteVarInt(stringData.Length);
			Writer.Write(stringData);
		}
		
		public string ReadStringArray(Encoding encoding = null)
		{
			if(encoding == null) encoding = Encoding.UTF8;
			
			int length = ReadVarInt();
			byte[] stringValue = ReadBytes(length);

			return encoding.GetString(stringValue);
		}
		
		
		public static int SizeVarInt(int varIntValue)
		{
			DataStream stream = new DataStream();
			stream.WriteVarInt(varIntValue);
			return stream.DataBytesCount32;
		}
	}
}
