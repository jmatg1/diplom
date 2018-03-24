
// This file has been generated by the GUI designer. Do not modify.
namespace Diplom
{
	public partial class SettingsWindows
	{
		private global::Gtk.HBox hbox5;
		
		private global::Gtk.Frame frame2;
		
		private global::Gtk.Alignment GtkAlignment2;
		
		private global::Gtk.HBox hbox6;
		
		private global::Gtk.VBox vbox9;
		
		private global::Gtk.Label label17;
		
		private global::Gtk.Label label18;
		
		private global::Gtk.Label label19;
		
		private global::Gtk.VBox vbox6;
		
		public global::Gtk.SpinButton tempBegin;
		
		private global::Gtk.SpinButton tempEnd;
		
		private global::Gtk.SpinButton tempStep;
		
		private global::Gtk.Label Frame1;
		
		private global::Gtk.Frame frame3;
		
		private global::Gtk.Alignment GtkAlignment3;
		
		private global::Gtk.HBox hbox7;
		
		private global::Gtk.VBox vbox7;
		
		private global::Gtk.Label label14;
		
		private global::Gtk.Label label15;
		
		private global::Gtk.VBox vbox8;
		
		private global::Gtk.SpinButton timeSp;
		
		private global::Gtk.SpinButton timeSp1;
		
		private global::Gtk.Label GtkLabel3;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.Button buttonOk;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Diplom.SettingsWindows
			this.Name = "Diplom.SettingsWindows";
			this.Title = global::Mono.Unix.Catalog.GetString ("Настройки");
			this.Icon = global::Stetic.IconLoader.LoadIcon (this, "gtk-execute", global::Gtk.IconSize.Menu);
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			this.DestroyWithParent = true;
			this.Gravity = ((global::Gdk.Gravity)(5));
			// Internal child Diplom.SettingsWindows.VBox
			global::Gtk.VBox w1 = this.VBox;
			w1.Name = "dialog1_VBox";
			// Container child dialog1_VBox.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox ();
			this.hbox5.Name = "hbox5";
			this.hbox5.Homogeneous = true;
			// Container child hbox5.Gtk.Box+BoxChild
			this.frame2 = new global::Gtk.Frame ();
			this.frame2.Name = "frame2";
			this.frame2.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame2.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.hbox6 = new global::Gtk.HBox ();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.vbox9 = new global::Gtk.VBox ();
			this.vbox9.Name = "vbox9";
			this.vbox9.Homogeneous = true;
			this.vbox9.Spacing = 6;
			// Container child vbox9.Gtk.Box+BoxChild
			this.label17 = new global::Gtk.Label ();
			this.label17.Name = "label17";
			this.label17.Xalign = 0F;
			this.label17.LabelProp = global::Mono.Unix.Catalog.GetString ("Начальная:");
			this.vbox9.Add (this.label17);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox9 [this.label17]));
			w2.Position = 0;
			// Container child vbox9.Gtk.Box+BoxChild
			this.label18 = new global::Gtk.Label ();
			this.label18.Name = "label18";
			this.label18.Xalign = 0F;
			this.label18.LabelProp = global::Mono.Unix.Catalog.GetString ("Конечная:");
			this.vbox9.Add (this.label18);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox9 [this.label18]));
			w3.Position = 1;
			// Container child vbox9.Gtk.Box+BoxChild
			this.label19 = new global::Gtk.Label ();
			this.label19.Name = "label19";
			this.label19.Xalign = 0F;
			this.label19.LabelProp = global::Mono.Unix.Catalog.GetString ("Шаг:");
			this.vbox9.Add (this.label19);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox9 [this.label19]));
			w4.Position = 2;
			this.hbox6.Add (this.vbox9);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.vbox9]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.vbox6 = new global::Gtk.VBox ();
			this.vbox6.Name = "vbox6";
			this.vbox6.Spacing = 6;
			// Container child vbox6.Gtk.Box+BoxChild
			this.tempBegin = new global::Gtk.SpinButton (-200D, 200D, 5D);
			this.tempBegin.CanFocus = true;
			this.tempBegin.Name = "tempBegin";
			this.tempBegin.Adjustment.PageIncrement = 5D;
			this.tempBegin.ClimbRate = 1D;
			this.tempBegin.Numeric = true;
			this.tempBegin.Value = -30D;
			this.vbox6.Add (this.tempBegin);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.tempBegin]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox6.Gtk.Box+BoxChild
			this.tempEnd = new global::Gtk.SpinButton (0D, 100D, 1D);
			this.tempEnd.CanFocus = true;
			this.tempEnd.Name = "tempEnd";
			this.tempEnd.Adjustment.PageIncrement = 10D;
			this.tempEnd.ClimbRate = 1D;
			this.tempEnd.Numeric = true;
			this.vbox6.Add (this.tempEnd);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.tempEnd]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox6.Gtk.Box+BoxChild
			this.tempStep = new global::Gtk.SpinButton (0D, 100D, 1D);
			this.tempStep.CanFocus = true;
			this.tempStep.Name = "tempStep";
			this.tempStep.Adjustment.PageIncrement = 10D;
			this.tempStep.ClimbRate = 1D;
			this.tempStep.Numeric = true;
			this.vbox6.Add (this.tempStep);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox6 [this.tempStep]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			this.hbox6.Add (this.vbox6);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox6 [this.vbox6]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			this.GtkAlignment2.Add (this.hbox6);
			this.frame2.Add (this.GtkAlignment2);
			this.Frame1 = new global::Gtk.Label ();
			this.Frame1.Name = "Frame1";
			this.Frame1.Xpad = 25;
			this.Frame1.Ypad = 5;
			this.Frame1.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Температура, C&#176;</b>");
			this.Frame1.UseMarkup = true;
			this.frame2.LabelWidget = this.Frame1;
			this.hbox5.Add (this.frame2);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.frame2]));
			w12.Position = 0;
			// Container child hbox5.Gtk.Box+BoxChild
			this.frame3 = new global::Gtk.Frame ();
			this.frame3.Name = "frame3";
			this.frame3.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame3.Gtk.Container+ContainerChild
			this.GtkAlignment3 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment3.Name = "GtkAlignment3";
			this.GtkAlignment3.LeftPadding = ((uint)(12));
			// Container child GtkAlignment3.Gtk.Container+ContainerChild
			this.hbox7 = new global::Gtk.HBox ();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.vbox7 = new global::Gtk.VBox ();
			this.vbox7.Name = "vbox7";
			this.vbox7.Homogeneous = true;
			// Container child vbox7.Gtk.Box+BoxChild
			this.label14 = new global::Gtk.Label ();
			this.label14.TooltipMarkup = "Сколько времени ждать, \nпосле того как уставка \nбудет равна текущей температуре";
			this.label14.Name = "label14";
			this.label14.Xalign = 0F;
			this.label14.LabelProp = global::Mono.Unix.Catalog.GetString ("Получение Sp<sub>t</sub>:");
			this.label14.UseMarkup = true;
			this.vbox7.Add (this.label14);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox7 [this.label14]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox7.Gtk.Box+BoxChild
			this.label15 = new global::Gtk.Label ();
			this.label15.Name = "label15";
			this.label15.Xalign = 0F;
			this.label15.LabelProp = global::Mono.Unix.Catalog.GetString ("Получение Sp<sub>t<sub>1</sub></sub>:");
			this.label15.UseMarkup = true;
			this.vbox7.Add (this.label15);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox7 [this.label15]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			this.hbox7.Add (this.vbox7);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.vbox7]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.vbox8 = new global::Gtk.VBox ();
			this.vbox8.Name = "vbox8";
			this.vbox8.Homogeneous = true;
			// Container child vbox8.Gtk.Box+BoxChild
			this.timeSp = new global::Gtk.SpinButton (0D, 100D, 1D);
			this.timeSp.CanFocus = true;
			this.timeSp.Name = "timeSp";
			this.timeSp.Adjustment.PageIncrement = 10D;
			this.timeSp.ClimbRate = 1D;
			this.timeSp.Numeric = true;
			this.vbox8.Add (this.timeSp);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox8 [this.timeSp]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child vbox8.Gtk.Box+BoxChild
			this.timeSp1 = new global::Gtk.SpinButton (0D, 100D, 1D);
			this.timeSp1.CanFocus = true;
			this.timeSp1.Name = "timeSp1";
			this.timeSp1.Adjustment.PageIncrement = 10D;
			this.timeSp1.ClimbRate = 1D;
			this.timeSp1.Numeric = true;
			this.vbox8.Add (this.timeSp1);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.vbox8 [this.timeSp1]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			this.hbox7.Add (this.vbox8);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox7 [this.vbox8]));
			w18.Position = 1;
			w18.Expand = false;
			w18.Fill = false;
			this.GtkAlignment3.Add (this.hbox7);
			this.frame3.Add (this.GtkAlignment3);
			this.GtkLabel3 = new global::Gtk.Label ();
			this.GtkLabel3.Name = "GtkLabel3";
			this.GtkLabel3.Xpad = 34;
			this.GtkLabel3.Ypad = 5;
			this.GtkLabel3.LabelProp = global::Mono.Unix.Catalog.GetString ("<b>Ожидание, мин</b>");
			this.GtkLabel3.UseMarkup = true;
			this.frame3.LabelWidget = this.GtkLabel3;
			this.hbox5.Add (this.frame3);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox5 [this.frame3]));
			w21.Position = 1;
			w1.Add (this.hbox5);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(w1 [this.hbox5]));
			w22.Position = 0;
			w22.Expand = false;
			w22.Fill = false;
			// Internal child Diplom.SettingsWindows.ActionArea
			global::Gtk.HButtonBox w23 = this.ActionArea;
			w23.Name = "dialog1_ActionArea";
			w23.Spacing = 10;
			w23.BorderWidth = ((uint)(5));
			w23.LayoutStyle = ((global::Gtk.ButtonBoxStyle)(4));
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanDefault = true;
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseStock = true;
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = "gtk-cancel";
			this.AddActionWidget (this.buttonCancel, -6);
			global::Gtk.ButtonBox.ButtonBoxChild w24 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w23 [this.buttonCancel]));
			w24.Expand = false;
			w24.Fill = false;
			// Container child dialog1_ActionArea.Gtk.ButtonBox+ButtonBoxChild
			this.buttonOk = new global::Gtk.Button ();
			this.buttonOk.CanDefault = true;
			this.buttonOk.CanFocus = true;
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.UseStock = true;
			this.buttonOk.UseUnderline = true;
			this.buttonOk.Label = "gtk-ok";
			this.AddActionWidget (this.buttonOk, -5);
			global::Gtk.ButtonBox.ButtonBoxChild w25 = ((global::Gtk.ButtonBox.ButtonBoxChild)(w23 [this.buttonOk]));
			w25.Position = 1;
			w25.Expand = false;
			w25.Fill = false;
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.DefaultWidth = 388;
			this.DefaultHeight = 224;
			this.Show ();
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
			this.buttonOk.Clicked += new global::System.EventHandler (this.OnButtonOkClicked);
		}
	}
}