﻿using System.Windows;


namespace Whathecode.TimeLine
{
	public class TimeLineHeader : TimeLineTick
	{
		static TimeLineHeader()
		{
			var type = typeof( TimeLineHeader );
			DefaultStyleKeyProperty.OverrideMetadata( type, new FrameworkPropertyMetadata( type ) );
		}
	}
}
