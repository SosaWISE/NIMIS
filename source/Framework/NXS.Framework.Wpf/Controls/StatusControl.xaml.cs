using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NXS.Framework.Wpf.Controls
{
	/// <summary>
	///     Interaction logic for StatusControl.xaml
	/// </summary>
	public partial class StatusControl : UserControl
	{
		#region Properties

		// Using a DependencyProperty as the backing store for StatusText.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty StatusTextProperty =
			DependencyProperty.Register("StatusText", typeof (string), typeof (StatusControl), new UIPropertyMetadata("INVALID"));


		// Using a DependencyProperty as the backing store for ItemStatus.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty ItemStatusProperty =
			DependencyProperty.Register("ItemStatus", typeof (ControlStatus), typeof (StatusControl),
				new UIPropertyMetadata(ControlStatus.INVALID, OnStatusChanged));

		private readonly Brush StatusBadBrush = new SolidColorBrush(Color.FromArgb(255, 183, 11, 11));
		private readonly Brush StatusGoodBrush = new SolidColorBrush(Color.FromArgb(255, 0, 149, 10));
		private readonly Brush StatusWarningBrush = new SolidColorBrush(Color.FromArgb(255, 183, 183, 0));

		public string StatusText
		{
			get { return (string) GetValue(StatusTextProperty); }
			set { SetValue(StatusTextProperty, value); }
		}

		public ControlStatus ItemStatus
		{
			get { return (ControlStatus) GetValue(ItemStatusProperty); }
			set { SetValue(ItemStatusProperty, value); }
		}

		#endregion Properties

		#region Constructors

		public StatusControl()
		{
			InitializeComponent();
			BrdrStatus.Background = StatusBadBrush;
		}

		#endregion Constructors

		#region Methods

		private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var cntrl = (StatusControl) d;
			var status = (ControlStatus) e.NewValue;

			switch (status)
			{
				case ControlStatus.INVALID:
					cntrl.BrdrStatus.Background = cntrl.StatusBadBrush;
					break;
				case ControlStatus.VALID:
					cntrl.BrdrStatus.Background = Brushes.Green;
					break;
				case ControlStatus.WARNING:
					cntrl.BrdrStatus.Background = Brushes.Yellow;
					break;
				default:
					cntrl.BrdrStatus.Background = Brushes.Red;
					break;
			}
		}

		#endregion Methods

		public void Connect(int connectionId, object target)
		{
			throw new System.NotImplementedException();
		}
	}

	public enum ControlStatus
	{
		VALID,
		INVALID,
		WARNING
	}
}