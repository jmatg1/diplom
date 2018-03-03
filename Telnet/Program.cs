using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static System.String;
using System.Collections.Generic;
using System.Linq;

namespace Diplom
{
	class Program
	{
		static void Main(string[] args)
		{
			
			//create a new telnet connection to hostname "gobelijn" on port "23"
			nalib.TelnetConnection tc = new nalib.TelnetConnection("127.0.0.1", 9090);
			Console.Write(tc.IsConnected);
			List<double> freq = tc.GetFreq(1);
			foreach (double combo in freq)
			{
				Console.WriteLine(combo);  
			}

			Console.ReadKey();
		



		}
	}
}