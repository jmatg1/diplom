using System;

namespace ConsoleDiplom
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			diplom.ModbusASCIIInterface com = new diplom.ModbusASCIIInterface();
			diplom.TelnetConnection tc = new diplom.TelnetConnection();
			//tc.Open("192.168.0.2");
			tc.WriteLine("asd");
			//com.initPort("COM11");
			com.setTargetTemperature(25);
			Console.WriteLine("GOOD");
			Console.ReadKey();
		}
	}
}
