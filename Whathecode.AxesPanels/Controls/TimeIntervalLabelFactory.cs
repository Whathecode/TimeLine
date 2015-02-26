using System;
using System.Windows;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeIntervalLabelFactory : AbstractXAxesFactory<DateTime, TimeSpan, double, double>
	{
		/// <summary>
		///   A DateTime format string, determining how to represent the occurance.
		/// </summary>
		public string LabelFormat { get; set; }


		protected override FrameworkElement CreateLabel()
		{
			return new TimeLineLabel { Interval = StepSize, LabelFormat = LabelFormat };
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
