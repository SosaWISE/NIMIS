using System.Windows;
using NXS.Framework.Wpf.Mvvm.ViewModels;

namespace NXS.Framework.Wpf.Mvvm.Managers
{
	public interface IDialogManager
	{
		bool ShowDialog(CloseableViewModel viewModel, int width, int height);
		bool ShowDialog(CloseableViewModel viewModel, ResizeMode resizeMode);
		bool ShowDialog(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode);
		bool ShowDialog(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode, bool allowTransparency);
		bool ShowDialog(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode, bool canEscapeClose, bool canEnterClose, bool? allowTransparency);

		Window Show(CloseableViewModel viewModel, int width, int height);
		Window Show(CloseableViewModel viewModel, ResizeMode resizeMode);
		Window Show(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode);
		Window Show(CloseableViewModel viewModel, Window owner, int? width, int? height, ResizeMode? resizeMode, bool canEscapeClose, bool canEnterClose);
	}
}