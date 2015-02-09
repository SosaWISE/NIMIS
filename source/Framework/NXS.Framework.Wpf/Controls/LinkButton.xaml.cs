using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NXS.Framework.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for LinkButton.xaml
	/// </summary>
	public partial class LinkButton : UserControl, ICommand
	{
		#region Events

		public event RoutedEventHandler Click;

		#endregion Events

		#region Properties

		#region Dependency
		public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static readonly DependencyProperty TextProperty =
			DependencyProperty.Register("Text", typeof(string), typeof(LinkButton), new UIPropertyMetadata(null));

		public static readonly DependencyProperty CommandParameterProperty =
			DependencyProperty.Register("CommandParameter", typeof(object), typeof(LinkButton));
		
		public static readonly DependencyProperty CommandProperty =
			DependencyProperty.Register("Command", typeof(ICommand), typeof(LinkButton), new PropertyMetadata(new PropertyChangedCallback(OnCommandChanged)));

		public ICommand Command
		{
			get { return (ICommand)GetValue(CommandProperty); }
			set { SetValue(CommandProperty, value); }
		}

		public static readonly DependencyProperty NeedsOverrideProperty =
			DependencyProperty.Register("NeedsOverride", typeof(bool), typeof(LinkButton), new PropertyMetadata(new PropertyChangedCallback(OnNeedsOverrideChanged)));
		public bool NeedsOverride
		{
			get { return (bool)GetValue(NeedsOverrideProperty); }
			set { SetValue(NeedsOverrideProperty, value); }
		}
		private static void OnNeedsOverrideChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LinkButton btn = d as LinkButton;
			bool newValue = (bool)e.NewValue;

			btn.securityShield.Visibility = (newValue) ? Visibility.Visible : Visibility.Collapsed;
			btn.bmpIcon.Visibility =(newValue) ? Visibility.Collapsed : Visibility.Visible;
		}

		public object CommandParameter
		{
			get { return (object)GetValue(CommandParameterProperty); }
			set { SetValue(CommandParameterProperty, value); }
		}



		public BitmapSource IconSource
		{
			get { return (BitmapSource)GetValue(IconSourceProperty); }
			set { SetValue(IconSourceProperty, value); }
		}

		public static readonly DependencyProperty IconSourceProperty =
			DependencyProperty.Register("IconSource", typeof(BitmapSource), typeof(LinkButton));



		#endregion Dependency

		#region ICommand Members

		public bool CanExecute(object parameter)
		{
			if (Command != null)
				return Command.CanExecute(this.CommandParameter ?? parameter);
			return false;
		}

		public void Execute(object parameter)
		{
			if (Command != null)
				Command.Execute(this.CommandParameter ?? parameter);
		}

		public event EventHandler CanExecuteChanged;
		private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LinkButton linkButton = d as LinkButton;
			ICommand oldCommand = e.OldValue as ICommand;
			ICommand newCommand = e.NewValue as ICommand;

			if (oldCommand != null) {
				oldCommand.CanExecuteChanged -= linkButton.CanExecuteChanged;
			}
			if (newCommand != null) {
				newCommand.CanExecuteChanged += linkButton.CanExecuteChanged;
			}
		}

		#endregion

		#region Public
		
		public object Argument { get; set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public LinkButton()
		{
			InitializeComponent();

			this.MouseLeftButtonUp += new MouseButtonEventHandler(LinkButton_MouseLeftButtonUp);
			this.Cursor = Cursors.Hand;

			this.Focusable = true;
			this.IsTabStop = true;
			this.KeyDown += new KeyEventHandler(LinkButton_KeyDown);

			this.CanExecuteChanged += LinkButton_CanExecuteChanged;
		}

		void LinkButton_CanExecuteChanged(object sender, EventArgs e)
		{
			this.IsEnabled = CanExecute(null);
			this.Cursor = this.IsEnabled ? Cursors.Hand : null;
		}

		#endregion Constructors

		#region Methods

		#region Protected

		protected void OnClick(RoutedEventArgs e)
		{
			if (Click != null)
				Click(this, e);
		}

		#endregion Protected

		#region Event Handlers

		private void LinkButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (e.ClickCount == 1) {
				RaiseClick();
			}
		}

		private void LinkButton_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter) {
				RaiseClick();
			}
		}

		private void RaiseClick()
		{
			OnClick(new RoutedEventArgs());

			if (CanExecute(null)) {
				Execute(null);
			}
		}

		#endregion Event Handlers

		#endregion Methods

		public void Connect(int connectionId, object target)
		{
			throw new NotImplementedException();
		}
	}
}