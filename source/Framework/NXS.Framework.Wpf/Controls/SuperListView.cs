using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Controls
{
	public class SuperListView : ListView
	{
		#region Events

		public event EventHandler<RowEventArgs> RowDoubleClick;

		#endregion Events

		#region Properties

		#region Dependency

		public static DependencyProperty DoubleClickCommandProperty = DependencyProperty.Register("DoubleClickCommand",
			typeof (ICommand), typeof (SuperListView));

		public static DependencyProperty AllowRowSelectionProperty = DependencyProperty.Register("AllowRowSelection",
			typeof (bool), typeof (SuperListView), new FrameworkPropertyMetadata(true));

		#endregion Dependency

		#region Private

		private List<GridViewColumn> _autoWidthColumns = new List<GridViewColumn>();
		private SortAdorner _lastAdorner = null;
		private GridViewColumnHeader _lastHeaderClicked = null;
		private GridViewColumnHeader _lastSecondaryHeaderClicked = null;
		private ListSortDirection _lastSecondarySortDirection = ListSortDirection.Descending;
		private ListSortDirection _lastSortDirection = ListSortDirection.Descending;
		private bool initialComplete;

		#endregion Private

		#region Public

		public int? InitialSortIndex { get; set; }
		public ListSortDirection InitialSortDirection { get; set; }
		public bool SecondaryChanged { get; set; }

		public ICommand DoubleClickCommand
		{
			get { return GetValue(DoubleClickCommandProperty) as ICommand; }
			set { SetValue(DoubleClickCommandProperty, value); }
		}

		public bool AllowRowSelection
		{
			get { return (bool) GetValue(AllowRowSelectionProperty); }
			set { SetValue(AllowRowSelectionProperty, value); }
		}

		#endregion Public

		#endregion Properties

		#region Constructors

		public SuperListView()
			: base()
		{
		}

		#endregion Constructors

		#region Methods

		#region Private

		private void ClearExistingAdorner()
		{
			// Remove the old adorner
			if (_lastHeaderClicked != null && _lastAdorner != null)
			{
				AdornerLayer layer = AdornerLayer.GetAdornerLayer(_lastHeaderClicked);
				if (layer != null)
					layer.Remove(_lastAdorner);
			}
		}

		private void DoRegularSort(GridViewColumnHeader sortColumn, ListSortDirection sortDirection, bool switchSortDirection)
		{
			var superSortColumn = sortColumn as SuperGridViewColumnHeader;
			var last = _lastHeaderClicked as SuperGridViewColumnHeader;
			if (last != null && sortColumn != last)
			{
				last.PropertyChanged -= SuperSortColumn_PropertyChanged;
			}
			else if (superSortColumn != null && superSortColumn != last)
			{
				superSortColumn.PropertyChanged += SuperSortColumn_PropertyChanged;
			}

			if (sortColumn.Tag != null || (superSortColumn != null && superSortColumn.SortComparer != null))
			{
				initialComplete = true;

				// Update the data display for the sort
				ICollectionView dataView = CollectionViewSource.GetDefaultView(ItemsSource);

				//Clears Existing Arrow
				ClearExistingAdorner();

				// Calculate the correct sort direction
				if (switchSortDirection)
				{
					sortDirection = (_lastSortDirection == ListSortDirection.Ascending)
						? ListSortDirection.Descending
						: ListSortDirection.Ascending;
				}

				// Track the current column and direction
				_lastHeaderClicked = sortColumn;
				_lastSortDirection = sortDirection;

				// Create the new adorner
				_lastAdorner = new SortAdorner(sortColumn, sortDirection);

				// Get the adorner layer for the column and add the new adorner
				AdornerLayer adorner = AdornerLayer.GetAdornerLayer(sortColumn);
				if (adorner != null)
					adorner.Add(_lastAdorner);

				using (dataView.DeferRefresh())
				{
					// Clear existing sort
					dataView.SortDescriptions.Clear();
					var listDataView = dataView as ListCollectionView;
					if (listDataView != null)
					{
						listDataView.CustomSort = null;
					}

					// Do the new sort
					if (listDataView != null && superSortColumn != null && superSortColumn.SortComparer != null)
					{
						// Set the sorter - do an Inverse if it's descending
						if (sortDirection == ListSortDirection.Descending)
						{
							listDataView.CustomSort = new InverseComparer(superSortColumn.SortComparer);
						}
						else
						{
							listDataView.CustomSort = superSortColumn.SortComparer;
						}
					}
					else // Parse sort descriptions from the tag
					{
						foreach (string currField in sortColumn.Tag.ToString().Split(','))
						{
							if (!string.IsNullOrEmpty(currField))
							{
								dataView.SortDescriptions.Add(new SortDescription(currField, sortDirection));
							}
						}
					}
				}
			}
		}

		private void DoSecondarySort(GridViewColumnHeader sortColumn, ListSortDirection sortDirection,
			bool switchSortDirection)
		{
			initialComplete = true;

			// Update the data display for the sort
			ICollectionView dataView = CollectionViewSource.GetDefaultView(ItemsSource);

			if (switchSortDirection)
				sortDirection = (_lastSecondarySortDirection == ListSortDirection.Ascending)
					? ListSortDirection.Descending
					: ListSortDirection.Ascending;

			// Track the current column and direction
			_lastSecondaryHeaderClicked = sortColumn;
			_lastSecondarySortDirection = sortDirection;

			//Gets the last description and deletes it
			if (dataView.SortDescriptions.Count == 2)
			{
				dataView.SortDescriptions.RemoveAt(1);
			}

			//Adds the new one in
			var field = sortColumn.Tag as string;
			dataView.SortDescriptions.Add(new SortDescription(field, _lastSecondarySortDirection));
			SecondaryChanged = false;

			//Refresh View
			dataView.Refresh();
		}

		private void DoInitialSort()
		{
			if (InitialSortIndex != null)
			{
				var viewInstance = View as GridView;
				if (viewInstance != null)
				{
					int sortIndex = InitialSortIndex.Value;
					if (sortIndex >= 0 && sortIndex < viewInstance.Columns.Count)
					{
						var header = viewInstance.Columns[sortIndex].Header as GridViewColumnHeader;
						if (header != null)
						{
							DoRegularSort(header, InitialSortDirection, false);
						}
					}
				}
			}
		}

		private void SetAutoWidths(Size size)
		{
			var viewInstance = View as GridView;
			if (viewInstance != null)
			{
				double totalWidth = 0;
				foreach (GridViewColumn currColumn in viewInstance.Columns)
				{
					if (!_autoWidthColumns.Contains(currColumn))
					{
						if (currColumn.Width.CompareTo(double.NaN) == 0)
							_autoWidthColumns.Add(currColumn);
						else
							totalWidth += currColumn.Width;
					}
				}

				if (_autoWidthColumns.Count > 0)
				{
					double autoSizeWidth = (size.Width - 33 - totalWidth)/_autoWidthColumns.Count;
					if (autoSizeWidth <= 0)
						autoSizeWidth = 30;
					foreach (GridViewColumn curr in _autoWidthColumns)
					{
						curr.Width = autoSizeWidth;
					}
				}
			}
		}

		#endregion Private

		#region Protected

		protected override void OnInitialized(EventArgs e)
		{
			// Let the base class do it's work
			base.OnInitialized(e);

			// Wire up event handlers
			AddHandler(MouseDoubleClickEvent, new RoutedEventHandler(InterceptRowDoubleClick));
			AddHandler(GridViewColumnHeader.ClickEvent, new RoutedEventHandler(InterceptColumnHeaderClicked));
		}

		protected virtual void OnRowDoubleClick(RowEventArgs e)
		{
			if (RowDoubleClick != null)
			{
				RowDoubleClick(this, e);
			}

			if (DoubleClickCommand != null && DoubleClickCommand.CanExecute(e.SelectedItem.DataContext))
			{
				DoubleClickCommand.Execute(e.SelectedItem.DataContext);
			}
		}

		protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
		{
			// Let the base class do it's work
			base.OnItemsSourceChanged(oldValue, newValue);

			if (newValue != null)
			{
				// Try to do the inital sort
				if (!initialComplete)
				{
					DoInitialSort();
				}
				else
				{
					Resort();
				}
			}
		}

		protected override Size ArrangeOverride(Size arrangeBounds)
		{
			if (!DesignerProperties.GetIsInDesignMode(this))
			{
				Size result = base.ArrangeOverride(arrangeBounds);
				SetAutoWidths(result);
				return result;
			}
			else
			{
				return base.ArrangeOverride(arrangeBounds);
			}
		}

		protected override void OnSelectionChanged(SelectionChangedEventArgs e)
		{
			if (!AllowRowSelection)
			{
				// Clear the selection
				SelectedItem = null;
			}
			else
			{
				// Let the base class do it's work
				base.OnSelectionChanged(e);
			}
		}

		#endregion Protected

		#region Public

		public void Resort()
		{
			if (initialComplete)
			{
				DoRegularSort(_lastHeaderClicked, _lastSortDirection, false);

				if (_lastSecondaryHeaderClicked != null)
					DoSecondarySort(_lastSecondaryHeaderClicked, _lastSecondarySortDirection, false);
			}
			else
				DoInitialSort();
		}

		#endregion Public

		#region Event Handlers

		private void InterceptRowDoubleClick(object sender, RoutedEventArgs e)
		{
			var source = e.OriginalSource as DependencyObject;
			if (source != null)
			{
				// First, walk up the logical tree to the top
				DependencyObject logicalParent = source;
				while (logicalParent != null)
				{
					logicalParent = LogicalTreeHelper.GetParent(source);
					if (logicalParent != null)
					{
						source = logicalParent;
					}
				}

				// Then, walk up the visual tree to find a ListViewItem
				while (source != null && source != this)
				{
					var item = source as ListViewItem;
					if (item != null)
					{
						OnRowDoubleClick(new RowEventArgs(item));
						break; // We found our item so we can exit now
					}
					source = VisualTreeHelper.GetParent(source);
				}
			}
		}

		private void InterceptColumnHeaderClicked(object sender, RoutedEventArgs e)
		{
			var header = e.OriginalSource as GridViewColumnHeader;
			if (header != null && header.Role != GridViewColumnHeaderRole.Padding)
			{
				if (SecondaryChanged)
				{
					bool switchDirection = (_lastSecondaryHeaderClicked != null) && (_lastSecondaryHeaderClicked == header)
						? true
						: false;
					DoSecondarySort(header, _lastSecondarySortDirection, switchDirection);
				}
				else
				{
					bool switchDirection = (_lastHeaderClicked != null) && (_lastHeaderClicked == header) ? true : false;
					DoRegularSort(header, _lastSortDirection, switchDirection);
				}
			}
		}

		private void SuperSortColumn_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == SuperGridViewColumnHeader.SortComparerChangedArgs.PropertyName)
			{
				DoRegularSort(sender as SuperGridViewColumnHeader, (initialComplete) ? _lastSortDirection : InitialSortDirection,
					false);
			}
		}

		#endregion Event Handlers

		#endregion Methods
	}
}