using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
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
			test.Content = new Border
			{
				Background = Brushes.SteelBlue,
				Width = 100,
				Height = 50,
				CornerRadius = new CornerRadius( 5 ),
				BorderBrush = Brushes.White,
				BorderThickness = new Thickness( 2 )
			};
			test.Occurance = new DateTime( 2015, 2, 23 );
			Items.Add( test );
		}
	}
}
