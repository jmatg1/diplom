using System;
using System.IO.Ports;
using System.Text;
using System.Threading;

namespace Diplom
{

	public class ModbusASCIIInterface : IDisposable
	{


		public static SerialPort comport; // com port
		public static string number; // номер порта, применяется в catch для воостановление соединения 
		public static string ComReply = ""; // строка ответа компорта
		public static int ReadTimeout = 10000; // время ожидания ответа

		public delegate void ConnectionDelegate();
		public event ConnectionDelegate Opened;
		public event ConnectionDelegate Closed;

		public ModbusASCIIInterface()
		{
			comport = new SerialPort();
		}
		public void Dispose()
		{
			Close();
		}
		/// <summary>
		/// Инициализация порта
		/// </summary>
		public bool initPort(string num)
		{
			try
			{
				// настройки порта
				//number = num; // Console Version
				number = MainClass.comPort; // Windows version
				comport.PortName = num;
				comport.BaudRate = 9600;
				comport.DataBits = 8;
				comport.Parity = System.IO.Ports.Parity.None;
				comport.StopBits = System.IO.Ports.StopBits.One;
				comport.ReadTimeout = 10000;
				comport.WriteTimeout = 10000;
				comport.DataReceived += new SerialDataReceivedEventHandler(ComDataRec); // функция ComDataRec.. вызывается когда пришло сообщение на ком порт
				comport.Open();


			}
			catch (Exception) // Выполняем это вслучаи любой ошибке
			{
				if (Diplom.MainProgramm.SetFlagThread == false)
				{
					return false;
					comport.Close();
				}
				Diplom.MainClass.win.Log (": ERROR: Порт " + number + " не отвечает. Открытие через 1 минуту.");
				//Console.WriteLine(System.DateTime.Now.ToLongTimeString() + ": ERROR: Порт " + num + " не отвечает. Открытие через 1 минуту." /*+ e.ToString()*/);
				Thread.Sleep(6000);// 60 000 = 1 минута
				initPort(number);
				return false;
			}
			return true;

		}
		public bool CheckOpen()
		{
			if (!comport.IsOpen)
				return false;

			return true;
		}
		public void Close()
		{
			comport.Close();
		}
		private void ComDataRec(object sender, SerialDataReceivedEventArgs e) // заносим сообщение в глобальную переменную Comreply
		{

			SerialPort sp = (SerialPort)sender;
			ComReply += sp.ReadExisting();
			//Console.WriteLine (ComReply);
		}
		private string ComRead()
		{
			int i = 1;
			while (string.IsNullOrEmpty(ComReply)) // выполняем цикл пока строка пуста, если прошло время, а соообщение так и не пришло то сообщение не получено
			{
				Thread.Sleep(100);
				i++;
				if (i == 10)
				{
					Console.WriteLine(System.DateTime.Now.ToLongTimeString() + ": ERROR: Время ожидания приема истекло.ComRead\n");
					return "false";
				}
			}
			string tmp = ComReply;
			ComReply = ""; // очищаем переменную
			return tmp;

		}
		/*Функция перевода из int в hex .*/
		public static string chardig(int temp)
		{

			//temp = Math.Round(temp, 2); // сокращем до десятой
			int intValue = temp * 10;// конвертируем в целое и умножаем на 10
			string hexValue = intValue.ToString("X4");//переводим в Hex формата 0000-FFFFH
			if (temp > 0)//Если число положительное то просто отправляем
			{
				return hexValue;
			}
			if (temp < 0)// если отрицательное, то берем первый четыре разряда, F..FFF06 = FF06
			{
				hexValue = hexValue.Substring(4);
				return hexValue;
			}
			return "0000";
		}

		/*На входе команда без символов : CR LC .На выходе контрольная сумма команды формата 00-FFH */
		private string calculateLRC(string command)
		{
			var bb = new byte[command.Length];
			for (int i = 0; i < command.Length; i += 2)
			{
				string one = Char.ToString(command[i]); // первый симмвло в стринге
				string two = Char.ToString(command[i + 1]);
				string d = one + two; // два символа в одной переменной (2 байта)
				int value = Convert.ToInt32(d, 16);
				bb[i] = Convert.ToByte(value);
				//Console.WriteLine(d + " = " + bb[i]);
			}
			byte LRC = 0x00;
			for (int i = 0; i < bb.Length; i++)
			{
				LRC = (byte)((LRC + bb[i]) & 0xFF);
			}
			return (String.Format("{0:X}", (byte)(((LRC ^ 0xFF) + 1) & 0xFF)));
		}
		/// <summary>
		/// Установка температуры
		/// </summary>
		public bool setTargetTemperature(int setTemp)
		{
			try
			{
				if (Diplom.MainProgramm.SetFlagThread == false)	
				{
					return false;
					comport.Close();
				}
				string command = "01060173" + chardig(setTemp);// команда установки уставки + температура в HEX
				command += calculateLRC(command); // добавляем контрольную сумма
				command = ":" + command + "\r\n"; // итоговая команда
				comport.Write(command); // отпраялем команду
				if (command == ComRead())// проверяем, пришла ли команда, что мы отправили(если да,то отправка успешна)
					return true;
				else return false;
			}
			catch (Exception)
			{
				Console.Write(System.DateTime.Now.ToLongTimeString() + ": ERROR:Неудачно отправка.Установка уставки setTargetTemperature\n");
				if (comport.IsOpen)
				{
					Console.Write("Порт открыт \n");
					return false;
				}
				else
				{
					Thread.Sleep(5000);
					initPort(number);
					return setTargetTemperature(setTemp);

				}
			}
		}

		///<summary>
		///Получаем значение температуры. В случаи ошибки вернет 404
		///</summary>
		public int getCurrentTemperature() //////
		{
			try
			{
				if (Diplom.MainProgramm.SetFlagThread == false)
					return 404;
				comport.Write(":010300000001FB\r\n");
				string tmp_st = ComRead();

				if (tmp_st == "false") // если чтение не удалось то вызываем функцию опять
				{
					return getCurrentTemperature();
				}
				if ( !(calculateLRC(tmp_st.Substring(1,10)) == tmp_st.Substring(11, 2)))
					return getCurrentTemperature();
				string temp = tmp_st.Substring(7, 4); //вырезаем температуру

				return int.Parse(temp, System.Globalization.NumberStyles.HexNumber) / 10; // переводим в 10 систему счисления, делим на 10 и возвращаем целое число
			}
			catch (Exception)
			{
				Console.Write(System.DateTime.Now.ToLongTimeString() + ": ERROR: Получение температуры. ");
				if (comport.IsOpen)
				{
					Console.Write("Порт открыт \n");
					return 404;
				}
				else
				{
					initPort(number);
					return getCurrentTemperature();

				}

			}
		}
		///<summary>
		///Получаем значение уставки. В случаи ошибки вернет 999.0 или завершение потока
		///</summary>
		public double getUst()
		{
			try
			{
				if (Diplom.MainProgramm.SetFlagThread == false)
					return 999.0;
				comport.Write(":010300300001CB\r\n");
				string tmp_st = ComRead();

				if (tmp_st == "false")
				{
					return 999.0;
				}
				string temp = tmp_st.Substring(7, 4);

				return int.Parse(temp, System.Globalization.NumberStyles.HexNumber) / 10;
			}
			catch (Exception)
			{
				Console.Write(System.DateTime.Now.ToLongTimeString() + ": ERROR:Неудачно. Получение уставки\n");
				return 999.0;
			}
		}




	}
}

