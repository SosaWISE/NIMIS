using AR = SOS.Data.SosCrm.MC_Address;
using ARCollection = SOS.Data.SosCrm.MC_AddressCollection;
using ARController = SOS.Data.SosCrm.MC_AddressController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	public static class MC_AddressControllerExtensions
	{
		public static AR CreateFromAddressID(this ARController oCntlr, long lAddressID)
		{
			/** Initialize. */
			AR oResult = oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.MC_AddressesCreateFromAddressID(lAddressID));

			/** Return result. */
			return oResult;
		}

		public static AR ByQlAddressId(this ARController cntlr, long qlAddressId)
		{
			return cntlr.LoadSingle(AR.Query().WHERE(AR.Columns.QlAddressId, qlAddressId));
		}
	}
}
