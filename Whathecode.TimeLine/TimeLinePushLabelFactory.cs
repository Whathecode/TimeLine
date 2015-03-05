using System;
using System.Linq;
using System.Windows;
using Whathecode.System.Linq;
using Whathecode.System.Windows.Controls;


namespace Whathecode.TimeLine
{
	public abstract class AbstractTimeLinePushLabelFactory : AbstractTimeIntervalLabelFactory
	{
		protected override void InitializeLabel( PositionedElement positioned, AxesIntervals<DateTime, TimeSpan, double, double> visible, Size panelSize )
		{
			// Save position where element is placed in 'Occurance'.
			// Actual X position is changed for the label to be 'pushed off' the screen by the next label.
			TimeLineItem header = (TimeLineItem)positioned.Element;
			header.Occurance = (DateTime)header.GetValue( TimePanel.XProperty );
		}

		protected override void UpdateLabel( PositionedElement label, AxesIntervals<DateTime, TimeSpan, double, double> visible, Size panelSize )
		{
			TimeLineItem header = (TimeLineItem)label.Element;
			if ( header.Visibility != Visibility.Visible )
			{
				return;
			}

			// Get next label.
			PositionedElement nextLabel = null;
			var nextLabels = VisibleLabels
				.Where( l => l.Position.Item1 > label.Position.Item1 )
				.ToList();
			if ( nextLabels.Count > 0 )
			{
				nextLabel = nextLabels.MinBy( l => l.Position.Item1 );
			}

			// Position earliest label on the top left, pushing it off the screen by the subsequent label when overlapping.
			DateTime updatedPosition = label.Position.Item1;
			if ( label.Position.Item1 < visible.IntervalX.Start )
			{
				if ( nextLabel != null )
				{
					double nextOffset = SizeToPixels( nextLabel.Position.Item1 - visible.IntervalX.Start, visible, panelSize );
					double overlap = Math.Max( 0, header.ActualWidth - nextOffset );
					updatedPosition = visible.IntervalX.Start - PixelsToSize( overlap, visible, panelSize );
				}
				else
				{
					updatedPosition = visible.IntervalX.Start;
				}
			}
			header.SetValue( TimePanel.XProperty, updatedPosition );
		}
	}
}
