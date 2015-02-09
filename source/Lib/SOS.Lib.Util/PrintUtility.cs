using System;
using System.Diagnostics;
using System.IO;

namespace SOS.Lib.Util
{
	public class PrintUtility
	{
		public static bool PrintFile(string filePath)
		{
			return PrintFile(filePath, null);
		}

		public static bool PrintFile(string filePath, string printerPath)
		{
			// Check if the incomming strings are null or empty.
			if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
			{
				return false;
			}

			// Initialize a new process for the printing
			var proc = new Process();
			try
			{
				//Print the file
				proc.StartInfo.FileName = filePath;
				proc.StartInfo.CreateNoWindow = true; // Don't show a window

				// Print to the selected printer (if one was specified).
				if (!string.IsNullOrEmpty(printerPath))
				{
					proc.StartInfo.Verb = "Printto";
					proc.StartInfo.Arguments = string.Format("\"{0}\"", printerPath);
				}
				else // Print to the default printer
				{
					proc.StartInfo.Verb = "Print";
				}

				proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				proc.StartInfo.UseShellExecute = true;
				proc.Start();
				proc.WaitForInputIdle(); // Give the process time to execute

				// Return true for success.
				return true;
			}
			catch
			{
				return false;
			}
			finally
			{
				// Close the window
				if (proc.MainWindowHandle != IntPtr.Zero)
				{
					proc.CloseMainWindow();
				}
				else // Just close the process
					proc.Close();
			}
		}

		public static bool PrintFile(byte[] data, string tempDirectoryPath, string extension)
		{
			return PrintFile(data, tempDirectoryPath, extension, null);
		}

		public static bool PrintFile(byte[] data, string tempDirectoryPath, string extension, string printerPath)
		{
			// Check if the incomming data is null or empty
			if (data == null || data.Length == 0)
			{
				return false;
			}

			try
			{
				// Save a temporary file
				string filePath = IOUtility.GetUniqueRandomFilePath(tempDirectoryPath, extension);
				IOUtility.WriteFile(data, filePath);

				// Print the file
				bool bResult = PrintFile(filePath, printerPath);

				// Delete the temporary file
				File.Delete(filePath);

				// Return true for success.
				return bResult;
			}
			catch
			{
				return false;
			}
		}
	}
}