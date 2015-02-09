using System;
using System.ComponentModel;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	/// <summary>
	/// Command that will be bound to the UI
	/// </summary>
	public class AbstractAction : RelayCommand<InvokeActionArgs>, INotifyPropertyChanged
	{
		#region Fields

		public readonly string ActionName;
		public readonly bool IsOverrideable;
		public readonly Action<InvokeActionArgs> ExecuteAction;
		public readonly Predicate<InvokeActionArgs> CanExecuteAction;
		public readonly ParameterDictionary Arguments;

		public readonly PermissionList Permissions;

		#endregion //Fields

		#region Properties

		public readonly static PropertyChangedEventArgs NeedsOverrideChangeArgs = ObservableHelper.CreateArgs<AbstractAction>(a => a.NeedsOverride);
		private bool _NeedsOverride;
		public bool NeedsOverride
		{
			get { return _NeedsOverride; }
			private set
			{
				if (_NeedsOverride != value) {
					_NeedsOverride = value;
					OnPropertyChanged(NeedsOverrideChangeArgs);
				}
			}
		}

		#endregion //Properties

		#region .ctors

		public AbstractAction(string actionName, bool isOverrideable, Action<InvokeActionArgs> execute, Predicate<InvokeActionArgs> canExecute)
			: this(actionName, isOverrideable, execute, canExecute, null)
		{
		}
		public AbstractAction(string actionName, bool isOverrideable, Action<InvokeActionArgs> execute, Predicate<InvokeActionArgs> canExecute, ParameterDictionary arguments)
			: base((param) => InvokeAction(execute, actionName, arguments), (param) => CanInvokeAction(canExecute, actionName, arguments))
		{
			if (actionName == null)
				throw new ArgumentNullException("actionName");

			this.ActionName = actionName;
			this.IsOverrideable = isOverrideable;
			this.ExecuteAction = execute;
			this.CanExecuteAction = canExecute;
			this.Arguments = arguments;

			this.Permissions = new PermissionList();
		}

		private static void InvokeAction(Action<InvokeActionArgs> execute, string actionName, ParameterDictionary arguments)
		{
			if (execute != null) {
				InvokeActionArgs args = new InvokeActionArgs(actionName, arguments);
				execute(args);
			}
		}
		private static bool CanInvokeAction(Predicate<InvokeActionArgs> canExecute, string actionName, ParameterDictionary arguments)
		{
			if (canExecute != null) {
				InvokeActionArgs args = new InvokeActionArgs(actionName, arguments);
				return canExecute(args);
			}
			return true;
		}

		#endregion //.ctors

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, e);
			}
		}

		#endregion //INotifyPropertyChanged Members

		#region Public Methods

		public void UpdateNeedsOverride(UserSecurityInfo usi)
		{
			this.NeedsOverride = IsOverrideable && !this.Permissions.HasPermission(usi);
		}

		#endregion //Public Methods
	}
}
