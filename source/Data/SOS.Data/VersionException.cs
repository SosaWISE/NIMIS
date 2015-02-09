using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.Data
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
			{
				return;
			}

			if (actualVersion < expectedVersion)
			{
				throw new VersionException("Outdated Version. Update your version before making changes.");
			}
			else
			{
				throw new VersionException("Invalid Version");
			}
		}
	}
}
