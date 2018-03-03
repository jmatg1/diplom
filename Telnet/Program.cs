using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using static System.String;


public class SynchronousSocketClient
{

	public static void StartClient()
	{
		// Data buffer for incoming data.
		byte[] bytes = new byte[256000];

		// Connect to a remote device.
		try
		{
			// Establish the remote endpoint for the socket.
			// This example uses port 11000 on the local computer.
			IPHostEntry ipHostInfo = Dns.GetHostEntry("192.168.0.1");

			IPAddress ipAddress = ipHostInfo.AddressList[0];
			IPEndPoint remoteEP = new IPEndPoint(ipAddress, 23);


			// Create a TCP/IP  socket.
			Socket sender = new Socket(AddressFamily.InterNetwork,
				SocketType.Stream, ProtocolType.Tcp);
			//sender.SendTimeout = 10000;
			sender.ReceiveTimeout = 10000; // ВРЕМЯ ОТВЕТА СЕРВЕРА
			// Connect the socket to the remote endpoint. Catch any errors.
			try
			{
				sender.Connect(remoteEP);

				Console.WriteLine("Socket connected to {0}",
					sender.RemoteEndPoint.ToString());

				// Encode the data string into a byte array.
				while (true)
				{
					
					//Console.WriteLine("bytesSent " + bytesSent);
					// Receive the response from the remote device.
					int bytesRec = sender.Receive(bytes);

					//Console.Write("bytesRec " + Convert.ToString(bytesRec));
					if (IsNullOrEmpty(Convert.ToString(bytesRec)))
					{
						Console.WriteLine("NO OTVETA");
					}


					Console.WriteLine("{0}",Encoding.ASCII.GetString(bytes, 0, bytesRec));

string n1 = Console.ReadLine();
byte[] msg = Encoding.ASCII.GetBytes(n1 + "\n");

// Отправляем. в инт кол-во символов
int bytesSent = sender.Send(msg);
				}
				// Release the socket.
				sender.Shutdown(SocketShutdown.Both);
				sender.Close();

			}
			catch (ArgumentNullException ane)
			{
				Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
				sender.Shutdown(SocketShutdown.Both);
				sender.Close();
			}
			catch (SocketException se)
			{
				Console.WriteLine("SocketException : {0}", se.ToString());
				sender.Shutdown(SocketShutdown.Both);
				sender.Close();
			}
			catch (Exception e)
			{
				Console.WriteLine("Unexpected exception : {0}", e.ToString());
				sender.Shutdown(SocketShutdown.Both);
				sender.Close();
			}

		}
		catch (Exception e)
		{
			Console.WriteLine(e.ToString());
		}
	}
	

	public static int Main(String[] args)
	{

			StartClient();

		//Console.ReadKey();

		return 0;
	}
}