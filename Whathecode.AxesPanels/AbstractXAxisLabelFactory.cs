using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Whathecode.System.Arithmetic.Range;


namespace Whathecode.AxesPanels
{
	public abstract class AbstractXAxesFactory<TX, TXSize, TY, TYSize> : AbstractAxesLabelFactory<TX, TXSize, TY, TYSize>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		public TX Anchor { get; set; }
		public TY FixedY { get; set; }
		public TXSize StepSize { get; set; }

		/// <summary>
		///   The minimum amount of pixels in between labels before they are hidden.
		/// </summary>
		public double MinimumPixelsBetweenLabels { get; set; }


		protected override Tuple<TXSize, TYSize> GetMaximumLabelSize( AxesIntervals<TX, TXSize, TY, TYSize> visible )
		{
			return new Tuple<TXSize, TYSize>( StepSize, default( TYSize ) );
		}

		protected override IEnumerable<Tuple<TX, TY>> GetPositions( AxesIntervals<TX, TXSize, TY, TYSize> intervals, Size panelSize )
		{
			// When not enough pixels in between labels, do not show any labels.
			double intervalSize = Interval<TX, TXSize>.ConvertSizeToDouble( intervals.IntervalX.Size );
			double stepSize = Interval<TX, TXSize>.ConvertSizeToDouble( StepSize );
			double pixelsBetween = panelSize.Width * ( stepSize / intervalSize );
			if ( pixelsBetween < MinimumPixelsBetweenLabels )
			{
				return new Tuple<TX, TY>[] { };
			}

			return intervals.IntervalX.GetValues( StepSize, Anchor ).Select( x => new Tuple<TX, TY>( x, FixedY ) );
		}
	}
}
