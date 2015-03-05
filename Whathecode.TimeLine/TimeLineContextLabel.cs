using System.Windows;


namespace Whathecode.TimeLine
{
	public class TimeLineContextLabel : TimeLineTick
	{
		static TimeLineContextLabel()
		{
			var type = typeof( TimeLineContextLabel );
			DefaultStyleKeyProperty.OverrideMetadata( type, new FrameworkPropertyMetadata( type ) );
		}
	}
}
