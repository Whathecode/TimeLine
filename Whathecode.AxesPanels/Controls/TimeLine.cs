using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace Whathecode.AxesPanels.Controls
{
	[TemplatePart( Name = LabelsPart, Type = typeof( ItemsControl ) )]
	public class TimeLine : TimeControl
	{
		const string LabelsPart = "PART_Labels";
		static readonly Type Type = typeof( TimeLine );

		readonly ObservableCollection<object> _labels = new ObservableCollection<object>(); 


		static TimeLine()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}


		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();

			// Add labels to the labels panel.
			var labels = GetTemplateChild( LabelsPart ) as Panel;
			if ( labels != null )
			{
				var now = new TimeIndicator();
				now.Occurance = new DateTime( 2015, 2, 23 );
				_labels.Add( now );
				labels.Children.Add( now );
			}
		}
	}
}
