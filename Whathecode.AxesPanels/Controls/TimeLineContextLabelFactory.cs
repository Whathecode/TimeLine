using System;
using System.Windows;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLineContextLabelFactory : AbstractTimeIntervalLabelFactory
	{
		protected override FrameworkElement CreateLabel()
		{
			return new TimeLineContextLabel
			{
				Interval = MaximumLabelSize,
				FactoryName = Name
			};
		}

		protected override void InitializeLabel( PositionedElement positioned, AxesIntervals<DateTime, TimeSpan, double, double> visible, Size panelSize )
		{
			// Nothing to do.
		}

		protected override void UpdateLabel( PositionedElement label, AxesIntervals<DateTime, TimeSpan, double, double> visible, Size panelSize )
		{
			// Nothing to do.
		}
	}
}
