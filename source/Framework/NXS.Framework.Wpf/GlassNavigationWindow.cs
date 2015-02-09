using System;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Navigation;

namespace NXS.Framework.Wpf
{
	public class GlassNavigationWindow : NavigationWindow
	{
		#region Constructors

		public GlassNavigationWindow()
			: base()
		{
		}

		#endregion Constructors

		#region Methods

		#region Private

		private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
		{
			if (msg == DWMHelper.WM_DWMCOMPOSITIONCHANGED)
			{
				// Re-enable glass
				GlassHelper.ExtendGlassFrame(this, new Thickness(-1));
				handled = true;
			}
			return IntPtr.Zero;
		}

		#endregion Private

		#region Protected

		protected override void OnSourceInitialized(EventArgs e)
		{
			// Let the base class do its work
			base.OnSourceInitialized(e);

			// This can't be done any earlier than the SourceInitialized event
			GlassHelper.ExtendGlassFrame(this, new Thickness(-1)); // -1 causes the glass look to fill the entire background

			// Attach a window procedure in order to detect later enabling of desktop composition
			IntPtr hwnd = new WindowInteropHelper(this).Handle;
			HwndSource.FromHwnd(hwnd).AddHook(new HwndSourceHook(WndProc));
		}

		#endregion Protected

		#endregion Methods
	}
}