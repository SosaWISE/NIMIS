using System;
using AR = SOS.Data.AuthenticationControl.AC_DateTimeView;
using ARCollection = SOS.Data.AuthenticationControl.AC_DateTimeViewCollection;
using ARController = SOS.Data.AuthenticationControl.AC_DateTimeViewController;

namespace SOS.Data.AuthenticationControl
{
// ReSharper disable once InconsistentNaming
	public static class AC_DateTimeViewControllerExtensions
	{
		public static DateTime? GetLocalDateTime(this ARController cntlr)
		{
			/** Initialize. */
			AR item = cntlr.LoadSingle(AR.Query());

			/** Return result. */
			return item.LocalDateTime;
		}

		public static DateTime? GetUTCDateTime(this ARController cntlr)
		{
			/** Initialize. */
			AR item = cntlr.LoadSingle(AR.Query());

			/** Return result. */
			return item.UTCDateTime;
		}
	}
}