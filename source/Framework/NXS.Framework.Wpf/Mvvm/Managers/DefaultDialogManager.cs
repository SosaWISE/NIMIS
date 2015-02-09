using System.Windows;
using NXS.Framework.Wpf.Mvvm.ViewModels;
using NXS.Framework.Wpf.Mvvm.Views;

namespace NXS.Framework.Wpf.Mvvm.Managers
{
	public class DefaultDialogManager : IDialogManager
	{
		#region Constructors

		#endregion Constructors

		#region Methods

		#region Private

		private CloseableViewModelDialog BuildDialog(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode, bool isDialog, bool canEscapeClose, bool canEnterClose, bool? allowTransparency)
		{
			// Initialize the dialog
			var result = new CloseableViewModelDialog
			{
				CanEscapeClose = canEscapeClose,
				CanEnterClose = canEnterClose,
				DataContext = viewModel,
				Owner = owner
			};

			// Use the same icon
			if (result.Owner != null)
			{
				result.Icon = result.Owner.Icon;
			}

			// Hide the 'Close' button if needed
			if (!viewModel.AllowManualClose)
			{
				result.CloseButtonVisibility = Visibility.Collapsed;
			}

			//set the size
			if (width.HasValue)
			{
				result.Width = width.Value;
			}
			if (height.HasValue)
			{
				result.Height = height.Value;
			}

			//set resize mode
			if (resizeMode.HasValue)
			{
				result.ResizeMode = resizeMode.Value;
			}

			if (allowTransparency.HasValue)
			{
				result.AllowsTransparency = allowTransparency.Value;
			}

			// Hookup so we can close the window
			viewModel.WireWindowClose(result, isDialog);
			
			return result;
		}

		#endregion Private

		#endregion Methods

		#region IDialogManager Members

		public bool ShowDialog(CloseableViewModel viewModel, int width, int height)
		{
			return ShowDialog(viewModel, null, width, height, null);
		}
		public bool ShowDialog(CloseableViewModel viewModel, ResizeMode resizeMode)
		{
			return ShowDialog(viewModel, null, null, null, resizeMode);
		}
		public bool ShowDialog(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode)
		{
			return ShowDialog(viewModel, owner, width, height, resizeMode, true, false, null);
		}
		public bool ShowDialog(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode, bool allowTransparency)
		{
			return ShowDialog(viewModel, owner, width, height, resizeMode, true, false, allowTransparency);
		}
		public bool ShowDialog(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode, bool canEscapeClose, bool canEnterClose, bool? allowTransparency)
		{
			// Build the window
			CloseableViewModelDialog window = BuildDialog(viewModel, owner, width, height, resizeMode, true, canEscapeClose, canEnterClose, allowTransparency);
			
			// Show the dialog & get the result
			viewModel.Focus();
			bool? result = window.ShowDialog();
			return result.HasValue && result.Value;
		}

		public Window Show(CloseableViewModel viewModel, int width, int height)
		{
			return Show(viewModel, null, width, height, null);
		}
		public Window Show(CloseableViewModel viewModel, ResizeMode resizeMode)
		{
			return Show(viewModel, null, null, null, resizeMode);
		}
		public Window Show(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode)
		{
			return Show(viewModel, owner, width, height, resizeMode, true, false);
		}
		public Window Show(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode, bool canEscapeClose, bool canEnterClose)
		{
			// Build the window
			CloseableViewModelDialog window = BuildDialog(viewModel, owner, width, height, resizeMode, false, canEscapeClose, canEnterClose, null);

			// Show the window
			viewModel.Focus();
			window.Show();

			return window;
		}

		#endregion
	}
}