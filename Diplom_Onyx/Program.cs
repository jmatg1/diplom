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
		public static string pathLog;

		public static void Main()
		{
			string path = Directory.GetCurrentDirectory();          //Место где лежит наша программа
			List<string> valueSettings;
			pathSettingsFile = path + Path.AltDirectorySeparatorChar + @"settings.txt";
			if (!Directory.Exists(path + Path.AltDirectorySeparatorChar + @"log"))
			{                   // Если нет папки то создаем
				Directory.CreateDirectory(path + Path.AltDirectorySeparatorChar + @"log");
			}

			if (!Directory.Exists(path + Path.AltDirectorySeparatorChar + @"out"))
			{                   // Если нет папки то создаем
				Directory.CreateDirectory(path + Path.AltDirectorySeparatorChar + @"out");
			}
			pathOutFileFreq = path + Path.AltDirectorySeparatorChar + @"out" + Path.AltDirectorySeparatorChar + "freq_" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ".txt";
			pathOutFileSp = path + Path.AltDirectorySeparatorChar + @"out" + Path.AltDirectorySeparatorChar + "Sp_" + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ".txt";
			pathLog = path + Path.AltDirectorySeparatorChar + @"log" + Path.AltDirectorySeparatorChar + DateTime.Now.Year + "." + DateTime.Now.Month + "." + DateTime.Now.Day + " " + DateTime.Now.Hour + "." + DateTime.Now.Minute + "." + DateTime.Now.Second + ".txt";
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
		private static bool flagThreadStart = true;                                             // Истина поток запущен, флаг проверяется в различных функция файлов cclin, nalib  и в этом классе
		public event Action<string> LogWriteLine;
		public event Action<List<double>> ShowC;//Вызывается здесь, обрабатываается в файле MainWindows.
		public event Action<bool> ClearCandMSD;
		public event Action<List<Complex>, List<Complex>, List<double>> ShowSSt;
		public event Action<List<double>> ShowMSD; //Вызывается здесь, обрабатываается в файле MainWindows. 
		public event Action<string> WriteLabelSetting;                              //Вывод уставки
		public event Action<string> WriteLabelTemp;
		public event Action<string> WriteLabelMSD;
		public event Action<bool> activateButtonStart;
		public event Action<string> ShowDialog;
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
		public double actualTemp;
		public void Start()
		{
			flagThreadStart = true;
            //ClearCandMSD(true); //очищаем графики
			int tempEnd;
			tempEnd = MainClass.tempEnd + 1;
			Console.WriteLine(tempEnd);
			                                       // текущая тмпература в камере
			List<Double> valueMSD = new List<Double>();      //Среднее квадратичное отклонение
			List<Double> SredneeValueMSD = new List<Double>();      //Среднее квадратичное отклонение отклоений 5 сек
			List<Complex> C = new List<Complex>();           //  это первая матрица рассеивания после готовой уставке(уст = тек темп в кам)
			List<double> modulC = new List<double>();           //  это 
			List<double> summModulC = new List<double>();           //  это 
			List<double> sigma = new List<double>();

			for (int i = MainClass.tempBegin; (i <= MainClass.tempEnd && MainClass.tempBegin < MainClass.tempEnd) ^ (i > tempEnd); i += MainClass.tempStep)
			{
	


				Com.initPort(MainClass.comPort); LogWriteLine("Подключились к " + MainClass.comPort);
				timeSecond = 0;

				actualTemp = Com.getCurrentTemperature();
				if (actualTemp <= 0 && i <= 0)
				{
					Com.setTargetTemperature(-15);
					double tektemp = Convert.ToDouble(i);
					do
					{
						Thread.Sleep(60000);
						actualTemp = Com.getCurrentTemperature(); WriteLabelTemp(actualTemp.ToString()); LogWriteLine("Температура в камере " + actualTemp.ToString());


					} while (Com.getCurrentTemperature() <= -15);
				}
					
				if (Com.setTargetTemperature(i) == true)
				{
					LogWriteLine("Установили УСТАВКУ: " + i + ". Каждые 60 сек. проверяем установление уставки");
					WriteLabelSetting(i.ToString());
				}
				goto ANALIZ;
				telnet.Connect("192.168.0.2");		LogWriteLine("Подключились к телнет серверу");
				C = telnet.doMeasurement(1, "S12");
				while (true)
				{
					

					freq = telnet.GetFreq(1);

					actualTemp = Com.getCurrentTemperature(); WriteLabelTemp(actualTemp.ToString()); LogWriteLine("Температура в камере " + actualTemp.ToString());

				

					//double temporaryMSD = telnet.MSD(S12, S12t);    //Расчет СКО

					//WriteLabelMSD(temporaryMSD.ToString());         // Выводим СКО
					double tektemp = Convert.ToDouble(i);
					if ((Math.Abs(tektemp - actualTemp)) < 0.2)
					{
						Thread.Sleep(60000);
						if ((Math.Abs(tektemp - Com.getCurrentTemperature())) < 0.2)
						{
							break;
						}
					}
					Thread.Sleep(60000);
					S12t = telnet.doMeasurement(1, "S12");
                    ShowSSt(C, S12t, freq);
				}	//конец цикла установление уставки
				LogWriteLine("Температура в камере равна уставке.");

				//ANALIZ
				ANALIZ:
				telnet.Connect("192.168.0.2");		LogWriteLine("Подключились к телнет серверу");				//-----
				C = telnet.doMeasurement(1, "S12");
                
				while (flagMsdZero)
				{
					LogWriteLine("Получаем частоты..");
					freq = telnet.GetFreq(1);

					for (int e = 1; e <= 20; e++)
					{
						LogWriteLine(e+":Сбор 10 штук модуля S12. Каждые " + MainClass.timeSp + "сек.");
						for (int n = 0; n < 10; n++)
						{
							LogWriteLine("Получаем S12");
							S12 = telnet.doMeasurement(1, "S12");
							modulC.Add(telnet.modulS(S12));	//Модуль S
							LogWriteLine("modulC = "+ modulC.Count);
							Thread.Sleep(MainClass.timeSp * 1000);
							ShowSSt(C, S12, freq);
							actualTemp = Com.getCurrentTemperature(); WriteLabelTemp(actualTemp.ToString());
						}
                        
						summModulC.Add(telnet.summModulS(modulC));  //считаем сумма всех модулей и заносим
						LogWriteLine("summModulC = " + summModulC.Last().ToString());
						modulC.Clear();
						File.AppendAllText(@"C:\summModulC.txt",summModulC.Last().ToString()+ Environment.NewLine, Encoding.UTF8);
                         ShowC(summModulC);       //вывод графика n штук сумммодуля
					}
					sigma.Add(telnet.sigma(summModulC.GetRange(summModulC.Count-20,20)));

					          
					LogWriteLine("Sigma = "+sigma.Last().ToString());
					WriteLabelMSD(sigma.Last().ToString());         // Выводим СКО
                    ShowMSD(sigma);
					if (sigma.Last() < MainClass.entryMSD)
					{
						
						//ЗАпись в файл
						WriteFileOutFreq(freq);
						WriteFileOutSp(S12);
						//--------------------------
						break;
					}



				}
				// очистка
				//summModulC.Clear();
				//sigma.Clear();
				//--------


			} // конец цикла с температурой


			if (Diplom.MainProgramm.Com.CheckOpen())            // закрываем ком порт, если включен
				Diplom.MainProgramm.Com.Dispose();
			if (Diplom.MainProgramm.telnet.IsOpen)            // закрываем телнет, если включен
				Diplom.MainProgramm.telnet.Dispose();
			activateButtonStart(true);                          // Включаем кнопку старт

			Gtk.Application.Invoke(delegate
			{
				MessageDialog md = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "РАБОТА ЗАВЕРШЕНА");
				md.Run();
				md.Destroy();
			});

			LogWriteLine("РАБОТА ЗАВЕРШЕНА!\n\n\n\n\n\n");


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
				File.AppendAllText(MainClass.pathOutFileFreq, "#temperature "+actualTemp+testFreq+Environment.NewLine + Environment.NewLine, Encoding.UTF8);
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
			File.AppendAllText(MainClass.pathOutFileSp, "#temperature "+actualTemp+ "END"+Environment.NewLine, Encoding.UTF8);
			//File.AppendAllText(MainClass.pathOutFileSp, Environment.NewLine, Encoding.UTF8);

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
					textSpSpt += Sp[i].Real.ToString() + "," + Sp[i].Imaginary.ToString() + Environment.NewLine +
						Spt[i].Real.ToString() + "," + Spt[i].Imaginary.ToString() + Environment.NewLine;
					//File.AppendAllText(MainClass.pathOutFileSp, 
				}
				File.AppendAllText(MainClass.pathOutFileSp, textSpSpt + Environment.NewLine, Encoding.UTF8);
			}
			catch
			{
				Diplom.MainClass.win.Log("Ошибка записи в файл Sp_.txt");
			}
		}
	}

}
