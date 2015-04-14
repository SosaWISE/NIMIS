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
	public class EnumTypesService
	{
		//string _gpEmployeeId;
		//public TypesService(string gpEmployeeId)
		//{
		//	_gpEmployeeId = gpEmployeeId;
		//}

		public async Task<Result<List<EnumType>>> FriendsAndFamilyTypes()
		{
			using (var db = CrmDb.Connect())
			{
				var tbl = db.MC_FriendsAndFamilyTypes;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<EnumType>>(value: items.ConvertAll(item => EnumType.FromFriendsAndFamilyType(item)));
				return result;
			}
		}
	}
}
