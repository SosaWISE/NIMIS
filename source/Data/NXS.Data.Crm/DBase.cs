using System;
using NXS.Data;

namespace NXS.Data.Crm
{
	public partial class DBase
	{
		HumanResource.DBase _hrDb;
		// This should only be used for cross db joins
		public HumanResource.DBase HrDb
		{
			get
			{
				if (_hrDb == null)
					_hrDb = HumanResource.DBase.Init(null);
				return _hrDb;
			}
		}

		public class LocationTable : Table<Location, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public LocationTable(DBase db, string tableName) : base(db, "L", tableName, "ID", "string", false) { }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
		}
	}
}
