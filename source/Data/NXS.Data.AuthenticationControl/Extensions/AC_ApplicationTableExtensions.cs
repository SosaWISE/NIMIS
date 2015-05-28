using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	using AR = AC_Application;
	using ARCollection = IEnumerable<AC_Application>;
	using ARTable = DBase.AC_ApplicationTable;
	public static class AC_ApplicationTableExtensions
	{
		public static ARCollection AllActive(this ARTable tbl)
		{
			var sql = Sequel.NewSelect(tbl.Star).From(tbl)
				.WhereActiveAndNotDeleted();
			return tbl.Db.Query<AR>(sql.Sql, sql.Params);
		}
	}
}
