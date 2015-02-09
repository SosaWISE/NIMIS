using System;
using System.Diagnostics;
using org.pdfclown.documents;
using org.pdfclown.documents.interaction.viewer;
using org.pdfclown.documents.interchange.metadata;
using files = org.pdfclown.files;

namespace NXS.Lib.Barcode
{
	public abstract class BaseBarcode
	{
		#region .ctor

		protected BaseBarcode(string outputFilePath)
		{
			OutputFilePath = outputFilePath;
			FontResourcePath =
				SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig(
					"NXS.Lib.Barcode.IDAutomationHC39M_FontResourceFolderPath");
		}

		#endregion .ctor

		#region Properties
		internal string FontResourcePath { get; set; }
		public string OutputFilePath { get; private set; }
		#endregion Properties

		#region Methods
		#region Protected
		#region Serialize OVERLOADING
		/**
   <summary>Serializes the given PDF Clown file object.</summary>
   <param name="file">PDF file to serialize.</param>
   <returns>Serialization path.</returns>
 */
		protected string Serialize(files::File file)
		{ return Serialize(file, null, null, null); }

		/**
		  <summary>Serializes the given PDF Clown file object.</summary>
		  <param name="file">PDF file to serialize.</param>
		  <param name="serializationMode">Serialization mode.</param>
		  <returns>Serialization path.</returns>
		*/
		protected string Serialize(
		  files::File file,
		  files::SerializationModeEnum? serializationMode
		  )
		{ return Serialize(file, serializationMode, null, null, null); }

		/**
		  <summary>Serializes the given PDF Clown file object.</summary>
		  <param name="file">PDF file to serialize.</param>
		  <param name="fileName">Output file name.</param>
		  <returns>Serialization path.</returns>
		*/
		protected string Serialize(
		  files::File file,
		  string fileName
		  )
		{ return Serialize(file, fileName, null, null); }

		/**
		  <summary>Serializes the given PDF Clown file object.</summary>
		  <param name="file">PDF file to serialize.</param>
		  <param name="fileName">Output file name.</param>
		  <param name="serializationMode">Serialization mode.</param>
		  <returns>Serialization path.</returns>
		*/
		protected string Serialize(
		  files::File file,
		  string fileName,
		  files::SerializationModeEnum? serializationMode
		  )
		{ return Serialize(file, fileName, serializationMode, null, null, null); }

		/**
		  <summary>Serializes the given PDF Clown file object.</summary>
		  <param name="file">PDF file to serialize.</param>
		  <param name="title">Document title.</param>
		  <param name="subject">Document subject.</param>
		  <param name="keywords">Document keywords.</param>
		  <returns>Serialization path.</returns>
		*/
		protected string Serialize(
		  files::File file,
		  string title,
		  string subject,
		  string keywords
		  )
		{ return Serialize(file, null, title, subject, keywords); }

		/**
		  <summary>Serializes the given PDF Clown file object.</summary>
		  <param name="file">PDF file to serialize.</param>
		  <param name="serializationMode">Serialization mode.</param>
		  <param name="title">Document title.</param>
		  <param name="subject">Document subject.</param>
		  <param name="keywords">Document keywords.</param>
		  <returns>Serialization path.</returns>
		*/
		protected string Serialize(
		  files::File file,
		  files::SerializationModeEnum? serializationMode,
		  string title,
		  string subject,
		  string keywords
		  )
		{ return Serialize(file, GetType().Name, serializationMode, title, subject, keywords); }

		/**
		  <summary>Serializes the given PDF Clown file object.</summary>
		  <param name="file">PDF file to serialize.</param>
		  <param name="fileName">Output file name.</param>
		  <param name="serializationMode">Serialization mode.</param>
		  <param name="title">Document title.</param>
		  <param name="subject">Document subject.</param>
		  <param name="keywords">Document keywords.</param>
		  <returns>Serialization path.</returns>
		*/
		protected string Serialize(
		  files::File file,
		  string fileName,
		  files::SerializationModeEnum? serializationMode,
		  string title,
		  string subject,
		  string keywords
		  )
		{
			ApplyDocumentSettings(file.Document, title, subject, keywords);

			if (!serializationMode.HasValue)
			{
				serializationMode = files::SerializationModeEnum.Standard;
			}

			
			// Save the file!
			/*
			  NOTE: You can also save to a generic target stream (see Save() method overloads).
			*/
			try
			{ file.Save(OutputFilePath, serializationMode.Value); }
			catch (Exception e)
			{
				Debug.WriteLine("File writing failed: " + e.Message);
				Debug.WriteLine(e.StackTrace);
				OutputFilePath = null;
			}
			Debug.WriteLine("\nOutput: " + OutputFilePath);

			return OutputFilePath;
		}

		#endregion Serialize OVERLOADING
		#endregion Protected

		#region Private
		private void ApplyDocumentSettings(
		  Document document,
		  string title,
		  string subject,
		  string keywords
		  )
		{
			if (title == null)
				return;

			// Viewer preferences.
			var view = new ViewerPreferences(document); // Instantiates viewer preferences inside the document context.
			document.ViewerPreferences = view; // Assigns the viewer preferences object to the viewer preferences function.
			view.DisplayDocTitle = true;

			// Document metadata.
			Information info = document.Information;
			info.Clear();
			info.Author = "Stefano Chizzolini";
			info.CreationDate = DateTime.Now;
			info.Creator = GetType().FullName;
			info.Title = "PDF Clown - " + title + " sample";
			info.Subject = "Sample about " + subject + " using PDF Clown";
			info.Keywords = keywords;
		}
		#endregion Private

		#endregion Methods
	}

	public enum PdfSerializationModeEnum
	{
		/**
		  <summary>Standard complete file serialization [PDF:1.6:3.4].</summary>
		  <remarks>It <i>writes the entire file</i>, generating a single-section cross-reference table
		  and removing obsolete data structures.
		  It reduces the serialization size, but it's more computationally-intensive (slower).</remarks>
		*/
		Standard = 0,
		/**
		  <summary>Standard incremental file serialization [PDF:1.6:2.2.7].</summary>
		  <remarks>It <i>leaves original contents intact, appending changes to the end of the file</i>
		  along with an additional cross-reference table section.
		  It increases the serialization size, but it's faster.</remarks>
		*/
		Incremental = 1,
		/**
		  <summary>Linearized file serialization [PDF:1.6:F].</summary>
		  <remarks>It organizes the file to enable <i>efficient incremental access in a network environment</i>.
		  It increases the serialization size and it's more computationally-intensive (slower).</remarks>
		*/
		Linearized = 2
	}
}
