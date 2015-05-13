using SOS.Lib.Core.ErrorHandling;
using System;

namespace SOS.Lib.Core
{
	public static class VersionHelper
	{
		public static string VersionErrMsg(int expectedVersion, int actualVersion)
		{
			if (actualVersion == expectedVersion)
				return "";

			if (actualVersion < expectedVersion)
				return "Outdated Version. Update your version before making changes.";
			else
				return "Invalid Version";
		}

		public static string ModifiedOnErrMsg(DateTime expectedDate, DateTime actualDate)
		{
			if (actualDate == expectedDate)
				return "";

			if (actualDate < expectedDate)
				return "Outdated ModifiedOn. Get the latest version before making changes.";
			else
				return "Invalid ModifiedOn";
		}

		public static Result<T> CheckModifiedOn<T>(DateTime expectedDate, DateTime actualDate, Result<T> result, Func<T> getValue = null, Func<string, string> getMsg = null)
		{
			var msg = ModifiedOnErrMsg(expectedDate, actualDate);
			if (string.IsNullOrEmpty((msg)))
				return result;
			result.Value = (getValue != null) ? getValue() : default(T);
			return result.Fail((int)BaseErrorCodes.ErrorCodes.InvalidModifiedOn, (getMsg != null) ? getMsg(msg) : msg);
		}
	}
}
