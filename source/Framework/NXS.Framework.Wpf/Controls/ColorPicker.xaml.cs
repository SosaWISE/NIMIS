using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NXS.Framework.Wpf.Controls
{
	public partial class ColorPicker : UserControl
	{
		#region Variables

		private Point _mousePos;

		#endregion //Variables

		#region DependencyProperty declaration
		private static readonly DependencyProperty CurrentColorProperty = DependencyProperty.Register(
			"CurrentColor", typeof(Color), typeof(ColorPicker),
			new FrameworkPropertyMetadata(Color.FromRgb(255, 255, 255),
			new PropertyChangedCallback(OnCurrentColorChanged)));

		private static readonly RoutedEvent CurrentColorChangedEvent = EventManager.RegisterRoutedEvent(
			"CurrentColorChanged",
			RoutingStrategy.Tunnel,
			typeof(RoutedEventHandler), typeof(ColorPicker));

		private static readonly DependencyProperty LastColorProperty = DependencyProperty.Register(
			"LastColor", typeof(Color), typeof(ColorPicker),
			new FrameworkPropertyMetadata(Color.FromRgb(255, 255, 255),
			new PropertyChangedCallback(OnLastColorChanged)));

		private static readonly RoutedEvent LastColorChangedEvent = EventManager.RegisterRoutedEvent(
			"LastColorChanged",
			RoutingStrategy.Tunnel,
			typeof(RoutedEventHandler), typeof(ColorPicker));
		#endregion

		#region .ctor

		public ColorPicker()
		{
			this.InitializeComponent();

			AddColorSelectorGradient();
		}

		#endregion //.ctor

		#region Event wrappers
		/// <summary>
		/// Occurs when the current color changed.
		/// </summary>
		public event RoutedEventHandler CurrentColorChanged
		{
			add { AddHandler(CurrentColorChangedEvent, value); }
			remove { RemoveHandler(CurrentColorChangedEvent, value); }
		}
		/// <summary>
		/// Occurs when the last color changed.
		/// </summary>
		public event RoutedEventHandler LastColorChanged
		{
			add { AddHandler(LastColorChangedEvent, value); }
			remove { RemoveHandler(LastColorChangedEvent, value); }
		}
		#endregion

		#region Properties
		///<summary>
		///Gets the current selected Color.
		/// </summary>
		public Color CurrentColor
		{
			get { return (Color)GetValue(CurrentColorProperty); }
		}
		/// <summary>
		/// Gets the last Selected Color.
		/// </summary>
		public Color LastColor
		{
			get { return (Color)GetValue(LastColorProperty); }
		}
		/// <summary>
		/// Gets the R value of the current color.
		/// </summary>
		public int R
		{
			get { return (int)((Color)GetValue(CurrentColorProperty)).R; }
		}
		/// <summary>
		/// Gets the G value of the current color.
		/// </summary>
		public int G
		{
			get { return (int)((Color)GetValue(CurrentColorProperty)).G; }
		}
		/// <summary>
		/// Gets the B value of the current color.
		/// </summary>
		public int B
		{
			get { return (int)((Color)GetValue(CurrentColorProperty)).B; }
		}

		#endregion

		#region Methodes
		/// <summary>
		/// Creates the gradient for the color selector.
		/// </summary>
		private void AddColorSelectorGradient()
		{
			LinearGradientBrush linearGradientBrush = new LinearGradientBrush();
			linearGradientBrush.StartPoint = new Point(0.5, 0);
			linearGradientBrush.EndPoint = new Point(0.5, 1);
			linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 000, 000), 0.020));
			linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 255, 000), 0.167));
			linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 000, 255, 000), 0.334));
			linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 000, 255, 255), 0.501));
			linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 000, 000, 255), 0.668));
			linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 000, 255), 0.835));
			linearGradientBrush.GradientStops.Add(new GradientStop(Color.FromArgb(255, 255, 000, 000), 0.975));
			_colorSelector.Background = linearGradientBrush;
		}
		/// <summary>
		/// Sets the value of the ValueBoxes.
		/// </summary>
		/// <param name="color"></param>
		private void SetColorInfos(Color color)
		{
			_vbR.Value = color.R;
			_vbG.Value = color.G;
			_vbB.Value = color.B;
			_cbRGB.Text = color.R.ToString() + ", " + color.G.ToString() + ", " + color.B.ToString();
			_cbHEX.Text = string.Format("{0:X8}", color.ToString());
		}
		/// <summary>
		/// Release the mouse when cliped.
		/// </summary>
		public void ReleaseMouse()
		{
			MouseCliping.Release();
		}
		#endregion

		#region Events

		#region ColorSelector
		private void _ColorSelectorUnit_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed) {
				//Sets the new position of the color selector stylus.
				_stylus.Margin = new Thickness(0, e.GetPosition(_ColorSelectorUnit).Y - 4, 0, 0);
				//Sets the background of the base color.
				_BaseColor.Background = new SolidColorBrush(MouseControling.PixelUnderMouse());
				//Gets the pixel color from the point mousePos.
				Color color = MouseControling.PixelColor(_mousePos);
				//Sets the current color property
				SetValue(CurrentColorProperty, color);
				//Sets the _currentColor.Background (_currentColor has been created of the methode AddColorBar ad is an border).
				_currentColor.Background = new SolidColorBrush(color);
			}
		}

		private void _ColorSelectorUnit_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			MouseCliping.OnUIElement(_colorSelectorCliping);
			_ColorSelectorUnit.Cursor = Cursors.None;
			//Gets a point which represents the center of the PickerStylus -> verry important because this is used on
			//ColorSelectorUnit_MouseMove to calculate the color.
			_mousePos = _PickerStylus.PointToScreen(new Point(7.5, 7.5));
			//Sets the mouse in the centere of stylus.
			MouseControling.SetOnUIElement(new Point(_stylus.Width / 2, (_stylus.Height / 2) - 1), _stylus);
		}

		private void _ColorSelectorUnit_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			//Release the mouse cliping.
			MouseCliping.Release();
			//Sets the cursor to arrow.
			_ColorSelectorUnit.Cursor = Cursors.Arrow;
			//Sets the new position of the stylus.
			_stylus.Margin = new Thickness(0, e.GetPosition(_ColorSelectorUnit).Y - 4, 0, 0);
		}

		private void _ColorSelectorUnit_MouseEnter(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed) {
				//the same as on _ColorSelectorUnit_PreviewMouseLeftButtonDown
				MouseCliping.OnUIElement(_colorSelectorCliping);
				_ColorSelectorUnit.Cursor = Cursors.None;
				_mousePos = _PickerStylus.PointToScreen(new Point(7.5, 7.5));
				MouseControling.SetOnUIElement(new Point(_stylus.Width / 2, (_stylus.Height / 2) - 1), _stylus);
			}
		}
		#endregion

		#region ColorPicker
		private void _ColorPickerUnit_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed) {
				//Sets the position of the PickerStylus.
				_PickerStylus.Margin = new Thickness(e.GetPosition(_colorPickerCliping).X - 7.5, e.GetPosition(_colorPickerCliping).Y - 7.5, 0, 0);
				//Sets the CurrentColorProperty to the color under the mouse.
				SetValue(CurrentColorProperty, MouseControling.PixelUnderMouse());
			}
		}

		private void _ColorPickerUnit_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			//Clips the cursor to the _ColorPickerCliping which is an Canvas.
			MouseCliping.OnUIElement(_colorPickerCliping);
			//Sets the visibility of the _PickerStylus.
			_PickerStylus.Visibility = Visibility.Collapsed;
			//Sets the cursor of the _ColorPickerUnit to Pen.
			_ColorPickerUnit.Cursor = Cursors.Pen;
			//Sets the mouse position to the centere of the _PickerStylus.
			MouseControling.SetOnUIElement(new Point(_PickerStylus.Width / 2, (_PickerStylus.Height / 2) - 1), _PickerStylus);
			//Sets the LastColorProperty.
			SetValue(LastColorProperty, CurrentColor);
			//_lastColor.Background which is an border gets the _currentColorBackground
			_lastColor.Background = _currentColor.Background;
		}

		private void _ColorPickerUnit_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			//Release the mouse cliping.
			MouseCliping.Release();
			//Sets the cursor to arrow
			_ColorPickerUnit.Cursor = Cursors.Arrow;
			//Sets the position of the _PickerStylus.
			_PickerStylus.Margin = new Thickness(e.GetPosition(_colorPickerCliping).X - 7.5, e.GetPosition(_colorPickerCliping).Y - 7.5, 0, 0);
			//Sets the visibility of the _PickerStylus
			_PickerStylus.Visibility = Visibility.Visible;
		}

		private void _ColorPickerUnit_MouseEnter(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed) {
				//Clips the mouse to the _colorPicerCliping.
				MouseCliping.OnUIElement(_colorPickerCliping);
				//Visibility of _PickerStylus
				_PickerStylus.Visibility = Visibility.Collapsed;
				//Sets the cursor of _ColorPickerUnit.
				_ColorPickerUnit.Cursor = Cursors.Pen;
				//Sets the mouse position to the centere of _PickerStylus
				MouseControling.SetOnUIElement(new Point(_PickerStylus.Width / 2, (_PickerStylus.Height / 2) - 1), _PickerStylus);
			}
		}

		#endregion
		private static void OnCurrentColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			ColorPicker cp = (ColorPicker)o;

			//Calls SetColorInfo which takes an color. 
			cp.SetColorInfos((Color)e.NewValue);
			//Sets the _currentColor.Background.
			cp._currentColor.Background = new SolidColorBrush((Color)e.NewValue);
			//If you use this Control and add an Event to LastColorChanged it would be thrown from here.
			cp._vbB.RaiseEvent(new RoutedEventArgs(CurrentColorChangedEvent, (Color)e.NewValue));
		}
		private static void OnLastColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
		{
			ColorPicker cp = (ColorPicker)o;

			//If you use this Control and add an Event to LastColorChanged it would be thrown from here.
			cp._vbB.RaiseEvent(new RoutedEventArgs(LastColorChangedEvent, (Color)e.NewValue));
		}
		#endregion

		public void Connect(int connectionId, object target)
		{
			throw new System.NotImplementedException();
		}
	}
}
