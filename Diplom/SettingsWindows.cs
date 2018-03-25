using System;
using System.IO.Ports;


namespace Diplom
{
	public partial class SettingsWindows : Gtk.Dialog
	{
		
		public SettingsWindows()
		{
			this.Modal = true; // то что это модальное окно
			this.TransientFor = Diplom.MainClass.win; // это его родитель
			this.Build(); // Построили форму теперь можем измять её поля!
			// Заносим переменные в форму
			this.tempBegin.Value = MainClass.tempBegin; 
			this.tempEnd.Value = MainClass.tempEnd; 
			this.tempStep.Value = MainClass.tempStep; 

			this.timeSp.Value = MainClass.timeSp; 
			this.timeSp1.Value = MainClass.timeSp1; 

			//this.comboPort.Clear ();
			this.comboPort.AppendText( MainClass.comPort);
			this.comboPort.Active = 0; 
			// получаем список доступных портов
			string[] ports = SerialPort.GetPortNames();
			for (int i=0; i<ports.Length;i++)
			{
				this.comboPort.AppendText(ports[i].ToString());
			}

		}

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			this.OnClose();
		}

		protected void OnButtonOkClicked(object sender, EventArgs e)
		{
			//Заносим переменные из формы
			MainClass.tempBegin = this.tempBegin.ValueAsInt;
			MainClass.tempEnd = this.tempEnd.ValueAsInt;
			MainClass.tempStep = this.tempStep.ValueAsInt; 

			MainClass.timeSp = this.timeSp.ValueAsInt;
			MainClass.timeSp1 = this.timeSp1.ValueAsInt;

			MainClass.comPort = this.comboPort.ActiveText;
			this.OnClose ();
		}


	}
}
