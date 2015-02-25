using System.Windows;


namespace Whathecode.AxesPanels.Controls
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
