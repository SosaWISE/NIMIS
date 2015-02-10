using System;
using System.Web.Mvc;
using SOS.Clients.MVC.CorpSite2.Helpers;
using SOS.Framework.Mvc.ActionResults;
using SOS.Framework.Mvc.Controllers;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.Services.Interfaces.Models;
using SOS.Services.Interfaces.Models.Authentication;
using SosResult = SOS.Clients.MVC.CorpSite2.Models.SosResult;

namespace SOS.Clients.MVC.CorpSite2.Controllers
{
	public class AuthSrvController : Controller, IAuthSrvController
	{
		#region Private Member Properties

		private const string _SOS_GPS_CLNT = "SOS_GPS_CLNT";

		#endregion Private Member Properties

		#region Private Member functions 

		public static bool ValidateSession(long lSessionID, string szApplicationToken)
		{
			/** Initialize. */
			bool bResult = false;
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();

			/** Get session information and check timestamp if expired. */
			var oSessionResult = oService.SessionValidate(lSessionID, szApplicationToken, 30);

			/** Check expiration. */
			if (oSessionResult.Code == (int)SosResult.MessageCodes.Success)
			{
				var oSession = (IFnsAcSessionModel) oSessionResult.GetValue();
				bResult = oSession.LastAccessedOn.AddMinutes(30) > DateTime.Now;

			}

			/** Return result. */
			return bResult;
		}

		#endregion Private Member functions 

		//
        // GET: /AuthSrv/
		[AcceptVerbs(HttpVerbs.Get)]
		[OutputCache(Duration = 0)]
		//[WebGet(UriTemplate = "SosStart?ApplicationToken={szApplicationToken}", ResponseFormat = WebMessageFormat.Json)]
		public JsonpResult SosStart(string szApplicationToken)
        {
            /** Initialize. */
			var oResult = new SosResult((int)SosResult.MessageCodes.GeneralWarning, "Initializing", typeof(SosSessionInfo).ToString());
			var szIPAddress = IPAddressUtil.ClientIPAddress();
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();

			try
			{
				/** Check to see if the session has a Authenticated Customer. */
				SosCustomer oSosCustomer;
				if (SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, ValidateSession, szApplicationToken, out oSosCustomer))
				{
					/** Check to see if this user has a valid SessionID. */
					IFnsResult<IFnsAcSessionModel> oResultModel = oService.SessionInfoGet(oSosCustomer.SessionID, szApplicationToken);
					// ** Check 
					if (oResultModel.Code == (int) SosResult.MessageCodes.Success)
					{
						/** Initialize. */
						var oValue = (IFnsAcSessionModel) oResultModel.GetValue();
						oResult.SessionId = oSosCustomer.SessionID;
						oResult.Code = (int) SosResult.MessageCodes.Success;
						oResult.Message = oResultModel.Message;
						oResult.Value = new SosSessionInfo
						{
							SessionId = oValue.SessionID,
							ApplicationId = oValue.ApplicationId,
							IPAddress = oValue.IPAddress,
							CreatedOn = oValue.CreatedOn,
							AuthCustomer = oSosCustomer
						};

						/** Save to cookie. */
						SessionCookie.SetSessionCookie(oSosCustomer, true, System.Web.HttpContext.Current);

						/** Return result. */
						return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
					}
				}

				/** Get new Session from database. */
				var oSrvSession = oService.SosStart(szApplicationToken, szIPAddress);


				oResult.SessionId = oSrvSession.SessionID;
				oResult.Code = (int) SosResult.MessageCodes.Success;
				oResult.Value = new SosSessionInfo
				{
					SessionId = oSrvSession.SessionID,
					ApplicationId = oSrvSession.ApplicationId,
					IPAddress = oSrvSession.IPAddress,
					CreatedOn = oSrvSession.CreatedOn
				};
			}
			catch (Exception oEx)
			{
				oResult.Code = (int) SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("The following error occurred: {0}", oEx.Message);
			}

			/** Return result. */
			//Response.Write("Successfully submitted your information.");
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
		}


		[AcceptVerbs(HttpVerbs.Get)]
		[OutputCache(Duration = 0)]
		public JsonpResult GeneralAuthentication(string sUsername, string sPassword)
		{
			/** Initialize. */
			var oResult = new SosResult((int)SosResult.MessageCodes.GeneralWarning, "Initializing",typeof(GeneralAuthenticationModel).ToString());
			var szIPAddress = IPAddressUtil.ClientIPAddress();
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();

			try
			{
				IFnsResult<IFnsAcGeneralAuthenticationModel> oItem = oService.GeneralAuthentication(sUsername, sPassword, szIPAddress);
				oResult.Code = oItem.Code;
				oResult.Message = oItem.Message;

				/** Check result. */
				if (oItem.Code == (int)SosResult.MessageCodes.Success)
				{
					var oModel = new GeneralAuthenticationModel
					             	{
					             		SessionID = ((IFnsAcGeneralAuthenticationModel) oItem.GetValue()).SessionID,
					             		AuthenticationToken = ((IFnsAcGeneralAuthenticationModel) oItem.GetValue()).AuthenticationToken,
					             		Url = ((IFnsAcGeneralAuthenticationModel) oItem.GetValue()).Url
					             	};
					oResult.Value = oModel;
				}
			}
			catch (Exception oEx)
			{
				oResult.Code = (int)SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("The following error occurred: {0}", oEx.Message);
			}

			/** Return result. */
			//Response.Write("Successfully submitted your information.");
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
		}


		[AcceptVerbs(HttpVerbs.Get)]
		[OutputCache(Duration = 0)]
		public JsonpResult TokenAuthentication(string sToken)
		{
			/** Initialize. */
			var oResult = new SosResult((int)SosResult.MessageCodes.GeneralWarning, "Initializing"
				, typeof(GeneralAuthenticationModel).ToString());
			var oSrvAuth = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
			var oSrvWCrm = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();

			/** Execute. */
			try
			{
				/** Get Token data. */
				IFnsResult<IFnsAcGeneralAuthenticationModel> oResultSrv = oSrvAuth.DecryptToken(sToken);

				/** Get Customer Information. */
				var oModel = (IFnsAcGeneralAuthenticationModel) oResultSrv.GetValue();
				var oAeCustomerResult = oSrvWCrm.AeCustomerRead(oModel.ID);
				var oAeCustomerModel = (IFnsCustomerFullDataModel)oAeCustomerResult.GetValue();

				var oSosCustomer = new SosCustomer
				    {
        		        CustomerID = oAeCustomerModel.CustomerID
						, SessionID = oModel.SessionID
						, CustomerMasterFileId = oAeCustomerModel.CustomerMasterFileId
						, CustomerTypeId = oAeCustomerModel.CustomerTypeId
						, DealerId = oAeCustomerModel.DealerId
						, LocalizationId = oAeCustomerModel.LocalizationId
						, Prefix = oAeCustomerModel.Salutation
						, Firstname = oAeCustomerModel.FirstName
						, MiddleName = oAeCustomerModel.MiddleName
						, Lastname = oAeCustomerModel.LastName
						, Postfix = oAeCustomerModel.Suffix
						, Gender = oAeCustomerModel.Gender
						, PhoneHome = oAeCustomerModel.PhoneHome
						, PhoneWork = oAeCustomerModel.PhoneWork
						, PhoneCell = oAeCustomerModel.PhoneMobile
						, Email = oAeCustomerModel.Email
						, DOB = oAeCustomerModel.DOB
						, SSN = oAeCustomerModel.SSN
						, Username = oAeCustomerModel.Username
				    };
				SessionCookie.SetSessionCookie(oSosCustomer, true, System.Web.HttpContext.Current);

				/** Cast to return value. */
				CmsModels.AeCustomer oAeCustomer = ConvertTo.CastFnsToAeCustomer(oAeCustomerModel);


				/** Return result. */
				oResult.Code = (int) SosResultCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oAeCustomer;

			}
			catch (Exception oEx)
			{
				oResult.Code = (int)SosResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("The following error occurred: {0}", oEx.Message);
			}

			/** Return result. */
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[OutputCache(Duration = 0)]
		public JsonpResult TerminateSession()
		{
			#region Authentication
			/** Authenticate. */
			var oResult = new SosResult((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", null);
			SosCustomer oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, ValidateSession, _SOS_GPS_CLNT, out oUser)) 
				return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);
			#endregion Authentication

			#region Execute Try
			try
			{
				/** Initialize. */
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
				IFnsResult<IFnsAcSessionModel> oFnsResult = oService.TerminateSession(oUser.SessionID);

				/** Check result. */
				oResult.Code = oFnsResult.Code;
				oResult.Message = oFnsResult.Message;

			}
			#endregion Execute Try

			#region Execute Catch
			catch (Exception oEx)
			{
				oResult = new SosResult((int)SosResultCodes.ExceptionThrown
					, string.Format("The following exception was thrown: {0}", oEx.Message), null);
			}
			#endregion Execute Catch

			#region Return Result

			/** Return result. */
			return this.Jsonp(oResult, JsonRequestBehavior.AllowGet);

			#endregion Return Result
		}
	}
}
