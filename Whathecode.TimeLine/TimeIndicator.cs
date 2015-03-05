using System.Windows;


namespace Whathecode.TimeLine
{
	public class TimeIndicator : TimeLineItem
	{
		static TimeIndicator()
		{
			var type = typeof( TimeIndicator );
			DefaultStyleKeyProperty.OverrideMetadata( type, new FrameworkPropertyMetadata( type ) );
		}
	}
}
