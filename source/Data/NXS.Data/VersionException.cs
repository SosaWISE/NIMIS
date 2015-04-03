using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Data
{
	public class VersionException : Exception
	{
		public VersionException(string msg)
			: base(msg)
		{
		}

		public static void CheckVersions(int expectedVersion, int actualVersion)
		{
			if (actualVersion == expectedVersion)
				return;

			if (actualVersion < expectedVersion)
				throw new VersionException("Outdated Version. Get the latest version before making changes.");
			else
				throw new VersionException("Invalid Version");
		}
		public static void CheckModifiedOn(DateTime expectedDate, DateTime actualDate)
		{
			if (expectedDate == actualDate)
				return;

			if (expectedDate < actualDate)
				throw new VersionException("Outdated ModifiedOn. Get the latest version before making changes.");
			else
				throw new VersionException("Invalid ModifiedOn");
		}
	}
}
