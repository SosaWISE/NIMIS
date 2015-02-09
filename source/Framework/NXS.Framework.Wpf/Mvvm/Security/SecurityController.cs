using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using NXS.Framework.Wpf.Mvvm.Managers;
using NXS.Framework.Wpf.Mvvm.Models;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Lib.Util;
using StructureMap;

namespace NXS.Framework.Wpf.Mvvm.Security
{
	public class SecurityController : IParentSubscriber, INotifyPropertyChanged
	{
		private Func<OverrideResult> _getOverride;

		private IParentCommunicator _parentComm;
		private Dictionary<string, AbstractAction> _abstractActions;
		private ConcreteCommandContainer _concreteCommandContainer;

		#region Properties

		public readonly static PropertyChangedEventArgs UserChangeArgs = ObservableHelper.CreateArgs<SecurityController>(a => a.User);
		private UserSecurityInfo _User;
		public UserSecurityInfo User
		{
			get { return _User; }
			private set
			{
				if (value == null)
					throw new ArgumentNullException("value");

				if (_User != value) {
					_User = value;
					OnPropertyChanged(UserChangeArgs);
				}
			}
		}
		public readonly static PropertyChangedEventArgs OverrideUserChangeArgs = ObservableHelper.CreateArgs<SecurityController>(a => a.OverrideUser);
		private UserSecurityInfo _OverrideUser;
		public UserSecurityInfo OverrideUser
		{
			get { return _OverrideUser; }
			private set
			{
				if (_OverrideUser != value) {
					_OverrideUser = value;
					OnPropertyChanged(OverrideUserChangeArgs);
				}
			}
		}

		public readonly static PropertyChangedEventArgs IsOverridingChangeArgs = ObservableHelper.CreateArgs<SecurityController>(a => a.IsOverriding);
		private bool _IsOverriding;
		public bool IsOverriding
		{
			get { return _IsOverriding; }
			private set
			{
				if (_IsOverriding != value) {
					_IsOverriding = value;
					OnPropertyChanged(IsOverridingChangeArgs);
				}
			}
		}

		public readonly static PropertyChangedEventArgs CurrentUserChangeArgs = ObservableHelper.CreateArgs<SecurityController>(a => a.CurrentUser);
		private UserSecurityInfo _CurrentUser;
		public UserSecurityInfo CurrentUser
		{
			get { return _CurrentUser; }
			private set
			{
				if (value == null) {
					throw new ArgumentNullException("value");
				}

				if (_CurrentUser != value) {

					_CurrentUser = value;

					//set IsOverriding after CurrentUser is set, but notify before
					IsOverriding = (OverrideUser != null);

					if (OverrideUser != null) {
						Debug.Assert(this.OverrideUser == _CurrentUser);
					}

					OnPropertyChanged(CurrentUserChangeArgs);
					OnCurrentUserChanged();
				}
			}
		}

		#endregion //Properties

		#region .ctors

		public SecurityController(UserSecurityInfo user, Func<OverrideResult> getOverride)
			: this(user, getOverride, null)
		{
		}
		public SecurityController(UserSecurityInfo user, Func<OverrideResult> getOverride, IParentCommunicator parentComm)
		{
			if (user == null)
				throw new ArgumentNullException("user");

			SetGetOverrideFunction(getOverride);

			this.User = user;
			_parentComm = parentComm;

			_abstractActions = new Dictionary<string, AbstractAction>(StringComparer.InvariantCultureIgnoreCase);
			_concreteCommandContainer = new ConcreteCommandContainer();

			//upate
			UpdateCurrentUser();

			//wire events
			_concreteCommandContainer.CommandsChanged += _concreteCommandContainer_CommandsChanged;
		}

		#endregion //.ctors

		#region Private Methods

		private void UpdateCurrentUser()
		{
			this.CurrentUser = (this.OverrideUser != null) ? this.OverrideUser : this.User;
		}

		#endregion //Private Methods

		#region Public Methods

		public void SetGetOverrideFunction(Func<OverrideResult> getOverride)
		{
			//if (getOverride == null)
			//    throw new ArgumentNullException("getOverride");

			_getOverride = getOverride;
		}

		/// <summary>
		/// Filter the menu based on the CurrentUser
		/// </summary>
		public List<MenuItem> GetUserMenu(List<MenuItem> menuItemList)
		{
			var result = new List<MenuItem>();

			foreach (var item in menuItemList) {
				//include if it can be overridden or invoked
				if (item.ActionName == null || item.IsOverrideable || CanInvokeAction(item.ActionName)) {
					result.Add(item);
				}
			}

			return result;
		}

		public bool StartOverride(UserSecurityInfo usi)
		{
			if (usi == null) return false;

			if (usi != CurrentUser) {
				if (usi == User) {

					//usi matches logged in user, end impersonation
					StopOverride();
					return false;
				}
				else {

					this.OverrideUser = usi;
					UpdateCurrentUser();
					return true;
				}
			}
			return false;
		}
		public UserSecurityInfo StopOverride()
		{
			UserSecurityInfo usi = this.OverrideUser;
			this.OverrideUser = null;
			UpdateCurrentUser();
			return usi;
		}

		public string GetActionName(InvokeActionArgs args)
		{
			return (args == null) ? null : args.ActionName;
		}

		#endregion //Public Methods

		#region Abstract Actions

		private AbstractAction CreateVirtualAction(string actionName, bool isOverrideable)
		{
			return new AbstractAction(actionName, isOverrideable, this.InvokeAction, this.CanInvokeAction);
		}
		private void UpdateAbstractActionNeedsOverride()
		{
			UserSecurityInfo usi = this.CurrentUser;
			foreach (AbstractAction action in _abstractActions.Values) {
				action.UpdateNeedsOverride(usi);
			}
		}

		public OwnedAbstractAction CreateOwnedAbstractActionGlobal(string actionName, Predicate<InvokeActionArgs> appendCanExecute)
		{
			return CreateOwnedAbstractAction(null, actionName, appendCanExecute);
		}
		public OwnedAbstractAction CreateOwnedAbstractAction(object owner, string actionName, Predicate<InvokeActionArgs> appendCanExecute)
		{
			AbstractAction abstractAction = GetAbstractAction(actionName);
			if (abstractAction == null) return null;

			OwnedAbstractAction ownedAbstractAction = OwnedAbstractAction.CreateFromAbstractAction(owner, abstractAction, appendCanExecute);
			return ownedAbstractAction;
		}

		public AbstractAction GetAbstractAction(string actionName)
		{
			AbstractAction abstractAction;
			if (actionName != null) {
				_abstractActions.TryGetValue(actionName, out abstractAction);
			}
			else {
				abstractAction = null;
			}
			return abstractAction;
		}

		public ConcreteCommand GetConcreteCommand(object owner, string actionName)
		{
			ConcreteCommand cmd = _concreteCommandContainer.GetCommand(owner, actionName);
			return cmd;
		}

		#region Add VirtualAction/Permissions

		public void AddPermission(string actionName, bool isOverrideable, string principalName, int permissionTypeID, bool allowAccess)
		{
			AddPermission(actionName, isOverrideable, principalName, (Permission.PermissionTypes)permissionTypeID, allowAccess);
		}
		public void AddPermission(string actionName, bool isOverrideable, string principalName, Permission.PermissionTypes permissionType, bool allowAccess)
		{
			AbstractAction abstractAction;

			if (!_abstractActions.TryGetValue(actionName, out abstractAction)) {
				abstractAction = CreateVirtualAction(actionName, isOverrideable);
				_abstractActions.Add(actionName, abstractAction);
			}

			Permission p = new Permission()
			{
				PermissionType = (Permission.PermissionTypes)permissionType,
				PrincipalName = principalName,
				AllowAccess = allowAccess,
			};
			abstractAction.Permissions.Add(p);
		}

		#endregion //Add VirtualAction/Permissions

		#region CanInvokeAction

		public bool CanInvokeAction(InvokeActionArgs args)
		{
			return CanInvokeAction(GetActionName(args));
		}
		public bool CanInvokeAction(string actionName)
		{
			return
				//CanInvokeActionLocally(actionName) &&
				(HasPermissionToInvoke(actionName) || CanOverrideAction(actionName))//allow override even without permission. these will be checked in InvokeAction
			;
		}

		public bool CanInvokeActionLocally(object owner, string actionName)
		{
			return
				actionName != null
				&& _concreteCommandContainer.ActionExists(owner, actionName)//concrete action must exist
				&& _abstractActions.ContainsKey(actionName)//abstract action must exist
			;
		}
		public bool HasPermissionToInvoke(string actionName)
		{
			return HasPermissionToInvoke(actionName, CurrentUser);
		}
		public bool HasPermissionToInvoke(string actionName, UserSecurityInfo usi)
		{
			AbstractAction abstractAction;
			return
				actionName != null
				&& _abstractActions.TryGetValue(actionName, out abstractAction)
				&& abstractAction.Permissions.HasPermission(usi)//has permission
			;
		}
		public bool CanOverrideAction(string actionName)
		{
			AbstractAction abstractAction;
			return
				actionName != null
				&& _abstractActions.TryGetValue(actionName, out abstractAction)
				&& abstractAction.IsOverrideable//is overrideable
			;
		}

		#endregion //CanInvokeAction

		#region InvokeAction

		public void InvokeActionFromParent(InvokeActionArgs args)
		{
			InvokeAction(args, false);
		}
		public void InvokeActionGlobal(string actionName, ParameterDictionary arguments)
		{
			InvokeAction(new InvokeActionArgs(null, actionName, arguments));
		}
		public void InvokeAction(InvokeActionArgs args)
		{
			InvokeAction(args, true);
		}
		public void InvokeAction(InvokeActionArgs args, bool canInvokeParent)
		{
			if (!args.IsHandled) {

				bool handled = false;

				string actionName = GetActionName(args);
				//is this action invocable locally
				if (CanInvokeActionLocally(args.Owner, actionName)) {

					OverrideResult overrideResult = null;
					UserSecurityInfo executingUser = null;
					if (HasPermissionToInvoke(actionName)) {
						executingUser = CurrentUser;
					}
					else if (CanOverrideAction(actionName))
					{
						//current user doesn't have permission
						//	try to get a user that does have permission
						overrideResult = GetOverridingUser(actionName);
						if (overrideResult != null) {

							if (StartOverride(overrideResult.User)) {
								executingUser = CurrentUser;

								Debug.Assert(executingUser == overrideResult.User);
							}
							else {
								//override didn't start, nullify result
								overrideResult = null;
							}
						}

						// If the override has been tried - just count it as handled
						handled = true;
					}

					if (executingUser != null) {

						ExecutionArgs executionArgs = new ExecutionArgs(/*executingUser, */args.Arguments);

						ConcreteCommand cmd = GetConcreteCommand(args.Owner, actionName);
						if (cmd.CanExecute(executionArgs)) {
							cmd.Execute(executionArgs);
							handled = true;
						}
					}

					//stop override unless otherwise specified
					if (overrideResult != null && !overrideResult.IsPermanent) {
						StopOverride();
					}
				}
				
				if (!handled && canInvokeParent && _parentComm != null) {
					// We can't handle it so bubble the action up to the parent
					handled = _parentComm.InvokeActionOnParent(args);
				}

				args.IsHandled = handled;
			}
		}

		private OverrideResult GetOverridingUser(string actionName)
		{
			OverrideResult result;

			if (_getOverride != null && CanOverrideAction(actionName)) {

				while (true) {

					result = _getOverride();

					if (result == null || HasPermissionToInvoke(actionName, result.User)) {
						break;
					}
					else {
						ObjectFactory.GetInstance<IMessageBoxManager>()
							.ShowWarning(string.Format("{0} does not have sufficient rights to perform override.", result.User.Username), "Insufficient Rights");
						result = null;
					}
				}
			}
			else {
				//can't override or unable to get override user
				result = null;
			}

			return result;
		}
		#endregion //InvokeAction

		#endregion //Abstract Actions

		#region Concrete Commands

		public void RegisterConcreteCommandDictionaryGlobal(ConcreteCommandDictionary ccd)
		{
			RegisterConcreteCommandDictionary(null, ccd);
		}
		public void RegisterConcreteCommandDictionary(object owner, ConcreteCommandDictionary ccd)
		{
			_concreteCommandContainer.Add(owner, ccd);
		}

		public bool UnregisterConcreteCommandDictionaryGlobal()
		{
			return UnregisterConcreteCommandDictionary(null);
		}
		public bool UnregisterConcreteCommandDictionary(object owner)
		{
			return _concreteCommandContainer.Remove(owner);
		}

		void _concreteCommandContainer_CommandsChanged(object sender, EventArgs e)
		{
			UpdateAbstractActionNeedsOverride();
		}

		#endregion //Concrete Commands

		#region Events

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null) {
				PropertyChanged(this, e);
			}
		}

		#endregion //INotifyPropertyChanged Members

		public event EventHandler CurrentUserChanged;
		private void OnCurrentUserChanged()
		{
			UpdateAbstractActionNeedsOverride();
			if (CurrentUserChanged != null) {
				CurrentUserChanged.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion //Events

		public void AddSubscriber(ISecuritySubscriber subscriber)
		{
			//unregister first
			subscriber.UnregisterConcreteCommands(this);
			//register
			subscriber.RegisterConcreteCommands(this);
			//let the subscriber set its local commands
			subscriber.SetLocalCommands(this);
		}
		public void RemoveSubscriber(ISecuritySubscriber subscriber)
		{
			subscriber.UnregisterConcreteCommands(this);
		}
	}
}
