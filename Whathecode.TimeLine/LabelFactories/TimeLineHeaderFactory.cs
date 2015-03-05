using System.Windows;


namespace Whathecode.TimeLine.LabelFactories
{
	public class AbstractTimeLineHeaderFactory : AbstractTimeLinePushLabelFactory
	{
		protected override FrameworkElement CreateLabel()
		{
			return new TimeLineHeader
			{
				Interval = MaximumLabelSize,
				FactoryName = Name
			};
		}
	}
}
