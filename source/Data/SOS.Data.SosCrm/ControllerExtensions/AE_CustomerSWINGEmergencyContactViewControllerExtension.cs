using AR = SOS.Data.SosCrm.AE_CustomerSWINGEmergencyContactView;
using ARCollection = SOS.Data.SosCrm.AE_CustomerSWINGEmergencyContactViewCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerSWINGEmergencyContactViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_CustomerSWINGEmergencyContactViewControllerExtensions
	{
        public static ARCollection GetCustomerSWINGEmergencyContact(this ARController cntlr, long InterimAccountID)
		{
			return cntlr.LoadCollection(SosCrmDataStoredProcedureManager.AE_CustomerSWINGGetEmergencyContacts(InterimAccountID));
		}
	}
}