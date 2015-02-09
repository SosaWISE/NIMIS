namespace NXS.Framework.Wpf.Mvvm.Managers
{
	public enum DialogButtonSetup
	{
		OK,
		OKCancel,
		YesNo,
		YesNoCancel
	}

	public enum DialogIcon
	{
		None,
		Information,
		Question,
		Exclamation,
		Stop,
		Warning
	}

	public enum DialogResult
	{
		None,
		OK,
		Cancel,
		Yes,
		No
	}

	public interface IMessageBoxManager
	{
		void ShowError(string message);
		void ShowError(string message, string dialogTitle);

		void ShowInformation(string message);
		void ShowInformation(string message, string dialogTitle);

		void ShowWarning(string message);
		void ShowWarning(string message, string dialogTitle);

		DialogResult ShowYesNo(string message, string dialogTitle, DialogIcon icon);
		DialogResult ShowYesNoCancel(string message, string dialogTitle, DialogIcon icon);
		DialogResult ShowOkCancel(string message, string dialogTitle, DialogIcon icon);

	}
}