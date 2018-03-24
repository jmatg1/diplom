using System;
using Gtk;



	public partial class MainWindow : Gtk.Window
	{
		public MainWindow() : base(Gtk.WindowType.Toplevel)
		{
			Build();
		}

		protected void OnDeleteEvent(object sender, DeleteEventArgs a)
		{
			Application.Quit();
			a.RetVal = true;
		}

		protected void OnActionSettingsActivated(object sender, EventArgs e)
		{
		//Dialog dialog;
		//dialog = new Dialog("sapmple",Diplom.MainClass.win, Gtk.DialogFlags.DestroyWithParent);
		//dialog.Modal = true;
		//dialog.AddButton("Close", ResponseType.Close);
		//dialog.Run();
		//dialog.Destroy();


			Diplom.MainClass.dl = new Diplom.Dialog();
		//Diplom.MainClass.dl.Run();
		//Diplom.MainClass.dl.Destroy();
			//Diplom.MainClass.win.Child = Diplom.MainClass.dl;
		//Diplom.MainClass.win.ChildVisible = true;
		//Diplom.MainClass.win.CanFocus = true;
			//Diplom.MainClass.dl.Md
			//Diplom.MainClass.win.Modal = true;
		}

	}

