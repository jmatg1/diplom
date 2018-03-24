using System;
using Gtk;
using System.IO.Ports;

namespace KTX
{
	class initPort
	{
	SerialPort port;
	// получаем список доступных портов
	string[] ports = SerialPort.GetPortNames();
	Console.WriteLine("Выберите порт:");
	// выводим список портов
	for (int i = 0; i<ports.Length;i++)
	{
	    Console.WriteLine("[" + i.ToString() + "] "+ports[i].ToString());
	}

	}
	class MainClass
	{
		public static void Main(string[] args)
		{
			
			Application.Init();
			MainWindow win = new MainWindow();
			win.Show();
			Application.Run();
		}
	}
}
