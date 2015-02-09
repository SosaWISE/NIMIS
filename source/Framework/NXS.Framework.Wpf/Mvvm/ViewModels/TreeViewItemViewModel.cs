using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using NXS.Framework.Wpf.Mvvm.Managers;
using SOS.Lib.Util;
using StructureMap;

namespace NXS.Framework.Wpf.Mvvm.ViewModels
{
	/// <summary>
	/// Base class for all ViewModel classes displayed by TreeViewItems.  
	/// This acts as an adapter between a raw data object and a TreeViewItem.
	/// </summary>
	public class TreeViewItemViewModel : INotifyPropertyChanged
	{
		#region Data

		readonly ObservableCollection<TreeViewItemViewModel> _children;
		protected readonly TreeViewItemViewModel _parent;
		public readonly object _item;

		static readonly TreeViewItemViewModel LazyLoadNode = new TreeViewItemViewModel(null, new object(), false);

		protected bool _isExpanded;
		bool _isSelected;

		public bool HasLoaded { get; protected set; }

		readonly static PropertyChangedEventArgs IsLoadingDataChangeArgs = ObservableHelper.CreateArgs<TreeViewItemViewModel>(x => x.IsLoadingData);
		bool _IsLoadingData;
		public bool IsLoadingData
		{
			get { return _IsLoadingData; }
			protected set
			{
				if (_IsLoadingData == value) return;

				_IsLoadingData = value;
				OnPropertyChanged(IsLoadingDataChangeArgs);
			}
		}

		#endregion // Data

		#region Constructors

		protected TreeViewItemViewModel(TreeViewItemViewModel parent, object item, bool showLazyLoad)
		{
			_parent = parent;
			_item = item;

			_children = new ObservableCollection<TreeViewItemViewModel>();
			if (showLazyLoad) {
				_children.Add(LazyLoadNode);
			}

			HasLoaded = false;
		}

		#endregion // Constructors

		#region Properties

		/// <summary>
		/// Returns the logical child items of this object.
		/// </summary>
		public ObservableCollection<TreeViewItemViewModel> Children
		{
			get { return _children; }
		}

		/// <summary>
		/// Gets/sets whether the TreeViewItem 
		/// associated with this object is expanded.
		/// </summary>
		public bool IsExpanded
		{
			get { return _isExpanded; }
			set
			{
				if (value != _isExpanded) {

					_isExpanded = value;
					this.OnPropertyChanged("IsExpanded");
				}

				// Expand all the way up to the root.
				if (_isExpanded && _parent != null) {

					_parent.IsExpanded = true;
				}

				// Lazy load the child items, if necessary.
				if (!this.HasLoaded) {
					Load();
				}
			}
		}

		/// <summary>
		/// Gets/sets whether the TreeViewItem 
		/// associated with this object is selected.
		/// </summary>
		public bool IsSelected
		{
			get { return _isSelected; }
			set
			{
				if (value != _isSelected) {

					_isSelected = value;
					this.OnPropertyChanged("IsSelected");
				}
			}
		}

		public TreeViewItemViewModel Parent
		{
			get { return _parent; }
		}

		private IMessageBoxManager _messageBoxManager;
		protected IMessageBoxManager MessageBoxManager
		{
			get
			{
				if (_messageBoxManager == null)
					_messageBoxManager = ObjectFactory.GetInstance<IMessageBoxManager>();
				return _messageBoxManager;
			}
		}

		#endregion //Properties

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
		{
			if (this.PropertyChanged != null) {

				this.PropertyChanged(this, args);
			}
		}

		#endregion // INotifyPropertyChanged Members

		public bool HasLazyLoadNode
		{
			get
			{
				return
					this.Children.Count == 1
					&& this.Children[0] == LazyLoadNode
				;
			}
		}

		protected void ClearLazyLoadNode()
		{
			if (HasLazyLoadNode) {
				_children.Clear();
			}
		}

		private void Load()
		{
			ClearLazyLoadNode();

			this.HasLoaded = true;
			this.LoadChildren();
		}

		public virtual string Name { get; protected set; }
		protected virtual void LoadChildren()
		{
			throw new NotImplementedException();
		}


		public void AddChild(TreeViewItemViewModel child)
		{
			if (HasLoaded)
				throw new Exception("Children have already been loaded, unable to add children manually.");

			HasLoaded = true;//don't allow any other loading, at least for now

			ClearLazyLoadNode();

			this.Children.Add(child);
		}
	}
}
