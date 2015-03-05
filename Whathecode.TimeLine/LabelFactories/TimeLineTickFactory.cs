using System;
using System.Windows;
using Whathecode.System.Windows.Controls;


namespace Whathecode.TimeLine.LabelFactories
{
	public class TimeLineTickFactory : AbstractTimeIntervalLabelFactory
	{
		protected override FrameworkElement CreateLabel()
		{
			return new TimeLineTick
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
