using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace NXS.Framework.Wpf
{
	public class DWMHelper
	{
		#region Structs

		[StructLayout(LayoutKind.Sequential)]
		public struct Margins
		{
			public int Left;
			public int Right;
			public int Top;
			public int Bottom;

			public Margins(Thickness t)
			{
				Left = (int)t.Left;
				Right = (int)t.Right;
				Top = (int)t.Top;
				Bottom = (int)t.Bottom;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct PSize
		{
			public int x;
			public int y;
		}

		[StructLayout(LayoutKind.Sequential)]
// ReSharper disable once InconsistentNaming
		public struct DWM_THUMBNAIL_PROPERTIES
		{
			public int dwFlags;
			public Rectangle rcDestination;
			public Rectangle rcSource;
			public byte opacity;
			public bool fVisible;
			public bool fSourceClientAreaOnly;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Rectangle
		{
			public Rectangle(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			public int Left;
			public int Top;
			public int Right;
			public int Bottom;
		}

		#endregion Structs

		#region Constants

		public const int WM_DWMCOMPOSITIONCHANGED = 0x031E;

		public const int DWM_TNP_VISIBLE = 0x8;
		public const int DWM_TNP_OPACITY = 0x4;
		public const int DWM_TNP_RECTDESTINATION = 0x1;

		#endregion Constants

		#region Constructors

		private DWMHelper()
		{
		}

		#endregion Constructors

		#region Methods

		#region Private
		#endregion Private

		#region Public

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);

		[DllImport("dwmapi.dll", PreserveSig = false)]
		public static extern bool DwmIsCompositionEnabled();

		[DllImport("dwmapi.dll")]
		public static extern int DwmRegisterThumbnail(IntPtr dest, IntPtr src, out IntPtr thumb);

		[DllImport("dwmapi.dll")]
		public static extern int DwmUnregisterThumbnail(IntPtr thumb);

		[DllImport("dwmapi.dll")]
		public static extern int DwmQueryThumbnailSourceSize(IntPtr thumb, out PSize size);

		[DllImport("dwmapi.dll")]
		public static extern int DwmUpdateThumbnailProperties(IntPtr hThumb, ref DWM_THUMBNAIL_PROPERTIES props);

		#endregion Public

		#endregion Methods
	}
}