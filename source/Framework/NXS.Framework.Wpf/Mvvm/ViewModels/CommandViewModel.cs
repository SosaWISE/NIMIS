using System;
using System.Windows.Input;

namespace NXS.Framework.Wpf.Mvvm.ViewModels
{
	/// <summary>
	/// Represents an actionable item displayed by a View.
	/// </summary>
	public class CommandViewModel : ViewModelBase
	{
		public CommandViewModel(string displayName, ICommand command)
		{
			if (command == null)
				throw new ArgumentNullException("command");

			DisplayName = displayName;
			Command = command;
		}

		public ICommand Command { get; private set; }
	}
}
