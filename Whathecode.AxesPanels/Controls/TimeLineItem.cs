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
		public enum TimeLineItemBinding
		{
			Occurance,
			Fill,
			Stroke,
			StrokeThickness
		}


		static readonly Type Type = typeof( TimeLineItem );
		public static readonly DependencyPropertyFactory<TimeLineItemBinding> PropertyFactory = new DependencyPropertyFactory<TimeLineItemBinding>();
		public static readonly DependencyProperty OccuranceProperty = PropertyFactory[ TimeLineItemBinding.Occurance ];
		public static readonly DependencyProperty FillProperty = PropertyFactory[ TimeLineItemBinding.Fill ];
		public static readonly DependencyProperty StrokeProperty = PropertyFactory[ TimeLineItemBinding.Stroke ];
		public static readonly DependencyProperty StrokeThicknessProperty = PropertyFactory[ TimeLineItemBinding.StrokeThickness ];

		[DependencyProperty( TimeLineItemBinding.Occurance )]
		public DateTime Occurance
		{
			get { return (DateTime)PropertyFactory.GetValue( this, TimeLineItemBinding.Occurance ); }
			set { PropertyFactory.SetValue( this, TimeLineItemBinding.Occurance, value ); }
		}

		[DependencyProperty( TimeLineItemBinding.Fill )]
		public Brush Fill
		{
			get { return (Brush)PropertyFactory.GetValue( this, TimeLineItemBinding.Fill ); }
			set {  PropertyFactory.SetValue( this, TimeLineItemBinding.Fill, value ); }
		}

		[DependencyProperty( TimeLineItemBinding.Stroke )]
		public Brush Stroke
		{
			get { return (Brush)PropertyFactory.GetValue( this, TimeLineItemBinding.Stroke ); }
			set {  PropertyFactory.SetValue( this, TimeLineItemBinding.Stroke, value ); }
		}

		[DependencyProperty( TimeLineItemBinding.StrokeThickness )]
		public double StrokeThickness
		{
			get { return (double)PropertyFactory.GetValue( this, TimeLineItemBinding.StrokeThickness ); }
			set {  PropertyFactory.SetValue( this, TimeLineItemBinding.StrokeThickness, value ); }
		}


		static TimeLineItem()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}
	}
}
