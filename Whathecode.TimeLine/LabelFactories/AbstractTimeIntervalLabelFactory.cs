using System;
using System.Collections.Generic;
using Whathecode.System;
using Whathecode.System.Extensions;
using Whathecode.System.Windows.Controls;


namespace Whathecode.TimeLine.LabelFactories
{
	public abstract class AbstractTimeIntervalLabelFactory : AbstractRegularXLabelFactory<DateTime, TimeSpan, double, double>
	{
		/// <summary>
		///   Name used to identify this factory.
		/// </summary>
		public string Name { get; set; }

		DateTimePart? _timeStepSize;
		/// <summary>
		///   Specify a step size, which can also represent an irregular interval, like years and months.
		///   This setting overrides <see cref="AbstractRegularXLabelFactory{TX,TXSize,TY,TYSize}.StepSize" />
		///   and <see cref="AbstractRegularXLabelFactory{TX,TXSize,TY,TYSize}.Anchor" />.
		/// </summary>
		public DateTimePart? TimeStepSize
		{
			get { return _timeStepSize; }
			set
			{
				_timeStepSize = value;
				switch ( _timeStepSize )
				{
					case DateTimePart.Millisecond:
						MaximumLabelSize = TimeSpan.FromMilliseconds( 1 );
						break;
					case DateTimePart.Second:
						MaximumLabelSize = TimeSpan.FromSeconds( 1 );
						break;
					case DateTimePart.Minute:
						MaximumLabelSize = TimeSpan.FromMinutes( 1 );
						break;
					case DateTimePart.Hour:
						MaximumLabelSize = TimeSpan.FromHours( 1 );
						break;
					case DateTimePart.Day:
						MaximumLabelSize = TimeSpan.FromDays( 1 );
						break;
					case DateTimePart.Month:
						MaximumLabelSize = TimeSpan.FromDays( 31 );
						break;
					case DateTimePart.Year:
						MaximumLabelSize = TimeSpan.FromDays( 366 );
						break;
					default:
						throw new NotSupportedException();
				}
			}
		}


		protected override IEnumerable<DateTime> GetXValues( AxesIntervals<DateTime, TimeSpan, double, double> intervals )
		{
			if ( TimeStepSize == null )
			{
				foreach ( var x in base.GetXValues( intervals ) )
				{
					yield return x;
				}
			}
			else
			{
				var xInterval = intervals.IntervalX;
				DateTime current = xInterval.Start.Round( TimeStepSize.Value );
				if ( xInterval.Start == current ) // Only include start when it is within the requested interval.
				{
					yield return current;
				}

				DateTime last = xInterval.End.Round( TimeStepSize.Value );
				while ( current != last )
				{
					switch ( TimeStepSize )
					{
						case DateTimePart.Millisecond:
							current = current.AddMilliseconds( 1 );
							break;
						case DateTimePart.Second:
							current = current.AddSeconds( 1 );
							break;
						case DateTimePart.Minute:
							current = current.AddMinutes( 1 );
							break;
						case DateTimePart.Hour:
							current = current.AddHours( 1 );
							break;
						case DateTimePart.Day:
							current = current.AddDays( 1 );
							break;
						case DateTimePart.Month:
							current = current.AddMonths( 1 );
							break;
						case DateTimePart.Year:
							current = current.AddYears( 1 );
							break;
						default:
							throw new NotSupportedException();
					}
					yield return current;
				}
			}
		}
	}
}
