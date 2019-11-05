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
using System.Collections.Generic;

namespace Blocks.Utils
{
	/// <summary>
	/// Properties config.
	/// </summary>
	public class Config
	{
		public const string SWITCH_ON = "on";
		public const string SWITCH_OFF = "off";
		
		string Source;
		public Dictionary<string, string> Properties;
		
		public string Path { get { return Source; } }
		
		public Config(string path)
		{
			Source = path;
			
			Properties = new Dictionary<string, string>();
			
			if(File.Exists(Path))
			{
				string[] lines = File.ReadAllLines(Path);
				foreach(string line in lines)
				{
					string key = line.Split('=')[0];
					string val = line.Split('=')[1];
					Properties[key] = val;
				}
			}
		}
		
		public string GetProperty(string Property)
		{
			if(ExistsProperty(Property))
				return Properties[Property];
			else return null;
		}
		
		public bool ExistsProperty(string Property)
		{
			return Properties.ContainsKey(Property);
		}
		
		public void SetProperty(string Property, string Value)
		{
			Properties[Property] = Value;
		}
		
		public void Clear()
		{
			Properties.Clear();
		}
		
		public void Save()
		{
			List<string> list = new List<string>();
			
			foreach(string key in Properties.Keys)
			{
				list.Add(key + "=" + Properties[key]);
			}
			
			File.WriteAllLines(Path, list);
		}
	}
}