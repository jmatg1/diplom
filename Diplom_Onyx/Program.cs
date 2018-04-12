using System.Collections.Generic;
using System.Numerics;
using System;
using System.IO.Ports;
using System.Threading; // for Sleep
using Gtk;
using OxyPlot.GtkSharp;
using OxyPlot;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Diplom
{


	class MainClass
	{
		public static MainWindow win;   // главное окно
		public static SettingsWindows dl;       // Диалоговое окно

		// Объявляем переменные их можно изменить в Диалоговом окне "Настройки" 
		// Само присвоение в файле SettingsWindows.cs функция OnButtonOkClicked
		public static int tempBegin = -30;
		public static int tempEnd = 100;
		public static int tempStep = 2;

		public static int timeSp = 300;
		public static int timeSp1 = 10;

		public static string comPort = "COM11";




		public static void Main()
		{


			Application.Init();
			win = new MainWindow();
			win.ShowAll();
			Application.Run();


		}

	}
	public class MainProgramm
	{



		public List<double> freq;
		public List<Complex> S11 = new List<Complex>();

		public List<Complex> S12 = new List<Complex>();
		public List<Complex> S12t = new List<Complex>();

		public List<Complex> S21 = new List<Complex>();
		public List<Complex> S22 = new List<Complex>();

		public bool flagMsdZero = true; 											// флаг =  ложь после время Sp1, тогда выход из цикла и будем считать что темп. в объекте равна темп. камеры
		private static bool flagThread; 											// Истина поток запущен, флаг проверяется в различных функция файлов cclin, nalib  и в этом классе
		public event Action<string> LogWriteLine;   								//Вызывается здесь, обрабатываается в файле MainWindows. 
		public event Action<List<Complex>, List<Complex>, List<double>> ShowSSt;    //Вызывается здесь, обрабатываается в файле MainWindows. 
		public event Action<string> WriteLabelSetting;								//Вывод уставки
		public event Action<string> WriteLabelTemp;
		public event Action<string> WriteLabelMSD;
		/// <summary>
		/// Истина - поток запущен, иначе остановлен
		/// </summary>
		public static bool SetFlagThread
		{
			get
			{
				return flagThread;
			}	
			set
			{
				flagThread = value;
			}
		}
		public static TelnetConnection telnet;
		public static ModbusASCIIInterface Com = new ModbusASCIIInterface();
		public static Thread flagZero; 											// Поток, в котором идет время Sp1, после истечение которого, меняем температуру в камере
		public void Start()
		{
			int tek_temp; // текущая тмпература в камере

			for (int i = MainClass.tempBegin; i <= MainClass.tempEnd; i += MainClass.tempStep)
			{
				Com.initPort(MainClass.comPort);
				LogWriteLine("Подключились к " + MainClass.comPort);

				if (Com.setTargetTemperature(i) == true)
				{
					LogWriteLine("Установили УСТАВКУ: " + i + ". Ждем когда Уст. = Тек. Темпер.");
					WriteLabelSetting(i.ToString());
				}
				//break;
				while (true)
				{

					Thread.Sleep(5000);
					tek_temp = Com.getCurrentTemperature();
					WriteLabelTemp(tek_temp.ToString());
					LogWriteLine("Температура в камере " + tek_temp.ToString());
					if (i == tek_temp)
						break;
					//System.Threading.Thread.Sleep(1000);
				}
				LogWriteLine("Температура в камере равна уставке. Ком порт отключен.");
				Com.Close();
				//TelnetConnection telnet = new TelnetConnection();
				telnet.Connect("192.168.0.2");
				LogWriteLine("Подключились к телнет серверу");

				flagZero = new Thread(FuncFlagZero); // Должен быть глобальным чтобы можно было завершить кнопкой стоп
				flagZero.Start();

				while (flagMsdZero)
				{
					LogWriteLine("Получаем частоты..");
					freq = telnet.GetFreq(1);
					
					//LogWriteLine ("Получаем S11");
					//S11 = telnet.doMeasurement (1, "S11");

					LogWriteLine("Получаем S12");
					S12 = telnet.doMeasurement(1, "S12");

					//LogWriteLine ("Получаем S21");
					//S21 = telnet.doMeasurement (1, "S21");

					//LogWriteLine ("Получаем S22");
					//S22 = telnet.doMeasurement (1, "S22");

					LogWriteLine("Ждем " + MainClass.timeSp  + " сек.");
					Thread.Sleep(MainClass.timeSp*1000);

					LogWriteLine("Получаем S12t");
					S12t = telnet.doMeasurement(1, "S12");
					telnet.MSD(S12, S12t);
					ShowSSt(S12, S12t, freq);
				}
				//Diplom.MainClass.
			}

		}
		public void FuncFlagZero()
		{
			Thread.Sleep(MainClass.timeSp1*1000);
			flagMsdZero = false;
		}


	}

}
