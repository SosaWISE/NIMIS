using System;

namespace SSE.Lib.SseGpsDeviceAPI.Helper
{
	public class DateTimeConversions
	{
		#region .ctor

		#endregion .ctor

		#region Methods

		public static string GetDateDeivceFormat(DateTime utcEventDate)
		{
			return string.Format("{0:D2}{1:D2}{2:D4}", utcEventDate.Month, utcEventDate.Day, utcEventDate.Year);
		}

		public static string GetTimeDeivceFormat(DateTime utcEventTime)
		{
			return string.Format("{0:D2}{1:D2}{2:D2}{3:D3}", utcEventTime.Hour, utcEventTime.Minute, utcEventTime.Second,
								 utcEventTime.Millisecond);
		}

		public static DateTime GetDateTimeFromData(string date, string time)
		{
			// ** Initialize
			int month = Convert.ToInt32(date.Substring(0, 2));
			int day = Convert.ToInt32(date.Substring(2, 2));
			int year = Convert.ToInt32(date.Substring(4, 4));
			int hour = Convert.ToInt32(time.Substring(0, 2));
			int minute = Convert.ToInt32(time.Substring(2, 2));
			int second = Convert.ToInt32(time.Substring(4, 2));
			int millisecond = Convert.ToInt32(time.Substring(6, 3));

			var result = new DateTime(year, month, day, hour, minute, second, millisecond, DateTimeKind.Utc);

			// ** return result.
			return result;
		}

		#endregion Methods
	}
}
