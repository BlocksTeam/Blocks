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
	public class Config : MetaTag
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
					if(line.Length > 0 && line[0] == '#') continue;
					
					string key = line.Split('=')[0];
					string val = line.Split('=')[1];
					Properties[key] = val;
				}
			}
		}
		
		public string GetProperty(string property)
		{
			if(ExistsProperty(property))
				return Properties[property];
			else return null;
		}
		
		public bool ExistsProperty(string property)
		{
			return Properties.ContainsKey(property);
		}
		
		public void SetProperty(string property, string value)
		{
			Properties[property] = value;
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