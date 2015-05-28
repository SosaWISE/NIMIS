using Dapper;
using NXS.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.Data.Sales
{
	public partial class Sprocs
	{
		private readonly DBase db;
		public Sprocs(DBase db)
		{
			this.db = db;
		}

		public Task<IEnumerable<T>> wiseSP_ExceptionsThrown<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("wiseSP_ExceptionsThrown", p, commandType: System.Data.CommandType.StoredProcedure);
		}
	}
}

