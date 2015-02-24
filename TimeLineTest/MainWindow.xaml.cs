﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Whathecode.AxesPanels.Controls;
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
			CurrentTime
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


		public MainWindow()
		{
			_properties = new NotifyPropertyFactory<Properties>( this, () => PropertyChanged );
			Items = new ObservableCollection<object>();
			CurrentTime = DateTime.Now;

			Timer update = new Timer( 100 );
			update.Elapsed += ( sender, args ) => CurrentTime = DateTime.Now;
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
				Occurance = DateTime.Now
			};
			Items.Add( test );
		}
	}
}
