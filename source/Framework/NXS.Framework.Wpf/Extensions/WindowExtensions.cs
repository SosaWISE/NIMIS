using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace NXS.Framework.Wpf.Extensions
{
	public static class WindowExtensions
	{
		[DllImport("user32.dll")]
		internal extern static int SetWindowLong(IntPtr hwnd, int index, int value);

		[DllImport("user32.dll")]
		internal extern static int GetWindowLong(IntPtr hwnd, int index);

		#region HideButtons

		const int GWL_STYLE = -16;

		const int MinimizeButton = -131073;
		const int MaximizeButton = -65537;
		const int WS_SYSMENU = 0x00080000;

		public static IntPtr GetWindowHandle(Window window)
		{
			return new System.Windows.Interop.WindowInteropHelper(window).Handle;
		}

		public static void HideMinimizeAndMaximizeButtons(this Window window)
		{
			window.HideButtons(MinimizeButton & MaximizeButton);
		}
		public static void HideMinimize(this Window window)
		{
			window.HideButtons(MinimizeButton);
		}
		public static void HideMaximize(this Window window)
		{
			window.HideButtons(MaximizeButton);
		}
		private static void HideButtons(this Window window, int buttonFlags)
		{
			IntPtr hwnd = GetWindowHandle(window);
			int value = (int)GetWindowLong(hwnd, GWL_STYLE);

			value = value & buttonFlags;

			SetWindowLong(hwnd, GWL_STYLE, value);
		}
		public static void HideCloseButton(this Window window)
		{
			IntPtr hwnd = GetWindowHandle(window);
			int value = (int)GetWindowLong(hwnd, GWL_STYLE);

			value = value.BooleanSet(WS_SYSMENU, false);

			SetWindowLong(hwnd, GWL_STYLE, value);
		}

		#endregion //HideButtons
	}
}
