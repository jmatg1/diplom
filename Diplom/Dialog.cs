using System;
namespace Diplom
{
	public partial class Dialog : Gtk.Dialog
	{
		public Dialog()
		{
			//this.Modal = true;
			//this.DestroyWithParent = true;
			this.Build();
		}

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			
			this.OnClose();
		}

		protected void OnButtonOkClicked(object sender, EventArgs e)
		{
		}
	}
}
