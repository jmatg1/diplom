
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;

	private global::Gtk.Action Action;

	private global::Gtk.Action Action1;

	private global::Gtk.Action goForwardAction;

	private global::Gtk.Action Action2;

	private global::Gtk.Action gfAction;

	private global::Gtk.Action CloseAction;

	private global::Gtk.Action executeAction;

	private global::Gtk.Action preferencesAction;

	private global::Gtk.VBox vbox1;

	private global::Gtk.Toolbar toolbar2;

	private global::Gtk.HPaned hpaned2;

	private global::Gtk.VBox vbox2;

	private global::Gtk.VBox vbox3;

	private global::Gtk.VBox vbox4;

	private global::Gtk.HBox hbox2;

	private global::Gtk.Label Уставка;

	private global::Gtk.Label label14;

	private global::Gtk.Label label16;

	private global::Gtk.HBox hbox3;

	private global::Gtk.Label LabelSetting;

	private global::Gtk.Label LabelTemp;

	private global::Gtk.Label LabelMSD;

	private global::Gtk.ScrolledWindow GtkScrolledWindow1;

	private global::Gtk.TextView textview3;

	private global::Gtk.HBox hbox1;

	private global::Gtk.Label label1;

	private global::Gtk.ComboBox comboS;

	private global::Gtk.Button butStart;

	private global::Gtk.Button butStop;

	protected virtual void Build()
	{
		global::Stetic.Gui.Initialize(this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup("Default");
		this.Action = new global::Gtk.Action("Action", global::Mono.Unix.Catalog.GetString("Настройки"), null, null);
		this.Action.ShortLabel = global::Mono.Unix.Catalog.GetString("Настройки");
		w1.Add(this.Action, null);
		this.Action1 = new global::Gtk.Action("Action1", global::Mono.Unix.Catalog.GetString("Настройки"), null, null);
		this.Action1.HideIfEmpty = false;
		this.Action1.ShortLabel = global::Mono.Unix.Catalog.GetString("Настройки");
		w1.Add(this.Action1, null);
		this.goForwardAction = new global::Gtk.Action("goForwardAction", global::Mono.Unix.Catalog.GetString("_Forward"), null, "gtk-go-forward");
		this.goForwardAction.ShortLabel = global::Mono.Unix.Catalog.GetString("_Forward");
		w1.Add(this.goForwardAction, null);
		this.Action2 = new global::Gtk.Action("Action2", global::Mono.Unix.Catalog.GetString("Настройки"), null, null);
		this.Action2.ShortLabel = global::Mono.Unix.Catalog.GetString("asd");
		w1.Add(this.Action2, null);
		this.gfAction = new global::Gtk.Action("gfAction", global::Mono.Unix.Catalog.GetString("gf"), null, null);
		this.gfAction.Sensitive = false;
		this.gfAction.ShortLabel = global::Mono.Unix.Catalog.GetString("gf");
		this.gfAction.Visible = false;
		this.gfAction.VisibleHorizontal = false;
		this.gfAction.VisibleVertical = false;
		w1.Add(this.gfAction, null);
		this.CloseAction = new global::Gtk.Action("CloseAction", global::Mono.Unix.Catalog.GetString("Close"), null, null);
		this.CloseAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Close");
		w1.Add(this.CloseAction, null);
		this.executeAction = new global::Gtk.Action("executeAction", null, null, "gtk-execute");
		w1.Add(this.executeAction, null);
		this.preferencesAction = new global::Gtk.Action("preferencesAction", global::Mono.Unix.Catalog.GetString("Настройки"), null, "gtk-preferences");
		this.preferencesAction.IsImportant = true;
		this.preferencesAction.ShortLabel = global::Mono.Unix.Catalog.GetString("Настройки");
		w1.Add(this.preferencesAction, null);
		this.UIManager.InsertActionGroup(w1, 0);
		this.AddAccelGroup(this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("Onyx");
		this.Icon = global::Stetic.IconLoader.LoadIcon(this, "gtk-yes", global::Gtk.IconSize.Menu);
		this.WindowPosition = ((global::Gtk.WindowPosition)(1));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString("<ui><toolbar name=\'toolbar2\'><toolitem name=\'preferencesAction\' action=\'preferenc" +
				"esAction\'/></toolbar></ui>");
		this.toolbar2 = ((global::Gtk.Toolbar)(this.UIManager.GetWidget("/toolbar2")));
		this.toolbar2.Name = "toolbar2";
		this.toolbar2.ShowArrow = false;
		this.toolbar2.ToolbarStyle = ((global::Gtk.ToolbarStyle)(1));
		this.vbox1.Add(this.toolbar2);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.toolbar2]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.hpaned2 = new global::Gtk.HPaned();
		this.hpaned2.CanFocus = true;
		this.hpaned2.Name = "hpaned2";
		this.hpaned2.Position = 1;
		// Container child hpaned2.Gtk.Paned+PanedChild
		this.vbox2 = new global::Gtk.VBox();
		this.vbox2.Name = "vbox2";
		this.vbox2.Homogeneous = true;
		this.vbox2.Spacing = 6;
		this.vbox2.BorderWidth = ((uint)(3));
		// Container child vbox2.Gtk.Box+BoxChild
		this.vbox3 = new global::Gtk.VBox();
		this.vbox3.Name = "vbox3";
		this.vbox3.Spacing = 6;
		// Container child vbox3.Gtk.Box+BoxChild
		this.vbox4 = new global::Gtk.VBox();
		this.vbox4.Name = "vbox4";
		this.vbox4.Spacing = 6;
		// Container child vbox4.Gtk.Box+BoxChild
		this.hbox2 = new global::Gtk.HBox();
		this.hbox2.Name = "hbox2";
		this.hbox2.Homogeneous = true;
		this.hbox2.Spacing = 6;
		// Container child hbox2.Gtk.Box+BoxChild
		this.Уставка = new global::Gtk.Label();
		this.Уставка.Name = "Уставка";
		this.Уставка.LabelProp = global::Mono.Unix.Catalog.GetString("Уставка");
		this.hbox2.Add(this.Уставка);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.Уставка]));
		w3.Position = 0;
		w3.Expand = false;
		w3.Fill = false;
		// Container child hbox2.Gtk.Box+BoxChild
		this.label14 = new global::Gtk.Label();
		this.label14.Name = "label14";
		this.label14.LabelProp = global::Mono.Unix.Catalog.GetString("Температура");
		this.hbox2.Add(this.label14);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.label14]));
		w4.Position = 1;
		w4.Expand = false;
		w4.Fill = false;
		// Container child hbox2.Gtk.Box+BoxChild
		this.label16 = new global::Gtk.Label();
		this.label16.Name = "label16";
		this.label16.LabelProp = global::Mono.Unix.Catalog.GetString("СКО");
		this.hbox2.Add(this.label16);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.label16]));
		w5.Position = 2;
		w5.Expand = false;
		w5.Fill = false;
		this.vbox4.Add(this.hbox2);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.hbox2]));
		w6.Position = 0;
		w6.Expand = false;
		w6.Fill = false;
		// Container child vbox4.Gtk.Box+BoxChild
		this.hbox3 = new global::Gtk.HBox();
		this.hbox3.Name = "hbox3";
		this.hbox3.Homogeneous = true;
		this.hbox3.Spacing = 6;
		// Container child hbox3.Gtk.Box+BoxChild
		this.LabelSetting = new global::Gtk.Label();
		this.LabelSetting.Name = "LabelSetting";
		this.LabelSetting.LabelProp = global::Mono.Unix.Catalog.GetString("0");
		this.hbox3.Add(this.LabelSetting);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.LabelSetting]));
		w7.Position = 0;
		w7.Expand = false;
		w7.Fill = false;
		// Container child hbox3.Gtk.Box+BoxChild
		this.LabelTemp = new global::Gtk.Label();
		this.LabelTemp.Name = "LabelTemp";
		this.LabelTemp.LabelProp = global::Mono.Unix.Catalog.GetString("0");
		this.hbox3.Add(this.LabelTemp);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.LabelTemp]));
		w8.Position = 1;
		w8.Expand = false;
		w8.Fill = false;
		// Container child hbox3.Gtk.Box+BoxChild
		this.LabelMSD = new global::Gtk.Label();
		this.LabelMSD.Name = "LabelMSD";
		this.LabelMSD.LabelProp = global::Mono.Unix.Catalog.GetString("0");
		this.hbox3.Add(this.LabelMSD);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.LabelMSD]));
		w9.Position = 2;
		w9.Expand = false;
		w9.Fill = false;
		this.vbox4.Add(this.hbox3);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.hbox3]));
		w10.Position = 1;
		w10.Expand = false;
		w10.Fill = false;
		this.vbox3.Add(this.vbox4);
		global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.vbox4]));
		w11.Position = 0;
		w11.Expand = false;
		w11.Fill = false;
		// Container child vbox3.Gtk.Box+BoxChild
		this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
		this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
		this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
		// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
		this.textview3 = new global::Gtk.TextView();
		this.textview3.CanFocus = true;
		this.textview3.Name = "textview3";
		this.textview3.Overwrite = true;
		this.textview3.WrapMode = ((global::Gtk.WrapMode)(3));
		this.GtkScrolledWindow1.Add(this.textview3);
		this.vbox3.Add(this.GtkScrolledWindow1);
		global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.GtkScrolledWindow1]));
		w13.Position = 1;
		// Container child vbox3.Gtk.Box+BoxChild
		this.hbox1 = new global::Gtk.HBox();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.label1 = new global::Gtk.Label();
		this.label1.Name = "label1";
		this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("График:");
		this.hbox1.Add(this.label1);
		global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label1]));
		w14.Position = 0;
		w14.Expand = false;
		w14.Fill = false;
		// Container child hbox1.Gtk.Box+BoxChild
		this.comboS = global::Gtk.ComboBox.NewText();
		this.comboS.Name = "comboS";
		this.hbox1.Add(this.comboS);
		global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.comboS]));
		w15.Position = 1;
		w15.Expand = false;
		w15.Fill = false;
		// Container child hbox1.Gtk.Box+BoxChild
		this.butStart = new global::Gtk.Button();
		this.butStart.CanFocus = true;
		this.butStart.Name = "butStart";
		this.butStart.UseUnderline = true;
		this.butStart.Label = global::Mono.Unix.Catalog.GetString("Старт");
		this.hbox1.Add(this.butStart);
		global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.butStart]));
		w16.Position = 2;
		w16.Expand = false;
		w16.Fill = false;
		// Container child hbox1.Gtk.Box+BoxChild
		this.butStop = new global::Gtk.Button();
		this.butStop.CanFocus = true;
		this.butStop.Name = "butStop";
		this.butStop.UseUnderline = true;
		this.butStop.Label = global::Mono.Unix.Catalog.GetString("Стоп");
		this.hbox1.Add(this.butStop);
		global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.butStop]));
		w17.Position = 3;
		w17.Expand = false;
		w17.Fill = false;
		this.vbox3.Add(this.hbox1);
		global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.hbox1]));
		w18.Position = 2;
		w18.Expand = false;
		w18.Fill = false;
		this.vbox2.Add(this.vbox3);
		global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.vbox3]));
		w19.Position = 0;
		this.hpaned2.Add(this.vbox2);
		global::Gtk.Paned.PanedChild w20 = ((global::Gtk.Paned.PanedChild)(this.hpaned2[this.vbox2]));
		w20.Resize = false;
		this.vbox1.Add(this.hpaned2);
		global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hpaned2]));
		w21.Position = 1;
		this.Add(this.vbox1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 987;
		this.DefaultHeight = 480;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.executeAction.Activated += new global::System.EventHandler(this.OnActionSettingsActivated);
		this.preferencesAction.Activated += new global::System.EventHandler(this.OnActionSettingsActivated);
		this.comboS.Changed += new global::System.EventHandler(this.OnComboSChanged);
		this.butStart.Clicked += new global::System.EventHandler(this.OnButStartClicked);
		this.butStop.Clicked += new global::System.EventHandler(this.OnButStopClicked);
	}
}
