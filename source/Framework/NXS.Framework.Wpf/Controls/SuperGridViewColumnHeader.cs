using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Controls
{
	public class SuperGridViewColumnHeader : GridViewColumnHeader, INotifyPropertyChanged
	{
		public static readonly DependencyProperty SortComparerProperty =
			DependencyProperty.Register("SortComparer", typeof (IComparer), typeof (SuperGridViewColumnHeader),
				new PropertyMetadata(new PropertyChangedCallback(SortComparerChangedCallback)));

		public static readonly PropertyChangedEventArgs SortComparerChangedArgs =
			ObservableHelper.CreateArgs<SuperGridViewColumnHeader>(param => param.SortComparer);

		public SuperGridViewColumnHeader()
			: base()
		{
		}

		public IComparer SortComparer
		{
			get { return (IComparer) GetValue(SortComparerProperty); }
			set { SetValue(SortComparerProperty, value); }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}

		protected static void SortComparerChangedCallback(DependencyObject target, DependencyPropertyChangedEventArgs e)
		{
			var header = target as SuperGridViewColumnHeader;
			if (header != null && e.Property == SortComparerProperty)
			{
				header.OnPropertyChanged(SortComparerChangedArgs);
			}
		}
	}
}