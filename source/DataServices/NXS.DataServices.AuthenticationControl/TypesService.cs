using NXS.Data;
using NXS.Data.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using SOS.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NXS.DataServices.AuthenticationControl
{
	public class TypesService
	{
		//string _gpEmployeeId;
		//public TypesService(string gpEmployeeId)
		//{
		//	_gpEmployeeId = gpEmployeeId;
		//}

		public async Task<Result<List<EnumType>>> RequestReasons()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.AC_RequestReasons;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<EnumType>>(value: items.ConvertAll(item => EnumType.FromDb(item)));
				return result;
			}
		}
	}
}
