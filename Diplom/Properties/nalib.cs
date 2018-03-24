using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Collections; // for array list
using System.Linq; // for List<>
using System.Windows; // for vector
using System.Numerics;
using System.Net.NetworkInformation;

namespace Diplom
{
	
	#region TelnetConnection - no need to edit

	/// <summary>
	/// Telnet Connection on port 5025 to an instrument
	/// </summary>
	public class TelnetConnection : IDisposable
	{
		public static string globalHostname = "192.168.0.2";
		TcpClient m_Client;
		NetworkStream m_Stream;
		bool m_IsOpen = false;
		string m_Hostname;
		int m_ReadTimeout = 1000; // ms
		public delegate void ConnectionDelegate();
		public event ConnectionDelegate Opened;
		public event ConnectionDelegate Closed;
		public bool IsOpen { get { return m_IsOpen; } }
		public TelnetConnection() { }
		public TelnetConnection(bool open) : this("localhost", true) { }
		public TelnetConnection(string host, bool open)
		{
			if (open)
				Connect(host);
		}
		/// <summary>
		/// Checks the open. Пингуем соединение с компьютером.
		/// Функция вызывается когда обращаемся к телнет серверу.
		/// </summary>
		/// <returns><c>true</c>,если компьютер отвечает, <c>false</c> otherwise.</returns>
		public  bool CheckOpen()
		{
			try
			{
				Ping pingSender = new Ping();
				PingOptions options = new PingOptions();
				options.DontFragment = true;
				string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
				byte[] buffer = Convert.FromBase64String(data);
				int timeout = 120;
				PingReply reply = pingSender.Send("192.168.0.2", timeout, buffer, options);
				if (reply.Status == IPStatus.Success)
				{
					Console.WriteLine("Соединение с телнетом есть");
					return true;
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message); // ВЫводим ошибку
				Connect(globalHostname); 			//Подключаемся заново
				return true;
			}
			return false; 	// По факту этот код никогда не будет выполнен, так как в случаи разъединения будет выполнятся только CATCH
							// mono требует это, поэтому это здесь
		}
		public string Hostname
		{
			get { return m_Hostname; }
		}
		public int ReadTimeout
		{
			set { m_ReadTimeout = value; if (IsOpen) m_Stream.ReadTimeout = value; }
			get { return m_ReadTimeout; }
		}
		public void Write(string str)
		{
			//FieldFox Programming Guide 6
			CheckOpen();
			byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
			m_Stream.Write(bytes, 0, bytes.Length);
			m_Stream.Flush();
		}
		public void WriteLine(string str)
		{
			CheckOpen();
			byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
			m_Stream.Write(bytes, 0, bytes.Length);
			WriteTerminator();
		}
		void WriteTerminator()
		{
			byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("\r\n\0");
			m_Stream.Write(bytes, 0, bytes.Length);
			m_Stream.Flush();
		}
		public string Read()
		{
			CheckOpen();
			byte[] read = ReadBytes();
			if (read.Length == 0)
				return "";
			//-------------
			return System.Text.ASCIIEncoding.ASCII.GetString(read);
		}

		/// <summary>
		/// Reads bytes from the socket and returns them as a byte[].
		/// </summary>
		/// <returns></returns>
		public byte[] ReadBytes()
		{
			try
			{
				int i = m_Stream.ReadByte();
				byte b = (byte)i;
				int bytesToRead = 0;
				var bytes = new List<byte>();
				if ((char)b == '#')
				{
					bytesToRead = ReadLengthHeader();
					if (bytesToRead > 0)
					{
						i = m_Stream.ReadByte();
						if ((char)i != '\n') // discard carriage return after length header.
							bytes.Add((byte)i);
					}
				}
				if (bytesToRead == 0)
				{
					while (i != -1 && b != (byte)'\n')
					{
						bytes.Add(b);
						i = m_Stream.ReadByte();
						b = (byte)i;
					}
				}
				else
				{
					int bytesRead = 0;
					while (bytesRead < bytesToRead && i != -1)
					{
						i = m_Stream.ReadByte();
						if (i != -1)
						{
							bytesRead++;
							// record all bytes except \n if it is the last char.
							if (bytesRead < bytesToRead || (char)i != '\n')
								bytes.Add((byte)i);
						}
					}
				}

				return bytes.ToArray();
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
				byte[] error = {};
				return error;
			}
				
		}

		int ReadLengthHeader()
		{
			int numDigits = Convert.ToInt32(new string(new char[] { (char)m_Stream.ReadByte() }));
			string bytes = "";
			for (int i = 0; i < numDigits; ++i)
				bytes = bytes + (char)m_Stream.ReadByte();

			return Convert.ToInt32(bytes);
		}

		/// <summary>
		/// Подключение к телнет серверу
		/// </summary>
		/// <returns>The connect.</returns>
		/// <param name="hostname">Hostname.</param>

		public void Connect(string hostname)
		{
			try
			{
				globalHostname = hostname;
				if (IsOpen)
					Close();
				m_Hostname = hostname;
				m_Client = new TcpClient(hostname, 5025);//5025
				m_Stream = m_Client.GetStream();
				m_Stream.ReadTimeout = 10000;//10 sec
				m_IsOpen = true;
				if (Opened != null)
					Opened();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message + " Повторно подключаюсь.");
				Connect(globalHostname);
			}
		}
		/// <summary>
		/// Закрыть телнет
		/// </summary>
		public void Close()
		{
			if (!m_IsOpen)
				//FieldFox Programming Guide 7
				return;
			m_Stream.Close();
			m_Client.Close();
			m_IsOpen = false;
			if (Closed != null)
				Closed();
		}
		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			Close();
		}
		#endregion




		/// <summary>
		/// Получение частот с канала 1 или 2
		/// </summary>
		public List<double> GetFreq(int channel)
		{
			string input = "";
			do
			{
				WriteLine(":SENS" + channel + ":FREQ:DATA?");
				input = Read();
			}
			while (input.Length < 1);
			List<string> freq_string = input.Split(',').ToList();
			List<double> freq = freq_string.Select(x => double.Parse(x)).ToList();// добавляем частоты в лист - аналог в си это вектор
			/*foreach (string combo in freq_string)
			{
				Console.WriteLine(combo);
			}*/
			return freq;
		}

		/// <summary>
		/// Получение измеренных данных с канала channel, параметра матрицы рассеяния Sp в виде Re(Sp), Im(Sp)
		/// Используем так: Scomplex[i].Real и Scomplex[i].Imaginary  =  Re_i + Im_i
		/// </summary>
		public List<Complex> doMeasurement(int channel, string Sp)
		{
			//-- Отправляем команду. Если пришло пустое сообщение, то проверяем связь и отправляем команду
			string input = "";
			do
			{
				WriteLine(":SENS" + channel + ":DATA:CORR? " + Sp);
				input = Read();
				Console.WriteLine("input.Length " + input);
			}
			while (input.Length < 1);//Выполняем пока ответ пришел пустой
			//---END----

			List<string> freq_string = input.Split(',').ToList();
			List<double> freq = freq_string.Select(x => double.Parse(x)).ToList();
			List<Complex> Scomplex = new List<Complex>();
			for (int i = 0; i < freq.Count; i += 2)
			{
				Scomplex.Add(new Complex(freq[i], freq[i + 1]));//амплитуда, и фаза в  виде Re(Sp) Im(Sp)
				 
			}

			//int[] scores = new int[] ;
			/*foreach (double combo in freq)
			{
				Console.Write(combo+",");
			}
			for (int i = 0; i < freq.Count; i += 2)
			{
				Console.Write(freq[i] + ",");
				//complex.AddRange(freq[i],
			}*/
			return Scomplex;
		}
		/// <summary>
		/// Получение измеренных данных с канала channel, параметра матрицы рассеяния Sp в виде abs(Sp)Дб, Phase(Sp)град
		/// </summary>
		public List<Complex> AmplitudeAndPhase(int channel, string Sp)
		{
			List<Complex> ReIm = doMeasurement(channel, Sp);
			List<Complex> AmPh = new List<Complex>();
			for (int i = 0; i < ReIm.Count; i ++)
			{
				AmPh.Add(new Complex(20 * Math.Log10(ReIm[i].Magnitude), 180 * ReIm[i].Phase / Math.PI)); //Добавляем в лист комплексное число со значениями Амплитуды и фазы. Для доступа к этим значениям: использовать *[i].Real и *[i].Imaginary, соответственно

			}
			return AmPh;

		}


		/// <summary>
		///Расчет среднего квадратичного отклонения. На входе два List<Complex> за время t и t1. 
		/// Переменные берутся из функции  doMeasurement
		/// </summary>
		/// <returns>Возвращает double</returns>
		public double MSD(List<Complex> St, List<Complex> St1)
		{
			double rezult = 0;
			for (int i = 0; i < St.Count; i++)
			{
				rezult += Math.Pow((St1[i].Real - St[i].Real) + (St1[i].Imaginary - St[i].Imaginary), 2);	//sum(((aj-a1j)+(bij-bi1j))^2) где j номер элемента, i - мнимая единица
 				//Console.WriteLine(rezult);
			}
			rezult = Math.Sqrt(rezult / (St.Count - 1));	// Корень( sum / (кол-во частот - 1) )
			//Console.WriteLine(St.Count  - 1);
			return rezult;
		}
	}
}