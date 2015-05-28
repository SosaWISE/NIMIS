using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	using AR = AC_Action;
	using ARCollection = IEnumerable<AC_Action>;
	using ARTable = DBase.AC_ActionTable;
	public static class AC_ActionTableExtensions
	{
		public static ARCollection AllActive(this ARTable tbl)
		{
			var sql = Sequel.NewSelect(tbl.Star).From(tbl)
				.WhereActiveAndNotDeleted();
			return tbl.Db.Query<AR>(sql.Sql, sql.Params);
		}
	}
}
