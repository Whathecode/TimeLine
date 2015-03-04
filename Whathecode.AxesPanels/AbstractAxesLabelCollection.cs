using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace Whathecode.AxesPanels
{
	/// <summary>
	///   A collection of labels which can be visualized in an <see cref="AxesPanel{TX,TXSize,TY,TYSize}" />.
	///   Extending classes which set attached properties of <see cref="AxesPanel{TX,TXSize,TY,TYSize}" /> need to do so before adding the elements to the collection,
	///   to guarantee expected behavior when using <see cref="OverrideGroup" />.
	/// </summary>
	public abstract class AbstractAxesLabelCollection<TX, TXSize, TY, TYSize> : ObservableCollection<FrameworkElement>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		public string OverrideGroup { get; set; }


		/// <summary>
		///   Called whenever either the visible interval has changed, or the size within which it is presented has changed.
		/// </summary>
		/// <param name="visible">The visible interval.</param>
		/// <param name="panelSize">The size within which the intervals are presented.</param>
		internal abstract void VisibleIntervalChanged( AxesIntervals<TX, TXSize, TY, TYSize> visible, Size panelSize );

		/// <summary>
		///   Called whenever a label added by the collection was resized.
		/// </summary>
		/// <param name="label">The label which was resized.</param>
		/// <param name="visible">The visible interval.</param>
		/// <param name="panelSize">The size within which the intervals are presented.</param>
		internal abstract void LabelResized( FrameworkElement label, AxesIntervals<TX, TXSize, TY, TYSize> visible, Size panelSize );
	}
}
