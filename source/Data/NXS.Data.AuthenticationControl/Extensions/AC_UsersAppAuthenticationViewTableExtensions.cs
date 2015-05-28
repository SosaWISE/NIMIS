using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	using AR = AC_UsersAppAuthenticationView;
	using ARCollection = IEnumerable<AC_UsersAppAuthenticationView>;
	using ARTable = DBase.AC_UsersAppAuthenticationViewTable;
	public static class AC_UsersAppAuthenticationViewTableExtensions
	{
		public static AR ByUsername(this ARTable tbl, string username)
		{
			var sql = Sequel.NewSelect().Top("1")
				.Columns(tbl.Star)
				.From(tbl)
				.Where(tbl.Username, Comparison.Equals, username)
				.And(tbl.IsActive, Comparison.Equals, true);
			return tbl.Db.Query<AR>(sql.Sql, sql.Params).FirstOrDefault();
		}
	}
}
