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
			LabelFactories,
			DominantLabelFactory
		}


		static readonly Type Type = typeof( TimeLine );
		public static readonly DependencyPropertyFactory<TimeLineProperties> Factory = new DependencyPropertyFactory<TimeLineProperties>();
		public static readonly DependencyProperty LabelFactoriesProperty = Factory[ TimeLineProperties.LabelFactories ];
		public static readonly DependencyProperty DominantLabelFactoryProperty = Factory[ TimeLineProperties.DominantLabelFactory ];


		[DependencyProperty( TimeLineProperties.LabelFactories )]
		public TimeLineLabelFactories LabelFactories
		{
			get { return (TimeLineLabelFactories)Factory.GetValue( this, TimeLineProperties.LabelFactories ); }
			set { Factory.SetValue( this, TimeLineProperties.LabelFactories, value ); }
		}

		[DependencyProperty( TimeLineProperties.DominantLabelFactory, AffectsMeasure = true )]
		public string DominantLabelFactory
		{
			get { return (string)Factory.GetValue( this, TimeLineProperties.DominantLabelFactory ); }
			private set { Factory.SetValue( this,TimeLineProperties.DominantLabelFactory, value ); }
		}


		static TimeLine()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			// Identify the dominant labels before they are displayed.
			TimePanel panel = (TimePanel)GetTemplateChild( "PART_Labels" );
			panel.LayoutUpdated += ( sender, args ) =>
			{
				if ( LabelFactories != null )
				{
					DominantLabelFactory = LabelFactories.GetDominantFactory( VisibleInterval, ActualWidth );
				}
			};
		}
	}
}
