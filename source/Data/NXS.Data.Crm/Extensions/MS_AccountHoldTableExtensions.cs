using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	using AR = MS_AccountHold;
	using ARCollection = IEnumerable<MS_AccountHold>;
	using ARTable = CrmDb.MS_AccountHoldTable;
	public static class MS_AccountHoldTableExtensions
	{
		public static Task<ARCollection> ByAccountIdAsync(this ARTable tbl, long accountId)
		{
			var qry = Sequel.NewSelect(tbl.Star).From(tbl).Where(tbl.AccountId, Comparison.Equals, accountId);
			return tbl.Db.QueryAsync<AR>(qry.Sql, qry.Params);
		}
	}
}
