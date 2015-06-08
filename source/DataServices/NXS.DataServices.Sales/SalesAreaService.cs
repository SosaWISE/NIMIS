using NXS.Data;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NXS.Data.Sales;
using NXS.DataServices.Sales.Models;
using Newtonsoft.Json;

namespace NXS.DataServices.Sales
{
	public class SalesAreaService
	{
		string _gpEmployeeId;
		public SalesAreaService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<List<SlArea>>> SalesAreasInBoundsAsync(string repCompanyID, int teamId, decimal minlat, decimal minlng, decimal maxlat, decimal maxlng)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<List<SlArea>>();
				var tbl = db.SL_Areas;
				var items = await db.SL_Areas.InBoundsAsync(repCompanyID, teamId, minlat, maxlat, minlng, maxlng).ConfigureAwait(false);
				result.Value = items.ConvertAll(a => SlArea.FromDb(a));
				return result;
			}
		}

		public async Task<Result<SlArea>> SaveSalesAreaAsync(SlArea inputItem)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<SlArea>();
				var tbl = db.SL_Areas;

				SL_Area item;
				if (inputItem.ID > 0)
				{
					item = await tbl.ByIdAsync(inputItem.ID).ConfigureAwait(false);
					if (item == null)
						return result.Fail(-1, "Invalid area id");
					var snapshot = Snapshotter.Start(item);
					inputItem.ToDb(item);
					await tbl.UpdateAsync(snapshot, _gpEmployeeId).ConfigureAwait(false);
				}
				else
				{
					item = new SL_Area();
					inputItem.ToDb(item);
					await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);
					inputItem.ID = item.ID;
				}

				result.Value = SlArea.FromDb(item);
				return result;
			}
		}

		public async Task<Result<bool>> DeleteSalesAreaAsync(int areaId)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<bool>();
				var tbl = db.SL_Areas;
				var item = await tbl.ByIdAsync(areaId).ConfigureAwait(false);
				if (item == null)
					return result.Fail(-1, "Invalid area id");
				var snapshot = Snapshotter.Start(item);
				item.IsDeleted = true;
				await tbl.UpdateAsync(snapshot, _gpEmployeeId);
				result.Value = true;
				return result;
			}
		}
	}
}
