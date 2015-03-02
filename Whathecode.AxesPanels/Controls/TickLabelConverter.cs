using System;
using System.Collections.Generic;
using Whathecode.System.Extensions;


namespace Whathecode.AxesPanels.Controls
{
	public class TickLabelConverter : AbstractTickLabelConverter
	{
		enum DominantFactory
		{
			Quarters,
			Hours,
			DayQuarters,
			Days,
			Weeks,
			Months,
			Years
		}

		static readonly Dictionary<string, DominantFactory> FactoryMapping = new Dictionary<string, DominantFactory>
		{
			{ "Quarters", DominantFactory.Quarters },
			{ "Hours", DominantFactory.Hours },
			{ "DayQuarters", DominantFactory.DayQuarters },
			{ "Days", DominantFactory.Days },
			{ "Weeks", DominantFactory.Weeks },
			{ "Months", DominantFactory.Months },
			{ "Years", DominantFactory.Years },
		};

		protected override string FormatLabel( DateTime occurance, TimeSpan interval, string factoryName, string dominantFactory )
		{
			if ( dominantFactory == "" )
			{
				return "";
			}
			DominantFactory factory = FactoryMapping[ dominantFactory ];

			// Secondary context labels.
			if ( factoryName == "DayContext" )
			{
				if ( factory < DominantFactory.Days )
				{
					return occurance.ToString( @"dddd d\t\h" );
				}
				if ( factory < DominantFactory.Weeks )
				{
					return occurance.ToString( @"ddd" );
				}
				return "";
			}
			if ( factoryName == "MonthContext" )
			{
				if ( factory >= DominantFactory.Weeks && factory < DominantFactory.Months )
				{
					return occurance.ToString( "MMMM" );
				}
				return "";
			}
			if ( factoryName == "YearContext" )
			{
				if ( factory >= DominantFactory.Months && factory < DominantFactory.Years )
				{
					return occurance.ToString( "yyyy" );
				}
				return "";
			}

			// Primary tick labels.
			switch ( dominantFactory )
			{
				case "Years":
					return occurance.ToString( "yyyy" );
				case "Months":
					return occurance.ToString( "MMM" );
				case "Days":
					return occurance.ToString( @"d\t\h" );
				case "Weeks":
					return factoryName == "Months" && occurance.Round( DayOfWeek.Monday ) != occurance
						? ""
						: occurance.ToString( @"d\t\h" );
				default:
					return occurance.ToString( "HH:mm" );
			}
		}
	}
}
