using System;
using Whathecode.System.Extensions;


namespace Whathecode.AxesPanels.Controls
{
	public class TickLabelConverter : AbstractTickLabelConverter
	{
		protected override string FormatLabel( DateTime occurance, TimeSpan interval, string dominantFactory )
		{
			switch ( dominantFactory )
			{
				case "Years":
					return occurance.ToString( "yyyy" );
				case "Months":
					return occurance.ToString( "MMMM" );
				case "Days":
					return occurance.ToString( @"d\t\h" );
				case "Weeks":
					return interval.TotalDays > 7 && occurance.Round( DayOfWeek.Monday ) != occurance
						? ""
						: occurance.ToString( @"d\t\h" );
				default:
					return occurance.ToString( "HH:mm" );
			}
		}
	}
}
