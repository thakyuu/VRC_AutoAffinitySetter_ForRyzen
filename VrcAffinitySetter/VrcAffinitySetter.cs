using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace VrcAffinitySetter
{
	internal static class VrcAffinitySetter
	{

		private static void SetAffinity(string processName,IntPtr affinity)
		{
			var targetProcesses = Process.GetProcessesByName(processName);
			if (targetProcesses.Length == 0)
			{
				Console.WriteLine($"{processName} not detected");
				Environment.Exit(0);
			}

			var targetProcess = targetProcesses[0];
			
			Console.WriteLine($"{processName} Detected!");
			targetProcess.ProcessorAffinity = affinity;
			Console.WriteLine($"{processName} Affinity Change Complete!");
		}

		public static void Main(string[] args)
		{
			var key = Registry.LocalMachine.OpenSubKey(@"HARDWARE\DESCRIPTION\System\CentralProcessor\0\");
			if (key == null)
			{
				Console.WriteLine("Can't Get Processor Information");
				Environment.Exit(1);
			}

			var processorName = key.GetValue("ProcessorNameString").ToString();
			Console.WriteLine($"Current Processor > {processorName}");

			var affinity = (IntPtr) 1;
			
			if (Regex.IsMatch(processorName, @" 3600(X|XT)?", RegexOptions.Singleline))
			{
				affinity = (IntPtr) 0x0FC0;
			}else if (Regex.IsMatch(processorName, @" 3[78]00(X|XT)?", RegexOptions.Singleline))
			{
				affinity = (IntPtr) 0xFF00;
			}else if (Regex.IsMatch(processorName, @" 3900(X|XT)?", RegexOptions.Singleline))
			{
				affinity = (IntPtr) 0x00FC0000;
			}else if (Regex.IsMatch(processorName, @" 3950(X|XT)?", RegexOptions.Singleline))
			{
				affinity = (IntPtr) 0xFF000000;
			}else if (Regex.IsMatch(processorName, @" 5900X", RegexOptions.Singleline))
			{
				affinity = (IntPtr) 0x00FFF000;
			}else if (Regex.IsMatch(processorName, @" 5950X", RegexOptions.Singleline))
			{
				affinity = (IntPtr) 0xFFFF0000;
			}
			else
			{
				Console.WriteLine("Processor Not Compatible");
				Environment.Exit(0);
			}

			Console.WriteLine("Check VRChat Process...");
			SetAffinity("VRChat", affinity);
		}
	}

}