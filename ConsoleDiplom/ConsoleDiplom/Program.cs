using System;
using System.Threading;
using System.Linq; // for List<>
using System.Numerics; // for complex
using System.Collections.Generic;
using System.Net.NetworkInformation;
namespace ConsoleDiplom
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			diplom.ModbusASCIIInterface com = new diplom.ModbusASCIIInterface();
			diplom.TelnetConnection tc = new diplom.TelnetConnection();

			//tc.Open("192.168.0.2");
			//tc.WriteLine("asd");
			//com.initPort("COM11");
			//com.setTargetTemperature(25);
			Console.WriteLine("1. com port\t2 telnet\t3 finale program");
			//tc.Open("192.168.0.2");
			//while (true)
			//{
				
			//	Console.ReadKey();
			//	tc.GetFreq(1);
			//}
			switch (int.Parse(Console.ReadLine()))
			{
				case 1:
					{
						Console.WriteLine("Соединение ком порт...");
						com.initPort("CO");
						for (int i = 0; i < 10; i++)
						{

							Console.WriteLine("Отпавляем уставку");
							if (com.setTargetTemperature(i))
								Console.Write("---Успешно. Ждем 25 сек");
							Thread.Sleep(25000);// 60 000 = 1 минута
							Console.WriteLine("Узнаем температуру");
							Console.Write("---" + com.getCurrentTemperature());
							Console.WriteLine("Ждем 25 сек и повтор еще" + i);
							Thread.Sleep(25000);// 60 000 = 1 минута

						}
						com.Close();
						if (com.CheckOpen())
							Console.WriteLine("Закрыли ком порт");
						else
							Console.WriteLine("Не закрыли ком порт!!");
						break;
					}
				case 2:
					{
						List<Complex> Scomplex = new List<Complex>();
						List<double> freq = new List<double>();
						Console.WriteLine("Соединение телнет...");
						tc.Open("192.168.0.2");
						for (int i = 0; i < 4; i++)
						{

								Console.WriteLine("Узнаем Sp re, im");
								Scomplex = tc.doMeasurement(1, "S11");
								Console.Write(" Получено данных: " + Scomplex.Count + " Ждем 25 сек");
								Thread.Sleep(25000);// 60 000 = 1 минута

							Console.WriteLine("Узнаем частоты c канала 1 ");
							freq = tc.GetFreq(1);
							Console.Write(" Получено данных: " + freq.Count + " Ждем 25 сек");
							Thread.Sleep(25000);// 60 000 = 1 минута

							Console.WriteLine("Узнаем Sp abs, im");
							tc.AmplitudeAndPhase(1, "S11");
							Console.Write(" Получено данных: " + Scomplex.Count + " Ждем 25 сек b Повторить еще " + i);
							Thread.Sleep(25000);// 60 000 = 1 минута

						}
						tc.Close();
						if (tc.IsOpen)
							Console.WriteLine("Закрыли телнет");
						else
							Console.WriteLine("Уже закрыт телнет!!");
						break;
					}
				case 3:
					{
						List<Complex> S = new List<Complex>();
						List<Complex> S1 = new List<Complex>();
						Console.WriteLine("Соединение телнет...");
						tc.Open("192.168.0.2");
						S = tc.doMeasurement(1, "S11");
						Console.WriteLine("S.count " + S.Count + "\tЖдем 2 минуты ");
						Thread.Sleep(120000);// 60 000 = 1 минута
						S1 = tc.doMeasurement(1, "S11");
						Console.WriteLine("S1.count " + S1.Count + "\t СКо =  " + tc.MSD(S, S1));
						tc.Close();
						if (tc.IsOpen)
							Console.WriteLine("Закрыли телнет");
						else
							Console.WriteLine("Уже закрыт телнет!!");
						break;
					}
			}
			Console.ReadKey();
		}
	}
}
