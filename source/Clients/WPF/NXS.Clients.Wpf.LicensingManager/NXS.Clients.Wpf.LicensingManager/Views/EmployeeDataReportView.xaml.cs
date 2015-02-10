using System.Windows.Controls;
using System.Windows.Input;
using NXS.Clients.Wpf.LicensingManager.ViewModels;

namespace NXS.Clients.Wpf.LicensingManager.Views
{
	/// <summary>
	/// Interaction logic for EmployeeDataReportView.xaml
	/// </summary>
// ReSharper disable once RedundantExtendsListEntry
	public partial class EmployeeDataReportView : UserControl
	{
		public EmployeeDataReportView()
		{
			InitializeComponent();
		}

		private void UIItem_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			var vm = (EmployeeDataReportViewModel)DataContext;
			if (e.Key == Key.Enter && vm.LoadReportCommand.CanExecute(null))
			{
				vm.LoadReportCommand.Execute(null);
			}
		}
	}
}
