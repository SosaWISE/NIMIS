using NXS.Data.HumanResource;
//using NXS.DataServices.HumanResource.Models;
using SOS.Lib.Core;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.DataServices.HumanResource
{
	public class ReportsService
	{
		//string _gpEmployeeId;
		//public ReportsService(string gpEmployeeId)
		//{
		//	_gpEmployeeId = gpEmployeeId;
		//}

		static readonly HashSet<string> KnownReports = new HashSet<string>(new []{
			"custReport_CreditAndInstalls",
			"custReport_MyAccounts",
			"custReport_Performance",
			"custReport_PerformanceOfficeBreakDown",
			"custReport_PerformanceSalesRepBreakDown",
			"custReport_AccountHolds",
			"custReport_PendingInstalls"
		});

		public async Task<Result<List<dynamic>>> RunReport(string name, IDictionary<string, string> qryParams)
		{
			var result = new Result<List<dynamic>>();

			name = "custReport_" + name;
			if (!KnownReports.Contains(name))
				return result.Fail(-1, "Unknown report");

			var p = new Dapper.DynamicParameters();
			foreach (var kvp in qryParams)
			{
				if (string.IsNullOrEmpty(kvp.Value))
					continue;
				p.Add(kvp.Key, kvp.Value);
			}

			using (var db = DBase.Connect())
			{
				var items = await db.QueryAsync(name, p, commandType: System.Data.CommandType.StoredProcedure);
				result.Value = items.ToList();
			}
			return result;
		}
	}
}
