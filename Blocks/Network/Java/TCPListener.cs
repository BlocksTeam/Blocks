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
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Blocks.Network.Java
{
	/// <summary>
	/// TCPListener: Java Edition Minecraft Listener.
	/// </summary>
	public class TCPListener : Listener
	{
		public TcpListener Listener;
		
		internal IPEndPoint EndPoint;
		
		internal RSACryptoServiceProvider CryptoServiceProvider;
		internal RSAParameters ServerKey;
		
		protected Object Locker = new Object();
		
		protected bool Listening = false;
		
		public TCPListener()
		{
			ConnectionType = Types.ConnectionType.TCP;
		}
		
		public readonly List<TcpClient> Connections = new List<TcpClient>();
		
		public override void StartListen()
		{
			Address = IPAddress.Any;
			Port = int.Parse(Server.Properties.GetProperty("javaedition.port"));
			
			CryptoServiceProvider = new RSACryptoServiceProvider(1024);
            ServerKey = CryptoServiceProvider.ExportParameters(true);
			
			EndPoint = new IPEndPoint(Address, Port);
			
			Listener = new TcpListener(EndPoint);
			
			Listening = true;
			
            Listener.Start();
            
            Listener.BeginAcceptTcpClient(AcceptClientAsync, null);
		}
		
		public override void Close()
		{
			Listener.Stop();
			
			Listening = false;
			
			Listener = null;
		}
		
		protected void AcceptClientAsync(IAsyncResult result)
		{
            lock (Locker)
            {
                if (Listener != null)
                {
	                TcpClient connection = Listener.EndAcceptTcpClient(result);
	                
	                Connections.Add(connection);
	                
	                HandleDataPacket(connection);
	                
	                Listener.BeginAcceptTcpClient(AcceptClientAsync, null);
                }
            }
        }
		
		public void HandleDataPacket(TcpClient connection)
		{
			if(Listening)
            {
				Queue<byte> queue = new Queue<byte>();
				
				while(connection.GetStream().DataAvailable)
					queue.Enqueue((byte) connection.GetStream().ReadByte());
            			
				PacketManager.Execute(new JavaPacket(queue.ToArray()), connection);
            }
		}
		
		public void SendPacket(TcpClient connection, JavaPacket packet)
		{
			if(Listening)
            {
				packet.BeginRead();

				connection.Client.Send(packet.DataBytes);
				
				//Logger.Info(packet.AsStringText());
            }
		}
	}
}
