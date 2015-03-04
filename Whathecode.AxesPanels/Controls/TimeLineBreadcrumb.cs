using System.Windows;


namespace Whathecode.AxesPanels.Controls
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
