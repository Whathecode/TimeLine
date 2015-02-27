using System;
using System.Windows;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLine : TimeControl
	{
		public enum TimeLineProperties
		{
			LabelFactories
		}


		static readonly Type Type = typeof( TimeLine );
		public static readonly DependencyPropertyFactory<TimeLineProperties> Factory = new DependencyPropertyFactory<TimeLineProperties>();
		public static readonly DependencyProperty LabelFactoriesProperty = Factory[ TimeLineProperties.LabelFactories ];


		[DependencyProperty( TimeLineProperties.LabelFactories )]
		public AxesLabelFactories<DateTime, TimeSpan, double, double> LabelFactories
		{
			get { return (AxesLabelFactories<DateTime, TimeSpan, double, double>)Factory.GetValue( this, TimeLineProperties.LabelFactories ); }
			set { Factory.SetValue( this, TimeLineProperties.LabelFactories, value ); }
		}


		static TimeLine()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}
	}
}
