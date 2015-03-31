using System;
using NXS.Data;

namespace NXS.Data.AuthenticationControl
{
	public partial class AuthControlDb : Database<AuthControlDb>
	{
		public AC_KeyValueTable AC_KeyValues { get; set; }

		public partial class AC_KeyValueTable : Table<AC_KeyValue, int>
		{
			public AuthControlDb Db { get { return (AuthControlDb)_database; } }
			public AC_KeyValueTable(AuthControlDb db) : base(db, "AcKV", "[dbo].[AC_KeyValues]", "ID", "int") { }
			public string ID { get { return _alias + "[ID]"; } }
			public string KeyValue { get { return _alias + "[KeyValue]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
		}

	}
}
