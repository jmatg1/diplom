
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;

	private global::Gtk.Action Action;

	private global::Gtk.Action Action1;

	private global::Gtk.Action goForwardAction;

	private global::Gtk.Action ActionSettings;

	private global::Gtk.Action gfAction;

	private global::Gtk.VBox vbox1;

	private global::Gtk.MenuBar menubar2;

	private global::Gtk.Statusbar statusbar1;

	private global::Gtk.Label labelCom;

	private global::Gtk.Label labelTelnet;

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
		this.ActionSettings = new global::Gtk.Action("ActionSettings", global::Mono.Unix.Catalog.GetString("Настройки"), null, null);
		this.ActionSettings.ShortLabel = global::Mono.Unix.Catalog.GetString("asd");
		w1.Add(this.ActionSettings, null);
		this.gfAction = new global::Gtk.Action("gfAction", global::Mono.Unix.Catalog.GetString("gf"), null, null);
		this.gfAction.Sensitive = false;
		this.gfAction.ShortLabel = global::Mono.Unix.Catalog.GetString("gf");
		this.gfAction.Visible = false;
		this.gfAction.VisibleHorizontal = false;
		this.gfAction.VisibleVertical = false;
		w1.Add(this.gfAction, null);
		this.UIManager.InsertActionGroup(w1, 0);
		this.AddAccelGroup(this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString("<ui><menubar name=\'menubar2\'><menu name=\'ActionSettings\' action=\'ActionSettings\'/" +
				"></menubar></ui>");
		this.menubar2 = ((global::Gtk.MenuBar)(this.UIManager.GetWidget("/menubar2")));
		global::Gtk.Tooltips w2 = new Gtk.Tooltips();
		w2.SetTip(this.menubar2, "asd", "asd");
		this.menubar2.Name = "menubar2";
		this.vbox1.Add(this.menubar2);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.menubar2]));
		w3.Position = 0;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbar1 = new global::Gtk.Statusbar();
		this.statusbar1.Sensitive = false;
		this.statusbar1.Name = "statusbar1";
		this.statusbar1.Homogeneous = true;
		this.statusbar1.Spacing = 6;
		// Container child statusbar1.Gtk.Box+BoxChild
		this.labelCom = new global::Gtk.Label();
		this.labelCom.Name = "labelCom";
		this.labelCom.LabelProp = global::Mono.Unix.Catalog.GetString("COMn: off");
		this.statusbar1.Add(this.labelCom);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.statusbar1[this.labelCom]));
		w4.Position = 1;
		w4.Expand = false;
		w4.Fill = false;
		// Container child statusbar1.Gtk.Box+BoxChild
		this.labelTelnet = new global::Gtk.Label();
		this.labelTelnet.Name = "labelTelnet";
		this.labelTelnet.LabelProp = global::Mono.Unix.Catalog.GetString("192.168.0.2: on");
		this.statusbar1.Add(this.labelTelnet);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.statusbar1[this.labelTelnet]));
		w5.Position = 2;
		w5.Expand = false;
		w5.Fill = false;
		this.vbox1.Add(this.statusbar1);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.statusbar1]));
		w6.Position = 2;
		w6.Expand = false;
		w6.Fill = false;
		this.Add(this.vbox1);
		if ((this.Child != null))
		{
			this.Child.ShowAll();
		}
		this.DefaultWidth = 400;
		this.DefaultHeight = 300;
		this.Show();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler(this.OnDeleteEvent);
		this.ActionSettings.Activated += new global::System.EventHandler(this.OnActionSettingsActivated);
	}
}