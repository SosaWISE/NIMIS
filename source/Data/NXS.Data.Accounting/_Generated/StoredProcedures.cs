


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace NXS.Data.Accounting {
	public partial class AccountingDataStoredProcedureManager {
		public static StoredProcedure Cust_GetEICFilingStatus() {
			var sp = new StoredProcedure("Cust_GetEICFilingStatus" ,DataService.GetInstance("NxsAccountingProvider"));
			return sp;
		}
		public static StoredProcedure Cust_GetFedFilingStatus() {
			var sp = new StoredProcedure("Cust_GetFedFilingStatus" ,DataService.GetInstance("NxsAccountingProvider"));
			return sp;
		}
		public static StoredProcedure Cust_GetStateFilingStatus(string StateAB) {
			var sp = new StoredProcedure("Cust_GetStateFilingStatus" ,DataService.GetInstance("NxsAccountingProvider"));
			sp.Command.AddParameter("@StateAB", StateAB, DbType.String);
			return sp;
		}
		public static StoredProcedure Cust_GetWorkersComp() {
			var sp = new StoredProcedure("Cust_GetWorkersComp" ,DataService.GetInstance("NxsAccountingProvider"));
			return sp;
		}
		public static StoredProcedure CustGetTaxCodes() {
			var sp = new StoredProcedure("CustGetTaxCodes" ,DataService.GetInstance("NxsAccountingProvider"));
			return sp;
		}
	}
}
 
