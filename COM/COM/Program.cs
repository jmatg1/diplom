using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.IO; // file



namespace COM
{


	class MainClass
	{
		static SerialPort port;


		public static void Main(string[] args)
		{

			ModbusASCIIInterface com = new ModbusASCIIInterface();
			double temp = -30;

фвыa
			asdasd



			while (com.initPort("COM12"))
			{
				if (!com.setTargetTemperature(temp))
				{
					ModbusASCIIInterface.comport.Close();
					Thread.Sleep(1000);
					continue;
				}
				Console.WriteLine(localDate.ToString()+": Установили температу: " + temp+ "\n. Ждем 5 минут...");
				Thread.Sleep(300000);
				if (Math.Abs(com.getCurrentTemperature() - 999) < 1)
					continue;
				Console.WriteLine(localDate.ToString()+": Температура в камере: " + temp+ "\n. ");

			}
			Console.ReadKey();
			//asd.DataRec();

			Console.Write(chardig(-25.5, true) + "\r\n");
			Console.Write(chardig(100, true) + "\r\n");
			Console.Write(chardig(-11.1, true) + "\r\n");
			Console.Write(chardig(-11.2, true) + "\r\n");
			//FileSetting();
			//char str = ReturnLRC("FFFF");
			//foreach (char ch in chars)
			//{
			//   try {
			//      byte result = Convert.ToByte(ch);
			//Console.WriteLine("{0} is converted to {1}.", ch, result);
			//   }   
			//   catch (OverflowException) {
			//      Console.WriteLine("Unable to convert u+{0} to a byte.", 
			//                        Convert.ToInt16(ch).ToString("X4"));
			//   }
			//}   
			//byte[] toBytes = Convert.ToByte('01');
			//Console.WriteLine(toBytes[0]);

			//string hexOutput = String.Format("{0:X}", toBytes[0]); Console.WriteLine(hexOutput);
			//			string a = "A";
			//			byte[] byteValue1 =Encoding.ASCII.GetBytes(a);
			//			Console.WriteLine(digchar(toBytes[0]));
			//			Console.WriteLine((toBytes[0]));


			//			//Console.WriteLine(byteValue1[1]);

			//			byte byteValue2 = 42;
			////Console.WriteLine(byteValue1[0]);


			//string a = "ABCDEFabcdef";
			//byte[] b = Encoding.ASCII.GetBytes(a);

			//Console.WriteLine(digchar(b[0]));

			string aa = "01060173FF90";
			var bb = new byte[aa.Length];
			for (int i = 0; i < aa.Length; i += 2)
			{
				string one = Char.ToString(aa[i]); // первый симмвло в стринге
				string two = Char.ToString(aa[i + 1]);
				string d = one + two; // два символа в одной переменной (2 байта)
				int value = Convert.ToInt32(d, 16);
				bb[i] = Convert.ToByte(value);
				Console.WriteLine(d + " = " + bb[i]);
			}
			Console.Write("LRC = " + calculateLRC(bb));

			/*
string hexValues = "48 65 6C 6C 6F 20 57 6F 72 6C 64 21";
string[] hexValuesSplit = hexValues.Split(' ');
foreach (String hex in hexValuesSplit)
{
    // Convert the number expressed in base-16 to an integer.
    int value = Convert.ToInt32(hex, 16);
// Get the character corresponding to the integral value.
string stringValue = Char.ConvertFromUtf32(value);
char charValue = (char)value;
Console.WriteLine("hexadecimal value = {0}, int value = {1}, char value = {2} or {3}",
                    hex, value, stringValue, charValue);
}
*/
			//byte[] b = a.GetBytes;
			//Console.WriteLine(byteValue1[0]); // 30
			//Console.WriteLine(digchar(48)); //30
			byte[] b = { 1, 3, 8, 252, 28, 249, 198, 247, 112, 245, 26 };
			//Console.WriteLine(calculateLRC(b));
			// Console.WriteLine(hexOutput);

			initPort();
			//send_command("010300000001");

			while (true)
			{
				string n = Console.ReadLine();
				send_command(n);
			}

			Console.ReadKey();
			//send_command("010300000001");
			Console.ReadKey();

		}

		public static bool initPort() //инициализация прта
		{

			// получаем список доступных портов
			string[] ports = SerialPort.GetPortNames();
			Console.WriteLine("Выберите порт:");
			// выводим список портов
			for (int i = 0; i < ports.Length; i++)
			{
				Console.WriteLine("[" + i.ToString() + "] " + ports[i].ToString());
			}

			port = new SerialPort();

			// читаем номер из консоли
			string n = Console.ReadLine();
			int num = int.Parse(n);

			try
			{
				// настройки порта
				port.PortName = ports[num];
				port.BaudRate = 9600;
				port.DataBits = 8;
				port.Parity = System.IO.Ports.Parity.None;
				port.StopBits = System.IO.Ports.StopBits.One;
				port.ReadTimeout = 1000;
				port.WriteTimeout = 1000;

				port.DataReceived += new SerialDataReceivedEventHandler(DataReceviedHandler);

				port.Open();

				Console.WriteLine("Press any key to continue...");
				Console.WriteLine();




			}
			catch (Exception e)
			{
				Console.WriteLine("ERROR: невозможно открыть порт:" + e.ToString());
				Console.ReadKey();
				return false;
			}




			/*
			//port.Write ("Hello from C#");
			while (port.BytesToRead > 0)
			{
				try
				{
					string message = port.ReadLine();
					Console.WriteLine(message);
				}
				catch (TimeoutException)
				{
					Console.WriteLine("_");
				}
			}
			port.Close();*/
			return true;
		}

		private static void DataReceviedHandler(object sender, SerialDataReceivedEventArgs e)
		{
			SerialPort sp = (SerialPort)sender;
			string indata = sp.ReadExisting();
			Console.WriteLine("");
			Console.Write(indata);
		}

		//Посылаем команды по RS-485
		public static void send_command(string command)
		{
			port.Write(command + "\r\n");
		}


		//Фукция перевода из ASCII в HEX
		public static byte digchar(byte v)
		{
			//значение переменной будет соответствовать символу ASCII
			v -= (byte)0;
			if (v > (byte)41)
				return (byte)(v - 86); //a..f
			if (v > (byte)64)
				return (byte)(v - 54);   //A..F
			return v;             //0..9
		}

		public static string calculateLRC(byte[] bytes)
		{
			byte LRC = 0x00;
			for (int i = 0; i < bytes.Length; i++)
			{
				LRC = (byte)((LRC + bytes[i]) & 0xFF);
			}

			return (String.Format("{0:X}", (byte)(((LRC ^ 0xFF) + 1) & 0xFF)));
		}

		//Функция подсчета суммы
		//public  byte LRC_calc(byte str)
		//{
		//	byte val = 0;
		//	while (str)
		//	{
		//		val += (digchar(str) << 4) | digchar(str[1]);
		//		str += 2;
		//	}
		//	return (byte)(-((byte)val));
		//}

		public static void FileSetting()
		{
			string path = @"set.ini";

			try
			{


				if (!(File.Exists(path)))
				{
					Console.WriteLine("FAYLA NET");
				}

				// Create the file.
				using (FileStream fs = File.Create(path))
				{
					Byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
					// Add some information to the file.
					fs.Write(info, 0, info.Length);
				}

				// Open the stream and read it back.
				using (StreamReader sr = File.OpenText(path))
				{
					string s = "";
					while ((s = sr.ReadLine()) != null)
					{
						Console.WriteLine(s);
					}
				}
			}

			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

		}
		//Функция перевода из INT в HEX
		public static string chardig(double temp, bool flag)
		{
			if (flag)
			{
				temp = Math.Round(temp, 2);
				int intValue = Convert.ToInt32(temp * 10);
				string hexValue = intValue.ToString("X4");
				if (temp > 0)
				{
					return hexValue;
				}
				if (temp < 0)
				{
					hexValue = hexValue.Substring(4);
					return hexValue;
				}
				return "0000";
			}
			return "0";
		}

	}
}

