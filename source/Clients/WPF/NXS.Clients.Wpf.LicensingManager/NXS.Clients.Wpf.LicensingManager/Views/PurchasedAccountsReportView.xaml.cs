using System.Windows.Controls;
using System.Windows.Input;
using NXS.Clients.Wpf.LicensingManager.ViewModels;

namespace NXS.Clients.Wpf.LicensingManager.Views
{
	/// <summary>
	/// Interaction logic for PurchasedAccountsReportView.xaml
	/// </summary>
	public partial class PurchasedAccountsReportView : UserControl
	{
		public PurchasedAccountsReportView()
		{
			InitializeComponent();
		}

		private void UIItem_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			var vm = (PurchasedAccountsReportViewModel)this.DataContext;
			if (e.Key == Key.Enter && vm.LoadReportCommand.CanExecute(null))
			{
				vm.LoadReportCommand.Execute(null);
			}
		}
	}
}
