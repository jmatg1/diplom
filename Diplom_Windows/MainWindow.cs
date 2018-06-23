﻿using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
	public MainWindow () : base (Gtk.WindowType.Toplevel)
	{
		
		Build ();
			
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnActionSettingsActivated (object sender, EventArgs e)
	{			
Diplom_Windows.MainClass.dl = new Diplom_Windows.SettingsWindows ();
	}


}
