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
	public class InventoryService
	{
		string _gpEmployeeId;
		public InventoryService(string gpEmployeeId)
		{
			_gpEmployeeId = gpEmployeeId;
		}

		public async Task<Result<List<MetadataType>>> LocationTypesAsync()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.IE_LocationTypes;
				var items = await tbl.AllAsync().ConfigureAwait(false);
				var result = new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromLocationType(item)));
				return result;
			}
		}

		public async Task<Result<List<MetadataType>>> LocationsByLocationTypeIdAsync(string locationTypeId)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.IE_LocationTypes;
				var items = await tbl.LocationsByLocationTypeIdAsync(locationTypeId: locationTypeId).ConfigureAwait(false);
				var result = new Result<List<MetadataType>>(value: items.ConvertAll(item => MetadataType.FromLocation(item)));
				return result;
			}
		}
	}
}
