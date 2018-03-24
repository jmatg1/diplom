using System;
using Gtk;

namespace Diplom
{
	
	class MainClass
	{
		public static MainWindow win;
		public static Dialog dl;
		public static void Main()
		{
			Application.Init();
			win = new MainWindow();
			win.Show();
			Application.Run();

		}
	}
}
