using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Whathecode.AxesPanels.Controls;
using Whathecode.System.Arithmetic.Range;


namespace TimeLineTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public ObservableCollection<object> Items { get; set; }


		public MainWindow()
		{
			Items = new ObservableCollection<object>();

			InitializeComponent();

			TimeControlItem test = new TimeControlItem();
			test.Content = new Label { Content = "Occurance is working!" };
			test.Occurance = new DateTime( 2015, 2, 23 );
			Items.Add( test );
		}
	}
}
