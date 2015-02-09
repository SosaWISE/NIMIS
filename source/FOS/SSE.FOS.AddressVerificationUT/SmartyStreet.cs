using Microsoft.VisualStudio.TestTools.UnitTesting;
using SSE.FOS.AddressVerification.Interfaces;
using SSE.FOS.AddressVerification.Models;
using SSE.Lib.Interfaces.FOS;

namespace SSE.FOS.AddressVerificationUT
{
	[TestClass]
	public class SmartyStreet
	{

		#region Unit Tests

		[TestMethod]
		public void StrikeIron_ArgValidation()
		{
			// ** Initialize. 
			var oService = new AddressVerification.Main(AddressVerification.Main.Vendors.SMARTY_STRT);

			IFosQlAddress address = new FosQlAddress();
			IFosAVResult<IFosAddressVerified> result = oService.VerifyAddress(address, 801, 5000, 1, 1, "ASOS001", "andres");

			Assert.IsTrue(result.Code == (int)ResultCodes.ArgumentValidationFailed, "Argument validation should have failed.");

			address.AddressLine1 = "Somewhere in the USA";
			address.PostalCode = string.Empty;
			result = oService.VerifyAddress(address, 801, 5000, 1, 1, "ASOS001", "andres");

			Assert.IsTrue(result.Code == (int)ResultCodes.ArgumentValidationFailed, "Argument validation should have failed.");
		}

		[TestMethod]
		public void StrikeIron_ArgValidationPasses()
		{
			// ** Initialize. 
			var oService = new AddressVerification.Main(AddressVerification.Main.Vendors.SMARTY_STRT);

			IFosQlAddress address = new FosQlAddress();
			address.AddressLine1 = "1573 N Technology Way";
			address.PostalCode = "84097";
//			IFosAVResult<IFosAddressVerified> result = oService.VerifyAddress(address, 801, 5000, "andres");

//			Assert.IsTrue(result.Code == (int)ResultCodes.Success, "Argument validation should have passed.");

			address.AddressLine1 = "1184 N 840 E";
			address.PostalCode = "84097";
			IFosAVResult<IFosAddressVerified> result = oService.VerifyAddress(address, 801, 5000, 1, 1, "ASOS001", "andres");

			Assert.IsTrue(result.Code == (int)ResultCodes.Success, "Argument validation should have passed.");
		}

		#endregion Unit Tests
	}
}
