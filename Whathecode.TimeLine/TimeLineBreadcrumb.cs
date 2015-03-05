using System.Windows;


namespace Whathecode.TimeLine
{
	public class TimeLineBreadcrumb : TimeLineTick
	{
		static TimeLineBreadcrumb()
		{
			var type = typeof( TimeLineBreadcrumb );
			DefaultStyleKeyProperty.OverrideMetadata( type, new FrameworkPropertyMetadata( type ) );
		}
	}
}
