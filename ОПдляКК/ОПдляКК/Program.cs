using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO.Ports;
using System.Text;
using System.Threading;

using System.IO; // file

namespace ОПдляКК
{
    static class Program
    {
        static SerialPort port;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
 
            fPort f2 = new fPort();
            f2.Show();

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
                port.Open();

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
        public static bool FileSetting()
        {
            string path = @"set.ini";

            try
            {

                // Delete the file if it exists.
                if ((File.Exists(path)))
                {
                    // Note that no lock is put on the
                    // file and the possibility exists
                    // that another process could do
                    // something with it between
                    // the calls to Exists and Delete.
                    File.Delete(path);
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
    }
}
