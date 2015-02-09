using System;
using System.IO;
using System.Reflection;

namespace SOS.Lib.Util
{
	public class ResourceManager
	{
		#region Constructors

		// Constructor is private because this class is designed to provide only static methods
		private ResourceManager()
		{
		}

		#endregion Constructors

		#region Methods

		#region Public

		public static string ReadEmbeddedResource(Assembly assm, string resourceKey)
		{
			// Validate arguments
			if (assm == null)
				throw new ArgumentNullException("assm");
			if (string.IsNullOrEmpty(resourceKey))
				throw new ArgumentNullException("resourceKey");

			// Make sure the given assembly contains the specified resource
			bool bFound = false;
			foreach (string curr in assm.GetManifestResourceNames())
			{
				if (curr.Equals(resourceKey, StringComparison.InvariantCultureIgnoreCase))
				{
					bFound = true;
					break;
				}
			}

			if (!bFound)
				throw new IndexOutOfRangeException(
					string.Format("Specified resource ({0}) does not exist in assembly {1}", resourceKey, assm.GetName()));

			// Read the resource
			using (TextReader reader = new StreamReader(assm.GetManifestResourceStream(resourceKey)))
			{
				return reader.ReadToEnd();
			}
		}

		public static Stream GetEmbeddedResourceStream(Assembly assm, string resourceKey)
		{
			// Validate arguments
			if (assm == null)
				throw new ArgumentNullException("assm");
			if (string.IsNullOrEmpty(resourceKey))
				throw new ArgumentNullException("resourceKey");

			// Make sure the given assembly contains the specified resource
			bool bFound = false;
			foreach (string curr in assm.GetManifestResourceNames())
			{
				if (curr.Equals(resourceKey, StringComparison.InvariantCultureIgnoreCase))
				{
					bFound = true;
					break;
				}
			}

			if (!bFound)
				throw new IndexOutOfRangeException(
					string.Format("Specified resource ({0}) does not exist in assembly {1}", resourceKey, assm.GetName()));

			// Return the stream
			return assm.GetManifestResourceStream(resourceKey);
		}

		#endregion Public

		#region Private

		#endregion Private

		#endregion Methods
	}
}