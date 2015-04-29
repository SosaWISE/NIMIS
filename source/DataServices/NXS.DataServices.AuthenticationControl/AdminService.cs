using NXS.Data.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.AuthenticationControl
{
	public class AdminService
	{
		string _gpEmployeeId;
		public AdminService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<List<MetadataType>>> ActionsAsync()
		{
			using (var db = AuthControlDb.Connect())
			{
				var tbl = db.AC_Actions;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				return new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromAction(item)));
			}
		}
		public async Task<Result<List<MetadataType>>> ApplicationsAsync()
		{
			using (var db = AuthControlDb.Connect())
			{
				var tbl = db.AC_Applications;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				return new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromApplication(item)));
			}
		}

		public async Task<Result<List<GroupItem>>> GroupItemsAsync()
		{
			using (var db = AuthControlDb.Connect())
			{
				// fetch both
				var actions = await db.AC_GroupActions.AllAsync().ConfigureAwait(false);
				var applications = await db.AC_GroupApplications.AllAsync().ConfigureAwait(false);
				// convert all
				var items = actions.ConvertAll(item => GroupItem.FromGroupAction(item));
				items.AddRange(applications.ConvertAll(item => GroupItem.FromGroupAction(item)));
				return new Result<List<GroupItem>>(value: items);
			}
		}

		//public async Task<Result<List<GroupItem>>> SaveGroupItems(string groupName, List<GroupItem> inputItems)
		//{
		//	using (var db = AuthControlDb.Connect())
		//	{
		//		// start fetching both
		//		var tActions = db.AC_GroupActions.AllAsync();
		//		var tApplications = db.AC_GroupApplications.AllAsync();
		//		// await
		//		var actions = await tActions.ConfigureAwait(false);
		//		var applications = await tApplications.ConfigureAwait(false);

		//		var item = new MS_AccountHold();
		//		holdNew.ToDb(item);
		//		item.IsActive = true;
		//		await tbl.InsertAsync(item, _gpEmployeeId);

		//		var result = new Result<MsHold>(value: MsHold.FromDb(item));
		//		return result;
		//	}
		//}
	}
}
