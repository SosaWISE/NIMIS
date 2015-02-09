using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NXS.Framework.Wpf
{
	public class FlowDocumentScrollViewerHelper
	{
		public static bool GetStopScrolling(DependencyObject obj)
		{
			return (bool)obj.GetValue(StopScrollingProperty);
		}

		public static void SetStopScrolling(DependencyObject obj, bool value)
		{
			obj.SetValue(StopScrollingProperty, value);
		}

		public static readonly DependencyProperty StopScrollingProperty =
			DependencyProperty.RegisterAttached("StopScrolling", typeof(bool), typeof(FlowDocumentScrollViewerHelper), new FrameworkPropertyMetadata(false, FlowDocumentScrollViewerHelper.OnStopScrollingPropertyChanged));

		public static void OnStopScrollingPropertyChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			var viewer = sender as FlowDocumentScrollViewer;
			if (viewer == null)
				throw new ArgumentException("The dependency property can only be attached to a FlowDocumentScrollViewerHelper", "sender");

			if ((bool)e.NewValue == true) {
				viewer.PreviewMouseWheel += HandlePreviewMouseWheel;
			}
			else if ((bool)e.NewValue == false) {
				viewer.PreviewMouseWheel -= HandlePreviewMouseWheel;
			}
		}

		private static void HandlePreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			if (sender is FlowDocumentScrollViewer && !e.Handled) {

				e.Handled = true;
				var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
				eventArg.RoutedEvent = UIElement.MouseWheelEvent;
				eventArg.Source = sender;
				var parent = ((Control)sender).Parent as UIElement;
				parent.RaiseEvent(eventArg);
			}
		}
	}
}
