using NXS.Data;
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
			using (var db = DBase.Connect())
			{
				var tbl = db.AC_Actions;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				return new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromDb(item)));
			}
		}
		public async Task<Result<List<MetadataType>>> ApplicationsAsync()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.AC_Applications;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				return new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromDb(item)));
			}
		}

		public async Task<Result<List<GroupActionItem>>> GroupActionItemsAsync()
		{
			using (var db = DBase.Connect())
			{
				// fetch both
				var actions = (await db.AC_GroupActions.AllAsync().ConfigureAwait(false)).ToList();
				var applications = (await db.AC_GroupApplications.AllAsync().ConfigureAwait(false)).ToList();
				// convert all
				var both = new ActsAndApps(actions, applications);
				return new Result<List<GroupActionItem>>(value: ToGroupActionItems(both));
			}
		}
		public async Task<Result<List<GroupActionItem>>> SaveGroupActionItems(string groupName, List<GroupActionItem> inputItems)
		{
			var result = new Result<List<GroupActionItem>>();
			foreach (var inputItem in inputItems)
				if (inputItem.GroupName != groupName)
					return result.Fail(-1, "Input item GroupName does not match group name");

			using (var db = DBase.Connect(360))
			{
				ActsAndApps both = null;
				await db.TransactionAsync(async () =>
				{
					// fetch both
					var actions = (await db.AC_GroupActions.ByGroupNameWithUpdateLockFullAsync(groupName).ConfigureAwait(false)).ToList();
					var applications = (await db.AC_GroupApplications.ByGroupNameWithUpdateLockFullAsync(groupName).ConfigureAwait(false)).ToList();

					// update all
					foreach (var inputItem in inputItems)
					{
						switch (inputItem.RefType)
						{
							case "Actions":
								if (!await SaveGroupActionAsync(db, result, inputItem, actions))
									return false;
								break;
							case "Applications":
								if (!await SaveGroupApplicationAsync(db, result, inputItem, applications))
									return false;
								break;
							default:
								result.Fail(-1, "Invalid RefType");
								return false;
						}
					}

					both = new ActsAndApps(actions, applications);
					return true;
				});

				// convert all
				if (both != null)
					result.Value = ToGroupActionItems(both);
				return result;
			}
		}
		private async Task<bool> SaveGroupActionAsync<T>(DBase db, Result<T> result, GroupActionItem inputItem, List<AC_GroupAction> items)
		{
			var tbl = db.AC_GroupActions;

			AC_GroupAction item;
			if (inputItem.ID <= 0)
			{
				// create new
				item = new AC_GroupAction();
				inputItem.ToDb(item);
				item.IsActive = true;
				await tbl.InsertAsync(item, _gpEmployeeId);
				// add to list
				items.Add(item);
			}
			else
			{
				// find item
				item = items.Where(a => a.ID == inputItem.ID).FirstOrDefault();
				if (item == null)
				{
					result.Fail(-1, "Invalid Group Action ID");
					return false;
				}
				// check ModifiedOn matches input
				if (VersionHelper.CheckModifiedOn(item.ModifiedOn, inputItem.ModifiedOn, result, getMsg: (msg) => "Group Action(" + inputItem.ID + "): " + msg).Failure)
					return false;

				// update item
				var snapShot = Snapshotter.Start(item);
				inputItem.ToDb(item);
				item.IsActive = true;
				await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
			}

			return true;
		}
		private async Task<bool> SaveGroupApplicationAsync<T>(DBase db, Result<T> result, GroupActionItem inputItem, List<AC_GroupApplication> items)
		{
			var tbl = db.AC_GroupApplications;

			AC_GroupApplication item;
			if (inputItem.ID <= 0)
			{
				// create new
				item = new AC_GroupApplication();
				inputItem.ToDb(item);
				item.IsActive = true;
				await tbl.InsertAsync(item, _gpEmployeeId);
				// add to list
				items.Add(item);
			}
			else
			{
				// find item
				item = items.Where(a => a.ID == inputItem.ID).FirstOrDefault();
				if (item == null)
				{
					result.Fail(-1, "Invalid Group Application ID");
					return false;
				}
				// check ModifiedOn matches input
				if (VersionHelper.CheckModifiedOn(item.ModifiedOn, inputItem.ModifiedOn, result, getMsg: (msg) => "Group Application(" + inputItem.ID + "): " + msg).Failure)
					return false;

				// update item
				var snapShot = Snapshotter.Start(item);
				inputItem.ToDb(item);
				item.IsActive = true;
				await tbl.UpdateAsync(snapShot, _gpEmployeeId).ConfigureAwait(false);
			}

			return true;
		}

		private List<GroupActionItem> ToGroupActionItems(ActsAndApps both)
		{
			// convert all
			var items = both.Item1.ConvertAll(item => GroupActionItem.FromDb(item));
			items.AddRange(both.Item2.ConvertAll(item => GroupActionItem.FromDb(item)));
			return items;
		}
		class ActsAndApps : Tuple<List<AC_GroupAction>, List<AC_GroupApplication>>
		{
			public ActsAndApps(List<AC_GroupAction> actions, List<AC_GroupApplication> apps) : base(actions, apps) { }
		}
	}
}
