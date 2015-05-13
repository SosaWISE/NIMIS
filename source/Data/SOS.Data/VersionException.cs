using SOS.Lib.Core;
using System;

namespace SOS.Data
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
	}
}
