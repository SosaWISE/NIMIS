using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Controls;

namespace NXS.Framework.Wpf.Mvvm
{
	/// <summary>
	/// Selector MouseDoubleClick calling ViewModel ICommand Behavior
	/// using a Naive approach (or so I believe) as we are maintaining
	/// references to the event handlers Target object which thus prevents
	/// it from being GC'd
	/// </summary>
	public static class SelectorDoubleClickCommandBehavior
	{
		#region Attached DPs

		#region HandleDoubleClick

		/// <summary>
		/// HandleDoubleClick Attached Dependency Property
		/// </summary>
		public static readonly DependencyProperty HandleDoubleClickProperty =
			DependencyProperty.RegisterAttached(
				"HandleDoubleClick"
				, typeof(bool)
				, typeof(SelectorDoubleClickCommandBehavior)
				, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnHandleDoubleClickChanged))
			);

		/// <summary>
		/// Gets the HandleDoubleClick property.  
		/// </summary>
		public static bool GetHandleDoubleClick(DependencyObject d)
		{
			return (bool)d.GetValue(HandleDoubleClickProperty);
		}
		/// <summary>
		/// Sets the HandleDoubleClick property. 
		/// </summary>
		public static void SetHandleDoubleClick(DependencyObject d, bool value)
		{
			d.SetValue(HandleDoubleClickProperty, value);
		}

		/// <summary>
		/// Hooks up a weak event against the source Selectors MouseDoubleClick
		/// if the Selector has asked for the HandleDoubleClick to be handled
		/// 
		/// If the source Selector has expressed an interest in not having its
		/// MouseDoubleClick handled the internal reference
		/// </summary>
		private static void OnHandleDoubleClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			Selector selector = d as Selector;
			if (selector != null) {

				if ((bool)e.NewValue) {
					selector.MouseDoubleClick -= OnMouseDoubleClick;
					selector.MouseDoubleClick += OnMouseDoubleClick;
				}
			}
		}
		#endregion //HandleDoubleClick

		#region CommandToRun

		/// <summary>
		/// CommandToRun : The actual ICommand to run
		/// </summary>
		public static readonly DependencyProperty CommandToRunProperty =
			DependencyProperty.RegisterAttached(
				"CommandToRun"
				, typeof(ICommand)
				, typeof(SelectorDoubleClickCommandBehavior)
				, new FrameworkPropertyMetadata((ICommand)null)
			);

		/// <summary>
		/// Gets the CommandToRun property.  
		/// </summary>
		public static ICommand GetCommandToRun(DependencyObject d)
		{
			return (ICommand)d.GetValue(CommandToRunProperty);
		}

		/// <summary>
		/// Sets the CommandToRun property.  
		/// </summary>
		public static void SetCommandToRun(DependencyObject d, ICommand value)
		{
			d.SetValue(CommandToRunProperty, value);
		}
		#endregion //CommandToRun

		#endregion //Attached DPs

		#region Private Methods

		/// <summary>
		/// Handle Selector.MouseDoubleClick but will only fire the associated
		/// ViewModel command if the MouseDoubleClick occurred over an actual
		/// ItemsControl item. This is nessecary as if we are using a ListView 
		/// we may have clicked the headers which are not items, so we don't want the
		/// associated ViewModel command to be run
		/// </summary>
		private static void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			//Get the ItemsControl and then get the item, and check there
			//is an actual item, as if we are using a ListView we may have clicked the
			//headers which are not items
			ItemsControl listView = sender as ItemsControl;
			DependencyObject originalSender = e.OriginalSource as DependencyObject;
			if (listView == null || originalSender == null) return;

			DependencyObject container = ItemsControl.ContainerFromElement(sender as ItemsControl, e.OriginalSource as DependencyObject);
			if (container == null || container == DependencyProperty.UnsetValue) return;

			// found a container, now find the item.
			object activatedItem = listView.ItemContainerGenerator.ItemFromContainer(container);
			if (activatedItem != null) {

				ICommand command = (ICommand)(sender as DependencyObject).GetValue(CommandToRunProperty);
				if (command != null && command.CanExecute(null)) {
					command.Execute(null);
				}
			}
		}
		#endregion //Private Methods
	}
}
