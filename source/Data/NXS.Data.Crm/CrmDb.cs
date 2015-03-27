using System;
using NXS.Data;

namespace NXS.Data.Crm
{
	public partial class CrmDb
	{
		public RU_TeamTable RU_Teams { get; set; }

		public partial class RU_TeamTable : Table<object, long>
		{
			public CrmDb Db { get { return (CrmDb)_database; } }
			public RU_TeamTable(CrmDb db) : base(db, "RuTeams", "[WISE_HumanResource].[dbo].[RU_Teams]", "TeamID", "int") { }
			public string TeamID { get { return _alias + "[TeamID]"; } }
			public string Description { get { return _alias + "[Description]"; } }
		}
	}
}
