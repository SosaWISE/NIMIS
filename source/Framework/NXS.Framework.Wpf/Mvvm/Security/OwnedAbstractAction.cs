using System;
using System.ComponentModel;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	/// <summary>
	/// Command that will be bound to the UI
	/// </summary>
	public class OwnedAbstractAction : RelayCommand<InvokeActionArgs>, INotifyPropertyChanged
	{
		#region Fields

		public readonly object Owner;
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

		public OwnedAbstractAction(string actionName, bool isOverrideable, Action<InvokeActionArgs> execute, Predicate<InvokeActionArgs> canExecute)
			: this(null, actionName, isOverrideable, execute, canExecute, null)
		{
		}
		public OwnedAbstractAction(string actionName, bool isOverrideable, Action<InvokeActionArgs> execute, Predicate<InvokeActionArgs> canExecute, ParameterDictionary arguments)
			: this(null, actionName, isOverrideable, execute, canExecute, arguments)
		{
		}

		public OwnedAbstractAction(object owner, string actionName, bool isOverrideable, Action<InvokeActionArgs> execute, Predicate<InvokeActionArgs> canExecute)
			: this(owner, actionName, isOverrideable, execute, canExecute, null)
		{
		}
		public OwnedAbstractAction(object owner, string actionName, bool isOverrideable, Action<InvokeActionArgs> execute, Predicate<InvokeActionArgs> canExecute, ParameterDictionary arguments)
			: base((param) => InvokeAction(owner, execute, actionName, arguments), (param) => CanInvokeAction(owner, canExecute, actionName, arguments))
		{
			if (actionName == null)
				throw new ArgumentNullException("actionName");

			this.Owner = owner;
			this.ActionName = actionName;
			this.IsOverrideable = isOverrideable;
			this.ExecuteAction = execute;
			this.CanExecuteAction = canExecute;
			this.Arguments = arguments;

			this.Permissions = new PermissionList();
		}

		private static void InvokeAction(object owner, Action<InvokeActionArgs> execute, string actionName, ParameterDictionary arguments)
		{
			if (execute != null) {
				InvokeActionArgs args = new InvokeActionArgs(owner, actionName, arguments);
				execute(args);
			}
		}
		private static bool CanInvokeAction(object owner, Predicate<InvokeActionArgs> canExecute, string actionName, ParameterDictionary arguments)
		{
			if (canExecute != null) {
				InvokeActionArgs args = new InvokeActionArgs(owner, actionName, arguments);
				return canExecute(args);
			}
			return true;
		}

		#endregion //.ctors

		public static OwnedAbstractAction CreateFromAbstractAction(object owner, AbstractAction abstractAction, Predicate<InvokeActionArgs> appendCanExecute)
		{
			Predicate<InvokeActionArgs> canExecute;
			if (appendCanExecute == null) {
				canExecute = abstractAction.CanExecuteAction;
			}
			else {
				canExecute = (param) => abstractAction.CanExecuteAction(param) && appendCanExecute(param);
			}

			OwnedAbstractAction result = new OwnedAbstractAction(owner, abstractAction.ActionName
				, abstractAction.IsOverrideable, abstractAction.ExecuteAction, canExecute, abstractAction.Arguments);

			result.NeedsOverride = abstractAction.NeedsOverride;
			abstractAction.PropertyChanged += result.AbstractAction_PropertyChanged;

			return result;
		}

		void AbstractAction_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (string.Compare(AbstractAction.NeedsOverrideChangeArgs.PropertyName, e.PropertyName, true) == 0) {
				this.NeedsOverride = ((AbstractAction)sender).NeedsOverride;
			}
		}

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
