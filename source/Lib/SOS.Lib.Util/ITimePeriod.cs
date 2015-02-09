using System;

namespace SOS.Lib.Util
{
	public interface ITimePeriod
	{
		string DateFormat { get; }
		DateTime GetStartDate(DateTime date);
		DateTime GetEndDate(DateTime date);
		DateTime GetNextDate(DateTime date);
	}

	public class DayTimePeriod : ITimePeriod
	{
		#region ITimePeriod Members

		public DateTime GetStartDate(DateTime date)
		{
			return date.Date;
		}

		public DateTime GetEndDate(DateTime date)
		{
			return date.Date.AddDays(1);
		}

		public DateTime GetNextDate(DateTime date)
		{
			return date.AddDays(1);
		}

		public string DateFormat
		{
			get { return "dd"; }
		}

		#endregion
	}

	public class WeekTimePeriod : ITimePeriod
	{
		public WeekTimePeriod(DayOfWeek startWeekDay)
		{
			StartWeekDay = startWeekDay;
		}

		public DayOfWeek StartWeekDay { get; set; }

		#region ITimePeriod Members

		public DateTime GetStartDate(DateTime date)
		{
			return DateUtility.GetWeekStart(date, StartWeekDay);
		}

		public DateTime GetEndDate(DateTime date)
		{
			return DateUtility.GetWeekEnd(date, StartWeekDay);
		}

		public DateTime GetNextDate(DateTime date)
		{
			return DateUtility.AddWeek(date);
		}

		public string DateFormat
		{
			get { return "MM/dd"; }
		}

		#endregion
	}

	public class MonthTimePeriod : ITimePeriod
	{
		#region ITimePeriod Members

		public DateTime GetStartDate(DateTime date)
		{
			return DateUtility.GetMonthStart(date);
		}

		public DateTime GetEndDate(DateTime date)
		{
			return DateUtility.GetMonthEnd(date);
		}

		public DateTime GetNextDate(DateTime date)
		{
			return date.AddMonths(1);
		}

		public string DateFormat
		{
			get { return "yyyy-dd"; }
		}

		#endregion
	}
}