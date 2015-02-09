using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SOS.FunctionalServices.Contracts.Helper;
using SSE.Services.CmsCORS.Controllers.Qualify;
using SSE.Services.CmsCORS.Models;
using SSE.Services.CmsCORS.Tests.Helpers;

namespace SSE.Services.CmsCORS.Tests.Controllers
{
	[TestClass]
	public class QualifyControllerTest
	{

		#region Test Initialization.

		[AssemblyInitialize]
		public static void AssemblyInit(TestContext context)
		{
			System.Diagnostics.Debug.WriteLine("QualifyControllerTest Assembly Init");

			MainConfig.Instance.Initialize();
		}

		#endregion Test Initialization.

		#region Unit Tests
		[TestMethod]
		public void RunCreditTest()
		{
			// ** Arrange
			var creditReportCntlr = new VerificationSrvController();
			var param = new LeadParam();
			param.LeadID = 1000003;
			param.CustomerTypeId = "1";
			param.CustomerMasterFileId = 100154;
			param.DealerId = 1;
			param.LocalizationId = "1";
			param.TeamLocationId = 1;
			param.SeasonId = 1;
			param.SalesRepId = "Andres";
			param.LeadSourceId = 1;
			param.LeadDispositionId = 1;
			param.LeadDispositionDateChange = DateTime.Now;
			param.Salutation = "Mr";
			param.FirstName = "Andres";
			param.MiddleName = "Tanner";
			param.LastName = "Sosa";
			param.Suffix = "Andres";
			param.Gender = "Male";
			param.SSN = "123-45-6789";
			param.DOB = Convert.ToDateTime("01/01/1980");
			param.DL = "Andres"; 
			param.DLStateId = "Andres";
			param.Email = "asosa@securitysciences.com";
			param.PhoneHome = "801-221-1234";
			param.PhoneWork = "801-224-1234";
			param.PhoneMobile = "801-830-1234";

			var leadResult = creditReportCntlr.SaveLead(param);
			var result = creditReportCntlr.RunCredit(((SOS.FunctionalServices.Contracts.Models.QualifyLead.IFnsQlLead)leadResult.Value).LeadID);

			// ** Check my result
			Assert.IsTrue(result.Code == (int)ErrorCodes.Success, "There was an error running credit");


		}

		[TestMethod]
		public void ValidateAddressTest()
		{
			// ** Arrange
			var creditReportCntlr = new VerificationSrvController();
			var addressparam = new AddressParam();
			addressparam.AddressId = 1000003;
			addressparam.DealerId = 1;
			addressparam.StreetAddress = "123 East";
			addressparam.StreetAddress2 = "4567 North";
			addressparam.City = "Orem";
			addressparam.StateId = "Utah";
			addressparam.PostalCode = "801";
			addressparam.PhoneNumber = "801-221-1234";


			CmsCORSResult<VerifyAddress>result= creditReportCntlr.VerifyAddress(addressparam);

			// ** Check my result
			Assert.IsTrue(result.Code == (int)ErrorCodes.Success, "There was an error validating address");


		}
		#endregion Unit Tests
	}
}
