using System;
using System.Linq;
using System.Windows;
using Whathecode.System;
using Whathecode.System.Linq;
using Whathecode.System.Windows.Controls;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;
using Whathecode.TimeLine.LabelFactories;


namespace Whathecode.TimeLine
{
	public class TimeLine : TimeControl
	{
		public enum TimeLineProperties
		{
			LabelFactories,
			DominantTickFactory,
			DominantHeaderFactory,
			DominantBreadcrumbFactory
		}


		static readonly Type Type = typeof( TimeLine );
		public static readonly DependencyPropertyFactory<TimeLineProperties> Factory = new DependencyPropertyFactory<TimeLineProperties>();
		public static readonly DependencyProperty LabelFactoriesProperty = Factory[ TimeLineProperties.LabelFactories ];
		public static readonly DependencyProperty DominantTickFactoryProperty = Factory[ TimeLineProperties.DominantTickFactory ];
		public static readonly DependencyProperty DominantHeaderFactoryProperty = Factory[ TimeLineProperties.DominantHeaderFactory ];
		public static readonly DependencyProperty DominantBreadcrumbFactoryProperty = Factory[ TimeLineProperties.DominantBreadcrumbFactory ];


		[DependencyProperty( TimeLineProperties.LabelFactories )]
		public TimeLineLabelFactories LabelFactories
		{
			get { return (TimeLineLabelFactories)Factory.GetValue( this, TimeLineProperties.LabelFactories ); }
			set { Factory.SetValue( this, TimeLineProperties.LabelFactories, value ); }
		}

		[DependencyProperty( TimeLineProperties.DominantTickFactory, DefaultValue = "" )]
		public string DominantTickFactory
		{
			get { return (string)Factory.GetValue( this, TimeLineProperties.DominantTickFactory ); }
			private set { Factory.SetValue( this,TimeLineProperties.DominantTickFactory, value ); }
		}

		[DependencyProperty( TimeLineProperties.DominantHeaderFactory, DefaultValue = "" )]
		public string DominantHeaderFactory
		{
			get { return (string)Factory.GetValue( this, TimeLineProperties.DominantHeaderFactory ); }
			private set { Factory.SetValue( this,TimeLineProperties.DominantHeaderFactory, value ); }
		}

		[DependencyProperty( TimeLineProperties.DominantBreadcrumbFactory, DefaultValue = "" )]
		public string DominantBreadcrumbFactory
		{
			get { return (string)Factory.GetValue( this, TimeLineProperties.DominantBreadcrumbFactory ); }
			private set { Factory.SetValue( this,TimeLineProperties.DominantBreadcrumbFactory, value ); }
		}


		static TimeLine()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
			VisibleIntervalProperty.AddOwner( typeof( TimeLine ) );
		}

		public TimeLine()
		{
			LabelFactories = new TimeLineLabelFactories
			{
				// Ticks, from quarters to years.
				new TimeLineTickFactory { Name="Quarters", StepSize = TimeSpan.FromMinutes( 15 ), MinimumPixelsBetweenLabels = 60, OverrideGroup = "Ticks" },
				new TimeLineTickFactory { Name="Hours", TimeStepSize = DateTimePart.Hour, MinimumPixelsBetweenLabels = 60, OverrideGroup = "Ticks" },
				new TimeLineTickFactory { Name="DayQuarters", StepSize = TimeSpan.FromHours( 6 ), MinimumPixelsBetweenLabels = 60, OverrideGroup = "Ticks" },
				new TimeLineTickFactory { Name="Days", TimeStepSize = DateTimePart.Day, MinimumPixelsBetweenLabels = 50, OverrideGroup = "Ticks" },
				new TimeLineTickFactory { Name="Weeks", StepSize = TimeSpan.FromDays( 7 ), MinimumPixelsBetweenLabels = 60, OverrideGroup = "Ticks" },
				new TimeLineTickFactory { Name="Months", TimeStepSize = DateTimePart.Month, MinimumPixelsBetweenLabels = 50, OverrideGroup = "Ticks" },
				new TimeLineTickFactory { Name="Years", TimeStepSize = DateTimePart.Year, MinimumPixelsBetweenLabels = 60, OverrideGroup = "Ticks" },
				// Additional context labels at bottom.
				new TimeLineContextLabelFactory { Name = "DayContext", TimeStepSize = DateTimePart.Day, MinimumPixelsBetweenLabels = 50, FixedY = 100 },
				new TimeLineContextLabelFactory { Name = "MonthContext", TimeStepSize = DateTimePart.Month, MinimumPixelsBetweenLabels = 100, FixedY = 100 },
				new TimeLineContextLabelFactory { Name = "YearContext", TimeStepSize = DateTimePart.Year, MinimumPixelsBetweenLabels = 100, FixedY = 100 },
				// Header labels.
				new TimeLineHeaderFactory { Name = "YearHeader", TimeStepSize = DateTimePart.Year, MinimumPixelsBetweenLabels = 200 },
				new TimeLineHeaderFactory { Name = "MonthHeader", TimeStepSize = DateTimePart.Month, MinimumPixelsBetweenLabels = 200 },
				new TimeLineHeaderFactory { Name = "WeekHeader", StepSize = TimeSpan.FromDays( 7 ), MinimumPixelsBetweenLabels = 200 },
				new TimeLineHeaderFactory { Name = "DayHeader", TimeStepSize = DateTimePart.Day, MinimumPixelsBetweenLabels = 200 },
				new TimeLineHeaderFactory { Name = "DayQuarterHeader", StepSize = TimeSpan.FromHours( 6 ), MinimumPixelsBetweenLabels = 200 },
				new TimeLineHeaderFactory { Name = "HourHeader", TimeStepSize = DateTimePart.Hour, MinimumPixelsBetweenLabels = 200 },
				new TimeLineHeaderFactory { Name = "QuarterHeader", StepSize = TimeSpan.FromMinutes( 15 ), MinimumPixelsBetweenLabels = 200 },
				// Breadcrumb labels underneath headers.
				new TimeLineBreadcrumbFactory { Name = "YearBreadcrumbs", TimeStepSize = DateTimePart.Year, MinimumPixelsBetweenLabels = 200 },
				new TimeLineBreadcrumbFactory { Name = "MonthBreadcrumbs", TimeStepSize = DateTimePart.Month, MinimumPixelsBetweenLabels = 200 },
				new TimeLineBreadcrumbFactory { Name = "DayBreadcrumbs", TimeStepSize = DateTimePart.Day, MinimumPixelsBetweenLabels = 200 },
				// Fixed elements.
				new TimeLineLabelCollection { new TimeIndicator() }
			};
		}


		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			TimePanel panel = (TimePanel)GetTemplateChild( "PART_Labels" );
			if ( panel == null )
			{
				return;
			}

			// Identify the dominant labels before they are displayed.
			panel.LayoutUpdated += ( sender, args ) =>
			{
				if ( LabelFactories != null )
				{
					// Find the factory which has populated the time line with the most tick labels which are still visible.
					var visibleTicks = LabelFactories.OfType<TimeLineTickFactory>().Where( f => !f.MinimumPixelsExceeded ).ToList();
					DominantTickFactory = visibleTicks.Count == 0 ? "" : visibleTicks.MinBy( f => f.MaximumLabelSize ).Name;

					// Get dominant header and breadcrumb factories.
					var dontFit = LabelFactories
						.OfType<TimeLineHeaderFactory>()  // Get the smallest interval which is too large to fit the screen vertically.
						.Where( f => ( (double)f.MaximumLabelSize.Ticks / VisibleInterval.Size.Ticks ) * ActualWidth > ActualHeight )
						.ToList();
					TimeLineHeaderFactory currentHeaderFactory = dontFit.Count == 0 ? null : dontFit.MinBy( f => f.MaximumLabelSize );
					if ( currentHeaderFactory != null )
					{
						DominantHeaderFactory = currentHeaderFactory.Name;

						// The dominant breadcrumb factory is the first factory bigger than the dominant header factory.
						TimeLineBreadcrumbFactory breadcrumbFactory = LabelFactories
							.OfType<TimeLineBreadcrumbFactory>()
							.OrderBy( f => f.MaximumLabelSize )
							.FirstOrDefault( f => f.MaximumLabelSize > currentHeaderFactory.MaximumLabelSize );
						DominantBreadcrumbFactory = breadcrumbFactory == null ? "" : breadcrumbFactory.Name;
					}
					else
					{
						DominantHeaderFactory = "";
					}
				}
			};
		}
	}
}
