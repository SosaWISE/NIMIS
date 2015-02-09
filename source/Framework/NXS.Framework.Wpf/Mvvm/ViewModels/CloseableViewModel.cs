using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using NXS.Framework.Wpf.ParentChildService;

namespace NXS.Framework.Wpf.Mvvm.ViewModels
{
	public abstract class CloseableViewModel : WorkspaceViewModel
	{
		#region Fields

		RelayCommand<bool?> _closeWorkSpaceCommand;

		#endregion // Fields

		#region Properties

		public bool AllowManualClose { get; set; }

		#endregion Properties

		#region Constructor

		protected CloseableViewModel(ParameterDictionary args) : base(args)
		{
			AllowManualClose = true;
		}

		#endregion // Constructor

		#region CloseWorkSpaceCommand

		/// <summary>
		/// Returns the command that, when invoked, attempts
		/// to remove this workspace from the user interface.
		/// </summary>
		public ICommand CloseWorkSpaceCommand
		{
			get
			{
				return _closeWorkSpaceCommand ??
					(_closeWorkSpaceCommand = new RelayCommand<bool?>(param => RaiseRequestCloseWorkSpace(param.HasValue && param.Value), param => CanCloseWorkSpace()));
			}
		}
		private bool CanCloseWorkSpace()
		{
			CancelEventArgs cea = new CancelEventArgs();
			return CanClose(cea);
		}

		#endregion // CloseWorkSpaceCommand

		#region RequestClose [event]

		/// <summary>
		/// Raised when this workspace should be removed from the UI.
		/// </summary>
		public event EventHandler<WorkspaceClosedArgs> RequestCloseWorkSpace;

		public virtual void RaiseRequestCloseWorkSpace(bool dialogResult)
		{
			EventHandler<WorkspaceClosedArgs> handler = RequestCloseWorkSpace;
			if (handler != null) {
				CancelEventArgs cea = new CancelEventArgs();
				OnViewClosing(cea);
				if (!cea.Cancel) {
					OnViewClose();
					handler(this, new WorkspaceClosedArgs(dialogResult));
				}
			}
		}

		public class WorkspaceClosedArgs : EventArgs
		{
			public bool DialogResult { get; private set; }

			public WorkspaceClosedArgs(bool dialogResult)
			{
				DialogResult = dialogResult;
			}
		}

		#endregion // RequestClose [event]

		public virtual void WireWindowClose(Window window)
		{
			WireWindowClose(window, false);
		}
		public virtual void WireWindowClose(Window window, bool isDialog)
		{
			// When the ViewModel asks to be closed, close the window.
			EventHandler<WorkspaceClosedArgs> handler = null;
			handler = delegate(object sender, WorkspaceClosedArgs args)
			{
				RequestCloseWorkSpace -= handler;
				if (isDialog)
				{
					window.DialogResult = args.DialogResult;
				}
				window.Close();
			};
			RequestCloseWorkSpace += handler;
		}
	}
}
