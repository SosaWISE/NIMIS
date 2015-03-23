


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

// ReSharper disable once CheckNamespace
namespace NXS.Data.Connext {
	public partial class NxseConnextDataStoredProcedureManager {
		public static StoredProcedure CX_AddressBySalesRepID(string SalesRepId) {
			StoredProcedure sp = new StoredProcedure("custCX_AddressBySalesRepID" ,DataService.GetInstance("NxseConnextProvider"));
			sp.Command.AddParameter("@SalesRepId", SalesRepId, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure CX_ContactBySalesRepID(string SalesRepId) {
			StoredProcedure sp = new StoredProcedure("custCX_ContactBySalesRepID" ,DataService.GetInstance("NxseConnextProvider"));
			sp.Command.AddParameter("@SalesRepId", SalesRepId, DbType.AnsiString);
			return sp;
		}
	}
}
 