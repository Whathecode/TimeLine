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
		public enum TimeControlBinding
		{
			VisibleInterval
		}


		static readonly Type Type = typeof( TimeControl );
		public static DependencyPropertyFactory<TimeControlBinding> PropertyFactory = new DependencyPropertyFactory<TimeControlBinding>();
		public static DependencyProperty VisibleIntervalProperty = PropertyFactory[ TimeControlBinding.VisibleInterval ];

		[DependencyProperty( TimeControlBinding.VisibleInterval )]
		public Interval<DateTime, TimeSpan> VisibleInterval
		{
			get { return (Interval<DateTime, TimeSpan>)PropertyFactory.GetValue( this, TimeControlBinding.VisibleInterval ); }
			set { PropertyFactory.SetValue( this, TimeControlBinding.VisibleInterval, value ); }
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