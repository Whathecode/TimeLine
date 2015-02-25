using System;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLineLabelCollection : AbstractAxesLabelCollection<DateTime, TimeSpan, double, double>
	{
		internal override void VisibleIntervalChanged( AxesIntervals<DateTime, TimeSpan, double, double> visible )
		{
			// Nothing to do.
		}
	}
}
