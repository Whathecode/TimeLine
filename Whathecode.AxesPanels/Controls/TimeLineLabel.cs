using System.Windows;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLineLabel : TimeLineItem
	{
		static TimeLineLabel()
		{
			var type = typeof( TimeLineLabel );
			DefaultStyleKeyProperty.OverrideMetadata( type, new FrameworkPropertyMetadata( type ) );
		}
	}
}
