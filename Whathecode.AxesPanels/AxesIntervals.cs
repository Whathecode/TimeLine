using System;
using Whathecode.System.Arithmetic.Range;


namespace Whathecode.AxesPanels
{
	public class AxesIntervals<TX, TXSize, TY, TYSize>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		public readonly Interval<TX, TXSize> IntervalX;
		public readonly Interval<TY, TYSize> IntervalY;


		public AxesIntervals( Interval<TX, TXSize> intervalX, Interval<TY, TYSize> intervalY )
		{
			IntervalX = intervalX;
			IntervalY = intervalY;
		}
	}
}
