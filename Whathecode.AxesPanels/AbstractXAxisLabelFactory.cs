using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Whathecode.System.Arithmetic.Range;


namespace Whathecode.AxesPanels
{
	public abstract class AbstractXAxisLabelFactory<TX, TXSize, TY, TYSize> : AbstractAxesLabelFactory<TX, TXSize, TY, TYSize>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		/// <summary>
		///   The maximum size along the x-axis of labels provided by this factory.
		/// </summary>
		public TXSize MaximumLabelSize { get; set; }

		/// <summary>
		///   The minimum amount of pixels in between labels before they are hidden.
		/// </summary>
		public double MinimumPixelsBetweenLabels { get; set; }

		/// <summary>
		///   Set to true when <see cref="MinimumPixelsBetweenLabels" /> has been exceeded.
		/// </summary>
		public bool MinimumPixelsExceeded { get; private set; }

		public TY FixedY { get; set; }


		protected override Tuple<TXSize, TYSize> GetMaximumLabelSize( AxesIntervals<TX, TXSize, TY, TYSize> visible )
		{
			return new Tuple<TXSize, TYSize>( MaximumLabelSize, default( TYSize ) );
		}

		protected override IEnumerable<Tuple<TX, TY>> GetPositions( AxesIntervals<TX, TXSize, TY, TYSize> intervals, Size panelSize )
		{
			// When not enough pixels in between labels, do not show any labels.
			double pixelsBetween = SizeToPixels( MaximumLabelSize, intervals, panelSize );
			if ( pixelsBetween < MinimumPixelsBetweenLabels )
			{
				MinimumPixelsExceeded = true;
				return new Tuple<TX, TY>[] { };
			}

			MinimumPixelsExceeded = false;
			return GetXValues( intervals ).Select( x => new Tuple<TX, TY>( x, FixedY ) );
		}

		protected static double SizeToPixels( TXSize size, AxesIntervals<TX, TXSize, TY, TYSize> intervals, Size panelSize )
		{
			double intervalSize = Interval<TX, TXSize>.ConvertSizeToDouble( intervals.IntervalX.Size );
			double requestedSize = Interval<TX, TXSize>.ConvertSizeToDouble( size );
			return panelSize.Width * ( requestedSize / intervalSize );
		}

		protected static TXSize PixelsToSize( double pixels, AxesIntervals<TX, TXSize, TY, TYSize> intervals, Size panelSize )
		{
			double intervalSize = Interval<TX, TXSize>.ConvertSizeToDouble( intervals.IntervalX.Size );
			double size = ( pixels / panelSize.Width ) * intervalSize;
			return Interval<TX, TXSize>.ConvertDoubleToSize( size );
		}

		protected abstract IEnumerable<TX> GetXValues( AxesIntervals<TX, TXSize, TY, TYSize> intervals );
	}
}
