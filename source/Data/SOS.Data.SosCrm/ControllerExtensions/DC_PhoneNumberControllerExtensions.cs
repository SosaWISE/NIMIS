using AR = SOS.Data.SosCrm.DC_PhoneNumber;
using ARCollection = SOS.Data.SosCrm.DC_PhoneNumberCollection;
using ARController = SOS.Data.SosCrm.DC_PhoneNumberController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class DC_PhoneNumberControllerExtensions
	{
		public static AR Find(this ARController cntlr, string phoneNumber)
		{
			return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.DC_PhoneNumbersGetByPhoneNumber(phoneNumber));
		}
	}
}
