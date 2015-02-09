using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using PPro.Framework.WpfFramework.Mvvm;

namespace PPro.Framework.WpfFramework.ParentChildService
{
	public class InvokeActionCommand : RelayCommand<ParameterDictionary>
	{
		#region Properties

		#region Private

		private string _label;

		#endregion Private

		#region Public

		public string Label
		{
			get { return _label; }
			set { _label = value; }
		}

		#endregion Public

		#endregion Properties

		#region Constructors

		public InvokeActionCommand(Action<ParameterDictionary> execute)
			: this(execute, null, null)
		{
		}

		public InvokeActionCommand(Action<ParameterDictionary> execute, string label)
			: this(execute, null, label)
		{
		}

		public InvokeActionCommand(Action<ParameterDictionary> execute, Predicate<ParameterDictionary> canExecute)
			: this(execute, canExecute, null)
		{
		}

		public InvokeActionCommand(Action<ParameterDictionary> execute, Predicate<ParameterDictionary> canExecute, string label)
			: base(execute, canExecute)
		{

			Label = label;
		}

		#endregion Constructors
	}
}
