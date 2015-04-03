using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.FunctionalServices.Models;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;
using SOS.Lib.Core;

namespace SSE.Services.CmsCORS.Controllers.Qualify
{
	[RoutePrefix("QualifySrv")]
	public class VerificationSrvController : ApiController
	{
		private IWiseCrmService Service
		{
			get
			{
				return SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			}
		}

		#region Validate Address

		[Route("VerifyAddress")]
		[HttpPost]
		[HttpOptions]
		public CmsCORSResult<SOS.Services.Interfaces.Models.QualifyLead.QlAddress> VerifyAddress(AddressParam jsonParam)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "VerifyAddress";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, oUser =>
				{
					#region Parameter Validation

					var aCORSArg = new List<CORSArg>
					{
                        //new CORSArg(jsonParam.AddressId,
                        //    (string.IsNullOrEmpty(jsonParam.AddressId.ToString(CultureInfo.InvariantCulture))),
                        //    "<li>'AddressId' can not be blank.</li>"),
						new CORSArg(jsonParam.DealerId, (string.IsNullOrEmpty(jsonParam.DealerId.ToString(CultureInfo.InvariantCulture))),
							"<li>'DealerId' can not be blank.</li>"),
						new CORSArg(jsonParam.StreetAddress, (string.IsNullOrEmpty(jsonParam.StreetAddress)),
							"<li>'StreetAddress' can not be blank.</li>"),
						//new CORSArg(jsonParam.StreetAddress2, (string.IsNullOrEmpty(jsonParam.StreetAddress2)),
						//	"<li>'StreetAddress2' can not be blank.</li>"),
						new CORSArg(jsonParam.City, (string.IsNullOrEmpty(jsonParam.City)), "<li>'City' can not be blank.</li>"),
						new CORSArg(jsonParam.StateId, (string.IsNullOrEmpty(jsonParam.StateId)), "<li>'StateId' can not be blank.</li>"),
						new CORSArg(jsonParam.PostalCode, (string.IsNullOrEmpty(jsonParam.PostalCode)),
							"<li>'PostalCode' can not be blank.</li>"),
						new CORSArg(jsonParam.PhoneNumber, (string.IsNullOrEmpty(jsonParam.PhoneNumber)),
							"<li>'PhoneNumber' was not set.</li>")
					};
					CmsCORSResult<SOS.Services.Interfaces.Models.QualifyLead.QlAddress> oResult;
					if (!CORSArg.ArgumentValidation(aCORSArg, out oResult, METHOD_NAME)) return oResult;

					#endregion Parameter Validation

					#region Execute Try

					try
					{
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
						var addressToVerify = new FnsVerifyAddress
						{
							AddressID = jsonParam.AddressId,
							DealerId = jsonParam.DealerId,
							TimeZoneId = jsonParam.TimeZoneId,
							StreetAddress = jsonParam.StreetAddress,
							StreetAddress2 = jsonParam.StreetAddress2,
							City = jsonParam.City,
							StateId = jsonParam.StateId,
							PostalCode = jsonParam.PostalCode,
							Phone = jsonParam.PhoneNumber,

							SeasonId = jsonParam.SeasonId,
							TeamLocationId = jsonParam.TeamLocationId,
							SalesRepId = jsonParam.SalesRepId,

						};
						IFnsResult<IFnsVerifyAddress> oFnsModel = oService.AddressVerify(addressToVerify, jsonParam.SeasonId, jsonParam.TeamLocationId, jsonParam.SalesRepId, oUser.Username);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							SessionCookie.DestroySessionCookie(System.Web.HttpContext.Current);
							oResult.Code = oFnsModel.Code;
							oResult.Message = oFnsModel.Message;
							return oResult;
						}

						/** Setup return corsResult. */
						var oQlAddress = ConvertTo.CastFnsToVerifyAddress((IFnsVerifyAddress)oFnsModel.GetValue());


						/** Save success results. */
						oResult.Code = (int)CmsResultCodes.Success;
						oResult.SessionId = jsonParam.SessionID;
						oResult.Message = "Success";
						oResult.Value = oQlAddress;

					}

					#endregion Execute Try

					#region Execute Catch

					catch (Exception oEx)
					{
						oResult.Code = (int)CmsResultCodes.ExceptionThrown;
						oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
					}

					#endregion Execute Catch

					#region Result

					return oResult;

					#endregion Result
				});
		}

		#endregion Validate Address

		#region Verify Home Owner

		[Route("VerifyHomeOwership")]
		[HttpPost]
		[HttpOptions]
		public CmsCORSResult<List<QlHomeOwner>> VerifyHomeOwnership(AddressParam jsonParam)
		{
			#region Initialize

			const string METHOD_NAME = "VerifyHomeOwnership";
			var oResult = new CmsCORSResult<List<QlHomeOwner>>((int)ErrorCodes.Initializing
				, string.Format("Initializing method '{0}'", METHOD_NAME)) { Value = null };

			#endregion Initialize

			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, oUser =>
				{
					#region Execute Try

					try
					{
						/** TODO: 
						 * Add signature in functional services to complete this method. */
					}

					#endregion Execute Try

					#region Execute Catch

					catch (Exception oEx)
					{
						oResult.Code = (int)CmsResultCodes.ExceptionThrown;
						oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
					}

					#endregion Execute Catch

					#region Result

					return oResult;

					#endregion Result
				});
		}

		#endregion Verify Home Owner

		#region Run Credit

		[Route("Leads")]
		[HttpPost]
		[HttpOptions]
		public Result<IFnsQlLead> SaveLead(LeadParam jsonParam)
		{
			return CORSSecurity.Authorize("SaveLead", null, null, oUser =>
			{
				var result = new Result<IFnsQlLead>();

				if (jsonParam == null)
				{
					result.Code = -1;
					result.Message = "Body not posted";
				}
				var argArray = new List<CORSArg>
				{
// ReSharper disable PossibleNullReferenceException
					new CORSArg(jsonParam.CustomerTypeId, string.IsNullOrEmpty(jsonParam.CustomerTypeId), "'CustomerTypeId' Has to be passed."),
					new CORSArg(jsonParam.AddressId, (jsonParam.AddressId == 0), "'AddressId' Has to be passed."),
					new CORSArg(jsonParam.DealerId, (jsonParam.DealerId == 0), "'DealerId' Has to be passed."),
					new CORSArg(jsonParam.TeamLocationId, (jsonParam.TeamLocationId == 0), "'TeamLocationId' Has to be passed."),
					new CORSArg(jsonParam.SeasonId, (jsonParam.SeasonId == 0), "'SeasonId' Has to be passed."),
					new CORSArg(jsonParam.SalesRepId, (string.IsNullOrEmpty(jsonParam.SalesRepId)), "'SalesRepId' Has to be passed."),
					new CORSArg(jsonParam.LocalizationId, (string.IsNullOrEmpty(jsonParam.LocalizationId)), "'LocalizationId' Has to be passed."),
					new CORSArg(jsonParam.LeadSourceId, (jsonParam.LeadSourceId == 0), "'LeadSourceId' Has to be passed."),
					new CORSArg(jsonParam.LeadDispositionId, (jsonParam.LeadDispositionId == 0), "'LeadDispositionId' Has to be passed."),
					new CORSArg(jsonParam.FirstName, (string.IsNullOrEmpty(jsonParam.FirstName)), "'FirstName' Has to be passed."),
					new CORSArg(jsonParam.LastName, (string.IsNullOrEmpty(jsonParam.LastName)), "'LastName' Has to be passed."),
					new CORSArg(jsonParam.SSN, (string.IsNullOrEmpty(jsonParam.SSN) && jsonParam.DOB == null), "'SSN or DOB' Has to be passed.")
// ReSharper restore PossibleNullReferenceException
				};
				if (!CORSArg.ArgumentValidation(argArray, result))
				{
					return result;
				}

				var fnsLead = new FnsQlLead
				{
					LeadID = jsonParam.LeadID,
					AddressId = jsonParam.AddressId,
					CustomerTypeId = jsonParam.CustomerTypeId,
					CustomerMasterFileId = jsonParam.CustomerMasterFileId,
					DealerId = oUser.DealerId,
					LocalizationId = jsonParam.LocalizationId,
					TeamLocationId = jsonParam.TeamLocationId,
					SeasonId = jsonParam.SeasonId,
					SalesRepId = jsonParam.SalesRepId,
					LeadSourceId = jsonParam.LeadSourceId,
					LeadDispositionId = jsonParam.LeadDispositionId,
					LeadDispositionDateChange = jsonParam.LeadDispositionDateChange,
					Salutation = jsonParam.Salutation,
					FirstName = jsonParam.FirstName,
					MiddleName = jsonParam.MiddleName,
					LastName = jsonParam.LastName,
					Suffix = jsonParam.Suffix,
					Gender = jsonParam.Gender,
					SSN = jsonParam.SSN,
					DOB = jsonParam.DOB,
					DL = jsonParam.DL,
					DLStateId = jsonParam.DLStateID,
					Email = jsonParam.Email,
					PhoneWork = jsonParam.PhoneWork,
					PhoneMobile = jsonParam.PhoneMobile,
					PhoneHome = jsonParam.PhoneHome,
					ProductSkwId = jsonParam.ProductSkwId
				};
				var fnsResult = Service.SaveLead(fnsLead, oUser.Username, jsonParam.CreateMasterLead);
				return result.FromFnsResult(fnsResult);
			});
		}

		[Route("RunCredit/{leadID}")]
		[HttpPost]
		[HttpOptions]
		public Result<IFnsQlCreditReport> RunCredit(long leadID, bool bypass = false)
		{
			return CORSSecurity.Authorize("RunCredit", null, null, oUser =>
			{
				var result = new Result<IFnsQlCreditReport>();
				if (leadID == 0)
				{
					result.Code = -1;
					result.Message = "Missing LeadID";
				}
				var fnsResult = Service.RunCredit(leadID, bypass, oUser.Username);
				return result.FromFnsResult(fnsResult);
			});
		}

		#endregion Run Credit
	}
}