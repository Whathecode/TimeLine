using System;
using System.Linq;
using System.Windows;
using Whathecode.System.Linq;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;


namespace Whathecode.AxesPanels.Controls
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

		[DependencyProperty( TimeLineProperties.DominantTickFactory )]
		public string DominantTickFactory
		{
			get { return (string)Factory.GetValue( this, TimeLineProperties.DominantTickFactory ); }
			private set { Factory.SetValue( this,TimeLineProperties.DominantTickFactory, value ); }
		}

		[DependencyProperty( TimeLineProperties.DominantHeaderFactory )]
		public string DominantHeaderFactory
		{
			get { return (string)Factory.GetValue( this, TimeLineProperties.DominantHeaderFactory ); }
			private set { Factory.SetValue( this,TimeLineProperties.DominantHeaderFactory, value ); }
		}

		[DependencyProperty( TimeLineProperties.DominantBreadcrumbFactory )]
		public string DominantBreadcrumbFactory
		{
			get { return (string)Factory.GetValue( this, TimeLineProperties.DominantBreadcrumbFactory ); }
			private set { Factory.SetValue( this,TimeLineProperties.DominantBreadcrumbFactory, value ); }
		}


		static TimeLine()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
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
						.OfType<AbstractTimeLineHeaderFactory>()  // Get the smallest interval which is too large to fit the screen vertically.
						.Where( f => ( (double)f.MaximumLabelSize.Ticks / VisibleInterval.Size.Ticks ) * ActualWidth > ActualHeight )
						.ToList();
					AbstractTimeLineHeaderFactory currentHeaderFactory = dontFit.Count == 0 ? null : dontFit.MinBy( f => f.MaximumLabelSize );
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
