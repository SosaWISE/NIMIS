using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace NXS.Framework.Wpf
{
	internal class GlassHelper
	{
		#region Constructors

		private GlassHelper()
		{
		}

		#endregion Constructors

		#region Methods

		#region Static

		internal static bool ExtendGlassFrame(Window window, Thickness margin)
		{
			if (!DWMHelper.DwmIsCompositionEnabled())
				return false;

			IntPtr hwnd = new WindowInteropHelper(window).Handle;
			if (hwnd == IntPtr.Zero)
				throw new InvalidOperationException(
				"The Window must be shown before extending glass.");

			// Set the background to transparent from both the WPF and Win32 perspectives
			window.Background = Brushes.Transparent;
			HwndSource.FromHwnd(hwnd).CompositionTarget.BackgroundColor = Colors.Transparent;

			DWMHelper.Margins margins = new DWMHelper.Margins(margin);
			DWMHelper.DwmExtendFrameIntoClientArea(hwnd, ref margins);
			return true;
		}

		#endregion Static

		#endregion Methods
	}
}