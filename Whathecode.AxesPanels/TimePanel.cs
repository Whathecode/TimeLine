using System;
using System.Windows;
using Whathecode.System.Arithmetic.Range;


namespace Whathecode.AxesPanels
{
	/// <summary>
	///   A timeline panel on which user controls can be outlined in time.
	/// </summary>
	public class TimePanel : AxesPanel<DateTime, TimeSpan, double, double>
	{
		public TimePanel()
		{
			MaximaX = new Interval<DateTime, TimeSpan>( DateTime.MinValue, DateTime.MaxValue );
			MaximaY = new Interval<double, double>( double.MinValue, double.MaxValue );
		}

		static TimePanel()
		{
			Type controlType = typeof( TimePanel );
			DefaultStyleKeyProperty.OverrideMetadata( controlType, new FrameworkPropertyMetadata( controlType ) );

			// Set conversion functions for datetime intervals.
			Interval<DateTime, TimeSpan>.ConvertDoubleToSize = d => new TimeSpan( (long)Math.Round( d ) );
			Interval<DateTime, TimeSpan>.ConvertSizeToDouble = s => s.Ticks;
		}


		protected override double ConvertFromIntervalXValue( DateTime value )
		{
			return value.Ticks;
		}

		protected override DateTime ConvertToIntervalXValue( double value )
		{
			return new DateTime( (long)value );
		}

		protected override double ConvertFromIntervalYValue( double value )
		{
			return value;
		}

		protected override double ConvertToIntervalYValue( double value )
		{
			return value;
		}

		protected override double ConvertFromXSizeValue( TimeSpan value )
		{
			return value.Ticks;
		}

		protected override double ConvertFromYSizeValue( double value )
		{
			return value;
		}
	}
}
