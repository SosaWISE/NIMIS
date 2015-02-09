using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace SOS.Lib.Util
{
	public class ZipUtility
	{
		#region Properties

		#region Private

		#endregion Private

		#region Public

		#endregion Public

		#endregion Properties

		#region Constructors

		#endregion Constructors

		#region Methods

		#region Private

		private static void CreateZipEntries(string directoryPath, string baseDirectoryPath, ZipOutputStream stream)
		{
			var buffer = new byte[8192];

			foreach (string curr in Directory.GetDirectories(directoryPath))
			{
				CreateZipEntries(curr, baseDirectoryPath, stream);
			}

			foreach (string curr in Directory.GetFiles(directoryPath))
			{
				// Get the file name for the zipped file
				string zipFileName = curr;
				if (zipFileName.StartsWith(baseDirectoryPath))
					zipFileName = zipFileName.Substring(baseDirectoryPath.Length);
				if (zipFileName.StartsWith(@"\") || zipFileName.StartsWith("/"))
					zipFileName = zipFileName.Substring(1);

				// Create the zip entry
				var entry = new ZipEntry(zipFileName);
				entry.DateTime = DateTime.Now;
				stream.PutNextEntry(entry);

				// Copy the file contents into the zip stream
				using (FileStream readStream = File.OpenRead(curr))
				{
					IOUtility.CopyStreamContents(readStream, stream, buffer);
				}

				// Close the entry
				stream.CloseEntry();
			}
		}

		#endregion Private

		#region Public

		public static void UnzipFile(string pathToZipFile, string destinationPath)
		{
			using (var s = new ZipInputStream(File.OpenRead(pathToZipFile)))
			{
				ZipEntry currEntry;
				while ((currEntry = s.GetNextEntry()) != null)
				{
					string directoryName = Path.GetDirectoryName(currEntry.Name);
					string fileName = Path.GetFileName(currEntry.Name);

					// Create Directory for the file - if it already exists this won't do anything
					if (!string.IsNullOrEmpty(directoryName))
						Directory.CreateDirectory(string.Format("{0}\\{1}", destinationPath, directoryName));

					// Save the file (if there is one and not just a directory)
					if (!string.IsNullOrEmpty(fileName))
					{
						string destFilePath = (string.IsNullOrEmpty(directoryName))
						                      	? string.Format("{0}\\{1}", destinationPath, fileName)
						                      	: string.Format("{0}\\{1}\\{2}", destinationPath, directoryName, fileName);

						IOUtility.WriteFile(IOUtility.ReadStream(s), destFilePath);
					}
				}
			}
		}

		public static void ZipDirectory(string directoryPathToZip, Stream outputStream)
		{
			var s = new ZipOutputStream(outputStream);
			s.SetLevel(6); // 0 = store only; 9 = maximum compression

			// Create the zip entries for the directory
			CreateZipEntries(directoryPathToZip, directoryPathToZip, s);

			// Finish the zip output
			s.Finish();
		}

		#endregion Public

		#endregion Methods
	}
}