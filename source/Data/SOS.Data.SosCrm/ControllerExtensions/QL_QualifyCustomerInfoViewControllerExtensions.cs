using AR = SOS.Data.SosCrm.QL_QualifyCustomerInfoView;
using ARCollection = SOS.Data.SosCrm.QL_QualifyCustomerInfoViewCollection;
using ARController = SOS.Data.SosCrm.QL_QualifyCustomerInfoViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class QL_QualifyCustomerInfoViewControllerExtensions
	{
		public static AR LoadByLeadId(this ARController cntlr, long leadId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.QL_QualifyCustomerInfoViewByLeadID(leadId));
		}

		public static AR LoadByCustomerId(this ARController cntlr, long customerId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.QL_QualifyCustomerInfoViewByCustomerID(customerId));
		}

		public static AR LoadByAccountId(this ARController cntlr, long accountId)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.QL_QualifyCustomerInfoViewByAccountID(accountId));
		}
	}
}
