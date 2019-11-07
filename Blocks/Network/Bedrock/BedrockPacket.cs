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
using System.Diagnostics;

namespace Blocks.Network.Bedrock
{
	/// <summary>
	/// Packet for Bedrock Edition.
	/// </summary>
	public class BedrockPacket : Packet
	{
		protected Types.PacketType PacketType;

		public BedrockPacket()
		{
			VersionType = Types.VersionType.BedrockEdition;
			PacketType = Types.PacketType.Simple;
		}

		public BedrockPacket(byte[] data)
		{
			VersionType = Types.VersionType.BedrockEdition;
			PacketType = Types.PacketType.Simple;

			WriteBytes(data);

			BeginRead();
		}

		protected virtual void Encode()
		{

		}
		protected virtual void Decode()
		{

		}

		public void WriteMagic()
		{
			WriteBytesArray(new byte[] { 0x00, 0xff, 0xff, 0x00, 0xfe, 0xfe, 0xfe, 0xfe, 0xfd, 0xfd, 0xfd, 0xfd, 0x12, 0x34, 0x56, 0x78 });
		}

		public void WriteTime()
		{
			WriteLong(Stopwatch.GetTimestamp());
		}

	}
}
