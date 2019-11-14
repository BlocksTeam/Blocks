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
using System.Threading;

namespace Blocks.Network.Java
{
	/// <summary>
	/// TCPListener: Java Edition Minecraft Listener.
	/// </summary>
	public class TCPListener : Listener
	{
		public Socket Listener;
		
		internal IPEndPoint EndPoint;
		
		protected bool Listening = false;
		
		public TCPListener()
		{
			ConnectionType = Types.ConnectionType.TCP;
			
			PacketManager.Initialize();
		}
		
		public override void StartListen()
		{
			Address = IPAddress.Any;
			Port = int.Parse(Server.Properties.GetProperty("javaedition.port"));
			
			EndPoint = new IPEndPoint(Address, Port);
			
			Server.Log("Starting TCPListener on {0}...", EndPoint.ToString());
			
			Listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			
			Listening = true;
			
			Listener.Bind(EndPoint);
			
			Listener.Listen(Server.MaxOnline);
            
            new Thread(DataStreamManage).Start();
            
            ConnectinsManage();
		}
		
		public override void Close()
		{
			Listener.Close();
			
			Listening = false;
			
			Listener = null;
		}
		
		public void HandleData(Socket connection)
		{
			if(Listening)
            {
				byte[] data = new byte[connection.Available];
				
				connection.Receive(data);
				
				Container container = new Container(data);
				
				//Server.Info("In container: {0} packets", container.ToPackets().Length);
				
				foreach(JavaPacket packet in container.ToPackets()) PacketManager.Execute(packet, connection);
            }
		}
		
		internal readonly List<Socket> ActiveConnections = new List<Socket>();
		
		public void SendPacket(Socket connection, JavaPacket packet, bool closeConnection = false)
		{
			if(Listening)
            {
				packet.BeginRead();
				
				if(closeConnection) 
				{	
					if(ActiveConnections.Contains(connection))
						ActiveConnections.Remove(connection);
				}
				
				if(connection.Connected) 
					connection.Send(packet.DataBytes);
				else connection.Close();
            }
		}
		
		public int CHECK_TIMEOUT_MS = 25;
		
		protected void DataStreamManage()
		{
			while(Listening)
			{
				if(ActiveConnections.Count > 0)
				{
					foreach(Socket c in ActiveConnections.ToArray())
					{
						if(!c.Connected)
						{
							ActiveConnections.Remove(c);
						}
						else if(c.Available > 0) HandleData(c);
					}
				}
				
				Thread.Sleep(CHECK_TIMEOUT_MS);
			}
		}
		
		protected void ConnectinsManage()
		{
			while(Listening)
			{
				Socket connection = Listener.Accept();
				
				ActiveConnections.Add(connection);
			}
		}
	}
}
