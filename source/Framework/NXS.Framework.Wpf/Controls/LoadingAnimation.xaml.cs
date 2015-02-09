using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NXS.Framework.Wpf.Controls
{
	/// <summary>
	/// Interaction logic for LoadingAnimation.xaml
	/// </summary>
	public partial class LoadingAnimation : UserControl
	{
		#region Properties

		#region Dependency

		public static DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(LoadingAnimation), new FrameworkPropertyMetadata("LOADING"));

		#endregion Dependency

		#region Private

		private Storyboard _animationStoryboard = null;
		private Storyboard AnimationStoryboard
		{
			get
			{
				if (_animationStoryboard == null)
				{
					_animationStoryboard = FindResource("CircleAnimation") as Storyboard;
				}
				return _animationStoryboard;
			}
		}

		private bool IsRunning { get; set; }

		#endregion Private

		#region Public

		public string Text
		{
			get { return GetValue(TextProperty) as string; }
			set { SetValue(TextProperty, value); }
		}

		#endregion Public

		#endregion Properties

		#region Constructors

		static LoadingAnimation()
		{
			VisibilityProperty.OverrideMetadata(typeof(LoadingAnimation), new PropertyMetadata(Visibility.Collapsed, new PropertyChangedCallback(OnVisibilityChanged)));
		}

		public LoadingAnimation()
		{
			InitializeComponent();

			tbLabel.DataContext = this;
		}

		#endregion Constructors

		#region Methods

		#region Private

		private void PauseAnimation()
		{
			if (this.AnimationStoryboard != null && this.IsRunning)
			{
				this.AnimationStoryboard.Pause(this);
				this.IsRunning = false;
			}
		}

		private void StartAnimation()
		{
			if (this.AnimationStoryboard != null && !this.IsRunning)
			{
				this.AnimationStoryboard.Begin(this, true);
				this.IsRunning = true;
			}
		}

		#endregion Private

		#region Public

		public void Show()
		{
			this.Visibility = Visibility.Visible;
		}

		public void Hide()
		{
			this.Visibility = Visibility.Collapsed;
		}

		public static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			LoadingAnimation animation = d as LoadingAnimation;
			if (animation != null && e.NewValue is Visibility)
			{
				switch ((Visibility)e.NewValue)
				{
					case Visibility.Visible :
						animation.StartAnimation();
						break;
					case Visibility.Hidden :
					case Visibility.Collapsed :
						animation.PauseAnimation();
						break;
				}
			}
		}

		#endregion Public

		#endregion Methods

		public void Connect(int connectionId, object target)
		{
			throw new NotImplementedException();
		}
	}
}