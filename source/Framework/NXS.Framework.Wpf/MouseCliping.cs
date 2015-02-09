using System.Runtime.InteropServices;
using System.Windows;

namespace NXS.Framework.Wpf
{
	/// <summary>
	///  Provides methodes to clip the mouse. 
	/// </summary>    
	public class MouseCliping
	{
		#region API - Methods

		[DllImport("user32.dll")]
		static extern bool ClipCursor(ref RECT lpRect);

		#endregion //API - Methods

		#region Fields

		private static RECT _rect = new RECT();
		private static UIElement _uiEelement;

		#endregion //Fields

		#region Structs

		private struct RECT
		{
			#region Fields

			/// <summary>
			/// Left position of the rectangle.
			/// </summary>
			public int Left;
			/// <summary>
			/// Top position of the rectangle.
			/// </summary>
			public int Top;
			/// <summary>
			/// Right position of the rectangle.
			/// </summary>
			public int Right;
			/// <summary>
			/// Bottom position of the rectangle.
			/// </summary>
			public int Bottom;

			#endregion //Fields

			#region .ctor

			/// <summary>
			/// Constructor.
			/// </summary>
			/// <param name="left">Horizontal position.</param>
			/// <param name="top">Vertical position.</param>
			/// <param name="right">Right most side.</param>
			/// <param name="bottom">Bottom most side.</param>
			public RECT(int left, int top, int right, int bottom)
			{
				Left = left;
				Top = top;
				Right = right;
				Bottom = bottom;
			}

			#endregion //.ctor

			//Not sure why this is here. I don't think it's needed
			#region Operators

			///// <summary>
			///// Operator to convert a RECT to Drawing.Rectangle.
			///// </summary>
			///// <param name="rect">Rectangle to convert.</param>
			///// <returns>A Drawing.Rectangle</returns>
			//public static implicit operator System.Drawing.Rectangle(RECT rect)
			//{
			//    return System.Drawing.Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
			//}

			///// <summary>
			///// Operator to convert Drawing.Rectangle to a RECT.
			///// </summary>
			///// <param name="rect">Rectangle to convert.</param>
			///// <returns>RECT rectangle.</returns>
			//public static implicit operator RECT(System.Drawing.Rectangle rect)
			//{
			//    return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
			//}

			#endregion //Operators
		}

		#endregion //Structs

		#region Properties

		/// <summary>
		/// Gets the Position of the Cliping.
		/// </summary>
		public static Point Position
		{
			get { return new Point(_rect.Left, _rect.Top); }
		}
		#endregion //Properties

		#region Methods

		/// <summary>
		/// Clips the cursor to an UIElement.
		/// </summary>
		/// <param name="element"></param>
		public static void OnUIElement(UIElement element)
		{
			_uiEelement = element;
			_rect.Left = (int)(element.PointFromScreen(new Point(0, 0)).X * -1);
			_rect.Top = (int)(element.PointFromScreen(new Point(0, 0)).Y * -1);
			_rect.Right = (int)((element.PointFromScreen(new Point(0, 0)).X * -1) + element.RenderSize.Width);
			_rect.Bottom = (int)((element.PointFromScreen(new Point(0, 0)).Y * -1) + element.RenderSize.Height);

			ClipCursor(ref _rect);
		}

		/// <summary>
		/// Release the cliping of the cursor.
		/// </summary>
		public static void Release()
		{
			_rect.Left = 0;
			_rect.Top = 0;
			_rect.Right = (int)SystemParameters.VirtualScreenWidth;
			_rect.Bottom = (int)SystemParameters.VirtualScreenHeight;

			ClipCursor(ref _rect);
		}

		#endregion //Methods
	}
}
