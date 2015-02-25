using System;
using System.Collections.ObjectModel;
using System.Windows;


namespace Whathecode.AxesPanels
{
	public abstract class AbstractAxesLabelCollection<TX, TXSize, TY, TYSize> : ObservableCollection<FrameworkElement>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		internal abstract void VisibleIntervalChanged( AxesIntervals<TX, TXSize, TY, TYSize> visible );
	}
}
