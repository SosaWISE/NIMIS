using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SOS.Lib.Util
{
	public class DateSpanList : List<DateSpan>
	{
		public DateSpanList MergeLikeSpans()
		{
			DateSpanList temp = Copy();
			temp.Sort();

			var result = new DateSpanList();
			int length = temp.Count;
			if (length > 0)
			{
				int current = 0;
				int i = 0;

				result.Add(temp[i]);

				i++;
				for (i = 1; i < length; i++)
				{
					if (result[current].CanMerge(temp[i]))
					{
						result[current] = result[current].Merge(temp[i]);
					}
					else
					{
						result.Add(temp[i]);
						current++;
					}
				}
			}
			return result;
		}

		public DateSpanList Copy()
		{
			var result = new DateSpanList();
			foreach (DateSpan span in this)
			{
				result.Add(span);
			}
			return result;
		}

		public void Sort(SortOrder order)
		{
			if (order != SortOrder.Unspecified)
			{
				Sort(new DateSpanComparer(order));
			}
		}
	}

	public class DateSpan : IComparable<DateSpan>
	{
		private static readonly string parseSplitValue = "-";
		private readonly DateTime _endDate;
		private readonly DateTime _startDate;

		public DateSpan(DateTime startDate, DateTime endDate)
		{
			if (!DateUtility.IsValidDateSpan(startDate, endDate))
				throw new Exception("Invalid Date Span");

			_startDate = startDate;
			_endDate = endDate;
		}

		public string DateFormat { get; set; }

		public DateTime StartDate
		{
			get { return _startDate; }
		}

		public DateTime EndDate
		{
			get { return _endDate; }
		}

		#region IComparable<DateSpan> Members

		public int CompareTo(DateSpan other)
		{
			int result;
			//starts first
			//don't care about end
			result = StartDate.CompareTo(other.StartDate);
			if (result == 0)
			{
				//same start
				//ends first
				result = EndDate.CompareTo(other.EndDate);
			}
			return result;
		}

		#endregion

		public static DateSpan Parse(string value)
		{
			string[] ray = value.Split(new[] {parseSplitValue}, StringSplitOptions.None);
			if (ray.Length != 2)
			{
				throw new Exception("Invalid DateSpan Parse Value");
			}

			var startDate = new DateTime(long.Parse(ray[0]));
			var endDate = new DateTime(long.Parse(ray[1]));

			return new DateSpan(startDate, endDate);
		}

		public string GetParsableValue()
		{
			return StartDate.Ticks + parseSplitValue + EndDate.Ticks;
		}

		public override string ToString()
		{
			return ToString(DateFormat);
			//return string.Format("{0} - {1}", StartDate, EndDate);
		}

		public string ToString(string dateFormat)
		{
			return string.Format("{0} - {1}", StartDate.ToString(dateFormat), EndDate.ToString(dateFormat));
		}

		public bool Overlaps(DateSpan other)
		{
			//if either is true the blocks don't overlap, return opposite
			return !(other.EndDate <= StartDate || other.StartDate >= EndDate);
		}

		public bool CanMerge(DateSpan other)
		{
			//same as Overlaps but includes touching
			//if either is true the blocks don't touch or overlap, return opposite
			return !(other.EndDate < StartDate || other.StartDate > EndDate);
		}

		public TimeSpan OverlapTimeSpan(DateSpan other)
		{
			if (Overlaps(other))
			{
				return DateUtility.Min(EndDate, other.EndDate) - DateUtility.Max(StartDate, other.StartDate);
			}
			return TimeSpan.Zero;
		}

		public double OverlapPercent(DateSpan other)
		{
			TimeSpan overlapSpan = OverlapTimeSpan(other);
			TimeSpan thisSpan = EndDate - StartDate;
			return overlapSpan.TotalMilliseconds/thisSpan.TotalMilliseconds;
		}

		public bool BeginsBefore(DateSpan span)
		{
			return BeginsBefore(span.StartDate);
		}

		public bool BeginsBefore(DateTime? date)
		{
			//returns false if date is null
			return StartDate < date;
		}

		public bool BeginsBeforeOrOn(DateTime? date)
		{
			//returns false if date is null
			return StartDate <= date;
		}

		public bool EndsBefore(DateSpan span)
		{
			return EndsBefore(span.EndDate);
		}

		public bool EndsBefore(DateTime? date)
		{
			//returns false if date is null
			return EndDate < date;
		}

		public DateSpan Merge(DateSpan other)
		{
			return new DateSpan(DateUtility.Min(StartDate, other.StartDate), DateUtility.Max(EndDate, other.EndDate));
		}

		public override bool Equals(object obj)
		{
			if (obj is DateSpan)
			{
				return this == (DateSpan) obj;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return StartDate.GetHashCode() + EndDate.GetHashCode();
		}

		public static bool operator ==(DateSpan left, DateSpan right)
		{
			// If both are null, or both are same instance, return true.
			if (ReferenceEquals(left, right))
			{
				return true;
			}

			// If one is null, but not both, return false.
			if (((object) left == null) || ((object) right == null))
			{
				return false;
			}

			return left.StartDate == right.StartDate && left.EndDate == right.EndDate;
		}

		public static bool operator !=(DateSpan left, DateSpan right)
		{
			return !(left == right);
		}

		public DateSpanList SplitByTimePeriod(ITimePeriod timePeriod)
		{
			var result = new DateSpanList();

			DateTime date = timePeriod.GetStartDate(StartDate);

			//check if at end
			while (date.CompareTo(EndDate) < 0)
			{
				var span = new DateSpan(date, timePeriod.GetEndDate(date));
				span.DateFormat = timePeriod.DateFormat;
				result.Add(span);

				//increment
				date = timePeriod.GetNextDate(date);
			}

			return result;
		}
	}

	public class DateSpanComparer : IComparer<DateSpan>
	{
		public DateSpanComparer(SortOrder order)
		{
			Order = order;
		}

		public SortOrder Order { get; set; }

		#region IComparer<DateSpan> Members

		public int Compare(DateSpan x, DateSpan y)
		{
			switch (Order)
			{
				case SortOrder.Unspecified:
					return 0;
				case SortOrder.Ascending:
					return x.CompareTo(y);
				case SortOrder.Descending:
					return y.CompareTo(x);
				default:
					throw new NotSupportedException();
			}
		}

		#endregion
	}
}