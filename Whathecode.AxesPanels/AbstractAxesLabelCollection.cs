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
	/// <typeparam name="TX"></typeparam>
	/// <typeparam name="TXSize"></typeparam>
	/// <typeparam name="TY"></typeparam>
	/// <typeparam name="TYSize"></typeparam>
	public abstract class AbstractAxesLabelCollection<TX, TXSize, TY, TYSize> : ObservableCollection<FrameworkElement>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		public string OverrideGroup { get; set; }


		internal abstract void VisibleIntervalChanged( AxesIntervals<TX, TXSize, TY, TYSize> visible, Size panelSize );
	}
}
