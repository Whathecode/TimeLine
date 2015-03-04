﻿using System.Windows;


namespace Whathecode.AxesPanels.Controls
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