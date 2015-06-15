using NXS.Data;
using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
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
			using (var db = DBase.Connect())
			{
				var items = await db.TS_Teams.AllFullAsync().ConfigureAwait(false);
				var result = new Result<List<TsTeam>>(value: items.ConvertAll(item => TsTeam.FromDb(item)));
				return result;
			}
		}
		public async Task<Result<List<TsTeam>>> SalesTeamsAsync()
		{
			using (var db = DBase.Connect())
			{
				var items = await db.TS_Teams.AllSalesTeamsFullAsync().ConfigureAwait(false);
				var result = new Result<List<TsTeam>>(value: items.ConvertAll(item => TsTeam.FromDb(item)));
				return result;
			}
		}
		public async Task<Result<TsTeam>> SaveTeamAsync(TsTeam team)
		{
			using (var db = DBase.Connect())
			{
				var result = new Result<TsTeam>();
				var tbl = db.TS_Teams;
				var item = await tbl.ByIdFullAsync(team.TeamId).ConfigureAwait(false);
				if (item == null)
				{
					item = new TS_Team();
					team.ToDb(item);
					await tbl.InsertAsync(item, _gpEmployeeId).ConfigureAwait(false);

					item = await tbl.ByIdFullAsync(team.TeamId).ConfigureAwait(false);
					result.Value = TsTeam.FromDb(item);
				}
				else
				{
					var snapShot = Snapshotter.Start(item);
					team.ToDb(item);
					await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);

					result.Value = TsTeam.FromDb(item);
				}

				return result;
			}
		}
	}
}
