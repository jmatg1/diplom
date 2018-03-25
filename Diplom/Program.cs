using System;
using Gtk;
using System.IO.Ports;

namespace Diplom
{
	
	class MainClass
	{
		public static MainWindow win; 	// главное окно
		public static Dialog dl;		// Диалоговое окно
		 // Объявляем переменные их можно изменить в Диалоговом окне "Настройки" 
		// Само присвоение в файле SettingsWindows.cs функция OnButtonOkClicked
		public static int tempBegin = -30;
		public static int tempEnd = 100;
		public static int tempStep = 2;

		public static int timeSp = 5;
		public static int timeSp1 = 10;

		public static string comPort = "COM9";
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
			//Com.initPort(SettingsWindows.tempBegin.Value);
		}

	}

}
