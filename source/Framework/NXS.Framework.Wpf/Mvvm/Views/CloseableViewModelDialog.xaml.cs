using System.Windows;
using System.Windows.Input;

namespace NXS.Framework.Wpf.Mvvm.Views
{
	/// <summary>
	/// Interaction logic for CloseableViewModelDialog.xaml
	/// </summary>
	public partial class CloseableViewModelDialog : Window
	{
		public bool CanEnterClose { get; set; }
		public bool CanEscapeClose { get; set; }
		public ICommand EscapeCloseWindowCommand { get; private set; }
		public ICommand EnterCloseWindowCommand { get; private set; }

		public Visibility CloseButtonVisibility
		{
			get { return btnClose.Visibility; }
			set { btnClose.Visibility = value; }
		}

		public CloseableViewModelDialog()
		{
			InitializeComponent();

			EscapeCloseWindowCommand = new RelayCommand((param) => this.Close(), (param) => CanEscapeClose);
			this.InputBindings.Add(new InputBinding(EscapeCloseWindowCommand, new KeyGesture(Key.Escape, ModifierKeys.None)));

			EnterCloseWindowCommand = new RelayCommand((param) => this.Close(), (param) => CanEnterClose);
			this.InputBindings.Add(new InputBinding(EnterCloseWindowCommand, new KeyGesture(Key.Enter, ModifierKeys.None)));
		}

		private void cbContent_HeaderMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			DragMove();
		}

		public void Connect(int connectionId, object target)
		{
			throw new System.NotImplementedException();
		}
	}
}
