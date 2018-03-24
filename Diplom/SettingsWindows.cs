using System;
namespace Diplom
{
	public partial class SettingsWindows : Gtk.Dialog
	{

		public SettingsWindows()
		{
			this.Modal = true; // то что это модальное окно
			this.TransientFor = Diplom.MainClass.win; // это его родитель
			//this.Deletable = true;
			//this.Decorated = false;
			//this.TransientFor = Diplom.MainClass.win;
			//this.Child = Diplom.MainClass.win;

			//

			this.Build();
//			this.Run ();
//			this.Destroy ();
			//this.Destroy ();
		}

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
//			Diplom.MainClass.win.Modal = false;
//			this.DestroyWithParent = false;
////			Diplom.MainClass.dl.Handle = true;

			this.OnClose();
		}

		protected void OnButtonOkClicked(object sender, EventArgs e)
		{
			
		}
	}
}
