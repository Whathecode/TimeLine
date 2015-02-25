using System;
using System.Collections.Generic;
using System.Linq;


namespace Whathecode.AxesPanels
{
	public abstract class AbstractXAxesFactory<TX, TXSize, TY, TYSize> : AbstractAxesLabelFactory<TX, TXSize, TY, TYSize>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		public TX Anchor { get; set; }
		public TY FixedY { get; set; }
		public TXSize StepSize { get; set; }


		protected override Tuple<TXSize, TYSize> GetMaximumLabelSize( AxesIntervals<TX, TXSize, TY, TYSize> visible )
		{
			return new Tuple<TXSize, TYSize>( StepSize, default( TYSize ) );
		}

		protected override IEnumerable<Tuple<TX, TY>> GetPositions( AxesIntervals<TX, TXSize, TY, TYSize> intervals )
		{
			return intervals.IntervalX.GetValues( StepSize, Anchor ).Select( x => new Tuple<TX, TY>( x, FixedY ) );
		}
	}
}
