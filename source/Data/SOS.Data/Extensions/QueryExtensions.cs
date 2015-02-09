using SubSonic;

namespace SOS.Data.Extensions
{
	public static class QueryExtensions
	{
		public static TList ToCollection<T, TList>(this Query q)
			where T : ActiveRecord<T>, new()
			where TList : ActiveList<T, TList>, new()
		{
			var result = new TList();
			result.LoadAndCloseReader(q.ExecuteReader());
			return result;
		}
		public static T ToItem<T>(this Query q)
			where T : ActiveRecord<T>, new()
		{
			var result = new T();
			result.LoadAndCloseReader(q.ExecuteReader());
			return result.IsLoaded ? result : null;
		}

		public static TList ToReadOnlyCollection<T, TList>(this Query q)
			where T : ReadOnlyRecord<T>, new()
			where TList : ReadOnlyList<T, TList>, new()
		{
			var result = new TList();
			result.LoadAndCloseReader(q.ExecuteReader());
			return result;
		}
		public static T ToReadOnlyItem<T>(this Query q)
			where T : ReadOnlyRecord<T>, new()
		{
			var result = new T();
			result.LoadAndCloseReader(q.ExecuteReader());
			return result.IsLoaded ? result : null;
		}

		public static T ExecuteScalar<T>(this Query q)
		{
			return StoredProcedureExtensions.ExecuteScalar<T>(q.ExecuteScalar());
		}

		#region Filters

		public static Query Active(this Query qry)
		{
			return qry.IsDeleted(false);
		}
		public static Query Deleted(this Query qry)
		{
			return qry.IsDeleted(true);
		}
		public static Query IsDeleted(this Query qry, bool isDeleted)
		{
			return qry.WHERE("IsDeleted", isDeleted);
		}

		#endregion //Filters
	}
}
