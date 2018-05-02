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
// for file
using System.Linq;
using System.Text;
using System.IO;

namespace Diplom
{


	class MainClass
	{
		public static MainWindow win;   // главное окно
		public static SettingsWindows dl;       // Диалоговое окно

		// Объявляем переменные их можно изменить в Диалоговом окне "Настройки" 
		// Само присвоение в файле SettingsWindows.cs функция OnButtonOkClicked
		public static int tempBegin = 32;
		public static int tempEnd = 35;
		public static int tempStep = 1;

		public static int timeSp = 5;
		public static int timeSp1 = 300;

		public static double entryMSD = 0.000004;

		public static string comPort = "COM11";

		public static string pathSettingsFile;
		public static string pathOutFileFreq;
		public static string pathOutFileSp;


		public static void Main()
		{
			Console.WriteLine(); // 01.01.0001 0:00:00
			string path = Directory.GetCurrentDirectory();          //Место где лежит наша программа
			List<string> valueSettings;
			pathSettingsFile = path + @"\settings.txt";
			if (!Directory.Exists(path + @"\log"))
			{                   // Если нет папки то создаем
				Directory.CreateDirectory(path + @"\log");
			}

			if (!Directory.Exists(path + @"\out"))
			{                   // Если нет папки то создаем
				Directory.CreateDirectory(path + @"\out");
			}
			pathOutFileFreq = path + @"\out\freq_" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute +"."+DateTime.Now.Second + ".txt";
			pathOutFileSp = path + @"\out\Sp_" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute + "."+DateTime.Now.Second + ".txt";
			if (File.Exists(pathSettingsFile)) // Если есть файла с настройками, то записываем их в переменные
			{
				string valueSettingsText = File.ReadAllText(pathSettingsFile);
				valueSettings = valueSettingsText.Split(',').ToList();

				MainClass.tempBegin = Convert.ToInt32(valueSettings[0]);
				MainClass.tempEnd = Convert.ToInt32(valueSettings[1]);
				MainClass.tempStep = Convert.ToInt32(valueSettings[2]);

				MainClass.timeSp = Convert.ToInt32(valueSettings[3]);
				MainClass.timeSp1 = Convert.ToInt32(valueSettings[4]);

				MainClass.entryMSD = Convert.ToDouble(valueSettings[5]);
				MainClass.comPort = valueSettings[6];

			}

			//				FileStream file1 = new FileStream(pathSettingsFile, FileMode.Open);
			//StreamWriter writer = new StreamWriter(file1);
			//writer.WriteLine("asd");
			//				writer.Close();


			Console.WriteLine(Directory.Exists(path + @"\log"));
			string pathLog = path + @"\log\" + DateTime.Now + ".txt";


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

		public static bool flagMsdZero = true;                                          // флаг =  ложь после время Sp1, тогда выход из цикла и будем считать что темп. в объекте равна темп. камеры
		private static bool flagThreadStart;                                             // Истина поток запущен, флаг проверяется в различных функция файлов cclin, nalib  и в этом классе
		public event Action<string> LogWriteLine;
		public event Action<List<Complex>, List<Complex>> ShowC;//Вызывается здесь, обрабатываается в файле MainWindows. 
		public event Action<List<Complex>, List<Complex>, List<double>> ShowSSt;
		public event Action<List<double>> ShowMSD; //Вызывается здесь, обрабатываается в файле MainWindows. 
		public event Action<string> WriteLabelSetting;                              //Вывод уставки
		public event Action<string> WriteLabelTemp;
		public event Action<string> WriteLabelMSD;
		/// <summary>
		/// Истина - поток запущен, иначе остановлен
		/// </summary>
		public static bool SetFlagThread
		{
			get
			{
				return flagThreadStart;
			}
			set
			{
				flagThreadStart = value;
			}
		}
		public static TelnetConnection telnet = new TelnetConnection();
		public static ModbusASCIIInterface Com = new ModbusASCIIInterface();
		public static Thread flagZero; // Должен быть глобальным чтобы можно было завершить кнопкой стоп 											// Поток, в котором идет время Sp1, после истечение которого, меняем температуру в камере
		public static int timeSecond;
		public void Start()
		{
			
			int tek_temp;                                       // текущая тмпература в камере
			List<Double> valueMSD = new List<Double>();      //Среднее квадратичное отклонение
			List<Complex> C = new List<Complex>();           //  это первая матрица рассеивания после готовой уставке(уст = тек темп в кам)
			List<Complex> Ct = new List<Complex>();           //  это 


			for (int i = MainClass.tempBegin; i <= MainClass.tempEnd; i += MainClass.tempStep)
			{
				flagZero = new Thread(new ThreadStart(FuncFlagZero));   // Создаем поток, который отсчитывает время timeSp1, по истечению времени уставка сменится
				flagZero.IsBackground = true;                           //теперь он фоновый и его можно закрыть по закытию  программы
				flagMsdZero = true;										// если лож то выходим из цикла и меняем уставку
				Com.initPort(MainClass.comPort);
				LogWriteLine("Подключились к " + MainClass.comPort);
				timeSecond = DateTime.Now.Second;
				if (Com.setTargetTemperature(i) == true)
				{
					LogWriteLine("Установили УСТАВКУ: " + i + ". Ждем 5 сек. когда Уст. = Тек. Темпер.");
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

				flagZero.Start();
				LogWriteLine("Смена температуры после " + Diplom.MainClass.timeSp1 + "сек. Если СКО не будет меньше " + MainClass.entryMSD.ToString() + "в течение этого времени");

				LogWriteLine("Получаем C12");
				C = (telnet.doMeasurement(1, "S12"));

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

					LogWriteLine("Ждем " + MainClass.timeSp + " сек.");
					Thread.Sleep(MainClass.timeSp * 1000);

					LogWriteLine("Получаем S12t");
					S12t = telnet.doMeasurement(1, "S12");

					LogWriteLine("Обновили график S12");
					ShowSSt(S12, S12t, freq);                      // Показываем график с разнице во времени S

					double temporaryMSD = telnet.MSD(S12, S12t);    //Расчет СКО

					WriteLabelMSD(temporaryMSD.ToString());         // Выводим СКО
					valueMSD.Add(temporaryMSD);                     // Заносим в массив ско

					LogWriteLine("Обновили график СКО");
					ShowMSD(valueMSD);                              // 
					LogWriteLine("Обновили график С12");
					//Ct.AddRange(S12t);
					ShowC(C, S12t);
					//ЗАпись в файл
					WriteFileOutFreq(freq);
					WriteFileOutSp(S12, S12t);

					if (valueMSD[valueMSD.Count - 1] <= MainClass.entryMSD)
					{
						LogWriteLine("Условие выполняется: " + valueMSD[valueMSD.Count - 1] + "<=" + MainClass.entryMSD + "Изменяем уставку");
						telnet.Dispose();
						LogWriteLine("Отключил телнет");
						flagZero.Abort();       //Закрываем поток
						break;
					}
					LogWriteLine("Условие не выполняется: " + valueMSD[valueMSD.Count - 1] + "<=" + MainClass.entryMSD);
				}
				//Diplom.MainClass.
			}

		}
		public static void FuncFlagZero()
		{
			Thread.Sleep(Diplom.MainClass.timeSp1 * 1000);
			Diplom.MainProgramm.flagMsdZero = false;
		}
		public static void WriteFileSetting(string text)
		{
			try
			{
				File.WriteAllText(MainClass.pathSettingsFile, text);
			}
			catch (Exception)
			{
				Diplom.MainClass.win.Log("Ошибка записи в файл settings.txt");
			}
		}
		/// <summary>
		/// Записываем в файл частоты. Каждый новый массив отделен новой пустой строкой
		/// </summary>
		/// <param name="Sp">Sp.</param>
		public void WriteFileOutFreq(List<double> Freq)
		{
			try
			{
				string testFreq = "";
				for (int i = 0; i < Freq.Count; i++)
				{
					testFreq += Freq[i].ToString() + Environment.NewLine;
					//File.AppendAllText(MainClass.pathOutFileFreq, Freq[i].ToString() + Environment.NewLine, Encoding.UTF8);
				}
				File.AppendAllText(MainClass.pathOutFileFreq, testFreq + Environment.NewLine, Encoding.UTF8);
			}
			catch
			{
				Diplom.MainClass.win.Log("Ошибка записи в файл freq_.txt");
			}

		}
		/// <summary>
		/// Записываем в файл матрицу рассеивания одну штуку
		/// </summary>
		/// <param name="Sp">Sp.</param>
		public void WriteFileOutSp(List<Complex> Sp)
		{

			for (int i = 0; i < Sp.Count; i++)
			{
				File.AppendAllText(MainClass.pathOutFileSp, Sp[i].Real.ToString() + "," + Sp[i].Imaginary.ToString() + Environment.NewLine, Encoding.UTF8);
			}
			File.AppendAllText(MainClass.pathOutFileSp, Environment.NewLine, Encoding.UTF8);

		}
		/// <summary>
		/// Записываем в файл две матрицы. Точки записываются так. Sp1, Spt1, Sp2, Spt2 
		/// </summary>
		/// <param name="Sp">Sp.</param>
		/// <param name="Spt">Spt.</param>
		public void WriteFileOutSp(List<Complex> Sp, List<Complex> Spt)
		{
			try
			{
				string textSpSpt = "";
				for (int i = 0; i < Sp.Count; i++)
				{
					textSpSpt+=		Sp[i].Real.ToString() + "," + Sp[i].Imaginary.ToString() + Environment.NewLine +
					                Spt[i].Real.ToString() + "," + Spt[i].Imaginary.ToString() + Environment.NewLine;
					//File.AppendAllText(MainClass.pathOutFileSp, Sp[i].Real.ToString() + "," + Sp[i].Imaginary.ToString() + Environment.NewLine, Encoding.UTF8);
					//File.AppendAllText(MainClass.pathOutFileSp, Spt[i].Real.ToString() + "," + Spt[i].Imaginary.ToString() + Environment.NewLine, Encoding.UTF8);
				}
				File.AppendAllText(MainClass.pathOutFileSp,textSpSpt + Environment.NewLine, Encoding.UTF8);
			}
			catch
			{
				Diplom.MainClass.win.Log("Ошибка записи в файл Sp_.txt");
			}
		}
	}

}
