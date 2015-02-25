using System;
using System.Collections.ObjectModel;
using System.Windows;
using Whathecode.System.Arithmetic.Range;


namespace Whathecode.AxesPanels
{
	/// <summary>
	///   A factory which facilitates generating recurrent labels which need to be represented on an <see cref="AxesPanel{TX, TXSize, TY, TYSize}"/>.
	/// </summary>
	public abstract class AbstractAxesLabelFactory<TX, TXSize, TY, TYSize>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
		public ObservableCollection<FrameworkElement> Labels { get; private set; }


		protected AbstractAxesLabelFactory()
		{
			Labels = new ObservableCollection<FrameworkElement>();
		}


		public abstract void VisibleIntervalChanged( Interval<TX, TXSize> intervalX, Interval<TY, TYSize> intervalY );
	}
}
