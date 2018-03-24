using System;
using Gtk;

namespace Diplom
{
	
	class MainClass
	{
		public static MainWindow win;
		public static Dialog dl;
		public static void Main()
		{
			Application.Init();
			win = new MainWindow();
			win.ShowAll();
			Application.Run();


		}
		public static void Start()
		{
			ModbusASCIIInterface Com = new ModbusASCIIInterface();
			Com.initPort(SettingsWindows.tempBegin.Value);
		}

	}

}
