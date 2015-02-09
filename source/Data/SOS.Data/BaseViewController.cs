using SubSonic;
using SOS.Data.Extensions;

namespace SOS.Data
{
	public class BaseViewController<T, TList>
		where T : ReadOnlyRecord<T>, new()
		where TList : ReadOnlyList<T, TList>, new()
	{
		//Query
		public T LoadSingle(Query q)
		{
			return q.ToReadOnlyItem<T>();
		}
		public TList LoadCollection(Query q)
		{
			return q.ToReadOnlyCollection<T, TList>();
		}

		//SqlQuery
		public T LoadSingle(SqlQuery q)
		{
			return q.ExecuteSingle<T>();
		}
		public TList LoadCollection(SqlQuery q)
		{
			return q.ExecuteAsCollection<TList>();
		}

		//SPs
		public T LoadSingle(StoredProcedure sp)
		{
			return sp.ToReadOnlyItem<T>();
		}
		public TList LoadCollection(StoredProcedure sp)
		{
			return sp.ToReadOnlyCollection<T, TList>();
		}
	}
}
