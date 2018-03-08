using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static System.String;
using System.Collections.Generic;
using System.Linq;

namespace diplom
{

	class Program
	{
		static TelnetConnection tc;
		static int Main(string[] args)
		{





List<List<int>> Mas = new List<List<int>>();    //динамический двумерный массив
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
            }









			// defaultHostName is host name to use if one is not specified on the command line.
			string defaultHostName = "192.168.0.2";
			string hostName = defaultHostName;

			try
			{
				tc = new TelnetConnection();
				tc.ReadTimeout = 10000; // 10 sec
										// open socket on hostName, which can be an IP address, or use host name (e.g. "A-N9912A-22762") used in lieu of IP address
				tc.Open(hostName);
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
					return -1;
				}
				//FieldFox Programming Guide 5
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				return -1;
			}
			// exit normally.
			return 0;
		}


	}

}