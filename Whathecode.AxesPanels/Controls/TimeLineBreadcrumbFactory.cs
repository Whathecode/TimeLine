using System.Windows;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLineBreadcrumbFactory : AbstractTimeLinePushLabelFactory
	{
		protected override FrameworkElement CreateLabel()
		{
			return new TimeLineBreadcrumb
			{
				Interval = MaximumLabelSize,
				FactoryName = Name
			};
		}
	}
}
