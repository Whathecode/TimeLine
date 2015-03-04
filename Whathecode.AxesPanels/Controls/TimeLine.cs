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
			DominantHeaderFactory
		}


		static readonly Type Type = typeof( TimeLine );
		public static readonly DependencyPropertyFactory<TimeLineProperties> Factory = new DependencyPropertyFactory<TimeLineProperties>();
		public static readonly DependencyProperty LabelFactoriesProperty = Factory[ TimeLineProperties.LabelFactories ];
		public static readonly DependencyProperty DominantTickFactoryProperty = Factory[ TimeLineProperties.DominantTickFactory ];
		public static readonly DependencyProperty DominantHeaderFactoryProperty = Factory[ TimeLineProperties.DominantHeaderFactory ];


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

					// Get the smallest interval which is too large to fit the screen vertically.
					var dontFit = LabelFactories
						.OfType<TimeLineHeaderFactory>()
						.Where( f => ( (double)f.MaximumLabelSize.Ticks / VisibleInterval.Size.Ticks ) * ActualWidth > ActualHeight )
						.ToList();
					DominantHeaderFactory = dontFit.Count == 0 ? "" : dontFit.MinBy( f => f.MaximumLabelSize ).Name;
				}
			};
		}
	}
}
