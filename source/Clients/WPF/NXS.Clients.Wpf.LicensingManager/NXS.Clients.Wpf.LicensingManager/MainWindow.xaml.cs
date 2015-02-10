using System.Windows;
using NXS.Framework.Wpf.Controls;

namespace NXS.Clients.Wpf.LicensingManager
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

			Loaded += new RoutedEventHandler(MainWindow_Loaded);
		}

		void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if (App.CurrentApp.Settings.IsMenuSliderOpen != sbtnMenuContainer.IsOpen)
			{

				double d = sbtnMenuContainer.AnimationDurationSeconds;
				sbtnMenuContainer.AnimationDurationSeconds = 0;
				sbtnMenuContainer.ToggleCommand.Execute(null);
				sbtnMenuContainer.AnimationDurationSeconds = d;
			}
		}

		private void SliderButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (string.Compare(e.PropertyName, SliderButton.IsOpenChangedArgs.PropertyName) == 0)
			{
				App.CurrentApp.Settings.IsMenuSliderOpen = sbtnMenuContainer.IsOpen;
			}
		}
	}
}
