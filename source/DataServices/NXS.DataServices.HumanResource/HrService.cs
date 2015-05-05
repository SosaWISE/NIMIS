using NXS.Data;
using NXS.Data.HumanResource;
//using NXS.DataServices.HumanResource.Models;
using SOS.Lib.Core;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.DataServices.HumanResource
{
	public class HrService
	{
		string _gpEmployeeId;
		public HrService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<List<PossibleReportTo>>> PossibleReportTosAsync(int seasonID, int userTypeID)
		{
			using (var db = DBase.Connect())
			{
				var items = await db.RU_Recruits.PossibleReportTos(seasonID, userTypeID).ConfigureAwait(false);
				return new Result<List<PossibleReportTo>>(value: items.ToList());
			}
		}
	}
}
