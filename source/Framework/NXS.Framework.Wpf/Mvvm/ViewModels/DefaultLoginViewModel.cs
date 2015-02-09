using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using NXS.Framework.Wpf.Mvvm.Models;
using NXS.Framework.Wpf.Mvvm.Security;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm.ViewModels
{
	public class DefaultLoginViewModel : CloseableViewModel
	{
		#region Fields

		private Func<string, string, bool> _isValidateUsernameAndPassword;
		private Func<string, List<string>> _getGroupsForUser;

		#endregion //Fields

		#region Properties

		//Binding Model
		public LoginModel Model { get; private set; }

		public UserSecurityInfo User { get; private set; }

		public static readonly PropertyChangedEventArgs IsInvalidLoginChangeArgs = ObservableHelper.CreateArgs<DefaultLoginViewModel>(x => x.IsInvalidLogin);
		private bool _IsInvalidLogin;
		public bool IsInvalidLogin
		{
			get { return _IsInvalidLogin; }
			private set
			{
				if (_IsInvalidLogin != value) {
					_IsInvalidLogin = value;
					OnPropertyChanged(IsInvalidLoginChangeArgs);
				}
			}
		}

		#endregion //Properties

		#region Commands

		private RelayCommand _TryLoginCommand;
		public ICommand TryLoginCommand
		{
			get { return _TryLoginCommand ?? (_TryLoginCommand = new RelayCommand(param => this.TryLogin(), param => Model.IsValid)); }
		}
		private RelayCommand _CancelCommand;
		public ICommand CancelCommand
		{
			get { return _CancelCommand ?? (_CancelCommand = new RelayCommand(param => this.Cancel())); }
		}

		#endregion //Commands

		#region .ctors

		public DefaultLoginViewModel(string displayName, Func<string, string, bool> isValidateUsernameAndPassword, Func<string, List<string>> getGroupsForUser)
			: base(null)
		{
			if (isValidateUsernameAndPassword == null)
				throw new ArgumentNullException("isValidateUsernameAndPassword");
			if (getGroupsForUser == null)
				throw new ArgumentNullException("getGroupsForUser");

			this.DisplayName = displayName;
			_isValidateUsernameAndPassword = isValidateUsernameAndPassword;
			_getGroupsForUser = getGroupsForUser;

			this.Model = new LoginModel();

			Load();
		}

		private void Reload()
		{
			Unload();
			Load();
		}
		private void Unload()
		{
			//reset model
			Model.Reset();
		}
		private void Load()
		{
			//do nothing

			//set clean after binding
			CleanAndValidateModel();
		}
		private void CleanAndValidateModel()
		{
			Model.Clean();
			Model.RunValidation();
		}

		#endregion //.ctors

		#region Actions

		void TryLogin()
		{
			IsInvalidLogin = false;
			User = null;

			string username = this.Model.Username.Value;
			string password = this.Model.Password.Value;

			IsInvalidLogin = !_isValidateUsernameAndPassword(username, password);
			if (!IsInvalidLogin) {

				User = new UserSecurityInfo(username, _getGroupsForUser(username));

				RaiseRequestCloseWorkSpace(true);
			}
			else {
				OnInvalidLogin();
			}
		}

		private void Cancel()
		{
			User = null;
			RaiseRequestCloseWorkSpace(false);
		}

		#endregion //Actions

		public event EventHandler InvalidLogin;
		private void OnInvalidLogin()
		{
			EventHandler handler = InvalidLogin;
			if (handler != null) {
				handler(this, EventArgs.Empty);
			}
		}
	}
}
