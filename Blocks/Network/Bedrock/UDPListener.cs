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
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Blocks.Network.Bedrock
{
	/// <summary>
	/// UDPListener: Bedrock Edition Minecraft Listener.
	/// </summary>
	public class UDPListener : Listener
	{
		public Socket UDPSocket;

		internal IPEndPoint ListenEndPoint;
		internal EndPoint ClientEndPoint;

		protected bool Listening = false;

		public UDPListener()
		{
			ConnectionType = Types.ConnectionType.UDP;
		}
		
		public override void StartListen()
		{
			Address = IPAddress.Any;
			Port = int.Parse(Server.Properties.GetProperty("bedrockedition.port"));

			ListenEndPoint = new IPEndPoint(Address, Port);

			UDPSocket = new Socket(ListenEndPoint.Address.AddressFamily, SocketType.Dgram, ProtocolType.Udp);

			UDPSocket.Bind(ListenEndPoint);

			ClientEndPoint = new IPEndPoint(Address, Port);

			Listening = true;

			Listen();
		}

		private void Listen()
		{
			byte[] data = new byte[1492];
			while (true)
			{
				if (UDPSocket.Available > 0)
				{
					try
					{
						UDPSocket.ReceiveFrom(data, ref ClientEndPoint);
						PacketManager.Execute(new BedrockPacket(data), ClientEndPoint);
					}
					catch
					{
						Logger.Error("UDPListenner dropped down due to error");
						Close();
						break;
					}
				}
			}
		}

		public void SendPacket(EndPoint connection, BedrockPacket packet)
		{
			if (Listening)
			{
				packet.BeginRead();

				UDPSocket.SendTo(packet.DataBytes, packet.DataBytes.Length, SocketFlags.None, connection);
			}
		}

		public override void Close()
		{
			UDPSocket.Close();

			Listening = false;

			ListenEndPoint = null;
			ClientEndPoint = null;
		}
	}
}
