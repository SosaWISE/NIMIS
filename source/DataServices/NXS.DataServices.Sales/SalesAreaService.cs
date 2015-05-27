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

		public async Task<Result<SlArea>> SaveSalesAreaAsync(AreaInput inputItem)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<SlArea>();
				var tbl = db.SL_Areas;

				SL_Area item;
				if (inputItem.AreaId > 0)
				{
					item = await tbl.ByIdAsync(inputItem.AreaId).ConfigureAwait(false);
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
					inputItem.AreaId = item.ID;
				}

				// update assignments 
				await SaveAreaAssignmentAsync(db, inputItem).ConfigureAwait(false);

				result.Value = SlArea.FromDb(item);
				return result;
			}
		}
		// saves the assigned office or sales rep to the area id in the database
		private async Task<SlAreaAssignment> SaveAreaAssignmentAsync(DBase db, AreaInput inputItem)
		{
			var tbl = db.SL_AreaAssignments;

			// unassign the area assignments for area, if any
			await DeleteSalesAreaAssignmentsAsync(db, inputItem.AreaId).ConfigureAwait(false);

			// don't assign if the office or rep is not provided
			if (inputItem.OfficeId <= 0 || string.IsNullOrEmpty(inputItem.RepCompanyID))
				return null;
			// assign it to the rep and/or office
			var item = new SL_AreaAssignment();
			inputItem.ToDb(item);
			await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);
			return SlAreaAssignment.FromDb(item); // ??????
		}
		private async Task DeleteSalesAreaAssignmentsAsync(DBase db, int areaId)
		{
			var tbl = db.SL_AreaAssignments;
			var item = await tbl.ByIdAsync(areaId).ConfigureAwait(false);
			if (item == null)
				return; // nothing to do here
			var snapshot = Snapshotter.Start(item);
			item.IsDeleted = true;
			await tbl.UpdateAsync(snapshot, _gpEmployeeId);
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
