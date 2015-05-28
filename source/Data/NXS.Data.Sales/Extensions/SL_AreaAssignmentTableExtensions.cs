﻿using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace NXS.Data.Sales
{
	using AR = SL_AreaAssignment;
	using ARCollection = IEnumerable<SL_AreaAssignment>;
	using ARTable = DBase.SL_AreaAssignmentTable;
	public static class SL_AreaAssignmentTableExtensions
	{
		public static async Task InsertAsync(this ARTable tbl, AR item, string gpEmployeeId)
		{
			item.ModifiedOn = item.CreatedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = item.CreatedBy = gpEmployeeId;
			item.ID = await tbl.InsertAsync(item).ConfigureAwait(false);
		}
		public static async Task UpdateAsync(this ARTable tbl, Snapshotter.Snapshot<AR> snapshot, string gpEmployeeId)
		{
			if (!snapshot.HasChange()) return;
			var item = snapshot.Value;
			item.ModifiedOn = DateTime.UtcNow.RoundToSqlDateTime();
			item.ModifiedBy = gpEmployeeId;
			await tbl.UpdateAsync(item.ID, snapshot.Diff()).ConfigureAwait(false);
		}
	}
}
