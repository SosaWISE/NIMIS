using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Data
{
	public class VersionException : Exception
	{
		public VersionException(string msg) : base(msg) { }

		public static void CheckVersions(int expectedVersion, int actualVersion)
		{
			var errMsg = VersionHelper.VersionErrMsg(expectedVersion, actualVersion);
			if (!string.IsNullOrEmpty(errMsg))
				throw new VersionException(errMsg);
		}
		public static void CheckModifiedOn(DateTime expectedDate, DateTime actualDate)
		{
			var errMsg = VersionHelper.ModifiedOnErrMsg(expectedDate, actualDate);
			if (!string.IsNullOrEmpty(errMsg))
				throw new VersionException(errMsg);
		}
	}
}
