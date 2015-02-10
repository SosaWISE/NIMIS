using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.Lib.RestCake;
using SOS.Lib.RestCake.Attributes;
using SOS.Services.Interfaces;
using SOS.Services.Interfaces.Models;
using SOS.Services.Wcf.Crm.Helper;

namespace SOS.Services.Wcf.Crm
{
	// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthSvc" in code, svc and config file together.
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	[RestService(Namespace = "SOS", ServiceContract = typeof(IAuthSvc))]
	public class AuthSvc : RestHttpHandler, IAuthSvc
	{
		#region Methods

		#region Private

		/*
		private void SetJsonOutput()
		{
			if (WebOperationContext.Current != null)
			{
				WebOperationContext.Current.OutgoingResponse.Format = WebMessageFormat.Json;
			}
		}
*/

		#endregion Private

		#endregion Methods

		#region Implementation of IAuthSvc

		public SosResult<SosSessionInfo> SosStart(string szApplicationToken)
		{
			/** Initialize New Session. */
			var oResult = new SosResult<SosSessionInfo>(0, "Success", "SosSessionInfo") { Value = null };
			var szIPAddress = IPAddressUtil.ClientIPAddress();

			/** Get new Session from database. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
			var oSrvSession = oService.SosStart(szApplicationToken, szIPAddress);
			oResult.SessionId = oSrvSession.SessionID;
			oResult.Value = new SosSessionInfo
								{
									SessionId = oSrvSession.SessionID,
									ApplicationId = oSrvSession.ApplicationId,
									IPAddress = oSrvSession.IPAddress,
									CreatedOn = oSrvSession.CreatedOn
								};

			/** Return result. */
			return oResult;
		}

		public SosResult<SosUser> CheckSessionStatus()
		{
			/** Initialize. */
			var oResult = new SosResult<SosUser>();

			if (Context != null)
			{
				SosUser oUser = SessionCookie.GetSessionCookie(Context);
				/** Check result. */
				if (oUser == null)
					oResult.Code = (int)SosResultCodes.CookieInvalid;
				else
					oResult.Value = oUser;
			}

			/** Return result. */
			return oResult;
		}

		public SosResult<SosUser> SosWiseCrmAuthenticate(long lSessionId, long lDealerId, string szUsername, string szPassword)
		{
			/** Initialize. */
			var oResult = new SosResult<SosUser>(0, "Success", "SosUser") { Value = null };

			/** Get new Session from database. */
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
			try
			{
				var oSrvResult = oService.AuthenticateDealerUser(lSessionId, lDealerId, szUsername, szPassword);
				/** Validate services result. */
				if (oSrvResult == null)
				{
					return new SosResult<SosUser>((int)SosResultCodes.GeneralError
						, string.Format("Credentials passed returned nothing.")
						, "SosUser") { Value = null };
				}

				var oResultCast =
					((FunctionalServices.Models.FnsResult<IFnsWiseCrmDealerUserModel>)
					 (oSrvResult));
				/** Check that there was no error. */
				if (oResultCast.Value == null)
				{
					return new SosResult<SosUser>(oResultCast.Code
						, string.Format("The following exception was thrown:\r\n{0}"
						, oResultCast.Message)
						, "SosUser") { Value = null };
				}
				var oSosUser = new SosUser
				{
					UserId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).AuthUserId
					, DealerUserId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).DealerUserID
					, SessionID = lSessionId
					, SalesRepId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).SalesRepId
					, Username = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).Username
					, Fullname = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).Fullname
					, Firstname = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).Firstname
					, Lastname = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).Lastname
					, LastLogin = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).LastLoginOn
					, DealerId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).DealerId
					, DealerName = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).DealerName
					, TeamLocationId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).TeamLocationId
					, SeasonId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).SeasonId
				};

				/** Set the session Cookie. */
				SessionCookie.SetSessionCookie(oSosUser, true, Context);

				/** Save return value. */
				oResult.Value = oSosUser;

				/** Create a cookie and save it. */
			}
			#region CATCH
			catch (Exception oEx)
			{
				return new SosResult<SosUser>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}"
									, oEx.Message)
					, "SosUser") { Value = null };
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public SosResult<SosUser> TokenAuthentication(string sToken)
		{
			#region Initialize.
			var oResult = new SosResult<SosUser>((int)SosResultCodes.CallInitialization, "Initializing", typeof(SosUser).ToString());
			var oSrv = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
			#endregion Initialize.

			#region TRY
			try
			{
				/** Decrypt Token. */
				IFnsResult<IFnsWiseCrmDealerUserModel> oSrvResult = oSrv.AuthenticationDealerViaToken(sToken);
				if (oSrvResult.Code != (int)SosResultCodes.Success)
				{
					return new SosResult<SosUser>((int)SosResultCodes.LoginFailure, oSrvResult.Message, typeof(SosUser).ToString());
				}

				/** Authenticate via token. */
				var oResultCast =
					((FunctionalServices.Models.FnsResult<IFnsWiseCrmDealerUserModel>)
					 (oSrvResult));
				/** Check that there was no error. */
				if (oResultCast.Value == null)
				{
					return new SosResult<SosUser>(oResultCast.Code
						, string.Format("The following exception was thrown:\r\n{0}"
						, oResultCast.Message)
						, "SosUser") { Value = null };
				}
				var oSosUser = new SosUser
				{
					UserId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).AuthUserId
					, DealerUserId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).DealerUserID
					, SessionID = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).SessionID
					, SalesRepId =  ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).SalesRepId
					, Username = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).Username
					, Fullname = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).Fullname
					, Firstname = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).Firstname
					, Lastname = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).Lastname
					, LastLogin = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).LastLoginOn
					, DealerId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).DealerId
					, DealerName = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).DealerName
					, TeamLocationId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).TeamLocationId
					, SeasonId = ((IFnsWiseCrmDealerUserModel)oSrvResult.GetValue()).SeasonId
				};

				/** Set the session Cookie. */
				SessionCookie.SetSessionCookie(oSosUser, true, Context);

				/** Save return value. */
				oResult.Code = oSrvResult.Code;
				oResult.Message = oSrvResult.Message;
				oResult.Value = oSosUser;


			}
			#endregion TRY
			#region CATCH
			catch (Exception oEx)
			{
				return new SosResult<SosUser>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}"
									, oEx.Message)
					, typeof(SosUser).ToString()) { Value = null };
			}

			#endregion CATCH
			/** Return result. */
			return oResult;
		}

		public SosResult<SosSessionInfo> TerminateCurrentSession()
		{
			/** Check authentication. */
			var oResult = new SosResult<SosSessionInfo>((int)SosResultCodes.CookieInvalid
				, "Validating Authentication Failed", typeof(CmsModels.QlLeadFullData).ToString());
			SosUser oUser;
			// Check user
			if (!SessionCookie.ValidateSessionCookie(Context, true, out oUser)) return oResult;

			/** Initialization */
			oResult = new SosResult<SosSessionInfo>((int)SosResultCodes.GeneralError
				, "Initializing", typeof(SosSessionInfo).ToString());

			#region BEGIN TRY
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
			try
			{
				IFnsResult<IFnsAcSessionModel> oSvcResult = oService.TerminateSession(oUser.SessionID);

				/** Convert to SosModel. */
				if (oSvcResult.Code == (int)SosResultCodes.Success)
				{
					SessionCookie.DestroySessionCookie(Context);
					oResult.Value = AcModelHelper.CastToSosSessionInfo((IFnsAcSessionModel)oSvcResult.GetValue());
				}
				oResult.Code = oSvcResult.Code;
				oResult.Message = oSvcResult.Message;
			}
			#endregion END TRY
			#region BEGIN CATCH
			catch (Exception oEx)
			{
				return new SosResult<SosSessionInfo>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(SosSessionInfo).ToString());
			}
			#endregion END CATCH

			/** Return result. */
			return oResult;
		}

		public SosResult<SosGeneralAuthenticationInfo> GeneralAuthentication(string szUsername, string szPassword)
		{
			/** Initialization. */
			var oResult = new SosResult<SosGeneralAuthenticationInfo>((int)SosResultCodes.ArgumentValidation
				, "Validating Arguments", typeof(SosGeneralAuthenticationInfo).ToString());

			#region BEGIN TRY
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
			try
			{
				IFnsResult<IFnsAcGeneralAuthenticationModel> oSvcResult = oService.GeneralAuthentication(szUsername, szPassword, "IPAddres GOEs here");

				/** Check results. */
				oResult.Code = oSvcResult.Code;
				oResult.Message = oSvcResult.Message;
				if (oSvcResult.Code == (int) SosResultCodes.Success)
				{
					var oItem = (IFnsAcGeneralAuthenticationModel) oSvcResult.GetValue();
					oResult.Value = new SosGeneralAuthenticationInfo
					                	{
					                		AuthenticationToken = oItem.AuthenticationToken,
											Url = oItem.Url
					                	};
				}
			}
			#endregion BEGIN TRY
			#region BEGIN CATCH
			catch (Exception oEx)
			{
				oResult.Code = (int)SosResultCodes.GeneralError;
				oResult.Message = string.Format("An exception was thrown: {0}", oEx.Message);
			}
			#endregion BEGIN CATCH

			/** Return result. */
			return oResult;
		}

		#endregion Implementation of IAuthSvc
	}
}