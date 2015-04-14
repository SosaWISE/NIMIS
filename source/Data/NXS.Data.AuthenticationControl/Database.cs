using System;
using NXS.Data;

namespace NXS.Data.AuthenticationControl
{
	public partial class AuthControlDb : Database<AuthControlDb>
	{
		public AC_ActionTable AC_Actions { get; set; }
		public AC_ApplicationTable AC_Applications { get; set; }
		public AC_KeyValueTable AC_KeyValues { get; set; }

		public partial class AC_ActionTable : Table<AC_Action, string>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_ActionTable(AuthControlDb db) : base(db, "AcA", "[dbo].[AC_Actions]", "ActionID", "varchar", false) { }
			public string ActionID { get { return _alias + "[ActionID]"; } }
			public string Name { get { return _alias + "[Name]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
		}
		public partial class AC_ApplicationTable : Table<AC_Application, string>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_ApplicationTable(AuthControlDb db) : base(db, "AcAp", "[dbo].[AC_Applications]", "ApplicationID", "varchar", false) { }
			public string ApplicationID { get { return _alias + "[ApplicationID]"; } }
			public string ApplicationName { get { return _alias + "[ApplicationName]"; } }
			public string ApplicationDesc { get { return _alias + "[ApplicationDesc]"; } }
			public string WebUrl { get { return _alias + "[WebUrl]"; } }
		}
		public partial class AC_KeyValueTable : Table<AC_KeyValue, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_KeyValueTable(AuthControlDb db) : base(db, "AcKV", "[dbo].[AC_KeyValues]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string KeyValue { get { return _alias + "[KeyValue]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
		}

	}
}
