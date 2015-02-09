using System.Windows.Input;

namespace NXS.Framework.Wpf
{
	public interface ICommand<in T> : ICommand
	{
		bool CanExecute(T parameter);
		void Execute(T parameter);
	}
}
