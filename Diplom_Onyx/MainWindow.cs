using System;
using Gtk;
using System.Threading;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.GtkSharp;
using OxyPlot.Series;
using System.Collections.Generic;
using System.Numerics;


public partial class MainWindow : Gtk.Window
{


	public static PlotView _plotView;
	public static PlotModel _plotTest = new PlotModel { Title = "Test"};
	public static PlotModel _plotS11 = new PlotModel();
	public static PlotModel _plotS12 = new PlotModel { Title = "S12/freq" };

	public static PlotModel _plotS21 = new PlotModel();
	public static PlotModel _plotS22 = new PlotModel();
	public static PlotModel _plotMSD = new PlotModel { Title = "MSD/t" };

	private Diplom.MainProgramm _start; // Обработчик событий
	public Thread threadStart; // Поток создается по кнопке старт
	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{


		Build();
		this.hpaned2.Position = 256;
		//--График

		//add in GTK#
		_plotView = new PlotView();
		_plotView.Name = "WidPlot";
		this.hpaned2.Add(_plotView);
		global::Gtk.Paned.PanedChild w4 = ((global::Gtk.Paned.PanedChild)(this.hpaned2[_plotView]));
		w4.Resize = false;

		_plotView.ShowAll();

		_plotS12.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Частота", Unit = "Гц^{}" });
		_plotS12.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Модуль", Unit = "|S12|^{}" });
		_plotMSD.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Время", Unit = "сек^{}" });
		_plotMSD.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Среднее квадратичное отклонение", Unit = "^[]" });
		//_plotView.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 80}); // линейка низ
		//_plotView.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 10}); // линейка лево



		var areaSeries1 = new LineSeries();
		var points1 = new List<DataPoint>();
		points1.Add(new DataPoint(0, 0));
		points1.Add(new DataPoint(1, 1));
		points1.Add(new DataPoint(2, 2));
		points1.Add(new DataPoint(3, 3));
		areaSeries1.ItemsSource = points1;
		areaSeries1.Title = "S";

		var areaSeries2 = new LineSeries();
		var points2 = new List<DataPoint>();
		points2.Add(new DataPoint(4, 4));
		points2.Add(new DataPoint(5, 5));
		points2.Add(new DataPoint(6, 6));
		points2.Add(new DataPoint(7, 7));
		areaSeries2.ItemsSource = points2;
		areaSeries2.Title = "St";
		areaSeries1.Color = OxyColors.Green;	//Зеленный
		areaSeries2.Color = OxyColors.Blue; //Голубой

		//_plotTest.Axes.Add(new LinearAxis(AxisPosition.Bottom, -1,1, "") );
		_plotTest.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Частота", Unit = "Гц^{}" });
		//_plotTest.Axes.Add(new LinearAxis { Position = AxisPosition.Right, Title = "Bottom"});
		//Console.WriteLine(_plotTest.ltYAxis);
		_plotTest.Series.Add(areaSeries1);
		_plotTest.Series.Add(areaSeries2);

		_plotView.Model = _plotTest;
				//_plotView.Model.Axes.Add(new LinearAxis {Position= AxisPosition.Bottom, Title = "Bottom" });
		//--

		this.comboS.AppendText("S12");
		this.comboS.AppendText("СКО");
		this.comboS.AppendText("Test");
		this.comboS.Active = 2;
		//Diplom.MainClass.PlotTest (this.widgetplot1.ShowPlot);
		//System.Threading.Thread.Sleep(5000);
		Thread threadtest = new Thread(new ThreadStart(test));
		threadtest.Start();
	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

	protected void OnActionSettingsActivated(object sender, EventArgs e)
	{
		Diplom.MainClass.dl = new Diplom.SettingsWindows();

	}
	public void test()
	{
		System.Threading.Thread.Sleep(1000);
		var areaSeries2 = new LineSeries();
		var points2 = new List<DataPoint>();
		points2.Add(new DataPoint(2, 7));
		points2.Add(new DataPoint(8, 6));
		points2.Add(new DataPoint(6, 6));
		points2.Add(new DataPoint(7, 7));
		areaSeries2.ItemsSource = points2;
		areaSeries2.Title = "St2";
		_plotTest.Series.Add(areaSeries2);
		_plotView.Model = _plotTest;
		_plotView.Model.InvalidatePlot(true);
	}



	protected void OnButStartClicked(object sender, EventArgs e)
	{
		butStart.Sensitive = false;
		_start = new Diplom.MainProgramm();
		_start.LogWriteLine += Log;
		_start.ShowSSt += ChartingSt;
		_start.WriteLabelSetting += _start_WriteLabelSetting;
		_start.WriteLabelTemp += _start_WriteLabelTemp;
		_start.WriteLabelMSD += _start_WriteLabelMSD;

		threadStart = new Thread(new ThreadStart(_start.Start));
		threadStart.IsBackground = true; //теперь он фоновый и его можно закрыть по закытию  программы
		Diplom.MainProgramm.SetFlagThread = true;
		threadStart.Start();



	}
	#region Вывод лога, уставки, температуры, ско 
	public void Log(string s)
	{
		Gtk.Application.Invoke(delegate
		{
			s = System.DateTime.Now.ToLongTimeString() + s + "\n";
			textview3.Buffer.Insert(textview3.Buffer.StartIter, s);
		});
	}

	void _start_WriteLabelSetting(string setting)
	{
		Gtk.Application.Invoke(delegate
		{
			this.LabelSetting.Text = setting;
		});	}

	void _start_WriteLabelTemp(string temp)
	{
		Gtk.Application.Invoke(delegate
		{
			this.LabelTemp.Text = temp;
		});
	}

	void _start_WriteLabelMSD(string MSD)
	{
		Gtk.Application.Invoke(delegate
		{
			this.LabelMSD.Text = MSD;
		});

	}
	#endregion
	/// <summary>
	/// Останавливаем поток.
	/// </summary>
	protected void OnButStopClicked(object sender, EventArgs e)
	{
		if (threadStart == null) 							// если поток не создан, то ничего не делаем
			return;
		if (Diplom.MainProgramm.flagZero.IsAlive == true)	// отключаем таймер, при котором температура в камере меняется по его истечению
		{
			Diplom.MainProgramm.flagZero.Abort();
			Diplom.MainProgramm.flagZero.Join();
		}
		if (threadStart.IsAlive)
		{
			Diplom.MainProgramm.SetFlagThread = false;          // ЗАкрытие потока
			threadStart.Abort();                                // Остановка
			threadStart.Join();                                 // ждем пока не закроется
			if (Diplom.MainProgramm.Com.CheckOpen())            // закрываем ком порт, если включен
				Diplom.MainProgramm.Com.Dispose();
			if (Diplom.MainProgramm.telnet.IsOpen)            // закрываем ком порт, если включен
				Diplom.MainProgramm.telnet.Dispose();
			butStart.Sensitive = true;                          // Включаем кнопку старт

		}
	}
	/// <summary>
	/// Два графика. Первый и второй такойже только после некоторго времени
	/// </summary>
	/// <param name="S">S.</param>
	/// <param name="St">St.</param>
	/// <param name="freq">Freq.</param>
	public void ChartingSt(List<Complex> S, List<Complex> St, List<double> freq)
	{
		_plotS12.Series.Clear(); // Очищаем точки
		var points = new List<DataPoint>();
		var points1 = new List<DataPoint>();
		//points.Add(new DataPoint(17500, 14350));
		for (int i = 0; i < S.Count; i++)
		{
			points.Add(new DataPoint(freq[i], Math.Sqrt(S[i].Real * S[i].Real + S[i].Imaginary * S[i].Imaginary))); // Амплитуда и фаза берется сразу из запроса не преобразовывается к виду Дб и Град
			points1.Add(new DataPoint(freq[i], Math.Sqrt(St[i].Real * St[i].Real + St[i].Imaginary * St[i].Imaginary)));
		}

		var areaSeries = new LineSeries();
		areaSeries.ItemsSource = points;
		areaSeries.Title = "S";

		var areaSeries1 = new LineSeries();
		areaSeries1.ItemsSource = points1;
		areaSeries1.Title = "St";

		_plotS12.Series.Add(areaSeries);
		_plotS12.Series.Add(areaSeries1);
		Gtk.Application.Invoke(delegate
		{
			_plotView.Model = _plotS12;
			_plotS12.InvalidatePlot(true);  //Должна обновить график!!! ПРОВЕРИТЬ
											//this.hpaned2.Child2.Hide();	//Если не поможет то вот это расскомментировать
											//this.hpaned2.Child2.ShowAll ();


		});
		//var model = new PlotModel { Title = "DateTimeAxis" };

		//var startDate = DateTime.Now.AddDays(-10);
		//var endDate = DateTime.Now;

		//var minValue = DateTimeAxis.ToDouble(startDate);
		//var maxValue = DateTimeAxis.ToDouble(endDate);

		//model.Axes.Add(new DateTimeAxis { Position = AxisPosition.Bottom, Minimum = minValue, Maximum = maxValue, StringFormat = "HH:mm:ss"});
		//model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 80}); // линейка низ
		//model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 10}); // линейка лево
		//model.Axes.Add(new LogarithmicAxis { Position = AxisPosition.Left , Minimum = 0, Maximum = 100000000}); //

		//model.Series.

		//myModel.Series.Add(new FunctionSeries(funcFreq, 10000, 100000000, 1000, "cos(x)"));
		//myModel.Series.Add( new Data
		//var	lineSeries1 = new DataPoint();
		//	model.Series.Add (lineSeries1);
		//var points = new System.Collections.Generic.List<DataPoint> ();
		//points.Add{1,2};
		//var Aremodel = new PlotModel ();
		//var areaSeries = new AreaSeries ();
		//areaSeries.Title = "AreaSeries";
		//areaSeries.DataFieldX = "asd";
		//areaSeries.Points 
		//Aremodel.Series.Add(areaSeries);
		//var points = new List<DataPoint> ();
		//points.Add(new DataPoint(17500, 14350));
		//points.Add(new DataPoint(19000, 25000));

	}
	/// <summary>
	/// Вывод среднее квадратичное отклонение от времени
	/// </summary>
	/// <param name="MSK">MD.</param>
	public void ChartingMSD(List<double> MSD)
	{

		var pointsMSD = new List<DataPoint>();
		//points.Add(new DataPoint(17500, 14350));
		for (int i = 0; i < MSD.Count; i++)
		{
			pointsMSD.Add(new DataPoint(i, MSD[i]));
		}
		var myModel = new PlotModel { Title = "MSD/t" };

		var areaSeries = new LineSeries();
		areaSeries.ItemsSource = pointsMSD;

		myModel.Series.Add(areaSeries);
		_plotView.Model = myModel;
	}
	protected void OnComboSChanged(object sender, EventArgs e)
	{
		switch (this.comboS.Active)
		{
			case 0: // S12
				{
					_plotView.Model = _plotS12;
					System.Diagnostics.Debug.WriteLine(this.comboS.ActiveText);
					break;
				}
			case 1: //MSD
				{
					System.Diagnostics.Debug.WriteLine(this.comboS.ActiveText);
					_plotView.Model = _plotMSD;
					_plotView.ShowNow();
					break;
				}
			case 2: // test
				{
					System.Diagnostics.Debug.WriteLine(this.comboS.ActiveText);
					_plotView.Model = _plotTest;
					_plotView.ShowNow();
					break;
				}

		}


	}
}
