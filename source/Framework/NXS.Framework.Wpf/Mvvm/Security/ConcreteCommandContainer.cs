using System;
using System.Collections.Generic;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	public class ConcreteCommandContainer
	{
		public event EventHandler CommandsChanged;

		Dictionary<object, ConcreteCommandDictionary> _dict;

		public ConcreteCommandContainer()
		{
			_dict = new Dictionary<object, ConcreteCommandDictionary>();
		}

		public void Add(object owner, ConcreteCommandDictionary ccd)
		{
			if (ccd == null)
				return;

			owner = GetOwner(owner);

			ccd.CommandsChanged += CommandsChanged;//relay event
			_dict.Add(owner, ccd);
			OnCommandsChanged();
		}
		public bool Remove(object owner)
		{
			owner = GetOwner(owner);

			bool result = false;

			ConcreteCommandDictionary ccd;
			if (_dict.TryGetValue(owner, out ccd)) {

				ccd.CommandsChanged -= CommandsChanged;
				result = _dict.Remove(owner);
				OnCommandsChanged();
			}

			return result;
		}

		public bool ActionExists(string actionName)
		{
			bool result = false;
			foreach (object owner in _dict.Keys) {
				if (ActionExists(owner, actionName)) {
					result = true;
					break;
				}
			}
			return result;
		}
		public bool ActionExists(object owner, string actionName)
		{
			owner = GetOwner(owner);

			bool result = false;

			ConcreteCommandDictionary ccd;
			if (_dict.TryGetValue(owner, out ccd)) {

				if (ccd.ActionExists(actionName)) {
					result = true;
				}
			}

			return result;
		}
		public ConcreteCommand GetCommand(string actionName)
		{
			ConcreteCommand result = null;
			foreach (object owner in _dict.Keys) {

				result = GetCommand(owner, actionName);
				if (result != null) {
					break;
				}
			}
			return result;
		}
		public ConcreteCommand GetCommand(object owner, string actionName)
		{
			owner = GetOwner(owner);

			ConcreteCommand result = null;

			ConcreteCommandDictionary ccd;
			if (_dict.TryGetValue(owner, out ccd)) {

				result = ccd.GetCommand(actionName);
			}

			return result;
		}

		private object GetOwner(object owner)
		{
			return owner ?? this;
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
