using System;
using System.Windows;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;


namespace Whathecode.TimeLine
{
	public class TimeLineTick : TimeLineItem
	{
		public enum LabelProperties
		{
			Interval,
			FactoryName
		}


		static readonly Type Type = typeof( TimeLineTick );
		public static readonly DependencyPropertyFactory<LabelProperties> Factory = new DependencyPropertyFactory<LabelProperties>();
		public static readonly DependencyProperty IntervalProperty = Factory[ LabelProperties.Interval ];
		public static readonly DependencyProperty FactoryNameProperty = Factory[ LabelProperties.FactoryName ];


		[DependencyProperty( LabelProperties.Interval )]
		public TimeSpan Interval
		{
			get { return (TimeSpan)Factory.GetValue( this, LabelProperties.Interval ); }
			set { Factory.SetValue( this, LabelProperties.Interval, value ); }
		}

		[DependencyProperty( LabelProperties.FactoryName )]
		public string FactoryName
		{
			get { return (string)Factory.GetValue( this, LabelProperties.FactoryName ); }
			set { Factory.SetValue( this, LabelProperties.FactoryName, value ); }
		}


		static TimeLineTick()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}
	}
}
