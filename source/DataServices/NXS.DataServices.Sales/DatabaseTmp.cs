using NXS.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.Data.Sales
{
	public partial class DBase : Database<DBase>
	{
		public new static DBase Connect(int commandTimeout = 3)
		{
			var cn = new MySql.Data.MySqlClient.MySqlConnection(ConnectionString);
			cn.Open();
			var db = Init(cn, commandTimeout);
			return db;
		}

		public partial class Table<T, TID> : Database<DBase>.Table<T, TID>
		{
			public Table(Database<DBase> database, string alias, string name, string pkName = "ID", string pkDbType = "SIGNED", bool hasIdentity = true)
				: base(database, alias, name, pkName, pkDbType, hasIdentity)
			{
			}

			public override async Task<TID> InsertAsync(dynamic data)
			{
				var sql = InsertSql((object)data);
				if (_hasIdentity)
				{
					var id = (await _database.QueryAsync(sql, (object)data).ConfigureAwait(false)).FirstOrDefault() as IDictionary<string, object>;
					return (TID)Convert.ChangeType(id.Values.Single(), typeof(TID));
				}
				else
				{
					await _database.ExecuteAsync(sql, (object)data).ConfigureAwait(false);
					return default(TID);
				}
			}

			protected override string InsertSql(object data)
			{
				List<string> paramNames = GetParamNames(data)
					.Where(name => !_hasIdentity || name != _pkName).ToList();

				string cols = string.Join(",", paramNames);
				string cols_params = string.Join(",", paramNames.Select(p => "@" + p));
				var sql = "INSERT " + TableName + " (" + cols + ") VALUES (" + cols_params + ")";
				if (_hasIdentity)
					//sql += ";SELECT LAST_INSERT_ID();";
					sql += ";SELECT CAST(LAST_INSERT_ID() AS " + _pkDbType + ")";
				return sql;
			}
		}




		public static readonly string Database = "jackrabbit";
		//public readonly Sprocs Sprocs;
		//public DBase()
		//{
		//	Sprocs = new Sprocs(this);
		//}

		public SalesAreaAssignmentTable SalesAreaAssignments { get; set; }
		public SalesAreaTable SalesAreas { get; set; }
		public SalesContactAddressTable SalesContactAddresses { get; set; }
		public SalesContactCategoryTable SalesContactCategories { get; set; }
		public SalesContactCategoriesBlacklistTable SalesContactCategoriesBlacklists { get; set; }
		public SalesContactFollowupTable SalesContactFollowups { get; set; }
		public SalesContactNoteTable SalesContactNotes { get; set; }
		public SalesContactTable SalesContacts { get; set; }
		public SalesOfficeTable SalesOffices { get; set; }
		public SalesRepTable SalesReps { get; set; }
		public SalesTrackingTable SalesTrackings { get; set; }
		public SystemTypeTable SystemTypes { get; set; }
		public UserPermissionTable UserPermissions { get; set; }
		public UserTable Users { get; set; }

		public partial class SalesAreaAssignmentTable : Table<SalesAreaAssignment, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesAreaAssignmentTable(DBase db) : base(db, "SAAA", "salesAreaAssignments", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string salesAreaId { get { return _alias + "salesAreaId"; } }
			public string officeId { get { return _alias + "officeId"; } }
			public string salesRepId { get { return _alias + "salesRepId"; } }
			public string startTimestamp { get { return _alias + "startTimestamp"; } }
			public string endTimestamp { get { return _alias + "endTimestamp"; } }
			public string status { get { return _alias + "status"; } }
		}
		public partial class SalesAreaTable : Table<SalesArea, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesAreaTable(DBase db) : base(db, "SA", "salesAreas", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string areaName { get { return _alias + "areaName"; } }
			public string minLatitude { get { return _alias + "minLatitude"; } }
			public string maxLatitude { get { return _alias + "maxLatitude"; } }
			public string minLongitude { get { return _alias + "minLongitude"; } }
			public string maxLongitude { get { return _alias + "maxLongitude"; } }
			public string pointData { get { return _alias + "pointData"; } }
			public string status { get { return _alias + "status"; } }
		}
		public partial class SalesContactAddressTable : Table<SalesContactAddress, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesContactAddressTable(DBase db) : base(db, "SCA", "salesContactAddresses", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string salesContactId { get { return _alias + "salesContactId"; } }
			public string address { get { return _alias + "address"; } }
			public string address2 { get { return _alias + "address2"; } }
			public string city { get { return _alias + "city"; } }
			public string state { get { return _alias + "state"; } }
			public string zip { get { return _alias + "zip"; } }
		}
		public partial class SalesContactCategoryTable : Table<SalesContactCategory, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesContactCategoryTable(DBase db) : base(db, "SCC", "salesContactCategories", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string userId { get { return _alias + "userId"; } }
			public string name { get { return _alias + "name"; } }
			public string sequence { get { return _alias + "sequence"; } }
			public string filename { get { return _alias + "filename"; } }
			public string status { get { return _alias + "status"; } }
		}
		public partial class SalesContactCategoriesBlacklistTable : Table<SalesContactCategoriesBlacklist, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesContactCategoriesBlacklistTable(DBase db) : base(db, "SCCB", "salesContactCategoriesBlacklist", "id", "SIGNED", true) { }

			public string categoryId { get { return _alias + "categoryId"; } }
			public string userId { get { return _alias + "userId"; } }
		}
		public partial class SalesContactFollowupTable : Table<SalesContactFollowup, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesContactFollowupTable(DBase db) : base(db, "SCF", "salesContactFollowups", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string contactId { get { return _alias + "contactId"; } }
			public string salesRepId { get { return _alias + "salesRepId"; } }
			public string followupTimestamp { get { return _alias + "followupTimestamp"; } }
			public string status { get { return _alias + "status"; } }
		}
		public partial class SalesContactNoteTable : Table<SalesContactNote, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesContactNoteTable(DBase db) : base(db, "SCN", "salesContactNotes", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string contactId { get { return _alias + "contactId"; } }
			public string salesRepId { get { return _alias + "salesRepId"; } }
			public string noteTimestamp { get { return _alias + "noteTimestamp"; } }
			public string salesRepLatitude { get { return _alias + "salesRepLatitude"; } }
			public string salesRepLongitude { get { return _alias + "salesRepLongitude"; } }
			public string firstName { get { return _alias + "firstName"; } }
			public string lastName { get { return _alias + "lastName"; } }
			public string categoryId { get { return _alias + "categoryId"; } }
			public string systemId { get { return _alias + "systemId"; } }
			public string note { get { return _alias + "note"; } }
		}
		public partial class SalesContactTable : Table<SalesContact, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesContactTable(DBase db) : base(db, "SC", "salesContacts", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string contactTimestamp { get { return _alias + "contactTimestamp"; } }
			public string latitude { get { return _alias + "latitude"; } }
			public string longitude { get { return _alias + "longitude"; } }
		}
		public partial class SalesOfficeTable : Table<SalesOffice, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesOfficeTable(DBase db) : base(db, "SO", "salesOffices", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string officeCity { get { return _alias + "officeCity"; } }
			public string officeState { get { return _alias + "officeState"; } }
			public string address { get { return _alias + "address"; } }
			public string status { get { return _alias + "status"; } }
		}
		public partial class SalesRepTable : Table<SalesRep, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesRepTable(DBase db) : base(db, "SR", "salesReps", "id", "SIGNED", true) { }

			public string userId { get { return _alias + "userId"; } }
			public string officeId { get { return _alias + "officeId"; } }
			public string status { get { return _alias + "status"; } }
		}
		public partial class SalesTrackingTable : Table<SalesTracking, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SalesTrackingTable(DBase db) : base(db, "ST", "salesTrackings", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string salesRepId { get { return _alias + "salesRepId"; } }
			public string trackingTimestamp { get { return _alias + "trackingTimestamp"; } }
			public string latitude { get { return _alias + "latitude"; } }
			public string longitude { get { return _alias + "longitude"; } }
		}
		public partial class SystemTypeTable : Table<SystemType, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SystemTypeTable(DBase db) : base(db, "ST", "systemTypes", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string officeId { get { return _alias + "officeId"; } }
			public string companyName { get { return _alias + "companyName"; } }
			public string sequence { get { return _alias + "sequence"; } }
			public string filename { get { return _alias + "filename"; } }
		}
		public partial class UserPermissionTable : Table<UserPermission, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public UserPermissionTable(DBase db) : base(db, "UP", "userPermissions", "id", "SIGNED", true) { }

			public string userId { get { return _alias + "userId"; } }
			public string permissionId { get { return _alias + "permissionId"; } }
		}
		public partial class UserTable : Table<User, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public UserTable(DBase db) : base(db, "U", "users", "id", "SIGNED", true) { }

			public string id { get { return _alias + "id"; } }
			public string firstName { get { return _alias + "firstName"; } }
			public string lastName { get { return _alias + "lastName"; } }
			public string email { get { return _alias + "email"; } }
			public string password { get { return _alias + "password"; } }
			public string PIN { get { return _alias + "PIN"; } }
			public string dateJoined { get { return _alias + "dateJoined"; } }
			public string GPID { get { return _alias + "GPID"; } }
			public string status { get { return _alias + "status"; } }
		}
	}
}
