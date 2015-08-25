using System;
using System.Collections.Generic;
using NXS.Data.AuthenticationControl;
using NXS.DataServices.AuthenticationControl.Models;
using SOS.Lib.Util;

namespace NXS.DataServices.AuthenticationControl
{
	public class KeyValueService
	{
		public List<AcKeyValue> UpdateAll(DateTime purgeExpiration, DateTime validExpiration, string newKeyValue)
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.AC_KeyValues;
				db.Transaction(() =>
				{
					// load all and lock table so we're the exclusive editors/readers
					// http://stackoverflow.com/a/4597035
					var items = tbl.AllWithUpdateLock();

					bool hasValidKey = false;
					foreach (var item in items)
					{
						if (item.CreatedOn < purgeExpiration)
							tbl.Delete(item.ID); // delete expired
						else if (!(item.CreatedOn < validExpiration))
							hasValidKey = true;
					}

					// add new only if there are none remaining and a new one was passed in
					if (!hasValidKey && newKeyValue != null)
					{
						var item = new AC_KeyValue();
						item.KeyValue = newKeyValue;
						item.CreatedOn = DateTime.UtcNow;
						item.ID = tbl.Insert(item);
					}

					// commit transaction
					return true;
				}, System.Data.IsolationLevel.Serializable);

				return ConvertKeyValues(tbl.All());
			}
		}

		public List<AcKeyValue> ReadAll()
		{
			using (var db = DBase.Connect())
			{
				var tbl = db.AC_KeyValues;
				return ConvertKeyValues(tbl.All());
			}
		}

		private static List<AcKeyValue> ConvertKeyValues(IEnumerable<AC_KeyValue> items)
		{
			var results = new List<AcKeyValue>();
			foreach (var item in items)
			{
				var kv = new AcKeyValue();
				kv.KeyValue = item.KeyValue;
				kv.CreatedOn = DateUtility.SpecifyUtcKind(item.CreatedOn);
				results.Add(kv);
			}
			return results;
		}
	}
}
