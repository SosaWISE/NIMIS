using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows.Data;
using NXS.Framework.Wpf.Mvvm.Security;
using NXS.Framework.Wpf.Mvvm.ViewModels;
using NXS.Framework.Wpf.ParentChildService;

namespace NXS.Framework.Wpf.Mvvm
{
	public delegate T Instantiator<T>(ParameterDictionary args);

	public class WorkspaceController : IDisposable
	{
		#region Events

		public event EventHandler SelectedWorkspaceChanged;

		#endregion Events

		#region Fields

		ObservableCollection<WorkspaceViewModel> _workspaces;
		private ICollectionView _workspacesView;
		private WorkspaceViewModel _previouslySelected = null;

		#endregion // Fields

		#region Properties

		public WorkspaceViewModel SelectedWorkspace
		{
			get
			{
				if (_workspacesView != null)
				{
					return _workspacesView.CurrentItem as WorkspaceViewModel;
				}
				else
				{
					return null;
				}
			}
		}

		#endregion Properties

		#region Private Helpers

		public void CloseActiveWorkspace()
		{
			ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
			if (collectionView != null) {

				CloseableViewModel workspace;

				//get the active workspace
				workspace = collectionView.CurrentItem as CloseableViewModel;
				if (workspace != null) {

					workspace.RaiseRequestCloseWorkSpace(true);
				}
			}
		}

		public void SetActiveWorkspace(WorkspaceViewModel workspace)
		{
			Debug.Assert(this.Workspaces.Contains(workspace));

			if (_workspacesView != null) {
				_workspacesView.MoveCurrentTo(workspace);
			}
		}

		#endregion // Private Helpers

		#region Workspaces

		/// <summary>
		/// Returns the collection of available workspaces to display.
		/// A 'workspace' is a ViewModel that can request to be closed.
		/// </summary>
		public ObservableCollection<WorkspaceViewModel> Workspaces
		{
			get
			{
				if (_workspaces == null) {
					_workspaces = new ObservableCollection<WorkspaceViewModel>();
					_workspaces.CollectionChanged += this.RaiseWorkspacesChanged;

					// Initialize the collection view
					_workspacesView = CollectionViewSource.GetDefaultView(_workspaces);
					_workspacesView.CurrentChanged += this.RaiseSelectedWorkspaceChanged;
				}
				return _workspaces;
			}
		}

		void RaiseWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			if (e.NewItems != null && e.NewItems.Count > 0) {
				foreach (WorkspaceViewModel workspace in e.NewItems) {
					CloseableViewModel closeable = workspace as CloseableViewModel;
					if (closeable != null) {
						closeable.RequestCloseWorkSpace += this.OnWorkspaceRequestClose;
					}
				}
			}

			if (e.OldItems != null && e.OldItems.Count > 0) {
				foreach (WorkspaceViewModel workspace in e.OldItems) {
					CloseableViewModel closeable = workspace as CloseableViewModel;
					if (closeable != null) {
						closeable.RequestCloseWorkSpace -= this.OnWorkspaceRequestClose;
					}
				}
			}
		}

		void OnWorkspaceRequestClose(object sender, CloseableViewModel.WorkspaceClosedArgs e)
		{
			WorkspaceViewModel workspace = sender as WorkspaceViewModel;
			CloseWorkspace(workspace);
		}

		void RaiseSelectedWorkspaceChanged(object sender, EventArgs e)
		{
			if (_previouslySelected != this.SelectedWorkspace)
			{
				_previouslySelected = this.SelectedWorkspace;
				OnSelectedWorkspaceChanged(e);
			}
		}

		private void CloseWorkspace(WorkspaceViewModel workspace)
		{
			if (workspace != null) {
				workspace.Dispose();
				this.Workspaces.Remove(workspace);
			}
		}

		public T ShowSingletonWorkspace<T>(ParameterDictionary args) where T : WorkspaceViewModel
		{
			return ShowSingletonWorkspace<T>(new ExecutionArgs(args));
		}

		public T ShowSingletonWorkspace<T>(ExecutionArgs args) where T : WorkspaceViewModel
		{
			// Get the type of our singleton workspace
			Type t = typeof(T);

			// Look for a constructor that takes an ExecutionArgs or one that takes a ParameterDictionary
			ConstructorInfo ctr = t.GetConstructor(new Type[] { typeof(ExecutionArgs) }) ?? t.GetConstructor(new Type[] { typeof(ParameterDictionary) });
			if (ctr != null)
			{
				// Create an instantiator
				Instantiator<T> instantiator = new Instantiator<T>(param => ctr.Invoke(new object[] { param }) as T);

				// Call the method that does the real work
				return ShowSingletonWorkspace<T>(instantiator, args);
			}

			return null;
		}

		public T ShowSingletonWorkspace<T>(Instantiator<T> instantiator, ParameterDictionary args) where T : WorkspaceViewModel
		{
			return ShowSingletonWorkspace<T>(instantiator, new ExecutionArgs(args));
		}

		public T ShowSingletonWorkspace<T>(Instantiator<T> instantiator, ExecutionArgs args) where T : WorkspaceViewModel
		{
			// Verify that we got a valid instantiator
			if (instantiator == null)
				throw new ArgumentNullException("instantiator");

			// Try to find an existing workspace of the given type
			T existing = FirstOrDefaultMatchingWorkspace<T>((args != null) ? args.Arguments : ParameterDictionary.Empty);

			// If not found, try to create one
			if (existing == null) {
				existing = instantiator.Invoke((args != null) ? args.Arguments : ParameterDictionary.Empty);
				if (existing != null) {
					// Add it to the list of workspaces if one was created
					Workspaces.Add(existing);
				}
			}

			// Focus the workspace (if we have a refrence to one)
			if (existing != null)
			{
				SetActiveWorkspace(existing);
			}

			return existing;
		}

		public T FirstOrDefaultMatchingWorkspace<T>(ParameterDictionary args) where T : WorkspaceViewModel
		{
			T existing = (T)Workspaces.FirstOrDefault(param => param.GetType() == typeof(T) && param.MatchesArguments(args ?? ParameterDictionary.Empty));
			return existing;
		}


		public T ShowWorkspace<T>(Instantiator<T> instantiator, ParameterDictionary args) where T : WorkspaceViewModel
		{
			return ShowWorkspace<T>(instantiator, new ExecutionArgs(args));
		}

		public T ShowWorkspace<T>(Instantiator<T> instantiator, ExecutionArgs args) where T : WorkspaceViewModel
		{
			// Verify that we got a valid instantiator
			if (instantiator == null)
				throw new ArgumentNullException("instantiator");

			// If not found, try to create one
			T vm = instantiator.Invoke((args != null) ? args.Arguments : ParameterDictionary.Empty);
			if (vm != null) {
				// Add it to the list of workspaces if one was created
				Workspaces.Add(vm);
				// Focus the workspace
				SetActiveWorkspace(vm);
			}

			return vm;
		}

		#endregion // Workspaces

		#region Methods

		#region Protected

		protected virtual void OnSelectedWorkspaceChanged(EventArgs e)
		{
			if (this.SelectedWorkspaceChanged != null)
			{
				this.SelectedWorkspaceChanged(this, e);
			}

			if (this.SelectedWorkspace != null)
			{
				this.SelectedWorkspace.Focus();
			}
		}

		#endregion Protected

		#endregion Methods

		#region IDisposable Members

		public void Dispose()
		{
			// Dispose workspaces
			foreach (WorkspaceViewModel wvd in Workspaces) {
				wvd.Dispose();
			}

			if (_workspaces != null)
			{
				_workspaces.CollectionChanged -= this.RaiseWorkspacesChanged;
			}

			if (_workspacesView != null)
			{
				_workspacesView.CurrentChanged -= this.RaiseSelectedWorkspaceChanged;
			}
		}

		#endregion // IDisposable Members
	}
}