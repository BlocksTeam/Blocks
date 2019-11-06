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
using System.Threading;

namespace Blocks.Task
{
	/// <summary>
	/// AsyncTask as Thread.
	/// </summary>
	public abstract class AsyncTask
	{
		string Name;
		bool Started;
		
		Thread CurrentThread;
		
		protected AsyncTask(string TaskName = "AsyncTask", bool AutoStart = false)
		{
			Name = TaskName;
			
			Started = false;
			
			if(AutoStart) Open();
		}
		
		public override string ToString()
		{
			return Name;
		}

		
		public void Open(params object[] args)
		{
			if(!Started)
			{
				CurrentThread = new Thread( (ThreadStart) delegate
				{
				           	Run(args);
				});
				CurrentThread.Start();
				
				Started = true;
			}
		}
		
		public void Kill()
		{
			if(Started)
			{
				CurrentThread.Abort();
			}
		}
		
		protected abstract void Run(params object[] args);
	}
}
