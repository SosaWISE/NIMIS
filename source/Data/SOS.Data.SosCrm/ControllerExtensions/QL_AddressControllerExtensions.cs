using System;
using AR = SOS.Data.SosCrm.QL_Address;
using ARCollection = SOS.Data.SosCrm.QL_AddressCollection;
using ARController = SOS.Data.SosCrm.QL_AddressController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class QL_AddressControllerExtensions
	{
		public static AR BindData(this ARController cntlr, object fnsObject, AR address, Func<object, AR, AR> fxBindAction)
		{
			if (fxBindAction == null) return address;

			return fxBindAction(fnsObject, address);
		}

		public static AR LoadByPrimaryKeySoft(this ARController cntlr, long addressId)
		{
			var result = cntlr.LoadByPrimaryKey(addressId);

			if (result.IsDeleted) return null;

			return result;
		}
	}
}
