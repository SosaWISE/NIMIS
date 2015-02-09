using System;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	/// <summary>
	/// Command used for mapping an action to a concrete implementation. This is only for mapping and shouldn't be bound to the UI.
	/// </summary>
	public class ConcreteCommand : RelayCommand<ExecutionArgs>
	{
		#region Fields

		public readonly string ActionName;

		#endregion //Fields

		#region Properties
		#endregion //Properties

		#region .ctors

		public ConcreteCommand(string actionName, Action<ExecutionArgs> execute)
			: this(actionName, execute, null)
		{
		}
		public ConcreteCommand(string actionName, Action<ExecutionArgs> execute, Predicate<ExecutionArgs> canExecute)
			: base(execute, canExecute)
		{
			this.ActionName = actionName;
		}

		#endregion //.ctors
	}
}
