using System;

namespace SOS.Lib.Util
{
	public class DateUtility
	{
		public static DateTime? SpecifyLocalKind(DateTime? dt)
		{
			return dt.HasValue ? SpecifyLocalKind(dt.Value) : dt;
		}
		public static DateTime SpecifyLocalKind(DateTime dt)
		{
			return DateTime.SpecifyKind(dt, DateTimeKind.Local).ToUniversalTime();
		}

		public static DateTime? SpecifyUtcKind(DateTime? dt)
		{
			return dt.HasValue ? SpecifyUtcKind(dt.Value) : dt;
		}
		public static DateTime SpecifyUtcKind(DateTime dt)
		{
			return DateTime.SpecifyKind(dt, DateTimeKind.Utc).ToUniversalTime();
		}


		public static readonly DateTime MinDate = new DateTime(1753, 1, 1, 0, 0, 0, 0);
		public static readonly DateTime MaxDate = new DateTime(9999, 12, 31, 11, 59, 59, 0);

		public static readonly TimeSpan ONE_DAY = new TimeSpan(1, 0, 0, 0);


		public static void GetWeekEdgeDates(DateTime date, DayOfWeek dateFirst, out DateTime startDate, out DateTime endDate)
		{
			GetWeekEdgeDates(date, dateFirst, 0, 0, 0, 0, out startDate, out endDate);
		}

		public static void GetWeekEdgeDates(DateTime date, DayOfWeek dateFirst, int hour, int minute, int second,
											int millisecond, out DateTime startDate, out DateTime endDate)
		{
			startDate = GetWeekStart(date, dateFirst);
			startDate = AdjustDate(startDate, hour, minute, second, millisecond);
			endDate = GetWeekEnd(startDate);
		}

		//public static DateTime GetWeekStart(DateTime date, DayOfWeek firstDayOfWeek)
		//{
		//    //how many days
		//    int days = (int)firstDayOfWeek - (int)date.DayOfWeek;
		//
		//    // add days
		//    date = date.AddDays(days);
		//
		//    if (0 < days)
		//    {
		//        //if we went forward, subtract a week
		//        date = SubtractWeek(date);
		//    }
		//
		//    //remove time
		//    return date.Date;
		//}
		public static DateTime GetWeekStart(DateTime date, DayOfWeek firstDayOfWeek)
		{
			int days;
			date = GetRawDateOnDayOfWeek(date, firstDayOfWeek, out days);

			if (0 < days)
			{
				//if we went forward, subtract a week
				date = SubtractWeek(date);
			}

			//remove time
			return date.Date;
		}

		public static DateTime GetNextOccurrence(DateTime date, DayOfWeek occurrenceDayOfWeek)
		{
			int days;
			date = GetRawDateOnDayOfWeek(date, occurrenceDayOfWeek, out days);

			if (days < 0)
			{
				//if we went back, add a week
				date = AddWeek(date);
			}

			return date;
		}

		private static DateTime GetRawDateOnDayOfWeek(DateTime date, DayOfWeek occurrenceDayOfWeek, out int days)
		{
			//how many days
			days = (int)occurrenceDayOfWeek - (int)date.DayOfWeek;

			// add days
			return date.AddDays(days);
		}

		public static DateTime GetWeekEnd(DateTime date, DayOfWeek dateFirst)
		{
			date = GetWeekStart(date, dateFirst);

			return GetWeekEnd(date);
		}

		public static DateTime GetWeekEnd(DateTime weekStartDate)
		{
			return AddWeek(weekStartDate).AddMilliseconds(-3);
		}

		public static DateTime AddWeek(DateTime date)
		{
			return date.AddDays(7);
		}

		public static DateTime AddWeek(DateTime date, int numWeeks)
		{
			return date.AddDays(7 * numWeeks);
		}

		public static DateTime SubtractWeek(DateTime date)
		{
			return date.AddDays(-7);
		}

		public static DateTime GetMonthStart(DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		public static DateTime GetMonthEnd(DateTime date)
		{
			return GetMonthStart(date).AddMonths(1).AddMilliseconds(-3);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="date"></param>
		/// <param name="hour">The hours (0 through 23).</param>
		/// <param name="minute">The minutes (0 through 59).</param>
		/// <param name="second">The seconds (0 through 59).</param>
		/// <param name="millisecond">The milliseconds (0 through 999).</param>
		/// <returns></returns>
		public static DateTime AdjustDate(DateTime date, int hour, int minute, int second, int millisecond)
		{
			return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond);
		}

		public static DateTime AdjustDate(DateTime date, TimeSpan span)
		{
			return new DateTime(date.Year, date.Month, date.Day, span.Hours, span.Minutes, span.Seconds, span.Milliseconds);
		}

		public static int DaySpan(DateTime startDate, DateTime endDate, DayOfWeek? excludeDay)
		{
			TimeSpan remainingDays = endDate.Date - startDate.Date;
			return remainingDays.Days - GetNumExcludeDays(startDate, endDate, excludeDay);
		}

		public static TimeSpan MilliVanilliSpan(DateTime startDate, DateTime endDate, DayOfWeek? excludeDay)
		{
			TimeSpan remainingDays = endDate - startDate;
			if (excludeDay != null)
			{
				if (endDate >= startDate) //check if endDate is after or equal to startDate
				{
					return GetMillisFunction(startDate, endDate, excludeDay.Value);
				}
				else
				{
					//swap start and end dates and return the value as negative
					return -GetMillisFunction(endDate, startDate, excludeDay.Value);
				}
			}
			return remainingDays;
		}

		private static TimeSpan GetMillisFunction(DateTime startDate, DateTime endDate, DayOfWeek excludeDay)
		{
			int days = DaySpan(startDate, endDate, excludeDay);
			var daySpan = new TimeSpan(days, 0, 0, 0);

			TimeSpan startTime = startDate.TimeOfDay;
			TimeSpan endTime = endDate.TimeOfDay;

			if (startDate.DayOfWeek == excludeDay)
			{
				startTime = TimeSpan.Zero;
			}
			if (endDate.DayOfWeek == excludeDay)
			{
				endTime = TimeSpan.Zero;
			}

			TimeSpan totalSpan = daySpan + (endTime - startTime);
			return totalSpan;
		}

		public static int GetSqlDateFirst(DayOfWeek dayOfStartWeek)
		{
			var dateFirst = (int)dayOfStartWeek;

			// take account for differences between DayOfWeek and DATEFIRST in Sql
			//	 only date that is different is Sunday/0
			if (dateFirst == 0)
				dateFirst = 7;

			return dateFirst;
		}

		public static bool ValidSqlDate(DateTime date)
		{
			return (MinDate <= date && date <= MaxDate);
		}

		public static DateTime GetOffsetTime(DateTime mst, int greenwichOffset)
		{
			//convert from mountain to greewich mean standard, then to offset time
			return mst.AddHours(7).AddHours(greenwichOffset);
		}

		public static DateTime GetEndDate(DateTime startDate, TimeSpan endTimeOfDay)
		{
			TimeSpan sTime = startDate.TimeOfDay;
			if (endTimeOfDay < sTime)
				throw new ArgumentException("endTimeOfDay must be greater than startDate's TimeOfDay");

			TimeSpan diff = endTimeOfDay - sTime;

			DateTime next = AddTimeToDate(startDate, diff);
			return next;
		}

		public static DateTime MergeDateAndTime(DateTime date, TimeSpan timeOfDay)
		{
			return new DateTime(date.Year, date.Month, date.Day, timeOfDay.Hours, timeOfDay.Minutes, timeOfDay.Seconds);
		}

		public static DateTime AddTimeToDate(DateTime date, TimeSpan time)
		{
			return new DateTime(date.Year, date.Month, date.Day, date.Hour + time.Hours, date.Minute + time.Minutes,
								date.Second + time.Seconds);
		}

		public static bool SpansOverlap(DateTime blockStart, DateTime blockEnd, DateTime otherStart, DateTime otherEnd)
		{
			return new DateSpan(blockStart, blockEnd).Overlaps(new DateSpan(otherStart, otherEnd));
		}

		public static bool IsValidDateSpan(DateTime start, DateTime end)
		{
			return start < end;
		}

		/// <summary>
		/// Returns the zero-based week index of the nextOccurence. Every 7 days the index increases by 1
		/// </summary>
		/// <param name="startDate">Date from which to start</param>
		/// <param name="nextOccurence"></param>
		/// <returns></returns>
		public static int GetWeekRecurrenceIndex(DateTime startDate, DateTime nextOccurence)
		{
			//TimeSpan diff = nextOccurence - startDate;
			TimeSpan diff = nextOccurence.Date - startDate.Date;

			int weekinstance = Convert.ToInt32(diff.Days / 7);

			return weekinstance;
		}

		public static DateTime Max(DateTime dt1, DateTime dt2)
		{
			return (dt1 > dt2) ? dt1 : dt2;
		}

		public static DateTime Min(DateTime dt1, DateTime dt2)
		{
			return (dt1 < dt2) ? dt1 : dt2;
		}


		public static DateSpan GetSameDayDateSpan(DateTime date, DateTime startTime, DateTime endTime)
		{
			return new DateSpan(MergeDateAndTime(date, startTime.TimeOfDay), MergeDateAndTime(date, endTime.TimeOfDay));
		}

		public static DateSpan GetNextDateSpan(DateTime startingDate, DateTime startTime, DateTime endTime,
											   DateTime? endRecurringDate, DayOfWeek day)
		{
			//can't start until started
			if (startingDate.Date < startTime.Date)
			{
				startingDate = startTime;
			}

			if (endRecurringDate != null)
			{
				DateTime next = GetNextOccurrence(startingDate, day);
				DateSpan nextDateSpan = GetSameDayDateSpan(next, startTime, endTime);

				if (nextDateSpan.BeginsBeforeOrOn(endRecurringDate))
				{
					return nextDateSpan;
				}
				else
				{
					return null;
				}
			}
			else
			{
				if (startTime.Date < startingDate.Date)
				{
					//null if time has passed
					return null;
				}
				else
				{
					return GetSameDayDateSpan(startTime, startTime, endTime);
				}
			}
		}

		public static bool IsValidMonth(int month)
		{
			return 0 < month && month < 13;
		}

		//public static bool IsValidYear(int year)
		//{
		//    return DateTime.MinValue.Year <= year && year <= DateTime.MaxValue.Year;
		//}
		public static bool IsValidExpirationYear(int year)
		{
			return 0 <= year && year < 100;
		}

		//public static bool IsValidExpirationDate(int month, int year)
		//{
		//    DateTime now = DateTime.Now;
		//    int currentMonth = now.Month;
		//    int currentYear = now.Year;
		//
		//    return
		//        (IsValidMonth(month) && month >= currentMonth)
		//        && (IsValidYear(year) && year >= currentYear);
		//}
		/// <summary>
		/// Returns a DateTime that represents the month and year as an expiration date
		/// </summary>
		/// <param name="month"></param>
		/// <param name="year">2 digit year</param>
		/// <returns></returns>
		public static bool TryGetExpirationDate(int month, int year, out DateTime expirationDate)
		{
			if (!IsValidMonth(month) || !IsValidExpirationYear(month))
			{
				expirationDate = DateTime.MinValue;
				return false;
			}

			expirationDate = new DateTime(2000 + year, month, 1, 0, 0, 0, 0);
			expirationDate = GetMonthEnd(expirationDate);
			return true;
		}

		public static bool IsValidExpirationDate(DateTime date)
		{
			DateTime now = DateTime.Now;
			return date > now;
		}

		public static int GetExpirationMonth(DateTime expirationDate)
		{
			return expirationDate.Month;
		}

		public static int GetExpirationYear(DateTime expirationDate)
		{
			int year = expirationDate.Year;
			if (2000 <= year && year < 2100)
			{
				return year - 2000;
			}
			return 0;
		}

		public static DateTime? EndRecurringDateToDaysEnd(DateTime? endRecurringDate, DayOfWeek recurrenceDayOfWeek)
		{
			if (endRecurringDate != null)
			{
				//if ends recurrence on same day of week
				if (endRecurringDate.Value.DayOfWeek == recurrenceDayOfWeek)
				{
					endRecurringDate = endRecurringDate.Value.Date.AddDays(1).AddMilliseconds(-3);
				}
			}
			return endRecurringDate;
		}

		#region Get Number of Exclude Days

		public static int GetNumExcludeDays(DateTime startDate, DateTime endDate, DayOfWeek? excludeDay)
		{
			int numberOfExcludeDays = 0;
			if (excludeDay != null)
			{
				if (startDate.Date == endDate.Date) //check if dates are equal
				{
					numberOfExcludeDays = 0;
				}
				else if (endDate > startDate) //check if endDate is after startDate
				{
					numberOfExcludeDays = GetNumExcludeDaysFunction(startDate, endDate, excludeDay.Value);
				}
				else
				{
					//swap start and end dates and return the value as negative
					numberOfExcludeDays = -GetNumExcludeDaysFunction(endDate, startDate, excludeDay.Value);
				}
			}
			return numberOfExcludeDays;
		}

		private static int GetNumExcludeDaysFunction(DateTime startDate, DateTime endDate, DayOfWeek excludeDay)
		{
			//TimeSpan remainingDays = endDate - startDate;
			TimeSpan remainingDays = endDate.Date - startDate.Date;

			//don't exclude day until it has been spanned.
			//	ending on excludeDay won't exclude it, but ending on day after excludeDay will exclude it
			//eg: excludeDay:Sunday
			//		startDate:Friday, endDate:Saturday	= exclude 0 days; span friday, 1 day
			//		startDate:Friday, endDate:Sunday	= exclude 0 days; span friday and saturday, 2 days
			//		startDate:Friday, endDate:Monday	= exclude 1 days; span friday and saturday, but not sunday, 2 days
			int endOfWeek = (int)excludeDay + 1;
			if (endOfWeek > 6) endOfWeek = 0;
			//get the number of days between startdate and the end of the week
			TimeSpan subFromStart = GetWeekEnd(startDate, (DayOfWeek)endOfWeek) - startDate;

			//get the number of times the excludeDay is contained within the time period
			//	-take off subFromStart days from remaining days to make the division work
			//	-divide by 7 and get the ceiling
			var numberOfExcludeDays = (int)Math.Ceiling((remainingDays.Days - subFromStart.Days) / 7d);
			return numberOfExcludeDays;
		}

		#endregion //Get Number of Exclude Days
	}

	public class DateValidator
	{
		public DateValidator(string dateText)
		{
			Text = StringUtility.NullIfWhiteSpace(dateText);

			DateTime tempDate;
			if (DateTime.TryParse(Text, out tempDate))
			{
				Date = tempDate;
				//IsValidDate = true;
				IsValidSqlDate = DateUtility.ValidSqlDate(tempDate);
			}
		}

		public DateValidator(DateTime date)
		{
			//IsValidDate = true;
			IsValidSqlDate = DateUtility.ValidSqlDate(date);
			Date = date;
			Text = date.ToString();
		}

		public string Text { get; private set; }

		//public bool IsValid
		//{
		//    get { return IsValidDate && IsValidSqlDate; }
		//}
		//public bool IsValidDate { get; private set; }
		public bool IsValidSqlDate { get; private set; }

		public DateTime? Date { get; private set; }
	}
}