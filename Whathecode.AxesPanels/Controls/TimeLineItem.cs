using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;


namespace Whathecode.AxesPanels.Controls
{
	public class TimeLineItem : Control
	{
		public enum Properties
		{
			Occurance,
			Fill,
			Stroke,
			StrokeThickness
		}


		static readonly Type Type = typeof( TimeLineItem );
		public static readonly DependencyPropertyFactory<Properties> PropertyFactory = new DependencyPropertyFactory<Properties>();
		public static readonly DependencyProperty OccuranceProperty = PropertyFactory[ Properties.Occurance ];
		public static readonly DependencyProperty FillProperty = PropertyFactory[ Properties.Fill ];
		public static readonly DependencyProperty StrokeProperty = PropertyFactory[ Properties.Stroke ];
		public static readonly DependencyProperty StrokeThicknessProperty = PropertyFactory[ Properties.StrokeThickness ];

		[DependencyProperty( Properties.Occurance )]
		public DateTime Occurance
		{
			get { return (DateTime)PropertyFactory.GetValue( this, Properties.Occurance ); }
			set { PropertyFactory.SetValue( this, Properties.Occurance, value ); }
		}

		[DependencyProperty( Properties.Fill )]
		public Brush Fill
		{
			get { return (Brush)PropertyFactory.GetValue( this, Properties.Fill ); }
			set {  PropertyFactory.SetValue( this, Properties.Fill, value ); }
		}

		[DependencyProperty( Properties.Stroke )]
		public Brush Stroke
		{
			get { return (Brush)PropertyFactory.GetValue( this, Properties.Stroke ); }
			set {  PropertyFactory.SetValue( this, Properties.Stroke, value ); }
		}

		[DependencyProperty( Properties.StrokeThickness )]
		public double StrokeThickness
		{
			get { return (double)PropertyFactory.GetValue( this, Properties.StrokeThickness ); }
			set {  PropertyFactory.SetValue( this, Properties.StrokeThickness, value ); }
		}


		static TimeLineItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}
	}
}
