using System.Windows;
using Microsoft.Win32;

namespace NXS.Framework.Wpf.Mvvm.Managers
{
	public class DefaultSaveFileManager : ISaveFileManager
	{
		#region Properties

		private readonly SaveFileDialog _dialog;

		#endregion Properties

		#region Constructors

		public DefaultSaveFileManager()
		{
			_dialog = new SaveFileDialog();
		}

		#endregion Constructors

		#region ISaveFileManager Members

		public string FullFilePath { get; set; }

		public bool ShowDialog(Window owner, string filePath, string initialDirectory, string filter, bool promptBeforeOverwrite)
		{
			// Reset the filename
			FullFilePath = null;

			// Set properties on the dialog
			_dialog.FileName = filePath;
			_dialog.Filter = filter;
			_dialog.InitialDirectory = initialDirectory;
			_dialog.OverwritePrompt = promptBeforeOverwrite;

			// Show the dialog
			bool result = _dialog.ShowDialog(owner).Value;

			// Capture the filename from the dialog
			FullFilePath = _dialog.FileName;

			// Return the result
			return result;
		}

		#endregion
	}
}