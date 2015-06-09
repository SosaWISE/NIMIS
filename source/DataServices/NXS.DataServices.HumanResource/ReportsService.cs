using NXS.Data;
using NXS.Data.HumanResource;
//using NXS.DataServices.HumanResource.Models;
using SOS.Lib.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using NXS.DataServices.HumanResource.Models;

namespace NXS.DataServices.HumanResource
{
	public class ReportsService
	{
		//string _gpEmployeeId;
		//public ReportsService(string gpEmployeeId)
		//{
		//	_gpEmployeeId = gpEmployeeId;
		//}

		static readonly HashSet<string> knownReports = new HashSet<string>(new string[]{
			"custReport_CreditAndInstalls",
		});

		public async Task<Result<List<dynamic>>> RunReport(string name, IDictionary<string, string> qryParams)
		{
			var result = new Result<List<dynamic>>();

			name = "custReport_" + name;
			if (!knownReports.Contains(name))
				return result.Fail(-1, "Unknown report");

			var p = new Dapper.DynamicParameters();
			foreach (var kvp in qryParams)
				p.Add(kvp.Key, kvp.Value);

			using (var db = DBase.Connect())
			{
				var items = await db.QueryAsync(name, p, commandType: System.Data.CommandType.StoredProcedure);
				result.Value = items.ToList();
			}
			return result;
		}
	}
}
