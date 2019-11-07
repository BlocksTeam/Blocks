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
	public class JavaPacket : Packet
	{
		public JavaPacket()
		{
			VersionType = Types.VersionType.JavaEdition;
		}
		
		public JavaPacket(byte[] data)
		{
			VersionType = Types.VersionType.JavaEdition;
			
			WriteBytes(data);
			
			BeginRead();
		}
		
		public JavaPacket(Stream stream)
		{
			VersionType = Types.VersionType.JavaEdition;
			
			WriteStream(stream);
			
			BeginRead();
		}
	}
}
