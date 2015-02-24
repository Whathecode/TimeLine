using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;


namespace Whathecode.AxesPanels.Controls
{
	[TemplatePart( Name = LabelsPart, Type = typeof( Panel ) )]
	public class TimeLine : TimeControl
	{
		public enum Properties
		{
			CurrentTime
		}


		const string LabelsPart = "PART_Labels";
		readonly ObservableCollection<TimeLineItem> _labels = new ObservableCollection<TimeLineItem>();

		static readonly Type Type = typeof( TimeLine );
		public static readonly DependencyPropertyFactory<Properties> Factory = new DependencyPropertyFactory<Properties>();
		public static readonly DependencyProperty CurrentTimeProperty = Factory[ Properties.CurrentTime ];


		[DependencyProperty( Properties.CurrentTime )]
		public DateTime CurrentTime
		{
			get { return (DateTime)Factory.GetValue( this, Properties.CurrentTime ); }
			set { Factory.SetValue( this, Properties.CurrentTime, value ); }
		}


		static TimeLine()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}

		public TimeLine()
		{
			// Add fixed labels.
			_labels.Add( new TimeIndicator() );
		}


		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			// Add labels to the labels panel.
			var timePanel = GetTemplateChild( LabelsPart ) as Panel;
			if ( timePanel != null )
			{
				foreach ( var label in _labels )
				{
					timePanel.Children.Add( label );
				}
			}
		}
	}
}
