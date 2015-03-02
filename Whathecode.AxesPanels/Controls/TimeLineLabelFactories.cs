using System;
using System.Linq;
using Whathecode.System.Arithmetic.Range;
using Whathecode.System.Linq;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLineLabelFactories : AxesLabelFactories<DateTime, TimeSpan, double, double>
	{
		/// <summary>
		///   Returns the name of the factory which has populated the time line with the most labels which are still visible.
		/// </summary>
		public string GetDominantFactory( Interval<DateTime, TimeSpan> visibleInterval, double inWidth )
		{
			var visible = this.OfType<TimeIntervalTickFactory>().Where( f => !f.MinimumPixelsExceeded ).ToList();
			if ( visible.Count == 0 )
			{
				return "";
			}

			return visible.MinBy( f => f.MaximumLabelSize ).Name;
		}
	}
}
