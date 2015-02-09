using AR = SOS.Data.SosCrm.AE_CustomerMasterFileGeneralView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerMasterFileGeneralViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerMasterFileGeneralViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class AE_CustomerMasterFileGeneralViewControllerExtensions
	{
		public static AE_CustomerMasterFileGeneralViewCollection Search(this AE_CustomerMasterFileGeneralViewController cntlr, long? dealerId, string city, string stateId, string postalCode, string email, string firstName, string lastName, string phoneNumber, int? pageSize, int? pageNumber)
		{
			return
				cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_CustomerMasterFileGeneralSearch(dealerId, city, stateId,
					postalCode, email, firstName, lastName, phoneNumber, pageSize, pageNumber));
		}
	}
}
