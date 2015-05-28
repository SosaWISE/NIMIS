using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.AE_Customer;
using ARCollection = SOS.Data.SosCrm.AE_CustomerCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class AE_CustomerControllerExtensions
	{
		//public static AR CreateFromLeadID(this ARController oCntlr, long lLeadID, string sCustomerTypeID, string customerAddressTypeId)
		//{
		//	/** Initialize. */
		//	AR oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerCreateFromLead(lLeadID, sCustomerTypeID, customerAddressTypeId, true, null));
		//
		//	/** Return result. */
		//	return oResult;
		//}

		public static AR CreateFromCustomerID(this ARController oCntlr, long lCustomerID, string sCustomerTypeId, long lBillingAddressId)
		{
			/** Init. */
			AR oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerCreateByCustomerID(lCustomerID, sCustomerTypeId, lBillingAddressId));

			/** Return result. */
			return oResult;
		}

		public static AR LoadByPrimaryKeyAndCMFId(this ARController oCntlr, long lCustomerId, long lCMFId, int nDealerId)
		{
			/** Initialize. */
			var oQuery = AR.Query()
				.WHERE(AR.Columns.CustomerID, lCustomerId)
				.WHERE(AR.Columns.CustomerMasterFileId, lCMFId)
				.WHERE(AR.Columns.DealerId, nDealerId);

			/** Execute. */
			var oResult = oCntlr.LoadSingle(oQuery);

			/** REturn result. */
			return oResult;
		}

		public static AR GetByAccountID(this ARController cntlr, long accountID, string customerTypeId)
		{
			/** Init. */
			AR oResult = cntlr.LoadSingle(SosCrmDataStoredProcedureManager.AE_CustomerGetByAccountID(accountID, StringUtility.NullIfWhiteSpace(customerTypeId)));

			/** Return result. */
			return oResult;
		}


		public static ARCollection ByCmfid(this ARController cntlr, long cmfid)
		{
			return cntlr.LoadCollection(AR.Query().WHERE(AR.Columns.CustomerMasterFileId, cmfid));
		}
		public static bool ExistsForCmfid(this ARController cntlr, long cmfid)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.CustomerMasterFileId, cmfid);

			var result = cntlr.LoadSingle(qry);

			return result.IsLoaded;
			//var qry = new SubSonic.Select(AR.Columns.CustomerID).From(AR.Schema)
			//	.Where(AR.Columns.CustomerMasterFileId).IsEqualTo(cmfid);
			//var v = qry.SQLCommand;
			//return qry.GetRecordCount() > 0;
		}

		public static AR ByLeadId(this ARController cntlr, long leadId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.LeadId, leadId)
				.AND(AR.Columns.IsActive, true)
				.AND(AR.Columns.IsDeleted, false);
			return cntlr.LoadSingle(qry);
		}
	}
}
