using System;
using System.Windows;
using Whathecode.System.Windows.Controls;


namespace Whathecode.TimeLine.LabelFactories
{
	public class TimeLineLabelCollection : AbstractAxesLabelCollection<DateTime, TimeSpan, double, double>
	{
		public override void VisibleIntervalChanged( AxesIntervals<DateTime, TimeSpan, double, double> visible, Size panelSize )
		{
			// Nothing to do.
		}

		public override void LabelResized( FrameworkElement label, AxesIntervals<DateTime, TimeSpan, double, double> visible, Size panelSize )
		{
			// Nothing to do.
		}
	}
}
