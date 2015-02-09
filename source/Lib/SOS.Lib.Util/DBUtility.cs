using System;

namespace SOS.Lib.Util
{
	public class DBUtility
	{
		public static T SafeCast<T>(object obj)
		{
			if (!Convert.IsDBNull(obj))
			{
				return (T) obj;
			}
			return default(T);
		}

		public static void CalculatePaging(int currentPage, int? pageSize, out int? pageStart, out int? pageEnd)
		{
			if (pageSize != null)
			{
				pageStart = (currentPage - 1)*pageSize.Value + 1;
				pageEnd = currentPage*pageSize.Value;
			}
			else
			{
				pageStart = pageEnd = null;
			}
		}

		public static int CalculatePages(int count, int? pageSize)
		{
			if (pageSize != null && count > 0)
			{
				if (pageSize.Value > 0)
				{
					return (int) Math.Ceiling(count/(double) pageSize.Value);
				}
				else
				{
					return count;
				}
			}
			return 0;
		}
	}
}