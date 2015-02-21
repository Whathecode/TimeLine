using Whathecode.System.Arithmetic.Range;


namespace Whathecode.AxesPanels
{
	/// <summary>
	///   A panel which allows positioning elements on a plane, and showing a specified region of the plane.
	/// </summary>
	public class PlanePanel : AxesPanel<double, double, double, double>
	{
		public PlanePanel()
		{
			MaximaX = new Interval<double, double>( double.MinValue, double.MaxValue );
			MaximaY = new Interval<double, double>( double.MinValue, double.MaxValue );
			VisibleIntervalX = new Interval<double, double>( 0, 100 );
			VisibleIntervalY = new Interval<double, double>( 0, 100 );
		}


		protected override double ConvertFromIntervalXValue( double value )
		{
			return value;
		}

		protected override double ConvertToIntervalXValue( double value )
		{
			return value;
		}

		protected override double ConvertFromIntervalYValue( double value )
		{
			return value;
		}

		protected override double ConvertToIntervalYValue( double value )
		{
			return value;
		}

		protected override double ConvertFromXSizeValue( double value )
		{
			return value;
		}

		protected override double ConvertFromYSizeValue( double value )
		{
			return value;
		}
	}
}
