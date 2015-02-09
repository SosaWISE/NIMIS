using System;
using System.Text;
using System.Runtime.InteropServices;

namespace NXS.Framework.Wpf
{
	public class Win32Helper
	{
		public delegate bool EnumWindowsCallback(IntPtr hwnd, int lParam);

		#region Enums

		#region Public

		/// <summary>Enumeration of the different ways of showing a window using ShowWindow</summary>
		/// <remarks>See http://www.pinvoke.net/default.aspx/user32/ShowWindow.html </remarks>
		public enum WindowShowStyle : uint
		{
			/// <summary>Hides the window and activates another window.</summary>
			/// <remarks>See SW_HIDE</remarks>
			Hide = 0,
			/// <summary>Activates and displays a window. If the window is minimized 
			/// or maximized, the system restores it to its original size and 
			/// position. An application should specify this flag when displaying 
			/// the window for the first time.</summary>
			/// <remarks>See SW_SHOWNORMAL</remarks>
			ShowNormal = 1,
			/// <summary>Activates the window and displays it as a minimized window.</summary>
			/// <remarks>See SW_SHOWMINIMIZED</remarks>
			ShowMinimized = 2,
			/// <summary>Activates the window and displays it as a maximized window.</summary>
			/// <remarks>See SW_SHOWMAXIMIZED</remarks>
			ShowMaximized = 3,
			/// <summary>Maximizes the specified window.</summary>
			/// <remarks>See SW_MAXIMIZE</remarks>
			Maximize = 3,
			/// <summary>Displays a window in its most recent size and position. 
			/// This value is similar to "ShowNormal", except the window is not 
			/// actived.</summary>
			/// <remarks>See SW_SHOWNOACTIVATE</remarks>
			ShowNormalNoActivate = 4,
			/// <summary>Activates the window and displays it in its current size 
			/// and position.</summary>
			/// <remarks>See SW_SHOW</remarks>
			Show = 5,
			/// <summary>Minimizes the specified window and activates the next 
			/// top-level window in the Z order.</summary>
			/// <remarks>See SW_MINIMIZE</remarks>
			Minimize = 6,
			/// <summary>Displays the window as a minimized window. This value is 
			/// similar to "ShowMinimized", except the window is not activated.</summary>
			/// <remarks>See SW_SHOWMINNOACTIVE</remarks>
			ShowMinNoActivate = 7,
			/// <summary>Displays the window in its current size and position. This 
			/// value is similar to "Show", except the window is not activated.</summary>
			/// <remarks>See SW_SHOWNA</remarks>
			ShowNoActivate = 8,
			/// <summary>Activates and displays the window. If the window is 
			/// minimized or maximized, the system restores it to its original size 
			/// and position. An application should specify this flag when restoring 
			/// a minimized window.</summary>
			/// <remarks>See SW_RESTORE</remarks>
			Restore = 9,
			/// <summary>Sets the show state based on the SW_ value specified in the 
			/// STARTUPINFO structure passed to the CreateProcess function by the 
			/// program that started the application.</summary>
			/// <remarks>See SW_SHOWDEFAULT</remarks>
			ShowDefault = 10,
			/// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
			/// that owns the window is hung. This flag should only be used when 
			/// minimizing windows from a different thread.</summary>
			/// <remarks>See SW_FORCEMINIMIZE</remarks>
			ForceMinimized = 11
		}

		/// <summary>Flags that control window style.</summary>
		/// <remarks>See http://www.pinvoke.net/default.aspx/Enums/WindowStylesEx.html </remarks>
		[Flags]
		public enum WindowExStyles : uint
		{
			/// <summary>
			/// Specifies that a window created with this style accepts drag-drop files.
			/// </summary>
			WS_EX_ACCEPTFILES = 0x00000010,
			/// <summary>
			/// Forces a top-level window onto the taskbar when the window is visible.
			/// </summary>
			WS_EX_APPWINDOW = 0x00040000,
			/// <summary>
			/// Specifies that a window has a border with a sunken edge.
			/// </summary>
			WS_EX_CLIENTEDGE = 0x00000200,
			/// <summary>
			/// Windows XP: Paints all descendants of a window in bottom-to-top painting order using double-buffering. For more information, see Remarks. This cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
			/// </summary>
			WS_EX_COMPOSITED = 0x02000000,
			/// <summary>
			/// Includes a question mark in the title bar of the window. When the user clicks the question mark, the cursor changes to a question mark with a pointer. If the user then clicks a child window, the child receives a WM_HELP message. The child window should pass the message to the parent window procedure, which should call the WinHelp function using the HELP_WM_HELP command. The Help application displays a pop-up window that typically contains help for the child window.
			/// WS_EX_CONTEXTHELP cannot be used with the WS_MAXIMIZEBOX or WS_MINIMIZEBOX styles.
			/// </summary>
			WS_EX_CONTEXTHELP = 0x00000400,
			/// <summary>
			/// The window itself contains child windows that should take part in dialog box navigation. If this style is specified, the dialog manager recurses into children of this window when performing navigation operations such as handling the TAB key, an arrow key, or a keyboard mnemonic.
			/// </summary>
			WS_EX_CONTROLPARENT = 0x00010000,
			/// <summary>
			/// Creates a window that has a double border; the window can, optionally, be created with a title bar by specifying the WS_CAPTION style in the dwStyle parameter.
			/// </summary>
			WS_EX_DLGMODALFRAME = 0x00000001,
			/// <summary>
			/// Windows 2000/XP: Creates a layered window. Note that this cannot be used for child windows. Also, this cannot be used if the window has a class style of either CS_OWNDC or CS_CLASSDC. 
			/// </summary>
			WS_EX_LAYERED = 0x00080000,
			/// <summary>
			/// Arabic and Hebrew versions of Windows 98/Me, Windows 2000/XP: Creates a window whose horizontal origin is on the right edge. Increasing horizontal values advance to the left. 
			/// </summary>
			WS_EX_LAYOUTRTL = 0x00400000,
			/// <summary>
			/// Creates a window that has generic left-aligned properties. This is the default.
			/// </summary>
			WS_EX_LEFT = 0x00000000,
			/// <summary>
			/// If the shell language is Hebrew, Arabic, or another language that supports reading order alignment, the vertical scroll bar (if present) is to the left of the client area. For other languages, the style is ignored.
			/// </summary>
			WS_EX_LEFTSCROLLBAR = 0x00004000,
			/// <summary>
			/// The window text is displayed using left-to-right reading-order properties. This is the default.
			/// </summary>
			WS_EX_LTRREADING = 0x00000000,
			/// <summary>
			/// Creates a multiple-document interface (MDI) child window.
			/// </summary>
			WS_EX_MDICHILD = 0x00000040,
			/// <summary>
			/// Windows 2000/XP: A top-level window created with this style does not become the foreground window when the user clicks it. The system does not bring this window to the foreground when the user minimizes or closes the foreground window. 
			/// To activate the window, use the SetActiveWindow or SetForegroundWindow function.
			/// The window does not appear on the taskbar by default. To force the window to appear on the taskbar, use the WS_EX_APPWINDOW style.
			/// </summary>
			WS_EX_NOACTIVATE = 0x08000000,
			/// <summary>
			/// Windows 2000/XP: A window created with this style does not pass its window layout to its child windows.
			/// </summary>
			WS_EX_NOINHERITLAYOUT = 0x00100000,
			/// <summary>
			/// Specifies that a child window created with this style does not send the WM_PARENTNOTIFY message to its parent window when it is created or destroyed.
			/// </summary>
			WS_EX_NOPARENTNOTIFY = 0x00000004,
			/// <summary>
			/// Combines the WS_EX_CLIENTEDGE and WS_EX_WINDOWEDGE styles.
			/// </summary>
			WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
			/// <summary>
			/// Combines the WS_EX_WINDOWEDGE, WS_EX_TOOLWINDOW, and WS_EX_TOPMOST styles.
			/// </summary>
			WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
			/// <summary>
			/// The window has generic "right-aligned" properties. This depends on the window class. This style has an effect only if the shell language is Hebrew, Arabic, or another language that supports reading-order alignment; otherwise, the style is ignored.
			/// Using the WS_EX_RIGHT style for static or edit controls has the same effect as using the SS_RIGHT or ES_RIGHT style, respectively. Using this style with button controls has the same effect as using BS_RIGHT and BS_RIGHTBUTTON styles.
			/// </summary>
			WS_EX_RIGHT = 0x00001000,
			/// <summary>
			/// Vertical scroll bar (if present) is to the right of the client area. This is the default.
			/// </summary>
			WS_EX_RIGHTSCROLLBAR = 0x00000000,
			/// <summary>
			/// If the shell language is Hebrew, Arabic, or another language that supports reading-order alignment, the window text is displayed using right-to-left reading-order properties. For other languages, the style is ignored.
			/// </summary>
			WS_EX_RTLREADING = 0x00002000,
			/// <summary>
			/// Creates a window with a three-dimensional border style intended to be used for items that do not accept user input.
			/// </summary>
			WS_EX_STATICEDGE = 0x00020000,
			/// <summary>
			/// Creates a tool window; that is, a window intended to be used as a floating toolbar. A tool window has a title bar that is shorter than a normal title bar, and the window title is drawn using a smaller font. A tool window does not appear in the taskbar or in the dialog that appears when the user presses ALT+TAB. If a tool window has a system menu, its icon is not displayed on the title bar. However, you can display the system menu by right-clicking or by typing ALT+SPACE. 
			/// </summary>
			WS_EX_TOOLWINDOW = 0x00000080,
			/// <summary>
			/// Specifies that a window created with this style should be placed above all non-topmost windows and should stay above them, even when the window is deactivated. To add or remove this style, use the SetWindowPos function.
			/// </summary>
			WS_EX_TOPMOST = 0x00000008,
			/// <summary>
			/// Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted. The window appears transparent because the bits of underlying sibling windows have already been painted.
			/// To achieve transparency without these restrictions, use the SetWindowRgn function.
			/// </summary>
			WS_EX_TRANSPARENT = 0x00000020,
			/// <summary>
			/// Specifies that a window has a border with a raised edge.
			/// </summary>
			WS_EX_WINDOWEDGE = 0x00000100
		}

		public enum GetWindowCmd : uint
		{
			GW_HWNDFIRST = 0,
			GW_HWNDLAST = 1,
			GW_HWNDNEXT = 2,
			GW_HWNDPREV = 3,
			GW_OWNER = 4,
			GW_CHILD = 5,
			GW_ENABLEDPOPUP = 6
		}

		public enum ShellExecuteCommand : int
		{
			SW_HIDE = 0,
			SW_SHOWNORMAL = 1,
			SW_NORMAL = 1,
			SW_SHOWMINIMIZED = 2,
			SW_SHOWMAXIMIZED = 3,
			SW_MAXIMIZE = 3,
			SW_SHOWNOACTIVATE = 4,
			SW_SHOW = 5,
			SW_MINIMIZE = 6,
			SW_SHOWMINNOACTIVE = 7,
			SW_SHOWNA = 8,
			SW_RESTORE = 9,
			SW_SHOWDEFAULT = 10,
			SW_FORCEMINIMIZE = 11,
			SW_MAX = 11
		}

		#endregion Public

		#endregion Enums

		#region Constants

		public const int GWL_ID = -12;
		public const int GWL_STYLE = -16;
		public const int GWL_EXSTYLE = -20;

		public const int MF_BYPOSITION = 0x400;
		public const int MF_REMOVE = 0x1000;

		public const int WM_GETMINMAXINFO = 0x0024;

		#endregion Constants

		#region Constructors

		// Mark default constructor as private b/c this class is designed to provide only static methods.
		private Win32Helper()
		{
		}

		#endregion Constructors

		#region Structs

		[StructLayout(LayoutKind.Sequential)]
		public struct Point32
		{
			public int x;
			public int y;

			/// <summary>
			/// Construct a point of coordinates (x,y).
			/// </summary>
			public Point32(int x, int y)
			{
				this.x = x;
				this.y = y;
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MinMaxInfo
		{
			public Point32 ptReserved;
			public Point32 ptMaxSize;
			public Point32 ptMaxPosition;
			public Point32 ptMinTrackSize;
			public Point32 ptMaxTrackSize;
		};


		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class MonitorInfo
		{
			public int Size = Marshal.SizeOf(typeof(MonitorInfo));

			public Rectangle32 rcMonitorArea = new Rectangle32();

			public Rectangle32 rcWorkArea = new Rectangle32();

			public int Flags = 0;
		}


		[StructLayout(LayoutKind.Sequential, Pack = 0)]
		public struct Rectangle32
		{
			public int left;
			public int top;
			public int right;
			public int bottom;

			public static readonly Rectangle32 Empty = new Rectangle32();

			public int Width
			{
				get { return Math.Abs(right - left); }
			}

			public int Height
			{
				get { return Math.Abs(bottom - top); }
			}

			public Rectangle32(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}
		}

		#endregion Structs

		#region Methods

		#region Private
		#endregion Private

		#region Public

		[DllImport("user32.dll")]
		public static extern int SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

		[DllImport("user32.dll")]
		public static extern int GetWindowLong(IntPtr window, int index);

		[DllImport("user32.dll")]
		public static extern int SetWindowLong(IntPtr window, int index, int value);

		[DllImport("user32.dll")]
		public static extern IntPtr GetTopWindow(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern int GetForegroundWindow();

		[DllImport("user32.dll")]
		public static extern int EnumWindows(EnumWindowsCallback lpEnumFunc, int lParam);

		[DllImport("user32.dll")]
		public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
		
		[DllImport("user32.dll")]
		public static extern int GetMenuItemCount(IntPtr hMenu);
		
		[DllImport("user32.dll")]
		public static extern bool DrawMenuBar(IntPtr hWnd);
		
		[DllImport("user32.dll")]
		public static extern bool RemoveMenu(IntPtr hMenu, uint uPosition, uint uFlags);

		[DllImport("user32.dll")]
		public static extern bool GetMonitorInfo(IntPtr hMonitor, MonitorInfo lpmi);

		[DllImport("user32.dll")]
		public static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

		[DllImport("shell32.dll")]
		public static extern IntPtr ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, ShellExecuteCommand nShowCmd);

		public static void WmGetMinMaxInfo(IntPtr hwnd, IntPtr lParam)
		{
			MinMaxInfo info = (MinMaxInfo)Marshal.PtrToStructure(lParam, typeof(MinMaxInfo));

			// Adjust the maximized size and position to fit the work area of the correct monitor
			int MONITOR_DEFAULTTONEAREST = 0x00000002;
			IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

			if (monitor != System.IntPtr.Zero)
			{
				MonitorInfo monitorInfo = new MonitorInfo();
				GetMonitorInfo(monitor, monitorInfo);
				Rectangle32 rcWorkArea = monitorInfo.rcWorkArea;
				Rectangle32 rcMonitorArea = monitorInfo.rcMonitorArea;

				info.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
				info.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
				info.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
				info.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
			}

			Marshal.StructureToPtr(info, lParam, true);
		}

		[DllImport("winspool.drv")]
		public static extern bool AddPrinterConnection(string pName);

		//Set the added printer as default printer.
		[DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool SetDefaultPrinter(string Name);

		public static void RemoveMaximizeButton(IntPtr hwnd)
		{
			long windowLong = GetWindowLong(hwnd, GWL_STYLE);

			windowLong = windowLong & -65537;

			SetWindowLong(hwnd, GWL_STYLE, Convert.ToInt32(windowLong));
			
		}

		#endregion Public

		#endregion Methods
	}
}