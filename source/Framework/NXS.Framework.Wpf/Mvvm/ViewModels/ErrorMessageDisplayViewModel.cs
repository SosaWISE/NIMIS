using System.Collections.Generic;
using NXS.Framework.Wpf.Mvvm.Security;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Lib.Core.ErrorHandling;

namespace NXS.Framework.Wpf.Mvvm.ViewModels
{
	public class ErrorMessageDisplayViewModel : CloseableViewModel
	{
		public List<IErrorMessage> ErrorMessages { get; private set; }

		public ErrorMessageDisplayViewModel(ExecutionArgs args, IErrorManager errorManager, string displayName)
			: base((args != null) ? args.Arguments : ParameterDictionary.Empty)
		{
			DisplayName = displayName ?? "Messages";

			if (errorManager.MessageCount > 0)
			{
				ErrorMessages = new List<IErrorMessage>(errorManager.ErrorMessages);
				ErrorMessages.Sort((x, y) => x.Type.CompareTo(y.Type));
			}
		}
	}
}
