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
			Console.WriteLine($"Current Enable Thread > {Environment.ProcessorCount}");
			
			var affinity = (IntPtr) 1;

			if (!Regex.IsMatch(processorName, @" Ryzen", RegexOptions.Singleline))
			{
				Console.WriteLine("Processor Not Compatible");
				Environment.Exit(0);
			}else if (Regex.IsMatch(processorName, @" [123]600(X|XT|AF)?", RegexOptions.Singleline))
			{
				// 6C/12T(3C/CCX)
				if (Environment.ProcessorCount == 12)
				{
					affinity = (IntPtr) 0x0FC0;
				}
				else
				{
					affinity = (IntPtr) 0x0038;
				}
			}else if (Regex.IsMatch(processorName, @" [123][78]00(X|XT)?", RegexOptions.Singleline))
			{
				// 8C/16T(4C/CCX)
				if (Environment.ProcessorCount == 16)
				{
					affinity = (IntPtr) 0xFF00;
				}
				else
				{
					affinity = (IntPtr) 0x00F0;
				}
			}else if (Regex.IsMatch(processorName, @" 3900(X|XT)?", RegexOptions.Singleline))
			{
				// 12C/24T(3C/CCX)
				if (Environment.ProcessorCount == 24)
				{
					affinity = (IntPtr) 0x00FC0000;
				}
				else
				{
					affinity = (IntPtr) 0x0E00;
				}
			}else if (Regex.IsMatch(processorName, @" 3950(X|XT)?", RegexOptions.Singleline))
			{
				// 16C/32T(4C/CCX)
				if (Environment.ProcessorCount == 32)
				{
					affinity = (IntPtr) 0xFF000000;
				}
				else
				{
					affinity = (IntPtr) 0xF000;
				}
				
			}else if (Regex.IsMatch(processorName, @" 5900X", RegexOptions.Singleline))
			{
				// 12C/24T(6C/CCX)
				if (Environment.ProcessorCount == 24)
				{
					affinity = (IntPtr) 0x00FFF000;
				}
				else
				{
					affinity = (IntPtr) 0x0FC0;
				}
			}else if (Regex.IsMatch(processorName, @" 5950X", RegexOptions.Singleline))
			{
				// 16C/32T(8C/CCX)
				if (Environment.ProcessorCount == 32)
				{
					affinity = (IntPtr) 0xFFFF0000;
				}
				else
				{
					affinity = (IntPtr) 0xFF00;
				}
			}
			else
			{
				Console.WriteLine("Processor Not Compatible");
				Environment.Exit(0);
			}

			Console.WriteLine("Check VRChat Launcher Process...");
			SetAffinity("start_protected_game", affinity);
		}
	}

}