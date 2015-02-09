using System.Windows;

namespace NXS.Framework.Wpf.Mvvm.Managers
{
	public interface IOpenFileManager
	{
		string FullFilePath { get; set; }

		bool ShowDialog(Window owner, string fileName, string filter, string initialDirectory);
	}
}