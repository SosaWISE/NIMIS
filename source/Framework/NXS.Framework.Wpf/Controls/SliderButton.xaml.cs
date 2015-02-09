using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using NXS.Framework.Wpf.Mvvm;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Controls
{
	public enum SlideDirection
	{
		Left,
		Right,
		//Up,
		//Down
	}

	/// <summary>
	///     Interaction logic for SliderButton.xaml
	/// </summary>
	public partial class SliderButton : UserControl, INotifyPropertyChanged
	{
		#region Properties

		#region Dependency

		public static readonly DependencyProperty MarginBufferProperty =
			DependencyProperty.Register("MarginBuffer", typeof (double), typeof (SliderButton), new UIPropertyMetadata(33D));

		public static readonly DependencyProperty TargetElementNameProperty =
			DependencyProperty.Register("TargetElementName", typeof (string), typeof (SliderButton));

		public static readonly DependencyProperty DirectionProperty =
			DependencyProperty.Register("Direction", typeof (SlideDirection), typeof (SliderButton),
				new UIPropertyMetadata(SlideDirection.Left));

		public static readonly DependencyProperty AnimationDurationSecondsProperty =
			DependencyProperty.Register("AnimationDurationSeconds", typeof (double), typeof (SliderButton),
				new UIPropertyMetadata(0.5D));

		public static readonly DependencyProperty CloseIconSourceProperty =
			DependencyProperty.Register("CloseIconSource", typeof (BitmapSource), typeof (SliderButton));

		public static readonly DependencyProperty OpenIconSourceProperty =
			DependencyProperty.Register("OpenIconSource", typeof (BitmapSource), typeof (SliderButton));

		public static readonly DependencyProperty ClosedStateToolTipProperty =
			DependencyProperty.Register("ClosedStateToolTip", typeof (object), typeof (SliderButton));

		public static readonly DependencyProperty OpenStateToolTipProperty =
			DependencyProperty.Register("OpenStateToolTip", typeof (object), typeof (SliderButton));

		#endregion Dependency

		#region Private

		public static PropertyChangedEventArgs IsOpenChangedArgs =
			ObservableHelper.CreateArgs<SliderButton>(param => param.IsOpen);

		private bool _isOpen = true;

		private FrameworkElement _outerContainer;

		private FrameworkElement _target;

		public bool IsOpen
		{
			get { return _isOpen; }
			private set
			{
				if (_isOpen != value)
				{
					_isOpen = value;
					OnPropertyChanged(IsOpenChangedArgs);

					if (_isOpen)
					{
						ToolTip = OpenStateToolTip;
					}
					else
					{
						ToolTip = ClosedStateToolTip;
					}
				}
			}
		}

		private FrameworkElement OuterContainer
		{
			get
			{
				if (_outerContainer == null)
				{
					var curr = Parent as FrameworkElement;
					if (curr != null)
					{
						while (curr != null)
						{
							_outerContainer = curr;
							curr = curr.Parent as FrameworkElement;
						}
					}
					else
					{
						_outerContainer = this;
					}
				}
				return _outerContainer;
			}
		}

		private FrameworkElement Target
		{
			get
			{
				if (_target == null)
				{
					_target = OuterContainer.FindName(TargetElementName) as FrameworkElement;

					if (_target != null)
					{
						OriginalMargin = _target.Margin;
					}
				}
				return _target;
			}
		}

		private Thickness OriginalMargin { get; set; }

		#endregion Private

		#region Public

		private ICommand _closeCommand;
		private ICommand _openCommand;
		private ICommand _toggleCommand;

		public double MarginBuffer
		{
			get { return (double) GetValue(MarginBufferProperty); }
			set { SetValue(MarginBufferProperty, value); }
		}

		public string TargetElementName
		{
			get { return (string) GetValue(TargetElementNameProperty); }
			set { SetValue(TargetElementNameProperty, value); }
		}

		public SlideDirection Direction
		{
			get { return (SlideDirection) GetValue(DirectionProperty); }
			set { SetValue(DirectionProperty, value); }
		}

		public double AnimationDurationSeconds
		{
			get { return (double) GetValue(AnimationDurationSecondsProperty); }
			set { SetValue(AnimationDurationSecondsProperty, value); }
		}

		public BitmapSource CloseIconSource
		{
			get { return (BitmapSource) GetValue(CloseIconSourceProperty); }
			set { SetValue(CloseIconSourceProperty, value); }
		}

		public BitmapSource OpenIconSource
		{
			get { return (BitmapSource) GetValue(OpenIconSourceProperty); }
			set { SetValue(OpenIconSourceProperty, value); }
		}

		public object ClosedStateToolTip
		{
			get { return GetValue(ClosedStateToolTipProperty); }
			set { SetValue(ClosedStateToolTipProperty, value); }
		}

		public object OpenStateToolTip
		{
			get { return GetValue(OpenStateToolTipProperty); }
			set { SetValue(OpenStateToolTipProperty, value); }
		}

		public ICommand CloseCommand
		{
			get
			{
				if (_closeCommand == null)
				{
					_closeCommand = new RelayCommand(param => CloseTarget(), param => IsOpen);
				}
				return _closeCommand;
			}
		}

		public ICommand OpenCommand
		{
			get
			{
				if (_openCommand == null)
				{
					_openCommand = new RelayCommand(param => OpenTarget(), param => !IsOpen);
				}
				return _openCommand;
			}
		}

		public ICommand ToggleCommand
		{
			get
			{
				if (_toggleCommand == null)
				{
					_toggleCommand = new RelayCommand(param => ToggleTarget());
				}
				return _toggleCommand;
			}
		}

		#endregion Public

		#endregion Properties

		#region Constructors

		public SliderButton()
		{
			InitializeComponent();

			// Set data context - for binding in the XAML
			DataContext = this;
		}

		#endregion Constructors

		#region Methods

		#region Private

		private void CloseTarget()
		{
			if (Target != null)
			{
				IsOpen = false;

				Target.BeginAnimation(MarginProperty,
					new ThicknessAnimation(CalculateClosedMargins(), new Duration(TimeSpan.FromSeconds(AnimationDurationSeconds))));
			}
		}

		private void OpenTarget()
		{
			if (Target != null)
			{
				IsOpen = true;

				var targetMargin = new Thickness(OriginalMargin.Left, OriginalMargin.Top, OriginalMargin.Right,
					OriginalMargin.Bottom);
				Target.BeginAnimation(MarginProperty,
					new ThicknessAnimation(targetMargin, new Duration(TimeSpan.FromSeconds(AnimationDurationSeconds))));
			}
		}

		private void ToggleTarget()
		{
			if (IsOpen)
			{
				CloseTarget();
			}
			else
			{
				OpenTarget();
			}
		}

		private Thickness CalculateClosedMargins()
		{
			Thickness result = OriginalMargin;
			if (Target != null)
			{
				switch (Direction)
				{
					case SlideDirection.Left:
						result.Left = MarginBuffer - (OriginalMargin.Left + Target.ActualWidth);
						break;
					case SlideDirection.Right:
						result.Right = MarginBuffer - (OriginalMargin.Right + Target.ActualWidth);
						break;
						//case SlideDirection.Up:
						//    result.Bottom = (this.OriginalMargin.Bottom + this.Target.ActualHeight) - this.MarginBuffer;
						//    break;
						//case SlideDirection.Down:
						//    result.Top = (this.OriginalMargin.Top + this.Target.ActualHeight) - this.MarginBuffer;
						//    break;
				}
			}
			return result;
		}

		#endregion Private

		#endregion Methods

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}

		#endregion

		public void Connect(int connectionId, object target)
		{
			throw new NotImplementedException();
		}
	}
}