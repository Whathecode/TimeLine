using System;
using System.Windows;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeIntervalLabelFactory : AbstractXAxesFactory<DateTime, TimeSpan, double, double>
	{
		protected override FrameworkElement CreateLabel()
		{
			return new TimeLineLabel { Interval = StepSize };
		}

		protected override void InitializeLabel( PositionedElement positioned, AxesIntervals<DateTime, TimeSpan, double, double> visible )
		{
			// Nothing to do.
		}

		protected override void UpdateLabel( PositionedElement label, AxesIntervals<DateTime, TimeSpan, double, double> visible )
		{
			// Nothing to do.
		}
	}
}
