using System;
using System.Collections.Generic;
using SOS.Data.Extensions;
using SubSonic;

namespace SOS.Data
{
	public class BaseModelController<T> where T : new()
	{
		public T LoadSingleByQuery(Query q)
		{
			return q.ExecuteReader().ToSingle<T>();
		}

		public IList<T> LoadCollectionByQuery(Query q)
		{
			return q.ExecuteReader().ToList<T>();
		}

		public T LoadSingleByProcedure(StoredProcedure sp)
		{
			return sp.GetReader().ToSingle<T>();
		}

		public IList<T> LoadCollectionByProcedure(StoredProcedure sp)
		{
			return sp.GetReader().ToList<T>();
		}

		[Obsolete("Remove we are done with this. User LoadCollectionByProcedure", true)]
		public IList<T> LoadCollection(StoredProcedure sp)
		{
			return sp.GetReader().ToList<T>();
		}
	}
}
