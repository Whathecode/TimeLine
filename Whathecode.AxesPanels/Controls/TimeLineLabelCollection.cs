using System;
using Whathecode.System.Arithmetic.Range;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLineLabelCollection : AbstractAxesLabelFactory<DateTime, TimeSpan, double, double>
	{
		public override void VisibleIntervalChanged( Interval<DateTime, TimeSpan> intervalX, Interval<double, double> intervalY )
		{
			// Nothing to do.
		}
	}
}
