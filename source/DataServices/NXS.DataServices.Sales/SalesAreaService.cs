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
		//string _gpEmployeeId;
		//public SalesAreaService(string gpEmployeeId)
		//{
		//	_gpEmployeeId = gpEmployeeId;
		//}

		public async Task<List<dynamic>> SalesAreasAsync(int salesRepId, int officeId, double minlat, double minlng, double maxlat, double maxlng)
		{
			using (var db = DBase.Connect())
			{
				var sql = @"SELECT sa.id, sa.pointData, sa.minLatitude, sa.maxLatitude, sa.minLongitude, sa.maxLongitude,
					ifnull(saa.officeId,0) as officeId, ifnull(saa.salesRepId,0) as salesRepId, saa.startTimestamp, saa.endTimestamp, 
					CONCAT(u.firstName, ' ', u.lastName) AS salesRepName 
					FROM salesAreas sa
					LEFT JOIN salesAreaAssignments saa ON sa.id=saa.salesAreaId AND saa.status='A'
					LEFT JOIN users AS u ON saa.salesRepId=u.id
					WHERE sa.maxLatitude > @minlat AND sa.minLatitude < @maxlat AND sa.maxLongitude > @minlng AND sa.minLongitude < @maxlng
					AND sa.status='A'";
				if (salesRepId != 0)
					sql += " AND saa.salesRepId=@salesRepId ";
				if (officeId != 0)
					sql += " AND saa.officeId=@officeId";

				var result = (await db.QueryAsync(sql, new { salesRepId, officeId, minlat, minlng, maxlat, maxlng }).ConfigureAwait(false)).ToList();
				return result;
			}
		}

		public async Task<SalesArea> SaveSalesAreaAsync(AreaInput inputItem)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.SalesAreas;

				SalesArea item;
				if (inputItem.areaId > 0)
				{
					item = await tbl.ByIdAsync(inputItem.areaId).ConfigureAwait(false);
					if (item == null)
						return null;
					var snapshot = Snapshotter.Start(item);
					inputItem.ToDb(item);
					await tbl.UpdateAsync(item.id, snapshot.Diff()).ConfigureAwait(false);
				}
				else
				{
					item = new SalesArea();
					inputItem.ToDb(item);
					inputItem.areaId = item.id = await tbl.InsertAsync(item).ConfigureAwait(false);
				}

				// update assignments 
				await SaveAreaAssignmentAsync(db, inputItem).ConfigureAwait(false);

				return item;
			}
		}
		// saves the assigned office or sales rep to the area id in the database
		private async Task<SalesAreaAssignment> SaveAreaAssignmentAsync(DBase db, AreaInput inputItem)
		{
			var tbl = db.SalesAreaAssignments;

			// unassign the area assignments for area
			await DeleteSalesAreaAssignmentsAsync(db, inputItem.areaId).ConfigureAwait(false);

			// don't assign if the office or rep is not provided
			if (inputItem.officeId <= 0 || inputItem.salesRepId <= 0)
				return null;
			// assign it to the rep and/or office
			var item = new SalesAreaAssignment();
			inputItem.ToDb(item);
			item.id = await tbl.InsertAsync(item).ConfigureAwait(false);
			return item;
		}
		private Task<int> DeleteSalesAreaAssignmentsAsync(DBase db, int areaId)
		{
			var tbl = db.SalesAreaAssignments;
			return tbl.UpdateAsync(areaId, new { status = "X" });
		}


		public async Task<bool> DeleteSalesAreaAsync(int areaId)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.SalesAreas;
				var deleted = 0 != await tbl.UpdateAsync(areaId, new { status = "X" });
				return deleted;
			}
		}
	}
}
