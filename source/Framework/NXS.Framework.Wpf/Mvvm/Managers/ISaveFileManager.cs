using System.Windows;

namespace NXS.Framework.Wpf.Mvvm.Managers
{
	public interface ISaveFileManager
	{
		string FullFilePath { get; set; }

		bool ShowDialog(Window owner, string filePath, string initialDirectory, string filter, bool promptBeforeOverwrite);
	}
}