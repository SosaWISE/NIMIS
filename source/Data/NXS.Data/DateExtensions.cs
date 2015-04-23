
namespace System
{
	public static class DateExtensions
	{
		public static DateTime RoundToSqlDateTime(this DateTime date)
		{
			return new System.Data.SqlTypes.SqlDateTime(date).Value;
		}
	}
}
