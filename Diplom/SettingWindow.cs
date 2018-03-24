using System;
namespace Diplom
{
	public partial class SettingWindow : Gtk.Window
	{
		public SettingWindow() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
