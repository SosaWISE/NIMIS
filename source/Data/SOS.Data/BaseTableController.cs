using SOS.Data.Extensions;
using SubSonic;

namespace SOS.Data
{
	public class BaseTableController<T, TList>
		where T : ActiveRecord<T>, new()
		where TList : ActiveList<T, TList>, new()
	{
		public T LoadByPrimaryKey(string szId)
		{
			var result = new T();
			result.LoadByKey(szId);
			return result.IsLoaded ? result : null;
		}

		public T LoadByPrimaryKey(int nId)
		{
			var result = new T();
			result.LoadByKey(nId);
			return result.IsLoaded ? result : null;
		}

		public T LoadByPrimaryKey(long lId)
		{
			var result = new T();
			result.LoadByKey(lId);
			return result.IsLoaded ? result : null;
		}

		public TList LoadAll()
		{
			return LoadCollection(ActiveRecord<T>.Query());
		}


		//Query
		public T LoadSingle(Query q)
		{
			return q.ToItem<T>();
		}
		public TList LoadCollection(Query q)
		{
			return q.ToCollection<T, TList>();
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
			return sp.ToItem<T>();
		}
		public TList LoadCollection(StoredProcedure sp)
		{
			return sp.ToCollection<T, TList>();
		}
	}
}