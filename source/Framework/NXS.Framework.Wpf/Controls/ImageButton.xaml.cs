using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NXS.Framework.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for ImageButton.xaml
	/// </summary>
	public partial class ImageButton : UserControl, ICommand
	{
		#region Events

		public event RoutedEventHandler Click;

		#endregion Events

		#region Properties

		#region DependencyProperties

		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand), typeof(ImageButton), new PropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));
		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}
		public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(ICommand), typeof(ImageButton));
		public object CommandParameter
		{
			get { return GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}

		public static readonly DependencyProperty NeedsOverrideProperty =
			DependencyProperty.Register("NeedsOverride", typeof(bool), typeof(ImageButton), new PropertyMetadata(new PropertyChangedCallback(OnNeedsOverrideChanged)));
		public bool NeedsOverride
		{
			get { return (bool)GetValue(NeedsOverrideProperty); }
			set { SetValue(NeedsOverrideProperty, value); }
		}
		private static void OnNeedsOverrideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ImageButton btn = d as ImageButton;
			bool newValue = (bool)e.NewValue;

			btn.securityShield.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
		}

		#endregion //DependencyProperties

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			if (Command != null)
				return Command.CanExecute(parameter);
			return false;
		}

		public void Execute(object parameter)
		{
			if (Command != null)
				Command.Execute(parameter);
		}

		public event EventHandler CanExecuteChanged;
		private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ImageButton btn = d as ImageButton;
			ICommand oldCommand = e.OldValue as ICommand;
			ICommand newCommand = e.NewValue as ICommand;

			if (oldCommand != null) {
				oldCommand.CanExecuteChanged -= btn.CanExecuteChanged;
			}
			if (newCommand != null) {
				newCommand.CanExecuteChanged += btn.CanExecuteChanged;
			}
		}

		#endregion

		#region Public

		public string Text
		{
			get { return tbText.Text; }
			set
			{
				tbText.Text = value;
				tbText.Visibility = string.IsNullOrEmpty(value) ? Visibility.Collapsed : Visibility.Visible;
			}
		}
		string _CheckedText;
		public string CheckedText
		{
			get { return _CheckedText; }
			set
			{
				if (_CheckedText != value) {
					_CheckedText = value;
					UpdateText(this);
				}
			}
		}
		string _UnCheckedText;
		public string UnCheckedText
		{
			get { return _UnCheckedText; }
			set
			{
				if (_UnCheckedText != value) {
					_UnCheckedText = value;
					UpdateText(this);
				}
			}
		}

		public BitmapSource IconSource
		{
			get { return bmpIcon.Source; }
			set { bmpIcon.Source = value; }
		}
		public double ImageWidth
		{
			get { return bmpIcon.Width; }
			set { bmpIcon.Width = value; }
		}
		public double ImageHeight
		{
			get { return bmpIcon.Height; }
			set { bmpIcon.Height = value; }
		}

		public bool CanCheck { get; set; }

		public static readonly DependencyProperty IsCheckedProperty =
			DependencyProperty.Register("IsChecked", typeof(bool?), typeof(ImageButton),
				new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Journal | FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
					, new PropertyChangedCallback(ImageButton.OnIsCheckedChanged))
			);
		public bool IsChecked
		{
			get { return (bool)GetValue(IsCheckedProperty); }
			set { SetValue(IsCheckedProperty, value); }
		}
		private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ImageButton btn = d as ImageButton;
			UpdateText(btn);
		}
		private static void UpdateText(ImageButton btn)
		{
			if (btn != null) {

				if (btn.IsChecked) {
					if (!string.IsNullOrEmpty(btn.CheckedText)) {
						btn.Text = btn.CheckedText;
					}
				}
				else {
					if (!string.IsNullOrEmpty(btn.UnCheckedText)) {
						btn.Text = btn.UnCheckedText;
					}
				}

				btn.SetStyle();
			}
		}

		public object Argument { get; set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public ImageButton()
		{
			InitializeComponent();

			Text = string.Empty;

			this.Cursor = Cursors.Hand;
			border.Style = _defaultStyle;

			this.Focusable = true;
			this.IsTabStop = true;
			this.KeyDown += ImageButton_KeyDown;

			this.CanExecuteChanged += ImageButton_CanExecuteChanged;
		}

		void ImageButton_CanExecuteChanged(object sender, EventArgs e)
		{
			this.IsEnabled = CanExecute(CommandParameter);
			this.Cursor = this.IsEnabled ? Cursors.Hand : null;
			this.tbText.Foreground = this.IsEnabled ? _enabledBrush : _disabledBrush;
		}

		#endregion Constructors

		#region Methods

		#region Protected

		private void OnClick()
		{
			if (!this.IsEnabled) return;

			IsChecked = (CanCheck) && !IsChecked;

			if (Click != null) {
				Click(this, new RoutedEventArgs());
			}

			object parameter = CommandParameter;
			if (CanExecute(parameter)) {
				Execute(parameter);
			}
		}

		#endregion Protected

		#region Event Handlers

		private void ImageButton_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter
				|| e.Key == Key.Space) {
				e.Handled = true;
				OnClick();
			}
		}

		#endregion Event Handlers

		#endregion Methods

		#region Style

		static Brush _enabledBrush;
		static Brush _disabledBrush;

		bool _isPressed;
		bool _isHovering;

		static Style _defaultStyle;
		static Style _lightStyle;
		static Style _darkTopStyle;
		static Style _darkBottomStyle;

		static ImageButton()
		{
			_enabledBrush = Brushes.Black;
			_disabledBrush = Brushes.Gray;

			Style style;

			Setter paddingSetter = new Setter(Border.PaddingProperty, new Thickness(2));
			Setter marginSetter = new Setter(Border.MarginProperty, new Thickness(0));
			Setter borderThicknessSetter = new Setter(Border.BorderThicknessProperty, new Thickness(1, 1, 1, 1));

			//default
			style = _defaultStyle = new Style();
			style.Setters.Add(paddingSetter);
			style.Setters.Add(marginSetter);
			style.Setters.Add(borderThicknessSetter);

			Setter borderBrushSetter = new Setter(Border.BorderBrushProperty, new SolidColorBrush(Color.FromArgb(255, 118, 172, 227)));
			Setter cornerRadiusSetter = new Setter(Border.CornerRadiusProperty, new CornerRadius(1, 1, 1, 1));

			//light
			style = _lightStyle = new Style();
			style.Setters.Add(paddingSetter);
			style.Setters.Add(marginSetter);
			style.Setters.Add(borderThicknessSetter);
			LinearGradientBrush hoverGradient = new LinearGradientBrush(Color.FromArgb(255, 238, 244, 251), Color.FromArgb(255, 193, 216, 241), new Point(0.5, 0), new Point(0.5, 1));
			style.Setters.Add(new Setter(Border.BackgroundProperty, hoverGradient));
			style.Setters.Add(borderBrushSetter);
			style.Setters.Add(cornerRadiusSetter);

			//darkTop
			style = _darkTopStyle = new Style();
			style.Setters.Add(paddingSetter);
			style.Setters.Add(marginSetter);
			style.Setters.Add(borderThicknessSetter);
			LinearGradientBrush darkTopGradient = new LinearGradientBrush(Color.FromArgb(255, 149, 200, 252), Color.FromArgb(255, 193, 216, 241), new Point(0.5, 0), new Point(0.5, 1));
			style.Setters.Add(new Setter(Border.BackgroundProperty, darkTopGradient));
			style.Setters.Add(borderBrushSetter);
			style.Setters.Add(cornerRadiusSetter);

			//darkBottom
			style = _darkBottomStyle = new Style();
			style.Setters.Add(paddingSetter);
			style.Setters.Add(marginSetter);
			style.Setters.Add(borderThicknessSetter);
			LinearGradientBrush darkBottomGradient = new LinearGradientBrush(Color.FromArgb(255, 193, 216, 241), Color.FromArgb(255, 149, 200, 252), new Point(0.5, 0), new Point(0.5, 1));
			style.Setters.Add(new Setter(Border.BackgroundProperty, darkBottomGradient));
			style.Setters.Add(borderBrushSetter);
			style.Setters.Add(cornerRadiusSetter);
		}

		#region Overrides

		protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
		{
			e.Handled = true;
			base.Focus();
			if (e.ButtonState == MouseButtonState.Pressed) {

				base.CaptureMouse();
				if (base.IsMouseCaptured) {

					if (e.ButtonState == MouseButtonState.Pressed) {

						if (!this._isPressed) {
							this.SetIsPressed(true);
						}
					}
				}
			}

			SetStyle();

			base.OnMouseLeftButtonDown(e);
		}
		protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
		{
			e.Handled = true;

			bool flag = this._isPressed;
			if (base.IsMouseCaptured) {
				base.ReleaseMouseCapture();
			}
			if (flag) {
				this.SetIsPressed(false);
				this.OnClick();
			}

			SetStyle();

			base.OnMouseLeftButtonUp(e);
		}

		protected override void OnMouseEnter(MouseEventArgs e)
		{
			base.OnMouseEnter(e);

			SetIsHovering(true);
			SetStyle();
		}
		protected override void OnMouseLeave(MouseEventArgs e)
		{
			base.OnMouseLeave(e);

			SetIsHovering(false);
			SetStyle();
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (base.IsMouseCaptured && (Mouse.PrimaryDevice.LeftButton == MouseButtonState.Pressed)) {

				Point position = Mouse.PrimaryDevice.GetPosition(this);
				if (((position.X >= 0.0) && (position.X <= border.ActualWidth)) && ((position.Y >= 0.0) && (position.Y <= border.ActualHeight))) {
					if (!this._isPressed) {
						this.SetIsPressed(true);
					}
				}
				else if (this._isPressed) {
					this.SetIsPressed(false);
				}

				SetStyle();

				e.Handled = true;
			}
		}

		#endregion //Overrides

		private void SetIsPressed(bool value)
		{
			_isPressed = value;
		}
		private void SetIsHovering(bool value)
		{
			_isHovering = value;
		}

		private void SetStyle()
		{
			Style s = _defaultStyle;

			if (this.IsEnabled) {

				if (IsChecked) {

					if (_isHovering && _isPressed) {
						s = _darkBottomStyle;
					}
					else if (_isHovering || _isPressed) {
						s = _darkTopStyle;
					}
					else {
						s = _darkBottomStyle;
					}
				}
				else {

					if (_isHovering && _isPressed) {
						s = _darkTopStyle;
					}
					else if (_isHovering || _isPressed) {
						s = _lightStyle;
					}
					else {
						s = _defaultStyle;
					}
				}
			}

			border.Style = s;
		}

		#endregion //Style

		//#region INotifyPropertyChanged Members

		//public event PropertyChangedEventHandler PropertyChanged;
		//protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		//{
		//    if (PropertyChanged != null) {
		//        PropertyChanged(this, e);
		//    }
		//}

		//#endregion
		public void Connect(int connectionId, object target)
		{
			throw new NotImplementedException();
		}
	}
}
