using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.AE_CustomerAddress;
using ARCollection = SOS.Data.SosCrm.AE_CustomerAddressCollection;
using ARController = SOS.Data.SosCrm.AE_CustomerAddressController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class AE_CustomerAddressControllerExtensions
	{
		public static AR ByCustomerID(this ARController cntlr, long customerId, string customerAddressTypeId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.CustomerId, customerId)
				.AND(AR.Columns.CustomerAddressTypeId, customerAddressTypeId);
			return cntlr.LoadSingle(qry);
		}
	}
}
