using System.ComponentModel;
using System.Collections.ObjectModel;
using SOS.Lib.Util;
using System.Windows.Data;

namespace NXS.Framework.Wpf.Mvvm
{
	public class SelectableList<T> : INotifyPropertyChanged
	{
		#region Properties

		private readonly ObservableCollection<T> _itemListInternal;
		public ObservableCollection<T> InternalList
		{
			get { return _itemListInternal; }
		}

		readonly static PropertyChangedEventArgs ItemListChangeArgs = ObservableHelper.CreateArgs<SelectableList<T>>(x => x.ItemList);
		private ReadOnlyObservableCollection<T> _ItemList;
		public ReadOnlyObservableCollection<T> ItemList
		{
			get { return _ItemList; }
			protected set
			{
				_ItemList = value;

				ItemListCV = CollectionViewSource.GetDefaultView(_ItemList);
				ItemListCV.MoveCurrentToPosition(-1);

				OnPropertyChanged(ItemListChangeArgs);
			}
		}
		readonly static PropertyChangedEventArgs ItemListCVChangeArgs = ObservableHelper.CreateArgs<SelectableList<T>>(x => x.ItemListCV);
		private ICollectionView _ItemListCV;
		public ICollectionView ItemListCV
		{
			get { return _ItemListCV; }
			private set
			{
				_ItemListCV = value;
				OnPropertyChanged(ItemListCVChangeArgs);
			}
		}
		readonly static PropertyChangedEventArgs SelectedItemChangeArgs = ObservableHelper.CreateArgs<SelectableList<T>>(x => x.SelectedItem);
		private T _SelectedItem;
		public T SelectedItem
		{
			get { return _SelectedItem; }
			protected set
			{
				if (!AreEqual(_SelectedItem, value)) {

					_SelectedItem = value;
					OnPropertyChanged(SelectedItemChangeArgs);
				}
			}
		}

		#endregion //Properties

		#region .ctors

		public SelectableList()
		{
			_itemListInternal = new ObservableCollection<T>();
			this.ItemList = new ReadOnlyObservableCollection<T>(_itemListInternal);
		}

		#endregion //.ctors

		#region Public Methods

		public void SelectCurrent()
		{
			this.SelectedItem = (T)this.ItemListCV.CurrentItem;
		}
		public virtual void Add(T item)
		{
			_itemListInternal.Add(item);
		}
		public virtual void Clear()
		{
			_itemListInternal.Clear();
		}

		#endregion //Public Methods

		#region Helper methods

		protected virtual bool AreEqual(T left, T right)
		{
			var result = Equals(left, right);
			return result;
		}

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, e);
			}
		}

		#endregion

		#endregion //Helper methods
	}

}
