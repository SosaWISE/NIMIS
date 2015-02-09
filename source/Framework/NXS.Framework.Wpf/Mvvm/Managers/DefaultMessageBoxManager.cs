using System.Windows;

namespace NXS.Framework.Wpf.Mvvm.Managers
{
	public class DefaultMessageBoxManager : IMessageBoxManager
	{
		#region Constructors

// ReSharper disable once EmptyConstructor
		public DefaultMessageBoxManager()
		{
		}

		#endregion Constructors

		#region Methods

		#region Private

		private void ShowMessage(string message, string dialogTitle, DialogIcon icon)
		{
			MessageBox.Show(message, dialogTitle, MessageBoxButton.OK, GetImage(icon));
		}

		private DialogResult ShowMessage(string message, string dialogTitle, DialogIcon icon, DialogButtonSetup button)
		{
			MessageBoxResult result = MessageBox.Show(message, dialogTitle, GetButton(button), GetImage(icon));
			return GetResult(result);
		}

		private MessageBoxImage GetImage(DialogIcon icon)
		{
			MessageBoxImage image = MessageBoxImage.None;

			switch (icon)
			{
				case DialogIcon.Information:
					image = MessageBoxImage.Information;
					break;
				case DialogIcon.Question:
					image = MessageBoxImage.Question;
					break;
				case DialogIcon.Exclamation:
					image = MessageBoxImage.Exclamation;
					break;
				case DialogIcon.Stop:
					image = MessageBoxImage.Stop;
					break;
				case DialogIcon.Warning:
					image = MessageBoxImage.Warning;
					break;
			}
			return image;
		}

		private MessageBoxButton GetButton(DialogButtonSetup btn)
		{
			MessageBoxButton button = MessageBoxButton.OK;

			switch (btn)
			{
				case DialogButtonSetup.OK:
					button = MessageBoxButton.OK;
					break;
				case DialogButtonSetup.OKCancel:
					button = MessageBoxButton.OKCancel;
					break;
				case DialogButtonSetup.YesNo:
					button = MessageBoxButton.YesNo;
					break;
				case DialogButtonSetup.YesNoCancel:
					button = MessageBoxButton.YesNoCancel;
					break;
			}

			return button;
		}

		private DialogResult GetResult(MessageBoxResult result)
		{
			DialogResult translatedResult = DialogResult.None;

			switch (result)
			{
				case MessageBoxResult.Cancel:
					translatedResult = DialogResult.Cancel;
					break;
				case MessageBoxResult.No:
					translatedResult = DialogResult.No;
					break;
				case MessageBoxResult.None:
					translatedResult = DialogResult.None;
					break;
				case MessageBoxResult.OK:
					translatedResult = DialogResult.OK;
					break;
				case MessageBoxResult.Yes:
					translatedResult = DialogResult.Yes;
					break;
			}

			return translatedResult;
		}

		#endregion Private

		#endregion Methods

		#region IMessageBoxManager Members

		public void ShowError(string message)
		{
			ShowError(message, "Error");
		}

		public void ShowError(string message, string dialogTitle)
		{
			ShowMessage(message, dialogTitle, DialogIcon.Stop);
		}

		public void ShowInformation(string message)
		{
			ShowInformation(message, "Information");
		}

		public void ShowInformation(string message, string dialogTitle)
		{
			ShowMessage(message, dialogTitle, DialogIcon.Information);
		}

		public void ShowWarning(string message)
		{
			ShowWarning(message, "Warning");
		}

		public void ShowWarning(string message, string dialogTitle)
		{
			ShowMessage(message, dialogTitle, DialogIcon.Warning);
		}

		public DialogResult ShowYesNo(string message, string dialogTitle, DialogIcon icon)
		{
			return ShowMessage(message, dialogTitle, icon, DialogButtonSetup.YesNo);
		}

		public DialogResult ShowYesNoCancel(string message, string dialogTitle, DialogIcon icon)
		{
			return ShowMessage(message, dialogTitle, icon, DialogButtonSetup.YesNoCancel);
		}

		public DialogResult ShowOkCancel(string message, string dialogTitle, DialogIcon icon)
		{
			return ShowMessage(message, dialogTitle, icon, DialogButtonSetup.OKCancel);
		}

		#endregion
	}
}