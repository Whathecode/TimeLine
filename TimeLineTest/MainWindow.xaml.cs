using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Whathecode.AxesPanels;
using Whathecode.AxesPanels.Controls;
using Whathecode.System.Arithmetic.Range;
using Whathecode.System.ComponentModel.NotifyPropertyFactory;
using Whathecode.System.ComponentModel.NotifyPropertyFactory.Attributes;


namespace TimeLineTest
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : INotifyPropertyChanged
	{
		enum Properties
		{
			Items,
			CurrentTime,
			VisibleInterval
		}

		public event PropertyChangedEventHandler PropertyChanged;
		readonly NotifyPropertyFactory<Properties> _properties;

		[NotifyProperty( Properties.Items )]
		public ObservableCollection<object> Items
		{
			get { return (ObservableCollection<object>)_properties.GetValue( Properties.Items ); }
			set { _properties.SetValue( Properties.Items, value ); }
		}

		[NotifyProperty( Properties.CurrentTime )]
		public DateTime CurrentTime
		{
			get { return (DateTime)_properties.GetValue( Properties.CurrentTime ); }
			set { _properties.SetValue( Properties.CurrentTime, value ); }
		}

		[NotifyProperty( Properties.VisibleInterval )]
		public Interval<DateTime, TimeSpan> VisibleInterval
		{
			get { return (Interval<DateTime, TimeSpan>)_properties.GetValue( Properties.VisibleInterval ); }
			set { _properties.SetValue( Properties.VisibleInterval, value ); }
		}


		public MainWindow()
		{
			DateTime now = DateTime.Now;

			_properties = new NotifyPropertyFactory<Properties>( this, () => PropertyChanged );
			Items = new ObservableCollection<object>();
			CurrentTime = now;
			TimeSpan zoom = TimeSpan.FromMinutes( 5 );
			VisibleInterval = new TimeInterval( CurrentTime - zoom, CurrentTime + zoom );
			//VisibleInterval = new Interval<DateTime, TimeSpan>( new DateTime( 2015, 3, 3, 13, 0, 0 ),  new DateTime( 2015, 3, 3, 14, 0, 0 ) );

			Timer update = new Timer( 100 );
			update.Elapsed += ( sender, args ) =>
			{
				CurrentTime = DateTime.Now;
				//VisibleInterval = VisibleInterval.Move( TimeSpan.FromSeconds( 20 ) );
				VisibleInterval = VisibleInterval.Scale( 1.01 );
			};
			update.Start();

			InitializeComponent();

			TimeControlItem test = new TimeControlItem
			{
				Content = new Border
				{
					Background = Brushes.SteelBlue,
					Width = 100,
					Height = 50,
					CornerRadius = new CornerRadius( 5 ),
					BorderBrush = Brushes.White,
					BorderThickness = new Thickness( 2 )
				},
				Occurance = now
			};
			test.SetValue( TimePanel.YProperty, 50.0 );
			Items.Add( test );
		}
	}
}
