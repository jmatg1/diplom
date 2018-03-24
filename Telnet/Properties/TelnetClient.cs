using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
namespace Network.Connect
{
	class nalib
	{
		static TelnetConnection tc;

		/// <summary>
		/// Write a SCPI command to the telnet connection.
		/// If the command has a '?', then read back the response and print
		/// it to the Console.
		/// </summary>
		/// <remarks>
		/// Note the '?' detection is naive, as a ? could occur in the middle
		/// of a SCPI string argument, and not actually signify a SCPI query.
		/// </remarks>
		/// <param name="s"></param>
		static void Write(string s)
		{
			Console.WriteLine(s);
			tc.WriteLine(s);
			if (s.IndexOf('?') >= 0)
				Read();
		}

		/// <summary>
		/// Read the telnet connection for a response, and print the response to the
		/// Console.
		/// </summary>
		static void Read()
		{
			Console.WriteLine(tc.Read());
		}
	}

	#region TelnetConnection - no need to edit

	/// <summary>
	/// Telnet Connection on port 5025 to an instrument
	/// </summary>
	public class TelnetConnection : IDisposable
	{
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
				Open(host);
		}
		void CheckOpen()
		{
			if (!IsOpen)
				throw new Exception("Connection not open.");
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
			return System.Text.ASCIIEncoding.ASCII.GetString(ReadBytes());
		}

		/// <summary>
		/// Reads bytes from the socket and returns them as a byte[].
		/// </summary>
		/// <returns></returns>
		public byte[] ReadBytes()
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

		int ReadLengthHeader()
		{
			int numDigits = Convert.ToInt32(new string(new char[] { (char)m_Stream.ReadByte() }));
			string bytes = "";
			for (int i = 0; i < numDigits; ++i)
				bytes = bytes + (char)m_Stream.ReadByte();

			return Convert.ToInt32(bytes);
		}


		public void Open(string hostname)
		{
			if (IsOpen)
				Close();
			m_Hostname = hostname;
			m_Client = new TcpClient(hostname, 5025);
			m_Stream = m_Client.GetStream();
			m_Stream.ReadTimeout = ReadTimeout;
			m_IsOpen = true;
			if (Opened != null)
				Opened();
		}
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
		public void Dispose()
		{
			Close();
		}
	}
	#endregion
}