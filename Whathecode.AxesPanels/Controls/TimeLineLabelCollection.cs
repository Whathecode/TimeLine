using System;
using System.Windows;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLineLabelCollection : AbstractAxesLabelCollection<DateTime, TimeSpan, double, double>
	{
		internal override void VisibleIntervalChanged( AxesIntervals<DateTime, TimeSpan, double, double> visible, Size panelSize )
		{
			// Nothing to do.
		}

		internal override void LabelResized( FrameworkElement label, AxesIntervals<DateTime, TimeSpan, double, double> visible, Size panelSize )
		{
			// Nothing to do.
		}
	}
}
