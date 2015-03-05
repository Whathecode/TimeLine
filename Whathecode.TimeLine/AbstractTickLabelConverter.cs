using System;
using Whathecode.System.Windows.Data;


namespace Whathecode.TimeLine
{
	public abstract class AbstractTickLabelConverter : AbstractMultiValueConverter<object, string>
	{
		public override string Convert( object[] values )
		{
			return FormatLabel(
				(DateTime)values[ 0 ],
				(TimeSpan)values[ 1 ],
				(string)values[ 2 ],
				(string)values[ 3 ] );
		}

		public override object[] ConvertBack( string value )
		{
			throw new NotSupportedException();
		}

		protected abstract string FormatLabel( DateTime occurance, TimeSpan interval, string factoryName, string dominantFactory );
	}
}
