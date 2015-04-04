using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Licensing;
using SOS.FunctionalServices.Models;
using SOS.Lib.Core.ErrorHandling;
using SOS.Services.Interfaces.Models;
using SOS.Services.Interfaces.Models.Licensing;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using AuthActions = SOS.Data.AuthenticationControl.AC_Action.MetaData;
using AuthApplications = SOS.Data.AuthenticationControl.AC_Application.MetaData;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.Qualify
{
	[RoutePrefix("QualifySrv")]
	public class AddressValidationController : ApiController
	{
		// GET QualifySrv/addressvalidation/5
		[Route("AddressValidation/{id}")]
		[HttpGet]
		public Result<IFnsVerifyAddress> Get(int id)
		{
			return CORSSecurity.Authorize("Get Address", AuthApplications.SSECmsCORSID, null, user =>
			{
				var result = new Result<IFnsVerifyAddress>();
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				var fnsResult = oService.AddressRead(id);
				return result.FromFnsResult(fnsResult);
			});
		}

		[HttpPost, Route("Address")]
		public Result<NxsVerifyAddressAndLicensing> SaveAddress([FromBody]FnsVerifyAddress address)
		{
			return CORSSecurity.Authorize("SaveAddress", AuthApplications.SSECmsCORSID, null, user =>
			{
				var result = new Result<NxsVerifyAddressAndLicensing>();

				var argArray = new List<CORSArg>
				{
					new CORSArg(null, (address.SeasonId == 0), "'SeasonId' is required."),
					new CORSArg(null, (address.TeamLocationId == 0), "'TeamLocationId' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.SalesRepId), "'SalesRepId' is required."),
					new CORSArg(null, (address.DealerId == 0), "'DealerId' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.Phone), "'Phone' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.PostalCode), "'PostalCode' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.StreetAddress), "'StreetAddress' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.City), "'City' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.County), "'County' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.StateId), "'StateId' is required."),
					new CORSArg(null, (address.TimeZoneId == 0), "'TimeZoneId' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.StreetNumber), "'StreetNumber' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.StreetName), "'StreetName' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.ValidationVendorId), "'ValidationVendorId' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.AddressValidationStateId), "'AddressValidationStateId' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.AddressTypeId), "'AddressTypeId' is required."),
					new CORSArg(null, string.IsNullOrEmpty(address.CountryId), "'CountryId' is required."),
				};
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				var fnsResult = service.SaveAddress(address, user.Username);

				// ** Verify that the Rep Can Sale here
				var fnsResultValue = (IFnsVerifyAddress)fnsResult.GetValue();
				var lService = SosServiceEngine.Instance.FunctionalServices.Instance<ILicencingManagementService>();
				var fnsLResult = lService.SalesRepComplianceGet(fnsResultValue.SalesRepId, fnsResultValue.CountryId,
					fnsResultValue.StateId,
					fnsResultValue.County, fnsResultValue.City, null, user.GPEmployeeID);
				// // **  Check result
				if (fnsLResult.Code != BaseErrorCodes.ErrorCodes.Success.Code())
				{
					result.Code = fnsLResult.Code;
					result.Message = fnsLResult.Message;
					return result;
				}

				// // ** Loop through results
				var fnsLicenseList = (List<IFnsLmSalesRepRequirementsView>)fnsLResult.GetValue();
				var licenseList = new List<LmSalesRepRequirement>();
				foreach (var licItem in fnsLicenseList)
				{
					if (!licItem.Status.Equals("Licensing Complete"))
					{
						licenseList.Add(ConvertTo.CastFnsToLmSalesRepRequirement(licItem));
					}
				}
				// // ** Build result value
				var resultValue = new NxsVerifyAddressAndLicensing
				{
					VerifiedAddress = ConvertTo.CastFnsToVerifyAddress(fnsResultValue),
					SalesLicReq = licenseList
				};

				result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
				result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
				result.Value = resultValue;

				return result;
			});
		}

		// POST QualifySrv/addressvalidation
		[Route("PremiseAddressValidation")]
		[HttpPost]
		[HttpOptions]
		public Result<NxsVerifyAddressAndLicensing> PremisePost([FromBody]FnsVerifyAddress addressToVerify)
		{
			return CORSSecurity.Authorize("PremiseAddressValidation", AuthApplications.SSECmsCORSID, null, user =>
			{
				var result = new Result<NxsVerifyAddressAndLicensing>();

				var argArray = new List<CORSArg>
				{
					new CORSArg(addressToVerify.SeasonId, (addressToVerify.SeasonId == 0), "'SeasonId' must be passed."),
					new CORSArg(addressToVerify.TeamLocationId, (addressToVerify.TeamLocationId == 0), "'TeamLocationId' must be passed."),
					new CORSArg(addressToVerify.SalesRepId, (string.IsNullOrEmpty(addressToVerify.SalesRepId)), "'SalesRepId' must be passed."),
					new CORSArg(addressToVerify.DealerId, (addressToVerify.DealerId == 0), "'DealerId' can not be blank."),
					new CORSArg(addressToVerify.StreetAddress, (string.IsNullOrEmpty(addressToVerify.StreetAddress)), "'Address' can not be blank."),
					//new CORSArg(jsonParam.City, (string.IsNullOrEmpty(jsonParam.City)), "'City' can not be blank."),
					//new CORSArg(jsonParam.StateId, (string.IsNullOrEmpty(jsonParam.StateId)), "'StateId' can not be blank."),
					new CORSArg(addressToVerify.PostalCode, (string.IsNullOrEmpty(addressToVerify.PostalCode)), "'PostalCode' can not be blank."),
					new CORSArg(addressToVerify.Phone, (string.IsNullOrEmpty(addressToVerify.Phone)), "'PhoneNumber' was not set."),
				};
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				var fnsResult = service.AddressVerify(addressToVerify, addressToVerify.SeasonId, addressToVerify.TeamLocationId, addressToVerify.SalesRepId, user.Username);

				// Check to see if the rep is legit.
				var fnsResultValue = (IFnsVerifyAddress)fnsResult.GetValue();
				if (fnsResultValue != null)
				{
					var lService = SosServiceEngine.Instance.FunctionalServices.Instance<ILicencingManagementService>();
					var fnsLResult = lService.SalesRepComplianceGet(fnsResultValue.SalesRepId, fnsResultValue.CountryId,
						fnsResultValue.StateId,
						fnsResultValue.County, fnsResultValue.City, null, user.GPEmployeeID);
					// // **  Check result
					if (fnsLResult.Code != BaseErrorCodes.ErrorCodes.Success.Code())
					{
						result.Code = fnsLResult.Code;
						result.Message = fnsLResult.Message;
						return result;
					}

					// // ** Loop through results
					var fnsLicenseList = (List<IFnsLmSalesRepRequirementsView>)fnsLResult.GetValue();
					var licenseList = new List<LmSalesRepRequirement>();
					foreach (var licItem in fnsLicenseList)
					{
						if (!licItem.Status.Equals("Licensing Complete"))
						{
							licenseList.Add(ConvertTo.CastFnsToLmSalesRepRequirement(licItem));
						}
					}
					// // ** Build result value
					var resultValue = new NxsVerifyAddressAndLicensing
					{
						VerifiedAddress = ConvertTo.CastFnsToVerifyAddress(fnsResultValue),
						SalesLicReq = licenseList
					};

					result.Code = BaseErrorCodes.ErrorCodes.Success.Code();
					result.Message = BaseErrorCodes.ErrorCodes.Success.Message();
					result.Value = resultValue;

				}
				else
				{
					result.Code = fnsResult.Code;
					result.Message = fnsResult.Message;
					result.Value = new NxsVerifyAddressAndLicensing
					{
						VerifiedAddress = null,
						SalesLicReq = new List<LmSalesRepRequirement>()
					};
				}

				return result;
			});
		}

		// POST QualifySrv/addressvalidation
		[Route("AddressValidation")]
		[HttpPost]
		[HttpOptions]
		public Result<IFnsVerifyAddress> Post([FromBody]FnsVerifyAddress addressToVerify)
		{
			return CORSSecurity.Authorize("AddressValidation", AuthApplications.SSECmsCORSID, null, user =>
			{
				var result = new Result<IFnsVerifyAddress>();

				var argArray = new List<CORSArg>
				{
					new CORSArg(addressToVerify.SeasonId, (addressToVerify.SeasonId == 0), "'SeasonId' must be passed."),
					new CORSArg(addressToVerify.TeamLocationId, (addressToVerify.TeamLocationId == 0), "'TeamLocationId' must be passed."),
					new CORSArg(addressToVerify.SalesRepId, (string.IsNullOrEmpty(addressToVerify.SalesRepId)), "'SalesRepId' must be passed."),
					new CORSArg(addressToVerify.DealerId, (addressToVerify.DealerId == 0), "'DealerId' can not be blank."),
					new CORSArg(addressToVerify.StreetAddress, (string.IsNullOrEmpty(addressToVerify.StreetAddress)), "'Address' can not be blank."),
					//new CORSArg(jsonParam.City, (string.IsNullOrEmpty(jsonParam.City)), "'City' can not be blank."),
					//new CORSArg(jsonParam.StateId, (string.IsNullOrEmpty(jsonParam.StateId)), "'StateId' can not be blank."),
					new CORSArg(addressToVerify.PostalCode, (string.IsNullOrEmpty(addressToVerify.PostalCode)), "'PostalCode' can not be blank."),
					new CORSArg(addressToVerify.Phone, (string.IsNullOrEmpty(addressToVerify.Phone)), "'PhoneNumber' was not set."),
				};
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var service = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				var fnsResult = service.AddressVerify(addressToVerify, addressToVerify.SeasonId, addressToVerify.TeamLocationId, addressToVerify.SalesRepId, user.Username);
				return result.FromFnsResult(fnsResult);
			});
		}
	}
}