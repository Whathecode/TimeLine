﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Whathecode.System.Arithmetic.Range;
using Whathecode.System.Operators;


namespace Whathecode.AxesPanels
{
	/// <summary>
	///   A factory which facilitates generating recurrent labels which need to be represented on an <see cref="AxesPanel{TX, TXSize, TY, TYSize}"/> at a certain X and Y position.
	/// </summary>
	public abstract class AbstractAxesLabelFactory<TX, TXSize, TY, TYSize> : AbstractAxesLabelCollection<TX, TXSize, TY, TYSize>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		protected class PositionedElement
		{
			public readonly FrameworkElement Element;
			public readonly Tuple<TX, TY> Position;

			public PositionedElement( FrameworkElement element, Tuple<TX, TY> position )
			{
				Element = element;
				Position = position;
			}
		}


		readonly List<PositionedElement> _visibleLabels = new List<PositionedElement>();
		readonly Stack<FrameworkElement> _availableLabels = new Stack<FrameworkElement>(); 


		internal override void VisibleIntervalChanged( AxesIntervals<TX, TXSize, TY, TYSize> visible )
		{
			// Create extended intervals.
			Interval<TX, TXSize> intervalX = visible.IntervalX;
			Interval<TY, TYSize> intervalY = visible.IntervalY;
			Tuple<TXSize, TYSize> maxLabelSize = GetMaximumLabelSize( visible );
			var additionalX = Operator<TXSize>.Add( maxLabelSize.Item1, maxLabelSize.Item1 );
			var additionalY = Operator<TYSize>.Add( maxLabelSize.Item2, maxLabelSize.Item2 );
			var extendedX = Operator<TXSize>.Add( intervalX.Size, additionalX );
			var extendedY = Operator<TYSize>.Add( intervalY.Size, additionalY );
			double scaleX = Interval<TX, TXSize>.ConvertSizeToDouble( extendedX ) / Interval<TX, TXSize>.ConvertSizeToDouble( intervalX.Size );
			double scaleY = Interval<TY, TYSize>.ConvertSizeToDouble( extendedY ) / Interval<TY, TYSize>.ConvertSizeToDouble( intervalY.Size );
			var extendedIntervals = new AxesIntervals<TX, TXSize, TY, TYSize>( intervalX.Scale( scaleX ), intervalY.Scale( scaleY ) );

			var toPosition = new HashSet<Tuple<TX, TY>>( GetPositions( extendedIntervals ) );

			// Free up labels which are no longer visible, and update those already positioned.
			var toRemove = new List<PositionedElement>();
			foreach ( var positioned in _visibleLabels )
			{
				if ( toPosition.Contains( positioned.Position ) )
				{
					UpdateLabel( positioned, visible );
					toPosition.Remove( positioned.Position );
				}
				else
				{
					Remove( positioned.Element );
					_availableLabels.Push( positioned.Element );
					toRemove.Add( positioned );
				}
			}
			toRemove.ForEach( r => _visibleLabels.Remove( r ) );

			// Position new labels.
			foreach ( var position in toPosition )
			{
				// Create a new label when needed, or retrieve existing one.
				FrameworkElement toPlace;
				if ( _availableLabels.Count == 0 )
				{
					toPlace = CreateLabel();
					toPlace.CacheMode = new BitmapCache();
					Add( toPlace );
				}
				else
				{
					toPlace = _availableLabels.Pop();
				}

				toPlace.SetValue( AxesPanel<TX, TXSize, TY, TYSize>.XProperty, position.Item1 );
				toPlace.SetValue( AxesPanel<TX, TXSize, TY, TYSize>.YProperty, position.Item2 );
				var positioned = new PositionedElement( toPlace, position );
				_visibleLabels.Add( positioned );
				InitializeLabel( positioned, visible );
			}
		}

		/// <summary>
		///   Returns the maximum size a label can have given specified visible intervals.
		/// </summary>
		protected abstract Tuple<TXSize, TYSize> GetMaximumLabelSize( AxesIntervals<TX, TXSize, TY, TYSize> visible );

		/// <summary>
		///   Returns all positions on which to place labels within the specified interval.
		///   Label size does not need to be taken into account, and is accounted for by <see cref="GetMaximumLabelSize" />.
		/// </summary>
		protected abstract IEnumerable<Tuple<TX, TY>> GetPositions( AxesIntervals<TX, TXSize, TY, TYSize> intervals );

		/// <summary>
		///   Create a new label which can be positioned later.
		/// </summary>
		protected abstract FrameworkElement CreateLabel();

		/// <summary>
		///   Initializes a label which has just been repositioned at a certain positioned.
		/// </summary>
		protected abstract void InitializeLabel( PositionedElement positioned, AxesIntervals<TX, TXSize, TY, TYSize> visible );

		/// <summary>
		///   Called for already positioned elements which might potentionally need to be updated based on a new visible interval.
		/// </summary>
		protected abstract void UpdateLabel( PositionedElement label, AxesIntervals<TX, TXSize, TY, TYSize> visible );
	}
}