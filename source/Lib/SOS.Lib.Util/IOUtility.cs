using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace SOS.Lib.Util
{
	public class IOUtility
	{
		#region Constructors

		// Mark default constructor as private since class is designed to provide only static methods
		private IOUtility()
		{
		}

		#endregion Constructors

		#region Methods

		#region Static

		public static byte[] ReadStream(Stream s)
		{
			var buffer = new byte[32768]; // 32K
			int nRead = 0;

			int nChunkSize;
			while ((nChunkSize = s.Read(buffer, nRead, buffer.Length - nRead)) > 0)
			{
				nRead += nChunkSize;

				// If we've reached the end of our buffer, check to see if there's more
				if (nRead == buffer.Length)
				{
					int nextByte = s.ReadByte();

					// If we've reached the end, we're done
					if (nextByte == -1)
						return buffer;

					// If not, resize the buffer and continue
					var newBuffer = new byte[buffer.Length*2];
					Array.Copy(buffer, newBuffer, buffer.Length);
					newBuffer[nRead] = (byte) nextByte;
					buffer = newBuffer;
					nRead += 1;
				}
			}

			// Trim the buffer to the proper size
			var final = new byte[nRead];
			Array.Copy(buffer, final, nRead);
			return final;
		}


		public static void CopyStreamContents(Stream inputStream, Stream outputStream, byte[] buffer)
		{
			if (inputStream == null)
				throw new ArgumentNullException("inputStream");
			if (outputStream == null)
				throw new ArgumentNullException("outputStream");
			if (buffer == null)
				throw new ArgumentNullException("buffer");
			if (!inputStream.CanRead)
				throw new InvalidOperationException("Cannot read from inputStream");
			if (!outputStream.CanWrite)
				throw new InvalidOperationException("Cannot write to outputStream");
			if (buffer.Length == 0)
				throw new InvalidOperationException("Length of buffer must be greater than 0");

			int nBytesRead;
			while ((nBytesRead = inputStream.Read(buffer, 0, buffer.Length)) > 0)
			{
				outputStream.Write(buffer, 0, nBytesRead);
			}
		}

		public static void WriteDataCSV(DataTable data, StreamWriter writer)
		{
			if (data == null)
				throw new ArgumentNullException("data");
			if (writer == null)
				throw new ArgumentNullException("writer");

			int nColumns = data.Columns.Count;
			var stringColumnIndexes = new Dictionary<int, bool>();
			foreach (DataColumn curr in data.Columns)
			{
				if (curr.DataType == typeof (string))
				{
					stringColumnIndexes.Add(curr.Ordinal, true);
				}
			}

			foreach (DataRow currRow in data.Rows)
			{
				for (int i = 0; i < nColumns; i++)
				{
					if (!currRow.IsNull(i))
					{
						if (stringColumnIndexes.ContainsKey(i))
							writer.Write(string.Format("\"{0}\"", currRow[i]));
						else
							writer.Write(currRow[i].ToString());
					}

					if (i < nColumns - 1)
						writer.Write(",");
				}
				writer.WriteLine();
			}
			writer.Flush();
		}

		public static void WriteDataTabDelimited(DataTable data, StreamWriter writer, bool includeHeaderRow)
		{
			WriteDataTabDelimited(data, writer, includeHeaderRow, null);
		}

		public static void WriteDataTabDelimited(DataTable data, StreamWriter writer, bool includeHeaderRow,
		                                         ICollection<string> columnsToInclude)
		{
			if (data == null)
				throw new ArgumentNullException("data");
			if (writer == null)
				throw new ArgumentNullException("writer");

			var columnIndexesToInclude = new Dictionary<int, bool>();
			int nColumns = data.Columns.Count;

			for (int i = 0; i < nColumns; i++)
			{
				if (columnsToInclude == null || columnsToInclude.Contains(data.Columns[i].ColumnName))
					columnIndexesToInclude.Add(i, true);
			}

			if (includeHeaderRow)
			{
				for (int i = 0; i < nColumns; i++)
				{
					if (columnIndexesToInclude.ContainsKey(i))
					{
						writer.Write(data.Columns[i].ColumnName);

						if (i < nColumns - 1)
							writer.Write("\t");
					}
				}
				writer.WriteLine();
			}

			foreach (DataRow currRow in data.Rows)
			{
				for (int i = 0; i < nColumns; i++)
				{
					if (columnIndexesToInclude.ContainsKey(i))
					{
						if (!currRow.IsNull(i))
						{
							writer.Write(currRow[i].ToString().Replace("\t", " "));
						}

						if (i < nColumns - 1)
							writer.Write("\t");
					}
				}
				writer.WriteLine();
			}
			writer.Flush();
		}

		public static void EnsureDirectoryExists(string directoryPath)
		{
			if (!Directory.Exists(directoryPath))
				Directory.CreateDirectory(directoryPath);
		}

		public static void WriteFile(byte[] data, string outputPath)
		{
			using (var writer = new BinaryWriter(File.Open(outputPath, FileMode.Create)))
			{
				writer.Write(data);
			}
		}

		public static string GetUniqueRandomFilePath(string directoryPath, string extension)
		{
			if (string.IsNullOrEmpty(directoryPath))
				throw new ArgumentNullException(directoryPath);

			if (!string.IsNullOrEmpty(extension) && !extension.StartsWith("."))
				extension = string.Format(".{0}", extension);

			string filename = StringUtility.GetRandomString(10);
			string result;

			while (!string.IsNullOrEmpty(result = string.Format(@"{0}\{1}{2}", directoryPath, filename, extension)) &&
			       File.Exists(result))
				filename = StringUtility.GetRandomString(10);

			return result;
		}

		public static string ReadFileText(string FilePath)
		{
			string text;
			using (var reader = new StreamReader(FilePath))
			{
				text = reader.ReadToEnd();
				reader.Close();
			}
			return text;
		}

		public static string ReadStreamText(Stream stream)
		{
			string text;
			using (var reader = new StreamReader(stream))
			{
				text = reader.ReadToEnd();
				reader.Close();
			}
			return text;
		}

		#endregion Static

		#endregion Methods
	}
}