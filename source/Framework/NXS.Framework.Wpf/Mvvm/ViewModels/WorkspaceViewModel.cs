using NXS.Framework.Wpf.ParentChildService;

namespace NXS.Framework.Wpf.Mvvm.ViewModels
{
	public class WorkspaceViewModel : ViewModelBase
	{
		protected ParameterDictionary Arguments { get; private set; }

		public virtual bool MatchesArguments(ParameterDictionary args)
		{
			return true;
		}

		public WorkspaceViewModel(ParameterDictionary args)
		{
			Arguments = args;
		}

		public virtual void Focus()
		{
		}
	}
}
