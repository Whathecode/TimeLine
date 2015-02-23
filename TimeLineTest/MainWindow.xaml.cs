using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Whathecode.AxesPanels.Controls;


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

			Label test = new Label { Content = "Occurance is working!" };
			test.SetValue( TimeLine.OccuranceProperty, new DateTime( 2015, 2, 23 ) );
			Items.Add( test );
		}
	}
}
