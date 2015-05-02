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
	public class PackagesService
	{
		string _gpEmployeeId;
		public PackagesService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		//public async Task<Result<List<MsPackageType>>> Types()
		//{
		//	using (var db = CrmDb.Connect())
		//	{
		//		var tbl = db.MS_AccountPackageItemTypes;
		//		var items = await tbl.AllAsync().ConfigureAwait(false);
		//		var result = new Result<List<MsPackageType>>(value: items.ConvertAll(item => MsPackageType.FromDb(item)));
		//		return result;
		//	}
		//}

		public async Task<Result<List<MsPackage>>> Packages()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.MS_AccountPackages;
				var items = await tbl.AllFullAsync().ConfigureAwait(false);
				var result = new Result<List<MsPackage>>(value: items.ConvertAll(item => MsPackage.FromDb(item)));
				return result;
			}
		}
	}
}
