using System;
using System.IO;
using SOS.Lib.Core;
using SOS.Lib.Util;

// ReSharper disable once CheckNamespace
namespace NXS.Data.FileStore
{
// ReSharper disable once InconsistentNaming
	public partial class FS_File
	{
		#region Methods

		#region Static

		public static FS_File CreateFromBytes(byte[] bytes, string filename, FileSource source, string createdByID)
		{
			var result = new FS_File
			{
				FileName = Path.GetFileName(filename),
				MimeType = WebUtility.GetMimeType(Path.GetExtension(filename)),
				FileData = bytes
			};
			result.FileSize = result.FileData.LongLength;
			result.FileSourceID = (int)source;
			result.CreatedByID = createdByID;
			result.CreatedByDate = DateTime.Now;

			return result;
		}

		public static FS_File CreateFromPhysicalFile(string pathAndFilename, FileSource source, string createdByID)
		{
			// Make sure file exists and can be read
			if (!File.Exists(pathAndFilename))
				throw new FileNotFoundException(string.Format("Specified file could not be found: {0}", pathAndFilename));

			var result = new FS_File
			{
				FileName = Path.GetFileName(pathAndFilename),
				MimeType = WebUtility.GetMimeType(Path.GetExtension(pathAndFilename)),
				FileData = File.ReadAllBytes(pathAndFilename)
			};
			result.FileSize = result.FileData.LongLength;
			result.FileSourceID = (int)source;
			result.CreatedByID = createdByID;
			result.CreatedByDate = DateTime.Now;

			return result;
		}

		public static FS_File CreateFromStream(Stream inputStream, string filename, FileSource source, string createdByID)
		{
			if (!inputStream.CanSeek || !inputStream.CanRead)
				throw new InvalidOperationException("Cannot seek or read from inputStream.");

			// Make sure the given stream is at the beginning
			inputStream.Seek(0, SeekOrigin.Begin);

			var result = new FS_File
			{
				FileName = filename,
				MimeType = WebUtility.GetMimeType(Path.GetExtension(filename)),
				FileData = IOUtility.ReadStream(inputStream)
			};
			result.FileSize = result.FileData.LongLength;
			result.FileSourceID = (int)source;
			result.CreatedByID = createdByID;
			result.CreatedByDate = DateTime.Now;

			return result;
		}

		#endregion Static

		#endregion Methods

	}
}
