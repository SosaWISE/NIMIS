using SubSonic;
using AR = SSE.Data.SurveyEngine.SV_Result;
using ARCollection = SSE.Data.SurveyEngine.SV_ResultCollection;
using ARController = SSE.Data.SurveyEngine.SV_ResultController;

namespace SSE.Data.SurveyEngine.ControllerExtensions
{
	public static class SV_ResultControllerExtensions
	{
		public static int SaveMapToTokenAnswers(this ARController ctlr, string username, long accountId,
			string PrimaryCustomer_Email,
			string SystemDetails_Password,
			short? ContractTerms_BillingDate,
			//BEGIN PrimaryCustomer.FullName
			string PrimaryCustomer_FullName_Prefix,
			string PrimaryCustomer_FullName_FirstName,
			string PrimaryCustomer_FullName_MiddleName,
			string PrimaryCustomer_FullName_LastName,
			string PrimaryCustomer_FullName_Postfix
			//END PrimaryCustomer.FullName
			)
		{
			return SurveyEngineDataStoredProcedureManager.SV_ResultsSaveMapToTokenAnswers(username, accountId,
				PrimaryCustomer_Email: PrimaryCustomer_Email,
				SystemDetails_Password: SystemDetails_Password,
				ContractTerms_BillingDate: ContractTerms_BillingDate,
				PrimaryCustomer_FullName_Prefix: PrimaryCustomer_FullName_Prefix,
				PrimaryCustomer_FullName_FirstName: PrimaryCustomer_FullName_FirstName,
				PrimaryCustomer_FullName_MiddleName: PrimaryCustomer_FullName_MiddleName,
				PrimaryCustomer_FullName_LastName: PrimaryCustomer_FullName_LastName,
				PrimaryCustomer_FullName_Postfix: PrimaryCustomer_FullName_Postfix).Execute();
		}
	}
}
