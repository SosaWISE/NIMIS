


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace SSE.Data.SurveyEngine {
	public partial class SurveyEngineDataStoredProcedureManager {
		public static StoredProcedure SV_ResultsSaveMapToTokenAnswers(string Username,long? AccountId,string PrimaryCustomer_Email,string SystemDetails_Password,short? ContractTerms_BillingDate,string PrimaryCustomer_FullName_Prefix,string PrimaryCustomer_FullName_FirstName,string PrimaryCustomer_FullName_MiddleName,string PrimaryCustomer_FullName_LastName,string PrimaryCustomer_FullName_Postfix) {
			StoredProcedure sp = new StoredProcedure("custSV_ResultsSaveMapToTokenAnswers" ,DataService.GetInstance("SseSurveyEngineProvider"));
			sp.Command.AddParameter("@Username", Username, DbType.String);
			sp.Command.AddParameter("@AccountId", AccountId, DbType.Int64);
			sp.Command.AddParameter("@PrimaryCustomer_Email", PrimaryCustomer_Email, DbType.AnsiString);
			sp.Command.AddParameter("@SystemDetails_Password", SystemDetails_Password, DbType.String);
			sp.Command.AddParameter("@ContractTerms_BillingDate", ContractTerms_BillingDate, DbType.Int16);
			sp.Command.AddParameter("@PrimaryCustomer_FullName_Prefix", PrimaryCustomer_FullName_Prefix, DbType.String);
			sp.Command.AddParameter("@PrimaryCustomer_FullName_FirstName", PrimaryCustomer_FullName_FirstName, DbType.String);
			sp.Command.AddParameter("@PrimaryCustomer_FullName_MiddleName", PrimaryCustomer_FullName_MiddleName, DbType.String);
			sp.Command.AddParameter("@PrimaryCustomer_FullName_LastName", PrimaryCustomer_FullName_LastName, DbType.String);
			sp.Command.AddParameter("@PrimaryCustomer_FullName_Postfix", PrimaryCustomer_FullName_Postfix, DbType.String);
			return sp;
		}
	}
}
 
