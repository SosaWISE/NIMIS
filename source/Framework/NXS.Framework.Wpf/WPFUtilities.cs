﻿using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NXS.Framework.Wpf
{
	public static class WPFUtilities
	{
		// Walks up the tree starting at the bottomMostVisual, until it finds the first item container for the ItemsControl
		// passed as a parameter.
		// In order to make sure it works with any control that derives from ItemsControl, this method makes no assumption 
		// about the type of that container.(it will get a ListBoxItem if it's a ListBox, a ListViewItem if it's a ListView...)
		public static FrameworkElement GetItemContainer(ItemsControl itemsControl, Visual bottomMostVisual)
		{
			FrameworkElement itemContainer = null;
			if (itemsControl != null && bottomMostVisual != null && itemsControl.Items.Count >= 1)
			{
				var firstContainer = itemsControl.ItemContainerGenerator.ContainerFromIndex(0);
				if (firstContainer != null)
				{
					Type containerType = firstContainer.GetType();

					itemContainer = FindAncestor(containerType, bottomMostVisual);

					// Make sure that the container found belongs to the items control passed as a parameter.
					if (itemContainer != null && itemContainer.DataContext != null)
					{
						FrameworkElement itemContainerVerify = itemsControl.ItemContainerGenerator.ContainerFromItem(itemContainer.DataContext) as FrameworkElement;
						if (itemContainer != itemContainerVerify)
						{
							itemContainer = null;
						}
					}
				}
			}
			return itemContainer;
		}

		public static FrameworkElement FindAncestor(Type ancestorType, Visual visual)
		{
			while (visual != null && !ancestorType.IsInstanceOfType(visual))
			{
				visual = (Visual)VisualTreeHelper.GetParent(visual);
			}
			return visual as FrameworkElement;
		}


		// Finds the orientation of the panel of the ItemsControl that contains the itemContainer passed as a parameter.
		// The orientation is needed to figure out where to draw the adorner that indicates where the item will be dropped.
		public static bool HasVerticalOrientation(FrameworkElement itemContainer)
		{
			bool hasVerticalOrientation = true;
			if (itemContainer != null)
			{
				Panel panel = VisualTreeHelper.GetParent(itemContainer) as Panel;
				StackPanel stackPanel;
				WrapPanel wrapPanel;

				if ((stackPanel = panel as StackPanel) != null)
				{
					hasVerticalOrientation = (stackPanel.Orientation == Orientation.Vertical);
				}
				else if ((wrapPanel = panel as WrapPanel) != null)
				{
					hasVerticalOrientation = (wrapPanel.Orientation == Orientation.Vertical);
				}
				// You can add support for more panel types here.
			}
			return hasVerticalOrientation;
		}

		public static void InsertItemInItemsControl(ItemsControl itemsControl, object itemToInsert, int insertionIndex)
		{
			if (itemToInsert != null)
			{
				IEnumerable itemsSource = itemsControl.ItemsSource;

				if (itemsSource == null)
				{
					itemsControl.Items.Insert(insertionIndex, itemToInsert);
				}
				// Is the ItemsSource IList or IList<T>? If so, insert the dragged item in the list.
				else if (itemsSource is IList)
				{
					((IList)itemsSource).Insert(insertionIndex, itemToInsert);
				}
				else
				{
					Type type = itemsSource.GetType();
					Type genericIListType = type.GetInterface("IList`1");
					if (genericIListType != null)
					{
						type.GetMethod("Insert").Invoke(itemsSource, new object[] { insertionIndex, itemToInsert });
					}
				}
			}
		}

		public static int RemoveItemFromItemsControl(ItemsControl itemsControl, object itemToRemove)
		{
			int indexToBeRemoved = -1;
			if (itemToRemove != null)
			{
				indexToBeRemoved = itemsControl.Items.IndexOf(itemToRemove);

				if (indexToBeRemoved != -1)
				{
					IEnumerable itemsSource = itemsControl.ItemsSource;
					if (itemsSource == null)
					{
						itemsControl.Items.RemoveAt(indexToBeRemoved);
					}
					// Is the ItemsSource IList or IList<T>? If so, remove the item from the list.
					else if (itemsSource is IList)
					{
						((IList)itemsSource).RemoveAt(indexToBeRemoved);
					}
					else
					{
						Type type = itemsSource.GetType();
						Type genericIListType = type.GetInterface("IList`1");
						if (genericIListType != null)
						{
							type.GetMethod("RemoveAt").Invoke(itemsSource, new object[] { indexToBeRemoved });
						}
					}
				}
			}
			return indexToBeRemoved;
		}

		public static bool IsInFirstHalf(FrameworkElement container, Point clickedPoint, bool hasVerticalOrientation)
		{
			if (hasVerticalOrientation)
			{
				return clickedPoint.Y < container.ActualHeight / 2;
			}
			return clickedPoint.X < container.ActualWidth / 2;
		}

		public static bool IsMovementBigEnough(Point initialMousePosition, Point currentPosition)
		{
			return (Math.Abs(currentPosition.X - initialMousePosition.X) >= SystemParameters.MinimumHorizontalDragDistance ||
				 Math.Abs(currentPosition.Y - initialMousePosition.Y) >= SystemParameters.MinimumVerticalDragDistance);
		}
	}
}