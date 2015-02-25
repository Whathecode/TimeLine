﻿using System;
using System.Collections.ObjectModel;


namespace Whathecode.AxesPanels
{
	public class AxesLabelFactories<TX, TXSize, TY, TYSize> : ObservableCollection<AbstractAxesLabelCollection<TX, TXSize, TY, TYSize>>
		where TX : IComparable<TX>
		where TY : IComparable<TY>
	{
	}
}