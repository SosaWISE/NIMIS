using System;
using System.Collections.Generic;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	public class ConcreteCommandDictionary
	{
		Dictionary<string, ConcreteCommand> _dict;

		public event EventHandler CommandsChanged;

		public ConcreteCommandDictionary()
		{
			_dict = new Dictionary<string, ConcreteCommand>(StringComparer.InvariantCultureIgnoreCase);
		}

		public bool ActionExists(string actionName)
		{
			return _dict.ContainsKey(actionName);
		}
		public ConcreteCommand GetCommand(string actionName)
		{
			ConcreteCommand result = null;
			_dict.TryGetValue(actionName, out result);
			return result;
		}
		public ConcreteCommand Add(string actionName, Action<ExecutionArgs> execute)
		{
			return Add(actionName, execute, null);
		}
		public ConcreteCommand Add(string actionName, Action<ExecutionArgs> execute, Predicate<ExecutionArgs> canExecute)
		{
			ConcreteCommand cmd = new ConcreteCommand(actionName, execute, canExecute);
			Add(cmd);
			return cmd;
		}
		public void Add(ConcreteCommand concreteCommand)
		{
			if (!_dict.ContainsKey(concreteCommand.ActionName)) {

				_dict.Add(concreteCommand.ActionName, concreteCommand);
				OnCommandsChanged();
			}
		}
		public void Clear()
		{
			if (_dict.Count > 0) {
				_dict.Clear();
				OnCommandsChanged();
			}
		}

		private void OnCommandsChanged()
		{
			EventHandler handler = CommandsChanged;
			if (handler != null) {
				handler(this, EventArgs.Empty);
			}
		}
	}
}
