﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Whathecode.AxesPanels.Internal;
using Whathecode.System.Arithmetic.Range;
using Whathecode.System.Windows.DependencyPropertyFactory;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes;
using Whathecode.System.Windows.DependencyPropertyFactory.Attributes.Coercion;


namespace Whathecode.AxesPanels
{
	/// <summary>
	///   Defines an area in which you can position child elements at a certain position along two axes.
	/// </summary>
	/// <typeparam name = "TX">The type which determines the X-axis.</typeparam>
	/// <typeparam name = "TXSize">The type used to specify distances in between two values of <see cref="TX" />.</typeparam>
	/// <typeparam name = "TY">The type which determines the Y-axis.</typeparam>
	/// <typeparam name = "TYSize">The type used to specify distances in between two values of <see cref="TY" />.</typeparam>
	abstract public class AxesPanel<TX, TXSize, TY, TYSize> : Panel
		where TX : IComparable<TX> where TY : IComparable<TY>
	{
		static readonly Type Type = typeof( AxesPanel<TX, TXSize, TY, TYSize> );
		public static readonly DependencyPropertyFactory<AxesPanelBinding> PropertyFactory 
			= new DependencyPropertyFactory<AxesPanelBinding>( typeof( AxesPanel<TX, TXSize, TY, TYSize> ) );

		// ReSharper disable StaticMemberInGenericType
		public static readonly DependencyProperty MaximaXProperty = PropertyFactory[ AxesPanelBinding.MaximaX ];
		public static readonly DependencyProperty MaximaYProperty = PropertyFactory[ AxesPanelBinding.MaximaY ];
		public static readonly DependencyProperty MinimumSizeXProperty = PropertyFactory[ AxesPanelBinding.MinimumSizeX ];
		public static readonly DependencyProperty MinimumSizeYProperty = PropertyFactory[ AxesPanelBinding.MinimumSizeY ];
		public static readonly DependencyProperty VisibleIntervalXProperty = PropertyFactory[ AxesPanelBinding.VisibleIntervalX ];
		public static readonly DependencyProperty VisibleIntervalYProperty = PropertyFactory[ AxesPanelBinding.VisibleIntervalY ];
		// ReSharper restore StaticMemberInGenericType


			/// <summary>
		///   The maximum range within which all values on the X-axis must lie.
		/// </summary>
		[DependencyProperty( AxesPanelBinding.MaximaX, DefaultValueProvider = typeof( EmptyIntervalProvider ) )]
		public Interval<TX, TXSize> MaximaX
		{
			get { return (Interval<TX, TXSize>)PropertyFactory.GetValue( this, AxesPanelBinding.MaximaX ); }
			set { PropertyFactory.SetValue( this, AxesPanelBinding.MaximaX, value ); }
		}

		/// <summary>
		///   The maximum range within which all values on the Y-axis must lie.
		/// </summary>
		[DependencyProperty( AxesPanelBinding.MaximaY, DefaultValueProvider = typeof( EmptyIntervalProvider ) )]
		public Interval<TY, TYSize> MaximaY
		{
			get { return (Interval<TY, TYSize>)PropertyFactory.GetValue( this, AxesPanelBinding.MaximaY ); }
			set { PropertyFactory.SetValue( this, AxesPanelBinding.MaximaY, value ); }
		}

		/// <summary>
		///   The minimum size of <see cref="VisibleIntervalX" />.
		/// </summary>
		[DependencyProperty( AxesPanelBinding.MinimumSizeX )]
		public TXSize MinimumSizeX
		{
			get { return (TXSize)PropertyFactory.GetValue( this, AxesPanelBinding.MinimumSizeX ); }
			set { PropertyFactory.SetValue( this, AxesPanelBinding.MinimumSizeX, value ); }
		}

		/// <summary>
		///   The minimum size of <see cref="VisibleIntervalY" />.
		/// </summary>
		[DependencyProperty( AxesPanelBinding.MinimumSizeY )]
		public TYSize MinimumSizeY
		{
			get { return (TYSize)PropertyFactory.GetValue( this, AxesPanelBinding.MinimumSizeY ); }
			set { PropertyFactory.SetValue( this, AxesPanelBinding.MinimumSizeY, value ); }
		}

		/// <summary>
		///   The visible interval along the X-axis.
		/// </summary>
		[DependencyProperty( AxesPanelBinding.VisibleIntervalX, DefaultValueProvider = typeof( EmptyIntervalProvider ), AffectsMeasure = true )]
		[CoercionHandler( typeof( VisibleIntervalCoercion ), Axis.X )]
		public Interval<TX, TXSize> VisibleIntervalX
		{
			get { return (Interval<TX, TXSize>)PropertyFactory.GetValue( this, AxesPanelBinding.VisibleIntervalX ); }
			set { PropertyFactory.SetValue( this, AxesPanelBinding.VisibleIntervalX, value ); }
		}

		/// <summary>
		///   The visible interval along the Y-axis.
		/// </summary>
		[DependencyProperty( AxesPanelBinding.VisibleIntervalY, DefaultValueProvider = typeof( EmptyIntervalProvider ), AffectsMeasure = true )]
		[CoercionHandler( typeof( VisibleIntervalCoercion ), Axis.Y )]
		public Interval<TY, TYSize> VisibleIntervalY
		{
			get { return (Interval<TY, TYSize>)PropertyFactory.GetValue( this, AxesPanelBinding.VisibleIntervalY ); }
			set { PropertyFactory.SetValue( this, AxesPanelBinding.VisibleIntervalY, value ); }
		}


		#region Attached properties

		/// <summary>
		///   Identifies the X property which indicates where the element should be positioned on the X-axis.
		/// </summary>
		public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached( @"X", typeof( TX ), Type );
		public static TX GetX( FrameworkElement element )
		{
			return (TX)element.GetValue( XProperty );
		}
		public static void SetX( FrameworkElement element, TX value )
		{
			element.SetValue( XProperty, value );
		}

		/// <summary>
		///   Identifies the Y property which indicates where the element should be positioned on the Y-axis.
		/// </summary>
		public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached( @"Y", typeof( TY ), Type );
		public static TY GetY( FrameworkElement element )
		{
			return (TY)element.GetValue( YProperty );
		}
		public static void SetY( FrameworkElement element, TY value )
		{
			element.SetValue( YProperty, value );
		}

		/// <summary>
		///   Identifies the AlignmentX property which indicates how the element should be aligned along the X-axis.
		/// </summary>
		// ReSharper disable once StaticMemberInGenericType
		public static readonly DependencyProperty AlignmentXProperty = DependencyProperty.RegisterAttached(
			@"AlignmentX", typeof( AxisAlignment ), Type,
			new FrameworkPropertyMetadata( AxisAlignment.AfterValue ) );
		public static AxisAlignment GetAlignmentX( FrameworkElement element )
		{
			return (AxisAlignment)element.GetValue( AlignmentXProperty );
		}
		public static void SetAlignmentX( FrameworkElement element, AxisAlignment value )
		{
			element.SetValue( AlignmentXProperty, value );
		}

		/// <summary>
		///   Identifies the AlignmentY property which indicates how the element should be aligned along the Y-axis.
		/// </summary>
		// ReSharper disable once StaticMemberInGenericType
		public static readonly DependencyProperty AlignmentYProperty = DependencyProperty.RegisterAttached(
			@"AlignmentY", typeof( AxisAlignment ), Type,
			new FrameworkPropertyMetadata( AxisAlignment.AfterValue ) );
		public static AxisAlignment GetAlignmentY( FrameworkElement element )
		{
			return (AxisAlignment)element.GetValue( AlignmentYProperty );
		}
		public static void SetAlignmentY( FrameworkElement element, AxisAlignment value )
		{
			element.SetValue( AlignmentYProperty, value );
		}

		/// <summary>
		///   Identifies the SizeX property which indicates the desired size of the element along the X-axis.
		/// </summary>
		// ReSharper disable once StaticMemberInGenericType
		public static readonly DependencyProperty SizeXProperty = DependencyProperty.RegisterAttached(
			@"SizeX", typeof( TXSize ), Type,
			new FrameworkPropertyMetadata( default( TXSize ), FrameworkPropertyMetadataOptions.AffectsMeasure ) );
		public static TXSize GetSizeX( FrameworkElement element )
		{
			return (TXSize)element.GetValue( SizeXProperty );
		}
		public static void SetSizeX( FrameworkElement element, TXSize value )
		{
			element.SetValue( SizeXProperty, value );
		}

		/// <summary>
		///   Identifies the SizeY property which indicates the desired size of the element along the Y-axis.
		/// </summary>
		// ReSharper disable once StaticMemberInGenericType
		public static readonly DependencyProperty SizeYProperty = DependencyProperty.RegisterAttached(
			@"SizeY", typeof( TYSize ), Type,
			new FrameworkPropertyMetadata( default( TYSize ), FrameworkPropertyMetadataOptions.AffectsMeasure ) );
		public static TYSize GetSizeY( FrameworkElement element )
		{
			return (TYSize)element.GetValue( SizeYProperty );
		}
		public static void SetSizeY( FrameworkElement element, TYSize value )
		{
			element.SetValue( SizeYProperty, value );
		}

		/// <summary>
		///   Identifies the IntervalX property which indicates the desired size and position of the element along the X-axis.
		/// </summary>
		// ReSharper disable once StaticMemberInGenericType
		public static readonly DependencyProperty IntervalXProperty = DependencyProperty.RegisterAttached(
			@"IntervalX", typeof( Interval<TX, TXSize> ), Type,
			new FrameworkPropertyMetadata( OnIntervalXChanged ) );
		static void OnIntervalXChanged( DependencyObject element, DependencyPropertyChangedEventArgs args )
		{
			var interval = (Interval<TX, TXSize>)args.NewValue;
			element.SetValue( XProperty, interval.Start );
			element.SetValue( SizeXProperty, interval.Size );
		}
		public static Interval<TX, TXSize> GetIntervalX( FrameworkElement element )
		{
			return (Interval<TX, TXSize>)element.GetValue( IntervalXProperty );
		}
		public static void SetIntervalX( FrameworkElement element, Interval<TX, TXSize> value )
		{
			element.SetValue( IntervalXProperty, value );
		}

		/// <summary>
		///   Identifies the IntervalY property which indicates the desired size and position of the element along the Y-axis.
		/// </summary>
		// ReSharper disable once StaticMemberInGenericType
		public static readonly DependencyProperty IntervalYProperty = DependencyProperty.RegisterAttached(
			@"IntervalY", typeof( Interval<TY, TYSize> ), Type,
			new FrameworkPropertyMetadata( OnIntervalYChanged ) );
		static void OnIntervalYChanged( DependencyObject element, DependencyPropertyChangedEventArgs args )
		{
			var interval = (Interval<TY, TYSize>)args.NewValue;
			element.SetValue( YProperty, interval.Start );
			element.SetValue( SizeYProperty, interval.Size );
		}
		public static Interval<TY, TYSize> GetIntervalY( FrameworkElement element )
		{
			return (Interval<TY, TYSize>)element.GetValue( IntervalYProperty );
		}
		public static void SetIntervalY( FrameworkElement element, Interval<TY, TYSize> value )
		{
			element.SetValue( IntervalYProperty, value );
		}

		#endregion // Attached properties


		static AxesPanel()
		{
			DefaultStyleKeyProperty.OverrideMetadata( Type, new FrameworkPropertyMetadata( Type ) );
		}


		protected override Size MeasureOverride( Size availableSize )
		{
			// Allow children to take up as much room as they want.
			foreach ( UIElement child in Children )
			{
				// Verify desired width and height, and set when needed.
				FrameworkElement element = child as FrameworkElement;
				if ( element != null )
				{
					// Resize in case size is specified.
					ValueSource sizeXSource = DependencyPropertyHelper.GetValueSource( child, SizeXProperty );
					object sizeX = sizeXSource.BaseValueSource == BaseValueSource.Default ? null : child.GetValue( SizeXProperty );
					ValueSource sizeYSource = DependencyPropertyHelper.GetValueSource( child, SizeYProperty );
					object sizeY = sizeYSource.BaseValueSource == BaseValueSource.Default ? null : child.GetValue( SizeYProperty );
					if ( sizeX != null )
					{
						element.Width = IntervalSize( VisibleIntervalX, (TXSize)sizeX, availableSize.Width );
					}
					if ( sizeY != null )
					{
						element.Height = IntervalSize( VisibleIntervalY, (TYSize)sizeY, availableSize.Height );
					}

					// In case elements are wider than the available size, ensure they are still sized as such, and simply cropped.
					availableSize = new Size(
						Math.Max( availableSize.Width, double.IsNaN( element.Width ) ? availableSize.Width : element.Width ),
						Math.Max( availableSize.Height, double.IsNaN( element.Height ) ? availableSize.Height : element.Height ) );
				}

				// Measure desired size within the available space.
				child.Measure( availableSize );
				availableSize = new Size(
					Math.Max( availableSize.Width, child.DesiredSize.Width ),
					Math.Max( availableSize.Height, child.DesiredSize.Height ) );
			}

			// The idea behind this panel is to display the specified plane area in the available size.
			return availableSize;
		}
		protected override Size ArrangeOverride( Size finalSize )
		{
			// Position children.
			foreach ( UIElement child in Children )
			{
				// Add translate transform.
				var translate = child.RenderTransform as TranslateTransform;
				if ( translate == null )
				{
					translate = new TranslateTransform();
					child.RenderTransform = translate;
				}

				// Get positioning information.
				ValueSource xSource = DependencyPropertyHelper.GetValueSource( child, XProperty );
				object x = xSource.BaseValueSource == BaseValueSource.Default ? null : child.GetValue( XProperty );
				ValueSource ySource = DependencyPropertyHelper.GetValueSource( child, YProperty );
				object y = ySource.BaseValueSource == BaseValueSource.Default ? null : child.GetValue( YProperty );
				AxisAlignment alignmentX = (AxisAlignment)child.GetValue( AlignmentXProperty );
				AxisAlignment alignmentY = (AxisAlignment)child.GetValue( AlignmentYProperty );

				// Position.
				if ( x != null )
				{
					translate.X = PositionInInterval( VisibleIntervalX, finalSize.Width, child.DesiredSize.Width, (TX)x, alignmentX );
				}
				if ( y != null )
				{
					translate.Y = PositionInInterval( VisibleIntervalY, finalSize.Height, child.DesiredSize.Height, (TY)y, alignmentY );
				}

				// Arrange.
				child.Arrange( new Rect( new Point( 0, 0 ), child.DesiredSize ) );
			}

			return finalSize;
		}

		double IntervalSize<T, TSize>( Interval<T, TSize> visible, TSize desiredSize, double visiblePixels )
			where T : IComparable<T>
		{
			double intervalSize = Interval<T, TSize>.ConvertSizeToDouble( visible.Size );
			double desired = Interval<T, TSize>.ConvertSizeToDouble( desiredSize );

			return ( desired / intervalSize ) * visiblePixels;
		}

		double PositionInInterval<T, TSize>( Interval<T, TSize> interval, double panelSize, double elementSize, T value, AxisAlignment alignment )
			where T : IComparable<T>
		{
			double percentage = interval.GetPercentageFor( value );
			double position = percentage * panelSize;
			switch ( alignment )
			{
				case AxisAlignment.AfterValue:
					return interval.IsReversed ? position - elementSize : position;
				case AxisAlignment.Center:
					return position - ( elementSize / 2 );
				case AxisAlignment.BeforeValue:
					return interval.IsReversed ? position : position - elementSize;
				default:
					throw new NotSupportedException( alignment + " is not supported by the AxesPanel." );
			}
		}

		protected Interval<double> ConvertToInternalIntervalX( Interval<TX, TXSize> interval )
		{
			double start = ConvertFromIntervalXValue( interval.Start );
			double end = ConvertFromIntervalXValue( interval.End );
			return new Interval<double>( start, interval.IsStartIncluded, end, interval.IsEndIncluded );
		}

		protected Interval<TX, TXSize> ConvertToIntervalX( Interval<double> internalInterval )
		{
			TX start = ConvertToIntervalXValue( internalInterval.Start );
			TX end = ConvertToIntervalXValue( internalInterval.End );
			return new Interval<TX, TXSize>( start, internalInterval.IsStartIncluded, end, internalInterval.IsEndIncluded );
		}

		protected Interval<double> ConvertToInternalIntervalY( Interval<TY, TYSize> interval )
		{
			double start = ConvertFromIntervalYValue( interval.Start );
			double end = ConvertFromIntervalYValue( interval.End );
			return new Interval<double>( start, interval.IsStartIncluded, end, interval.IsEndIncluded );
		}

		protected Interval<TY, TYSize> ConvertToIntervalY( Interval<double> internalInterval )
		{
			TY start = ConvertToIntervalYValue( internalInterval.Start );
			TY end = ConvertToIntervalYValue( internalInterval.End );
			return new Interval<TY, TYSize>( start, internalInterval.IsStartIncluded, end, internalInterval.IsEndIncluded );
		}


		protected abstract double ConvertFromIntervalXValue( TX value );
		protected abstract TX ConvertToIntervalXValue( double value );
		protected abstract double ConvertFromIntervalYValue( TY value );
		protected abstract TY ConvertToIntervalYValue( double value );
		protected abstract double ConvertFromXSizeValue( TXSize value );
		protected abstract double ConvertFromYSizeValue( TYSize value );
	}
}
