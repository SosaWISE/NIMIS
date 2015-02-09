using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace NXS.Framework.Wpf.Controls
{
	/// <summary>
	///     Interaction logic for SmallLoadingAnimation.xaml
	/// </summary>
// ReSharper disable once RedundantExtendsListEntry
	public partial class SmallLoadingAnimation : UserControl
	{
		#region Properties

		#region Private

		private Storyboard _animationStoryboard;

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

		#endregion Properties

		#region Constructors

		static SmallLoadingAnimation()
		{
			VisibilityProperty.OverrideMetadata(typeof (SmallLoadingAnimation),
				new PropertyMetadata(Visibility.Collapsed, OnVisibilityChanged));
		}

		public SmallLoadingAnimation()
		{
			InitializeComponent();
		}

		#endregion Constructors

		#region Methods

		#region Private

		private void PauseAnimation()
		{
			if (AnimationStoryboard != null && IsRunning)
			{
				AnimationStoryboard.Pause(this);
				IsRunning = false;
			}
		}

		private void StartAnimation()
		{
			if (AnimationStoryboard != null && !IsRunning)
			{
				AnimationStoryboard.Begin(this, true);
				IsRunning = true;
			}
		}

		#endregion Private

		#region Public

		public void Show()
		{
			Visibility = Visibility.Visible;
		}

		public void Hide()
		{
			Visibility = Visibility.Collapsed;
		}

		public static void OnVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var animation = d as SmallLoadingAnimation;
			if (animation != null && e.NewValue is Visibility)
			{
				switch ((Visibility) e.NewValue)
				{
					case Visibility.Visible:
						animation.StartAnimation();
						break;
					case Visibility.Hidden:
					case Visibility.Collapsed:
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