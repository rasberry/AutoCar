using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoCar
{
	public class Config
	{
		//SimpleServer
		public static int Port { get { return GetInt("Port"); }}
		public static string OutFile { get { return GetString("OutFile"); }}
		//Video for Linux
		public static string DeviceOne { get { return GetString("DeviceOne"); }}
		public static string DeviceTwo { get { return GetString("DeviceTwo"); }}
		//StereoSGBM
		public static int MinDisparity { get { return GetInt("MinDisparity"); }}
		public static int NumDisparities { get { return GetInt("NumDisparities"); }}
		public static int SADWindowSize { get { return GetInt("SADWindowSize"); }}
		public static int P1 { get { return GetInt("P1"); }}
		public static int P2 { get { return GetInt("P2"); }}
		public static int Disp12MaxDiff { get { return GetInt("Disp12MaxDiff"); }}
		public static int PreFilterCap { get { return GetInt("PreFilterCap"); }}
		public static int UniquenessRatio { get { return GetInt("UniquenessRatio"); }}
		public static int SpeckleWindowSize { get { return GetInt("SpeckleWindowSize"); }}
		public static int SpeckleRange { get { return GetInt("SpeckleRange"); }}
		//StereoGC
		public static int NumberOfDisparities { get { return GetInt("NumberOfDisparities"); }}
		public static int MaxIters { get { return GetInt("MaxIters"); }}

		static int GetInt(string name)
		{
			ReadConfig();
			return (int)parsed[name]; //should fail here if name or type is bad
		}
		static string GetString(string name)
		{
			ReadConfig();
			return (string)parsed[name]; //should fail here if name or type is bad
		}

		static Dictionary<string,object> parsed;
		static void ReadConfig()
		{
			if (parsed != null) { return; }
			parsed = new Dictionary<string,object>(StringComparer.OrdinalIgnoreCase);
			string[] lines = File.ReadAllLines("config.ini");

			foreach(string l in lines)
			{
				if (l.StartsWith("#")) { continue; }
				string k; int v; int x;
				if (-1 != (x = l.IndexOf('='))) {
					k = l.Substring(0,x).Trim();
					string sv = l.Substring(x+1).Trim();
					if (int.TryParse(sv,out v)) {
						parsed[k] = v;
					} else {
						parsed[k] = sv;
					}
				}
			}
		}
	}
}
