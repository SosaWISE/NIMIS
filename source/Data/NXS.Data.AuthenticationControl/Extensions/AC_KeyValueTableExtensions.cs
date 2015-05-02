using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	using AR = AC_KeyValue;
	using ARCollection = IEnumerable<AC_KeyValue>;
	using ARTable = DBase.AC_KeyValueTable;
	public static class AC_KeyValueTableExtensions
	{
		public static ARCollection AllWithUpdateLock(this ARTable tbl)
		{
			// load all and lock table so we're the exclusive editors/readers
			// http://stackoverflow.com/a/4597035
			var sql = Sequel.NewSelect(tbl.Star).From(tbl).With("UPDLOCK,TABLOCK");
			return tbl.Db.Query<AR>(sql.Sql, sql.Params);
		}
	}
}
