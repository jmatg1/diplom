using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static System.String;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;//for complex
namespace diplom
{

	class Program
	{
		static TelnetConnection tc;
		static int Main(string[] args)
		{
			/*List<List<int>> Mas = new List<List<int>>();    //динамический двумерный массив
			List<int> row = new List<int>();                //строка массива
			int n = 3;
 
            for (int i = 0; i<n; i++)
            {
                row = new List<int>();
                for (int j = 0; j<n; j++) row.Add(i + j); //строка массива заполняется просто суммой i и j
                Mas.Add(row);                               //строка добавляется в массив
            }
 
            for (int i = 0; i<n; i++)                     //вывод массива
            {             
                for (int j = 0; j<n; j++)
                    Console.Write(Mas[i][j].ToString()+" ");
                Console.WriteLine();
            }*/
			List<Complex> Scomplex = new List<Complex>();
			Scomplex.Add(new Complex(Math.Pow(10,-26.083/20),	Math.Pow(Math.E, -10.5595383210021*Math.PI/180)));
			Scomplex.Add(new Complex(20 * Math.Log10(0.0496378742882),	180 * -0.184298711192/Math.PI));
			Scomplex.Add(new Complex(1,2));
		
			Console.WriteLine(Scomplex[0].Real + " " + Scomplex[0].Imaginary);
			Console.WriteLine(Scomplex[1].Real+ " " + Scomplex[1].Imaginary);
			Console.Write(Scomplex[2].Real + " " + Scomplex[2].Imaginary);


			string hostName = "192.168.0.2";

			try
			{
				tc = new TelnetConnection();
				tc.ReadTimeout = 10000; // 10 sec
										// open socket on hostName, which can be an IP address, or use host name (e.g. "A-N9912A-22762") used in lieu of IP address
				tc.Open(hostName); // hostname
				if (tc.IsOpen)
				{

					//Start your program here
					//tc.WriteLine(":SENS:DATA:CORR? S11");
					//Console.WriteLine(tc.Read());
					//tc.GetFreq(1);
					tc.doMeasurement(1, "S11");


					Console.WriteLine("n");
					//tc.WriteLine(":SENS:DATA:CORR? S11");
					//Console.WriteLine(tc.Read());
					//tc.Dispose();
					Console.WriteLine("Press any key to exit.");

					Console.ReadKey(); // continue after reading a key from the keyboard.
				}
				else
				{
					Console.WriteLine("Error opening " + hostName);
					Console.ReadKey();
					return -1;
				}
				//FieldFox Programming Guide 5
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.ReadKey();
				return -1;
			}
			// exit normally
			Console.ReadKey();
			return 0;
		}


	}

}