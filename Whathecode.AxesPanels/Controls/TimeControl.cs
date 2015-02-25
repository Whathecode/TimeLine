using System;
using System.Windows;
using System.Windows.Controls;
using Whathecode.System.Arithmetic.Range;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeControl : ItemsControl
	{
		public enum Properties
		{
			VisibleInterval,
			CurrentTime
		}


		class DefaultProvider : IDefaultValueProvider<Properties>
		{
			public object GetDefaultValue( Properties property, Type propertyType )
			{
				switch ( property )
				{
					case Properties.VisibleInterval:
						DateTime now = DateTime.Now;
						TimeSpan span = TimeSpan.FromHours( 12 );
						return new Interval<DateTime, TimeSpan>( now - span, now + span );
					default:
						return null;
				}
			}
		}


		static readonly Type Type = typeof( TimeControl );
		public static DependencyPropertyFactory<Properties> PropertyFactory = new DependencyPropertyFactory<Properties>();
		public static DependencyProperty VisibleIntervalProperty = PropertyFactory[ Properties.VisibleInterval ];
		public static readonly DependencyProperty CurrentTimeProperty = PropertyFactory[ Properties.CurrentTime ];

		[DependencyProperty( Properties.VisibleInterval, DefaultValueProvider = typeof( DefaultProvider ) )]
		public Interval<DateTime, TimeSpan> VisibleInterval
		{
			get { return (Interval<DateTime, TimeSpan>)PropertyFactory.GetValue( this, Properties.VisibleInterval ); }
			set { PropertyFactory.SetValue( this, Properties.VisibleInterval, value ); }
		}

		[DependencyProperty( Properties.CurrentTime )]
		public DateTime CurrentTime
		{
			get { return (DateTime)PropertyFactory.GetValue( this, Properties.CurrentTime ); }
			set { PropertyFactory.SetValue( this, Properties.CurrentTime, value ); }
		}


		static TimeControl()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}


		protected override bool IsItemItsOwnContainerOverride( object item )
		{
			return item is TimeControlItem;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			return new TimeControlItem();
		}
	}
}