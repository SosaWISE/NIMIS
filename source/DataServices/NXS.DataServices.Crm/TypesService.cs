using NXS.Data;
using NXS.Data.Crm;
using NXS.DataServices.Crm.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm
{
	public class TypesService
	{
		//string _gpEmployeeId;
		//public TypesService(string gpEmployeeId)
		//{
		//	_gpEmployeeId = gpEmployeeId;
		//}

		public async Task<Result<List<MetadataType>>> FriendsAndFamilyTypesAsync()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.MC_FriendsAndFamilyTypes;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromDb(item)));
				return result;
			}
		}
		public async Task<Result<List<MetadataType>>> AccountCancelReasonsAsync()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.MC_AccountCancelReasons;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromDb(item)));
				return result;
			}
		}
	}
}
