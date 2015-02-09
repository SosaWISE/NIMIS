using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Interop;
using SOS.Lib.Util;

namespace NXS.Framework.Wpf.ParentChildService
{
	public interface IParentCommunicator
	{
		void InvokeAction(InvokeActionArgs args);
		bool CanInvokeAction(InvokeActionArgs args);

		bool InvokeActionOnParent(InvokeActionArgs args);
	}
	public class ParentCommunicator : IParentCommunicator, IChildWindow, IDisposable
	{
		#region Properties

		#region Private

		protected readonly WindowApplicationChild<ParentCommunicator> _childService;
		protected readonly IApplicationParent _parent;

		List<IParentSubscriber> _parentSubscribers = new List<IParentSubscriber>();

		#endregion Private

		#region Protected

		WindowInteropHelper _interopHelper;
		protected WindowInteropHelper InteropHelper
		{
			get
			{
				if (_interopHelper == null) {
					_interopHelper = new WindowInteropHelper(MainWindow);
				}
				return _interopHelper;
			}
		}

		protected bool IsParentAvailable { get; set; }

		#endregion Protected

		#region Public

		public Guid ChildID { get; protected set; }

		public Window MainWindow { get; private set; }

		#endregion Public

		#endregion Properties

		#region Constructors

		public ParentCommunicator(Window mainWindow)
		{
			if (mainWindow == null)
				throw new ArgumentNullException("mainWindow");

			this.MainWindow = mainWindow;

			// Initialize the child controller
			_childService = new WindowApplicationChild<ParentCommunicator>(this);

			// Get the Child ID from the arguments
			ChildID = _childService.GetChildWindowIDParameter();
			if (ChildID != Guid.Empty) {
				// Start the service for communication
				_childService.InitializeService();

				// Get a reference to the parent and notify the parent that we're ready
				_parent = _childService.RegisterParent();
				if (_parent != null) {
					try {
						_parent.NotifyChildReady(ChildID);
						IsParentAvailable = true;
					}
					catch {
						IsParentAvailable = false;
					}
				}
			}
			else {
				IsParentAvailable = false;
			}
		}

		#endregion Constructors

		#region Methods

		#region Protected

		public bool GetStartupAction(out string startupAction, out ParameterDictionary arguments)
		{
			arguments = GetStartupParameterDictionary();
			return GetStartupAction(arguments, out startupAction);
		}
		public bool GetStartupAction(ParameterDictionary arguments, out string startupAction)
		{
			return TryGetArgument(arguments, ParameterNames.ActionName, out startupAction);
		}
		public bool GetVersion(ParameterDictionary arguments, out string version)
		{
			return TryGetArgument(arguments, ParameterNames.Version, out version);
		}

		public static bool TryGetArgument(ParameterDictionary arguments, string argumentName, out string argumentValue)
		{
			// Look for the initial action to run
			if (arguments.HasParameter(argumentName)) {

				argumentValue = arguments.GetParameterValue(argumentName);

				if (!string.IsNullOrEmpty(argumentValue)) {

					return true;
				}
			}
			argumentValue = null;
			return false;
		}
		public ParameterDictionary GetStartupParameterDictionary()
		{
			// Read arguments passed in
			return new ParameterDictionary(StringUtility.GetParameters(Environment.GetCommandLineArgs()));
		}

		#endregion Protected

		#endregion Methods

		#region IChildWindow Members

		public void ParentClosed()
		{
			IsParentAvailable = false;

			MainWindow.Show();
		}
		public long GetMainWindowHandle()
		{
			return InteropHelper.Handle.ToInt64();
		}
		/// <summary>
		/// Invokes the action on the first subscriber
		/// </summary>
		public void InvokeAction(string actionName, ParameterDictionary arguments)
		{
			InvokeActionArgs args = new InvokeActionArgs(actionName, arguments);
			InvokeAction(args);
		}
		public void InvokeAction(InvokeActionArgs args)
		{
			foreach (IParentSubscriber ps in _parentSubscribers) {

				if (args.IsHandled) {
					break;
				}

				if (ps.CanInvokeAction(args)) {
					ps.InvokeAction(args);
				}
			}
		}

		public bool CanInvokeAction(string actionName)
		{
			return CanInvokeAction(new InvokeActionArgs(actionName, null));
		}
		/// <summary>
		/// Returns true if at least one of the subscriber returns true
		/// </summary>
		/// <param name="actionName"></param>
		/// <returns></returns>
		public bool CanInvokeAction(InvokeActionArgs args)
		{
			bool result = false;

			foreach (IParentSubscriber ps in _parentSubscribers) {

				if (ps.CanInvokeAction(args)) {

					result = true;//return true on first
					break;
				}
			}

			return result;
		}
		
		public void Close()
		{
			MainWindow.Close();
		}
		public WindowState WindowState
		{
			get { return MainWindow.WindowState; }
			set { MainWindow.WindowState = value; }
		}

		public void ShowWindow()
		{
			MainWindow.Show();
			MainWindow.Activate();

			if (MainWindow.WindowState == WindowState.Minimized)
			{
				MainWindow.WindowState = WindowState.Normal;
			}
		}

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			// Make sure the child service gets terminated
			_childService.TerminateService();
		}

		#endregion

		public void InvokeActionOnParent(string actionName, ParameterDictionary arguments)
		{
			InvokeActionOnParent(new InvokeActionArgs(actionName, arguments));
		}
		public bool InvokeActionOnParent(InvokeActionArgs args)
		{
			if (IsParentAvailable) {

				try {
					_parent.InvokeAction(args.ActionName, args.Arguments);
					return true;
				}
				catch { }
			}
			return false;
		}

		public void SubscribeToParentInvocations(IParentSubscriber subscriber)
		{
			if (subscriber != null) {
				_parentSubscribers.Add(subscriber);
			}
		}
		public void UnsubscribeFromParentInvocations(IParentSubscriber subscriber)
		{
			_parentSubscribers.Remove(subscriber);
		}
	}
}
