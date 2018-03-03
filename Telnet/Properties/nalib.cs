// minimalistic telnet implementation
// conceived by Tom Janssens on 2007/06/06  for codeproject
//
// http://www.corebvba.be



using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Linq;

namespace nalib
{
	enum Verbs
	{
		WILL = 251,
		WONT = 252,
		DO = 253,
		DONT = 254,
		IAC = 255
	}

	enum Options
	{
		SGA = 3
	}

	class TelnetConnection
	{
		TcpClient tcpSocket;

		int TimeOutMs = 100;

		public TelnetConnection(string Hostname, int Port)
		{
			tcpSocket = new TcpClient(Hostname, Port);

		}
		/// <summary>
		/// Отправить команду
		/// </summary>
		public void WriteLine(string cmd)
		{
			Write(cmd + "\n");
		}

		private void Write(string cmd)
		{
			if (!tcpSocket.Connected) return;
			byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
			tcpSocket.GetStream().Write(buf, 0, buf.Length);
		}
		/// <summary>
		/// Получить ответ
		/// </summary>
		public string Read()
		{
			if (!tcpSocket.Connected) return null;
			StringBuilder sb = new StringBuilder();
			do
			{
				ParseTelnet(sb);
				System.Threading.Thread.Sleep(TimeOutMs);
			} while (tcpSocket.Available > 0);
			return sb.ToString();
		}
		/// <summary>
		/// Проверить соединение с сервером Телнет
		/// </summary>
		public bool IsConnected
		{
			get { return tcpSocket.Connected; }
		}

		void ParseTelnet(StringBuilder sb)
		{
			try
			{
				while (tcpSocket.Available > 0)
				{
					int input = tcpSocket.GetStream().ReadByte();
					switch (input)
					{
						case -1:
							break;
						case (int)Verbs.IAC:
							// interpret as command
							int inputverb = tcpSocket.GetStream().ReadByte();
							if (inputverb == -1) break;
							switch (inputverb)
							{
								case (int)Verbs.IAC:
									//literal IAC = 255 escaped, so append char 255 to string
									sb.Append(inputverb);
									break;
								case (int)Verbs.DO:
								case (int)Verbs.DONT:
								case (int)Verbs.WILL:
								case (int)Verbs.WONT:
									// reply to all commands with "WONT", unless it is SGA (suppres go ahead)
									int inputoption = tcpSocket.GetStream().ReadByte();
									if (inputoption == -1) break;
									tcpSocket.GetStream().WriteByte((byte)Verbs.IAC);
									if (inputoption == (int)Options.SGA)
										tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WILL : (byte)Verbs.DO);
									else
										tcpSocket.GetStream().WriteByte(inputverb == (int)Verbs.DO ? (byte)Verbs.WONT : (byte)Verbs.DONT);
									tcpSocket.GetStream().WriteByte((byte)inputoption);
									break;
								default:
									break;
							}
							break;
						default:
							sb.Append((char)input);
							break;
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("WAIT 1 MINUTE \r\n" + e);
				System.Threading.Thread.Sleep(60000);
			}
		}

		/// <summary>
		/// Запрашиваем частоты, на которых измеряем
		/// </summary>
		public List<double> GetFreq(int channel)
		{
		    WriteLine(":SENS"+channel+":FREQ:DATA?");
			string input = Read();
			List<string> freq_string = input.Split(' ').ToList();
			List<double> freq = freq_string.Select(x => double.Parse(x)).ToList();
			/*foreach (double combo in freq)
        {
				Console.WriteLine(combo);
        }*/


			return freq;
		}
	}
}
