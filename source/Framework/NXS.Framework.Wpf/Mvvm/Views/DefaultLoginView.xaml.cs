using System.Windows;
using System.Windows.Controls;
using NXS.Framework.Wpf.Mvvm.ViewModels;

namespace NXS.Framework.Wpf.Mvvm.Views
{
	public partial class DefaultLoginView : UserControl
	{
		public DefaultLoginView()
		{
			InitializeComponent();

			this.DataContextChanged += DefaultLoginView_DataContextChanged;
		}

		void DefaultLoginView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			DefaultLoginViewModel oldValue = e.OldValue as DefaultLoginViewModel;
			if (oldValue != null) {
				oldValue.InvalidLogin -= dataContext_InvalidLogin;
			}

			DefaultLoginViewModel newValue = e.NewValue as DefaultLoginViewModel;
			if (newValue != null) {
				newValue.InvalidLogin += dataContext_InvalidLogin;
			}
		}

		void dataContext_InvalidLogin(object sender, System.EventArgs e)
		{
			txtPassword.Focus();
			txtPassword.SelectAll();
		}

		private void txtSelectAll_GotFocus(object sender, RoutedEventArgs e)
		{
			ViewHelper.TextBoxSelectAll(sender);
			ViewHelper.PasswordBoxSelectAll(sender);
		}

		private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
		{
			DefaultLoginViewModel vm = (DefaultLoginViewModel)this.DataContext;
			vm.Model.Password.Value = txtPassword.Password;
		}

		public void Connect(int connectionId, object target)
		{
			throw new System.NotImplementedException();
		}
	}
}
