using AR = SOS.Data.SosCrm.QL_LeadSearchResultView;
using ARCollection = SOS.Data.SosCrm.QL_LeadSearchResultViewCollection;
using ARController = SOS.Data.SosCrm.QL_LeadSearchResultViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class QL_LeadSearchResultViewControllerExtensions
	{
		public static ARCollection Search(this ARController oCntlr
			, string szFirstName
			, string szLastName
			, string szPhone
			, int? nDealerId
			, string szEmail
			, int? nLeadId
			, int? nDispositionId
			, int? nSourceId
			, int? nPageSize
			, int? nPageNumber)
		{
			/** Init. */
			if (string.IsNullOrWhiteSpace(szFirstName)) szFirstName = null;
			if (string.IsNullOrWhiteSpace(szLastName)) szLastName = null;
			if (string.IsNullOrWhiteSpace(szPhone)) szPhone = null;
			if (string.IsNullOrWhiteSpace(szEmail)) szEmail = null;

			ARCollection oResult = oCntlr.LoadCollection(SosCrmDataStoredProcedureManager.QL_LeadSearchResultViewSearch(
				szFirstName
				, szLastName
				, szPhone
				, nDealerId
				, szEmail
				, nLeadId
				, nDispositionId
				, nSourceId
				, nPageSize
				, nPageNumber));

			/** Return result. */
			return oResult;
		}
	}
}
