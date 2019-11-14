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

namespace Blocks.Network.Java
{
	public class JavaPacket : Blocks.Network.Packet
	{
		public JavaPacket()
		{
			VersionType = Types.VersionType.JavaEdition;
		}
		
		public JavaPacket(byte[] data)
		{
			VersionType = Types.VersionType.JavaEdition;
			
			PacketId = -1;
			Name = "Unnamed Packet";
			
			WriteBytes(data);
			
			BeginRead();
		}
		
		public JavaPacket(Stream stream)
		{
			VersionType = Types.VersionType.JavaEdition;
			
			PacketId = -1;
			Name = "Unnamed Packet";
			
			WriteStream(stream);
			
			BeginRead();
		}
		
		public static JavaPacket MakePacket(JavaPacket packet, int ext = 0)
		{
			packet.BeginRead();
			
			byte[] data = packet.DataBytes;
			
			JavaPacket pack = new JavaPacket();
			
			pack.WriteVarInt(data.Length + ext);
			pack.WriteBytes(data);
			
			return pack;
		}
		
		public int PacketId
		{
			get;
			protected set;
		}
		
		public string Name
		{
			get;
			protected set;
		}
		
		//-=R E A D=-
		protected virtual void Decode()
		{
			
		}
		
		//-=W R I T E =-
		protected virtual void Encode()
		{
			Clean();
		}
		
		
	}
}
