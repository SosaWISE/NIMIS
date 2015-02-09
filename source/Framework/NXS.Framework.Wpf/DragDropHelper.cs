using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Collections;
using System.Windows.Media;
using System.Windows.Input;

namespace NXS.Framework.Wpf
{
	public class DragDropHelper
	{
		#region Properties

		#region Dependency

		public static readonly DependencyProperty IsDragSourceProperty =
			DependencyProperty.RegisterAttached("IsDragSource", typeof(bool), typeof(DragDropHelper), new UIPropertyMetadata(false, IsDragSourceChanged));

		public static readonly DependencyProperty IsDropTargetProperty =
			DependencyProperty.RegisterAttached("IsDropTarget", typeof(bool), typeof(DragDropHelper), new UIPropertyMetadata(false, IsDropTargetChanged));

		public static readonly DependencyProperty DragDropTemplateProperty =
			DependencyProperty.RegisterAttached("DragDropTemplate", typeof(DataTemplate), typeof(DragDropHelper), new UIPropertyMetadata(null));

		#endregion Dependency

		#region Private

		private DataFormat _format = DataFormats.GetDataFormat("DragDropItemsControl");
		private Point InitialMousePosition { get; set; }
		private object DraggedData { get; set; }
		private object DraggedItem { get; set; }
		private DraggedAdorner DraggedAdorner { get; set; }
		private InsertionAdorner InsertionAdorner { get; set; }
		private Window TopWindow { get; set; }

		private ItemsControl SourceItemsControl { get; set; }
		private FrameworkElement SourceItemContainer { get; set; }

		private ItemsControl TargetItemsControl { get; set; }
		private FrameworkElement TargetItemContainer { get; set; }
		private bool HasVerticalOrientation { get; set; }
		private int InsertionIndex { get; set; }
		private bool IsInFirstHalf { get; set; }

		#endregion Private

		#endregion Properties

		#region Methods

		#region Private

		// If the types of the dragged data and ItemsControl's source are compatible, 
		// there are 3 situations to have into account when deciding the drop target:
		// 1. mouse is over an items container
		// 2. mouse is over the empty part of an ItemsControl, but ItemsControl is not empty
		// 3. mouse is over an empty ItemsControl.
		// The goal of this method is to decide on the values of the following properties: 
		// targetItemContainer, insertionIndex and isInFirstHalf.
		private void DecideDropTarget(DragEventArgs e)
		{
			int targetItemsControlCount = this.TargetItemsControl.Items.Count;
			object draggedItem = e.Data.GetData(this._format.Name);

			if (IsDropDataTypeAllowed(draggedItem))
			{
				if (targetItemsControlCount > 0)
				{
					this.HasVerticalOrientation = WPFUtilities.HasVerticalOrientation(this.TargetItemsControl.ItemContainerGenerator.ContainerFromIndex(0) as FrameworkElement);
					this.TargetItemContainer = WPFUtilities.GetItemContainer(this.TargetItemsControl, e.OriginalSource as Visual);

					if (this.TargetItemContainer != null)
					{
						Point positionRelativeToItemContainer = e.GetPosition(this.TargetItemContainer);
						this.IsInFirstHalf = WPFUtilities.IsInFirstHalf(this.TargetItemContainer, positionRelativeToItemContainer, this.HasVerticalOrientation);
						this.InsertionIndex = this.TargetItemsControl.ItemContainerGenerator.IndexFromContainer(this.TargetItemContainer);

						if (!this.IsInFirstHalf)
						{
							this.InsertionIndex++;
						}
					}
					else
					{
						this.TargetItemContainer = this.TargetItemsControl.ItemContainerGenerator.ContainerFromIndex(targetItemsControlCount - 1) as FrameworkElement;
						this.IsInFirstHalf = false;
						this.InsertionIndex = targetItemsControlCount;
					}
				}
				else
				{
					this.TargetItemContainer = null;
					this.InsertionIndex = 0;
				}
			}
			else
			{
				this.TargetItemContainer = null;
				this.InsertionIndex = -1;
				e.Effects = DragDropEffects.None;
			}
		}

		// Can the dragged data be added to the destination collection?
		// It can if destination is bound to IList<allowed type>, IList or not data bound.
		private bool IsDropDataTypeAllowed(object draggedItem)
		{
			bool isDropDataTypeAllowed;
			IEnumerable collectionSource = this.TargetItemsControl.ItemsSource;
			if (draggedItem != null)
			{
				if (collectionSource != null)
				{
					Type draggedType = draggedItem.GetType();
					Type collectionType = collectionSource.GetType();

					Type genericIListType = collectionType.GetInterface("IList`1");
					if (genericIListType != null)
					{
						Type[] genericArguments = genericIListType.GetGenericArguments();
						isDropDataTypeAllowed = genericArguments[0].IsAssignableFrom(draggedType);
					}
					else if (typeof(IList).IsAssignableFrom(collectionType))
					{
						isDropDataTypeAllowed = true;
					}
					else
					{
						isDropDataTypeAllowed = false;
					}
				}
				else // the ItemsControl's ItemsSource is not data bound.
				{
					isDropDataTypeAllowed = true;
				}
			}
			else
			{
				isDropDataTypeAllowed = false;
			}
			return isDropDataTypeAllowed;
		}

		// Creates or updates the dragged Adorner. 
		private void ShowDraggedAdorner(Point currentPosition)
		{
			if (this.DraggedAdorner == null)
			{
				var adornerLayer = AdornerLayer.GetAdornerLayer(this.SourceItemsControl);
				this.DraggedAdorner = new DraggedAdorner(this.DraggedData ?? this.DraggedItem, GetDragDropTemplate(this.SourceItemsControl), this.SourceItemContainer, adornerLayer);
			}
			this.DraggedAdorner.SetPosition(currentPosition.X - this.InitialMousePosition.X, currentPosition.Y - this.InitialMousePosition.Y);
		}

		private void RemoveDraggedAdorner()
		{
			if (this.DraggedAdorner != null)
			{
				this.DraggedAdorner.Detach();
				this.DraggedAdorner = null;
			}
		}

		private void CreateInsertionAdorner()
		{
			if (this.TargetItemContainer != null)
			{
				// Here, I need to get adorner layer from targetItemContainer and not targetItemsControl. 
				// This way I get the AdornerLayer within ScrollContentPresenter, and not the one under AdornerDecorator (Snoop is awesome).
				// If I used targetItemsControl, the adorner would hang out of ItemsControl when there's a horizontal scroll bar.
				var adornerLayer = AdornerLayer.GetAdornerLayer(this.TargetItemContainer);
				this.InsertionAdorner = new InsertionAdorner(this.HasVerticalOrientation, this.IsInFirstHalf, this.TargetItemContainer, adornerLayer);
			}
		}

		private void UpdateInsertionAdornerPosition()
		{
			if (this.InsertionAdorner != null)
			{
				this.InsertionAdorner.IsInFirstHalf = this.IsInFirstHalf;
				this.InsertionAdorner.InvalidateVisual();
			}
		}

		private void RemoveInsertionAdorner()
		{
			if (this.InsertionAdorner != null)
			{
				this.InsertionAdorner.Detach();
				this.InsertionAdorner = null;
			}
		}

		#endregion Private

		#region Static

		public static bool GetIsDragSource(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsDragSourceProperty);
		}

		public static void SetIsDragSource(DependencyObject obj, bool value)
		{
			obj.SetValue(IsDragSourceProperty, value);
		}

		public static bool GetIsDropTarget(DependencyObject obj)
		{
			return (bool)obj.GetValue(IsDropTargetProperty);
		}

		public static void SetIsDropTarget(DependencyObject obj, bool value)
		{
			obj.SetValue(IsDropTargetProperty, value);
		}

		public static DataTemplate GetDragDropTemplate(DependencyObject obj)
		{
			return (DataTemplate)obj.GetValue(DragDropTemplateProperty);
		}

		public static void SetDragDropTemplate(DependencyObject obj, DataTemplate value)
		{
			obj.SetValue(DragDropTemplateProperty, value);
		}

		#endregion Static

		#region Event Handlers

		private static void IsDragSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var dragSource = obj as ItemsControl;
			if (dragSource != null)
			{
				if (Object.Equals(e.NewValue, true))
				{
					dragSource.PreviewMouseLeftButtonDown += Instance.DragSource_PreviewMouseLeftButtonDown;
					dragSource.PreviewMouseLeftButtonUp += Instance.DragSource_PreviewMouseLeftButtonUp;
					dragSource.PreviewMouseMove += Instance.DragSource_PreviewMouseMove;
				}
				else
				{
					dragSource.PreviewMouseLeftButtonDown -= Instance.DragSource_PreviewMouseLeftButtonDown;
					dragSource.PreviewMouseLeftButtonUp -= Instance.DragSource_PreviewMouseLeftButtonUp;
					dragSource.PreviewMouseMove -= Instance.DragSource_PreviewMouseMove;
				}
			}
		}

		private static void IsDropTargetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			var dropTarget = obj as ItemsControl;
			if (dropTarget != null)
			{
				if (Object.Equals(e.NewValue, true))
				{
					dropTarget.AllowDrop = true;
					dropTarget.PreviewDrop += Instance.DropTarget_PreviewDrop;
					dropTarget.PreviewDragEnter += Instance.DropTarget_PreviewDragEnter;
					dropTarget.PreviewDragOver += Instance.DropTarget_PreviewDragOver;
					dropTarget.PreviewDragLeave += Instance.DropTarget_PreviewDragLeave;
				}
				else
				{
					dropTarget.AllowDrop = false;
					dropTarget.PreviewDrop -= Instance.DropTarget_PreviewDrop;
					dropTarget.PreviewDragEnter -= Instance.DropTarget_PreviewDragEnter;
					dropTarget.PreviewDragOver -= Instance.DropTarget_PreviewDragOver;
					dropTarget.PreviewDragLeave -= Instance.DropTarget_PreviewDragLeave;
				}
			}
		}

		private void DragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.SourceItemsControl = (ItemsControl)sender;
			Visual visual = e.OriginalSource as Visual;

			this.TopWindow = (Window)WPFUtilities.FindAncestor(typeof(Window), this.SourceItemsControl);
			this.InitialMousePosition = e.GetPosition(this.TopWindow);

			this.SourceItemContainer = WPFUtilities.GetItemContainer(this.SourceItemsControl, visual);
			if (this.SourceItemContainer != null)
			{
				this.DraggedData = this.SourceItemContainer.DataContext;
				this.DraggedItem = this.SourceItemContainer;
			}
		}

		// Drag = mouse down + move by a certain amount
		private void DragSource_PreviewMouseMove(object sender, MouseEventArgs e)
		{
			if (this.DraggedData != null || this.DraggedItem != null)
			{
				// Only drag when user moved the mouse by a reasonable amount.
				if (WPFUtilities.IsMovementBigEnough(this.InitialMousePosition, e.GetPosition(this.TopWindow)))
				{
					DataObject data = new DataObject(this._format.Name, this.DraggedData ?? this.DraggedItem);

					// Adding events to the window to make sure dragged adorner comes up when mouse is not over a drop target.
					bool previousAllowDrop = this.TopWindow.AllowDrop;
					this.TopWindow.AllowDrop = true;
					this.TopWindow.DragEnter += TopWindow_DragEnter;
					this.TopWindow.DragOver += TopWindow_DragOver;
					this.TopWindow.DragLeave += TopWindow_DragLeave;

					DragDropEffects effects = DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Move);

					// Without this call, there would be a bug in the following scenario: Click on a data item, and drag
					// the mouse very fast outside of the window. When doing this really fast, for some reason I don't get 
					// the Window leave event, and the dragged adorner is left behind.
					// With this call, the dragged adorner will disappear when we release the mouse outside of the window,
					// which is when the DoDragDrop synchronous method returns.
					RemoveDraggedAdorner();

					this.TopWindow.AllowDrop = previousAllowDrop;
					this.TopWindow.DragEnter -= TopWindow_DragEnter;
					this.TopWindow.DragOver -= TopWindow_DragOver;
					this.TopWindow.DragLeave -= TopWindow_DragLeave;

					this.DraggedData = null;
					this.DraggedItem = null;
				}
			}
		}

		private void DragSource_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			this.DraggedData = null;
			this.DraggedItem = null;
		}

		private void DropTarget_PreviewDragEnter(object sender, DragEventArgs e)
		{
			this.TargetItemsControl = (ItemsControl)sender;
			object draggedItem = e.Data.GetData(this._format.Name);

			DecideDropTarget(e);
			if (draggedItem != null)
			{
				// Dragged Adorner is created on the first enter only.
				ShowDraggedAdorner(e.GetPosition(this.TopWindow));
				CreateInsertionAdorner();
			}
			e.Handled = true;
		}

		private void DropTarget_PreviewDragOver(object sender, DragEventArgs e)
		{
			object draggedItem = e.Data.GetData(this._format.Name);

			DecideDropTarget(e);
			if (draggedItem != null)
			{
				// Dragged Adorner is only updated here - it has already been created in DragEnter.
				ShowDraggedAdorner(e.GetPosition(this.TopWindow));
				UpdateInsertionAdornerPosition();
			}
			e.Handled = true;
		}

		private void DropTarget_PreviewDrop(object sender, DragEventArgs e)
		{
			object draggedItem = e.Data.GetData(this._format.Name);
			int indexRemoved = -1;

			if (draggedItem != null)
			{
				if ((e.Effects & DragDropEffects.Move) != 0)
				{
					indexRemoved = WPFUtilities.RemoveItemFromItemsControl(this.SourceItemsControl, draggedItem);
				}
				// This happens when we drag an item to a later position within the same ItemsControl.
				if (indexRemoved != -1 && this.SourceItemsControl == this.TargetItemsControl && indexRemoved < this.InsertionIndex)
				{
					this.InsertionIndex--;
				}
				WPFUtilities.InsertItemInItemsControl(this.TargetItemsControl, draggedItem, this.InsertionIndex);

				RemoveDraggedAdorner();
				RemoveInsertionAdorner();
			}
			e.Handled = true;
		}

		private void DropTarget_PreviewDragLeave(object sender, DragEventArgs e)
		{
			// Dragged Adorner is only created once on DragEnter + every time we enter the window. 
			// It's only removed once on the DragDrop, and every time we leave the window. (so no need to remove it here)
			object draggedItem = e.Data.GetData(this._format.Name);

			if (draggedItem != null)
			{
				RemoveInsertionAdorner();
			}
			e.Handled = true;
		}

		private void TopWindow_DragEnter(object sender, DragEventArgs e)
		{
			ShowDraggedAdorner(e.GetPosition(this.TopWindow));
			e.Effects = DragDropEffects.None;
			e.Handled = true;
		}

		private void TopWindow_DragOver(object sender, DragEventArgs e)
		{
			ShowDraggedAdorner(e.GetPosition(this.TopWindow));
			e.Effects = DragDropEffects.None;
			e.Handled = true;
		}

		private void TopWindow_DragLeave(object sender, DragEventArgs e)
		{
			RemoveDraggedAdorner();
			e.Handled = true;
		}

		#endregion Event Handlers

		#endregion Methods

		#region Singleton Implementation

		private DragDropHelper()
		{
		}

		public static DragDropHelper Instance
		{
			get
			{
				return Nested.HelperInstance;
			}
		}

		private class Nested
		{
			static Nested()
			{
			}

			internal static readonly DragDropHelper HelperInstance = new DragDropHelper();
		}

		#endregion Singleton Implementation
	}
}