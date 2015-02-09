using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using NXS.Framework.Wpf.Mvvm.Managers;
using NXS.Framework.Wpf.Mvvm.Security;
using NXS.Framework.Wpf.Mvvm.ViewModels;
using NXS.Framework.Wpf.ParentChildService;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util;
using StructureMap;

namespace NXS.Framework.Wpf.Mvvm
{
	public abstract class ViewModelBase : IDisposable, INotifyPropertyChanged
	{
		private static string _appVersion;
		public static string AppVersion
		{
			get { return _appVersion; }
			set
			{
				if (value == _appVersion) return;

				_appVersion = value;
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#region Fields

		private RelayCommand<object> _activatedCommand;
		private RelayCommand<object> _deactivatedCommand;
		private RelayCommand<object> _loadedCommand;
		private RelayCommand<object> _unloadedCommand;
		private RelayCommand<CancelEventArgs> _closeCommand;
		private RelayCommand<CancelEventArgs> _closingCommand;

		/// <summary>
		/// This event should be raised to close the view.  Any view tied to this
		/// ViewModel should register a handler on this event and close itself when
		/// this event is raised.  If the view is not bound to the lifetime of the
		/// ViewModel then this event can be ignored.
		/// </summary>
		//public event EventHandler<EventArgs> CloseRequest;

		/// <summary>
		/// This event should be raised to activate the UI.  Any view tied to this
		/// ViewModel should register a handler on this event and close itself when
		/// this event is raised.  If the view is not bound to the lifetime of the
		/// ViewModel then this event can be ignored.
		/// </summary>
		public event EventHandler<EventArgs> ActivateRequest;

		#endregion // Fields

		#region Properties

		public virtual bool ModelIsDirty { get { return false; } }

		#region Private

		private IErrorManager _errorManager;
		private IDialogManager _dialogManager;
		private IMessageBoxManager _messageBoxManager;
		private IOpenFileManager _openFileManager;
		private ISaveFileManager _saveFileManager;

		#endregion Private

		#region Protected

		protected IErrorManager ErrorManager
		{
			get
			{
				if (_errorManager == null)
					_errorManager = ObjectFactory.GetInstance<IErrorManager>();
				return _errorManager;
			}
		}

		protected IDialogManager DialogManager
		{
			get
			{
				if (_dialogManager == null)
					_dialogManager = ObjectFactory.GetInstance<IDialogManager>();
				return _dialogManager;
			}
		}

		protected IMessageBoxManager MessageBoxManager
		{
			get
			{
				if (_messageBoxManager == null)
					_messageBoxManager = ObjectFactory.GetInstance<IMessageBoxManager>();
				return _messageBoxManager;
			}
		}

		protected IOpenFileManager OpenFileManager
		{
			get
			{
				if (_openFileManager == null)
					_openFileManager = ObjectFactory.GetInstance<IOpenFileManager>();
				return _openFileManager;
			}
		}

		protected ISaveFileManager SaveFileManager
		{
			get
			{
				if (_saveFileManager == null)
					_saveFileManager = ObjectFactory.GetInstance<ISaveFileManager>();
				return _saveFileManager;
			}
		}

		protected bool SetErrorWindowOwner { get; set; }

		#endregion Protected

		//#region Public
		//
		//public virtual InputBindingCollection InputBindings { get; protected set; }
		//
		//#endregion //Public

		#endregion Properties

		#region Commands

		/// <summary>
		/// ActivatedCommand : View Lifetime command
		/// </summary>
		public ICommand ActivatedCommand
		{
			get { return _activatedCommand ?? (_activatedCommand = new RelayCommand<object>(param => this.OnViewActivated())); }
		}
		/// <summary>
		/// DeactivatedCommand : View Lifetime command
		/// </summary>
		public ICommand DeactivatedCommand
		{
			get { return _deactivatedCommand ?? (_deactivatedCommand = new RelayCommand<object>(param => this.OnViewDeactivated())); }
		}
		/// <summary>
		/// LoadedCommand : View Lifetime command
		/// </summary>
		public ICommand LoadedCommand
		{
			get { return _loadedCommand ?? (_loadedCommand = new RelayCommand<object>(param => this.OnViewLoaded())); }
		}
		/// <summary>
		/// UnloadedCommand : View Lifetime command
		/// </summary>
		public ICommand UnloadedCommand
		{
			get { return _unloadedCommand ?? (_unloadedCommand = new RelayCommand<object>(param => this.OnViewDeactivated())); }
		}
		/// <summary>
		/// Returns the command that, when invoked, attempts to remove this workspace from the user interface.
		/// </summary>
		public RelayCommand<CancelEventArgs> CloseCommand
		{
			get { return _closeCommand ?? (_closeCommand = new RelayCommand<CancelEventArgs>(param => this.OnViewClose(), this.CanClose)); }
		}
		/// <summary>
		/// Returns the command that is invoked when the view is closing. Use this to cancel the close and/or clean up before the close
		/// </summary>
		public ICommand ClosingCommand
		{
			get { return _closingCommand ?? (_closingCommand = new RelayCommand<CancelEventArgs>(param => this.OnViewClosing(param))); }
		}

		#endregion // Commands

		#region Constructor

		protected ViewModelBase()
		{
			this.SetErrorWindowOwner = true;
		}

		#endregion // Constructor

		#region DisplayName

		public readonly static PropertyChangedEventArgs DisplayNameChangeArgs = ObservableHelper.CreateArgs<ViewModelBase>(a => a.DisplayName);
		private string _displayName;
		/// <summary>
		/// Returns the user-friendly name of this object. Child classes can set this property to a new value.
		/// </summary>
		public string DisplayName
		{
			get { return _displayName; }
			set
			{
				if (_displayName != value) {

					_displayName = value;
					OnPropertyChanged(DisplayNameChangeArgs);
				}
			}
		}

		#endregion // DisplayName

		#region Debugging Aides

		/// <summary>
		/// Returns whether an exception is thrown, or if a Debug.Fail() is used
		/// when an invalid property name is passed to the VerifyPropertyName method.
		/// The default value is false, but subclasses used by unit tests might 
		/// override this property's getter to return true.
		/// </summary>
		protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

		#endregion // Debugging Aides

		#region IDisposable Members

		/// <summary>
		/// Invoked when this object is being removed from the application
		/// and will be subject to garbage collection.
		/// </summary>
		public void Dispose()
		{
			this.OnDispose();
		}

		/// <summary>
		/// Child classes can override this method to perform 
		/// clean-up logic, such as removing event handlers.
		/// </summary>
		protected virtual void OnDispose()
		{
		}

#if DEBUG
		/// <summary>
		/// Useful for ensuring that ViewModel objects are properly garbage collected.
		/// </summary>
		~ViewModelBase()
		{
			string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
			System.Diagnostics.Debug.WriteLine(msg);
		}
#endif

		#endregion // IDisposable Members

		#region Public/Protected Methods/Events

		///// <summary>
		///// This raises the CloseRequest event to close the UI.
		///// </summary>
		//public virtual void RaiseCloseWorkSpaceRequest()
		//{
		//    EventHandler<EventArgs> handlers = CloseRequest;

		//    // Invoke the event handlers
		//    if (handlers != null) {
		//        try {
		//            handlers(this, EventArgs.Empty);
		//        }
		//        catch (Exception ex) {
		//            LogExceptionIfLoggerAvailable(ex);
		//        }
		//    }
		//}

		/// <summary>
		/// This raises the ActivateRequest event to activate the UI.
		/// </summary>
		public virtual void RaiseActivateRequest()
		{
			EventHandler<EventArgs> handlers = ActivateRequest;

			// Invoke the event handlers
			if (handlers != null) {
				try {
					handlers(this, EventArgs.Empty);
				}
				catch (Exception ex) {
					LogExceptionIfLoggerAvailable(ex);
				}
			}
		}

		private static readonly object _syncRootDisplayMessages = new object();
		public void DisplayErrorMessages()
		{
			DisplayErrorMessages(this.ErrorManager, this.DialogManager, this.SetErrorWindowOwner);

			//ErrorMessageDisplayViewModel viewModel = null;
			//
			//if (ErrorManager != null && ErrorManager.MessageCount > 0)
			//{
			//    // Make sure only one thread can do this at a time
			//    lock (_syncRootDisplayMessages)
			//    {
			//        if (ErrorManager != null && ErrorManager.MessageCount > 0)
			//        {
			//            // Create the ViewModel
			//            viewModel = new ErrorMessageDisplayViewModel(new ExecutionArgs(ParameterDictionary.Empty), ErrorManager, GetVersionDisplayName("Messages"));
			//
			//            // Save and clear messages
			//            ErrorManager.PersistMessages();
			//            ErrorManager.ClearMessages();
			//        }
			//    }
			//}
			//
			//// Show a dialog if a ViewModel was created
			//if (viewModel != null)
			//{
			//    Window owner = (SetErrorWindowOwner) ? Application.Current.MainWindow : null;
			//    DialogManager.ShowDialog(viewModel, owner, 600, 300, null, true, true);
			//}
		}
		public static void DisplayErrorMessages(IErrorManager errorManager, IDialogManager dialogManager, bool setErrorWindowOwner)
		{
			ErrorMessageDisplayViewModel viewModel = null;

			if (errorManager != null && errorManager.MessageCount > 0) {
				// Make sure only one thread can do this at a time
				lock (_syncRootDisplayMessages) {
					if (errorManager != null && errorManager.MessageCount > 0) {
						// Create the ViewModel
						viewModel = new ErrorMessageDisplayViewModel(new ExecutionArgs(ParameterDictionary.Empty), errorManager, GetVersionDisplayName("Messages"));

						// Save and clear messages
						errorManager.PersistMessages();
						errorManager.ClearMessages();
					}
				}
			}

			// Show a dialog if a ViewModel was created
			if (viewModel != null) {
				Window owner = (setErrorWindowOwner) ? Application.Current.MainWindow : null;
				dialogManager.ShowDialog(viewModel, owner, 600, 300, null, true, true, null);
			}
		}

		public static string GetVersionDisplayName(string displayName)
		{
			return string.IsNullOrEmpty(AppVersion) ? displayName : string.Format("{0} - v. {1}", displayName, AppVersion);
		}

		protected void LogAndDisplayException(Exception ex)
		{
			AsyncHelper.ExecuteSync(
				delegate()
				{
					ErrorManager.AddCriticalMessage(ex);
					DisplayErrorMessages();
				});
		}

		#endregion //Public/Protected Methods/Events

		#region Window hook methods
		/// <summary>
		/// Allows Window.Activated hook
		/// </summary>
		protected virtual void OnViewActivated() { }
		/// <summary>
		/// Allows Window.Deactivated hook
		/// </summary>
		protected virtual void OnViewDeactivated() { }
		/// <summary>
		/// Allows Window.Loaded/UserControl.Loaded hook
		/// </summary>
		protected virtual void OnViewLoaded() { }
		/// <summary>
		/// Allows Window.Unloaded/UserControl.Unloaded hook
		/// </summary>
		protected virtual void OnViewUnloaded() { }
		/// <summary>
		/// Allows Window.Closed hook
		/// </summary>
		protected virtual void OnViewClose() { }
		/// <summary>
		/// Determine whether the CloseCommand can be executed
		/// </summary>
		protected virtual bool CanClose(CancelEventArgs cea)
		{
			return cea == null || !cea.Cancel;
		}
		/// <summary>
		/// Allows Window.Closing hook
		/// </summary>
		protected virtual void OnViewClosing(CancelEventArgs cea) { }

		#endregion //Window hook methods

		#region Private Methods
		/// <summary>
		/// Logs a message if there is a ILoggerService available. And then throws
		/// new ApplicationException which should be caught somewhere external
		/// to this class
		/// </summary>
		/// <param name="ex">Exception to log</param>
		private static void LogExceptionIfLoggerAvailable(Exception ex)
		{
			//if (logger != null)
			//    logger.Log(LogType.Error, ex);

			throw new ApplicationException(ex.Message);
		}
		#endregion //Private Methods

		protected virtual void OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}
	}
}
