using System.Windows;
using Microsoft.Win32;

namespace NXS.Framework.Wpf.Mvvm.Managers
{
	public class DefaultOpenFileManager : IOpenFileManager
	{
		#region Properties

		private OpenFileDialog _dialog;

		#endregion Properties

		#region Constructors

		public DefaultOpenFileManager()
		{
			_dialog = new OpenFileDialog();
		}

		#endregion Constructors

		#region IOpenFileManager Members

		public string FullFilePath { get; set; }

		public bool ShowDialog(Window owner, string fileName, string filter, string initialDirectory)
		{
			// Reset the selected file path
			FullFilePath = null;

			// Set properties on the dialog
			_dialog.FileName = fileName;
			_dialog.Filter = filter;
			_dialog.InitialDirectory = initialDirectory;

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
