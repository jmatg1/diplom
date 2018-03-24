using System;

namespace Diplom
{
	public partial class Form : Gtk.Window
	{
		public Form () :
			base (Gtk.WindowType.Toplevel)
		{
			this.Build ();
		}
	}
}

