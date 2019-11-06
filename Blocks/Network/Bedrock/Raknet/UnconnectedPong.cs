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

namespace Blocks.Network.Bedrock.Raknet
{
	/// <summary>
	/// UnconnectedPong packet.
	/// </summary>
	public class UnconnectedPong : BedrockPacket
	{
		public long ServerID = 0x00;
		public string ServerName;
		
		protected override void Encode()
		{
			WriteByte(PacketIdentifiers.UNCONNECTED_PONG);
			WriteTime();
			WriteLong(ServerID);
			WriteMagic();
			WriteString(ServerName);
		}

		protected override void Decode()
		{
			//TODO
		}
	}
}
