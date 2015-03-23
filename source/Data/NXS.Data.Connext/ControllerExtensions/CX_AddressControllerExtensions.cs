using AR = NXS.Data.Connext.CX_Address;
using ARCollection = NXS.Data.Connext.CX_AddressCollection;
using ARController = NXS.Data.Connext.CX_AddressController;

namespace NXS.Data.Connext.ControllerExtensions
{
	public static class CX_AddressControllerExtensions
	{
		public static ARCollection LoadBySalesRepId(this ARController cntlr, string gpEmployeeID)
		{

			return cntlr.LoadCollection(NxseConnextDataStoredProcedureManager.CX_AddressBySalesRepID(gpEmployeeID));
		}
	}
}
