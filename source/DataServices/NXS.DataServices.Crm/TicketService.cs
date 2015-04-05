﻿using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class TicketService
	{
		string _gpEmployeeId;
		public TicketService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<List<TsTeam>>> TeamsAsync()
		{
			using (var db = CrmDb.Connect())
			{
				var items = await db.TS_Teams.AllFullAsync().ConfigureAwait(false);
				var result = new Result<List<TsTeam>>(value: items.ConvertAll(item => TsTeam.FromDb(item)));
				return result;
			}
		}
		public async Task<Result<TsTeam>> SaveTeamAsync(TsTeam team)
		{
			using (var db = CrmDb.Connect())
			{
				var result = new Result<TsTeam>();
				var tbl = db.TS_Teams;
				var item = await db.TS_Teams.ByIdFullAsync(team.TeamId).ConfigureAwait(false);
				if (item == null)
				{
					item = new TS_Team();
					team.ToDb(item);
					item.ModifiedOn = item.CreatedOn = DateTime.UtcNow;
					item.ModifiedBy = item.CreatedBy = _gpEmployeeId;
					await tbl.InsertAsync(item, hasIdentity: false).ConfigureAwait(false);

					item = await db.TS_Teams.ByIdFullAsync(team.TeamId).ConfigureAwait(false);
					result.Value = TsTeam.FromDb(item);
				}
				else
				{
					team.ToDb(item);
					item.ModifiedOn = DateTime.UtcNow;
					item.ModifiedBy = _gpEmployeeId;
					await tbl.UpdateAsync(item.TeamId, item).ConfigureAwait(false);

					result.Value = TsTeam.FromDb(item);
				}

				return result;
			}
		}
	}
}