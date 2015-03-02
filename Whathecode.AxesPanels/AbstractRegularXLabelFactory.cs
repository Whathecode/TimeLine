using System;
using System.Collections.Generic;


namespace Whathecode.AxesPanels
{
	public abstract class AbstractRegularXLabelFactory<TX, TXSize, TY, TYSize> : AbstractXAxisLabelFactory<TX, TXSize, TY, TYSize>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		public TX Anchor { get; set; }

		TXSize _stepSize;
		public TXSize StepSize
		{
			get { return _stepSize; }
			set
			{
				_stepSize = value;
				MaximumLabelSize = _stepSize;
			}
		}


		protected override IEnumerable<TX> GetXValues( AxesIntervals<TX, TXSize, TY, TYSize> intervals )
		{
			return intervals.IntervalX.GetValues( StepSize, Anchor );
		}
	}
}
