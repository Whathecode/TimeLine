using System;
using Whathecode.System.Arithmetic.Range;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes.Coercion;


namespace Whathecode.TimeLine
{
	public class VisibleIntervalCoercion : IControlCoercion<TimeControl.Properties, Interval<DateTime, TimeSpan>>
	{
		public Interval<DateTime, TimeSpan> Coerce( object context, Interval<DateTime, TimeSpan> interval )
		{
			// Don't allow reversed intervals.
			return interval.IsReversed ? interval.Reverse() : interval;
		}

		public TimeControl.Properties DependentProperties
		{
			get { return 0; }
		}
	}
}
