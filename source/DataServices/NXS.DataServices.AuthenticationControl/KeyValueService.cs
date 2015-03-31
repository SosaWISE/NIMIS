using NXS.Data.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using SOS.Lib.Core;
using SOS.Lib.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.AuthenticationControl
{
	public class KeyValueService
	{
		public void UpdateAll(List<AcKeyValue> keyValues)
		{
			using (var db = AuthControlDb.Connect())
			{
				// map to dictionary
				var dict = new Dictionary<int, AcKeyValue>(keyValues.Count);
				{
					var id = 0;
					foreach (var kv in keyValues)
					{
						// add to dict (key is the id and the index+1)
						dict.Add(++id, kv);
					}
				}

				var tbl = db.AC_KeyValues;
				db.Transaction(() =>
				{
					var items = tbl.All();

					foreach (var item in items)
					{
						var id = item.ID;
						if (dict.ContainsKey(id))
						{
							// update existing
							var kv = dict[id];
							// update data
							item.KeyValue = kv.KeyValue;
							item.CreatedOn = kv.CreatedOn.ToUniversalTime();
							// remove from dict
							dict.Remove(id);
						}
						else
						{
							// null out data
							item.KeyValue = null;
							item.CreatedOn = null;
						}
						// start save and add to waiting list
						tbl.Update(id, item);
					}

					// add new
					foreach (var kvp in dict)
					{
						var item = new AC_KeyValue();
						item.ID = kvp.Key;
						item.KeyValue = kvp.Value.KeyValue;
						item.CreatedOn = kvp.Value.CreatedOn.ToUniversalTime();
						// start insert and add to waiting list
						tbl.Insert(item, hasIdentity: false);
					}

					//
					return true;
				}, System.Data.IsolationLevel.Serializable);
			}
		}
		public List<AcKeyValue> ReadAll()
		{
			using (var db = AuthControlDb.Connect())
			{
				var results = new List<AcKeyValue>();

				var tbl = db.AC_KeyValues;
				db.Transaction(() =>
				{
					var items = tbl.All();

					foreach (var item in items)
					{
						if (item.KeyValue != null && item.CreatedOn.HasValue)
						{
							var kv = new AcKeyValue();
							kv.KeyValue = item.KeyValue;
							kv.CreatedOn = DateUtility.SpecifyUtcKind(item.CreatedOn.Value);
							results.Add(kv);
						}
					}

					//
					return true;
				}, System.Data.IsolationLevel.Serializable);

				return results;
			}
		}
	}
}
