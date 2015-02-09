using System.Windows.Input;
using NXS.Framework.Wpf.ParentChildService;

namespace NXS.Framework.Wpf.Mvvm.ViewModels
{
	public abstract class MainWindowViewModel : CloseableViewModel
	{
		public virtual InputBindingCollection InputBindings { get; protected set; }

		#region Constructor

		public MainWindowViewModel(ParameterDictionary args)
			: base(args)
		{
		}

		#endregion // Constructor
	}
}
