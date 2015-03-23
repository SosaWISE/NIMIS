using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Models;
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
		public Result<IFnsVerifyAddress> SaveAddress([FromBody]FnsVerifyAddress address)
		{
			return CORSSecurity.Authorize("SaveAddress", AuthApplications.SSECmsCORSID, null, user =>
			{
				var result = new Result<IFnsVerifyAddress>();

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
				return result.FromFnsResult(fnsResult);
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