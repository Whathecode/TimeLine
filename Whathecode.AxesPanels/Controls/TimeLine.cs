using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Whathecode.System.Arithmetic.Range;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLine : ItemsControl
	{
		public enum TimeLineBinding
		{
			VisibleInterval
		}


		static readonly Type Type = typeof( TimeLine );
		public static DependencyPropertyFactory<TimeLineBinding> PropertyFactory = new DependencyPropertyFactory<TimeLineBinding>();
		public static DependencyProperty VisibleIntervalProperty = PropertyFactory[ TimeLineBinding.VisibleInterval ];

		[DependencyProperty( TimeLineBinding.VisibleInterval )]
		public Interval<DateTime, TimeSpan> VisibleInterval
		{
			get { return (Interval<DateTime, TimeSpan>)PropertyFactory.GetValue( this, TimeLineBinding.VisibleInterval ); }
			set { PropertyFactory.SetValue( this, TimeLineBinding.VisibleInterval, value ); }
		}


		#region Attached properties

		/// <summary>
		///   Identifies the Occurance property which indicates where the element should be positioned in time.
		/// </summary>
		public static readonly DependencyProperty OccuranceProperty = DependencyProperty.RegisterAttached( @"Occurance", typeof( DateTime ), Type );
		public static DateTime GetOccurance( FrameworkElement element )
		{
			return (DateTime)element.GetValue( OccuranceProperty );
		}
		public static void SetOccurance( FrameworkElement element, DateTime value )
		{
			element.SetValue( OccuranceProperty, value );
		}

		#endregion // Attached properties.


		public TimeLine()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}


		protected override bool IsItemItsOwnContainerOverride( object item )
		{
			return item is TimeLineItem;
		}

		protected override DependencyObject GetContainerForItemOverride()
		{
			var item = new TimeLineItem();
			if ( ItemContainerStyle != null )
			{
				// Style affects layout. Only set after loaded.
				Dispatcher.BeginInvoke( new Action( () => item.Style = ItemContainerStyle ), DispatcherPriority.Loaded );
			}
			item.SnapsToDevicePixels = SnapsToDevicePixels;

			return item;
		}
	}
}