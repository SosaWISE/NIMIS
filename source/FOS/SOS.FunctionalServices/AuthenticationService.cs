/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 17:28
 * 
 * Description:  Authenticates a request.
 *********************************************************************************************************************/

using Newtonsoft.Json;
using SOS.Data.AuthenticationControl;
using SOS.Data.HumanResource;
using SOS.Data.HumanResource.ControllerExtensions;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AuthenticationControl;
using SOS.FunctionalServices.Contracts.Models.Data;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.AuthenticationControl;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Util.ActiveDirectory;
using SOS.Lib.Util.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using ErrorCodes = SOS.FunctionalServices.Contracts.Helper.ErrorCodes;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class AuthenticationService : IAuthenticationService
	{
		public readonly bool MockADGroups;
		public AuthenticationService()
		{
			MockADGroups = string.Compare(SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig("MockADGroups"), "true", true) == 0;
		}

		#region Implementation of IAuthenticationService

		public IFnsAcSessionModel SosStart(string szAccessToken, string szIPAddress, int timezoneOffset)
		{
			/** Initialization. */
			AC_Session oSession;

			try
			{
				/** Set timestamp. */
				oSession = SosAuthControlDataContext.Instance.AC_Sessions.StartSession(szAccessToken, szIPAddress, timezoneOffset);
			}
			catch (Exception oEx)
			{
				Data.Logging.DBErrorManager.Instance.AddMessage(ErrorMessageType.Exception, oEx
					, "Error on SosStart"
					, oEx.Message);
				return new FnsAcSessionModel(0, string.Empty, string.Empty, DateTime.Now);
			}

			/** Bind dataobject to Model. */
			var oSessionResult = new FnsAcSessionModel(oSession);

			/** Result. */
			return oSessionResult;
		}

		public IFnsResult<IFnsAcSessionModel> SseStart(string szAccessToken, string szIPAddress)
		{
			var oResult = new FnsResult<IFnsAcSessionModel>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing SessionInfoGet"
			};
			return oResult;
		}

		public IFnsResult<IFnsAcSessionModel> SessionInfoGet(long lSessionID, string szApplicationToken)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAcSessionModel>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing SessionInfoGet"
			};

			/** Validate. */
			if (lSessionID == 0)
			{
				return new FnsResult<IFnsAcSessionModel>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "SessionInfoGet is missing lSessionID argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				AC_Session oResulItem = SosAuthControlDataContext.Instance.AC_Sessions.SessionGetByIdAndApplicationID(lSessionID, szApplicationToken);

				// ** Check to see if we got a hit.
				if (oResulItem != null)
				{
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = new FnsAcSessionModel(oResulItem);
				}
				else
				{
					oResult.Code = (int)ErrorCodes.SessionExp;
					oResult.Message = "Unable to find Session";
				}
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAcSessionModel>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at SessionInfoGet: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsAcSessionModel> SessionValidate(long lSessionID, string szApplicationToken, int nMinutes)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAcSessionModel>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing SessionValidate"
			};

			/** Validate. */
			if (lSessionID == 0)
			{
				return new FnsResult<IFnsAcSessionModel>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "SessionValidate is missing lSessionID argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				AC_Session oResulItem = SosAuthControlDataContext.Instance.AC_Sessions.SessionValidate(lSessionID, szApplicationToken, nMinutes);

				// ** Check to see if we got a hit.
				if (oResulItem != null)
				{
					oResult.Code = (int)ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = new FnsAcSessionModel(oResulItem);
				}
				else
				{
					oResult.Code = (int)ErrorCodes.SessionExp;
					oResult.Message = "Session is expired";
				}
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAcSessionModel>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at SessionValidate: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsAcSessionModel> TerminateSession(long lSessionId)
		{
			/** Initialize. */
// ReSharper disable RedundantAssignment
			IFnsResult<IFnsAcSessionModel> oResult = new FnsResult<IFnsAcSessionModel>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "Initializing"
				};
// ReSharper restore RedundantAssignment

			try
			{
				AC_Session oSession = SosAuthControlDataContext.Instance.AC_Sessions.TerminateSession(lSessionId);

				oResult = new FnsResult<IFnsAcSessionModel>
					{
						Code = (int) ErrorCodes.Success
						, Message = "Success"
						, Value = new FnsAcSessionModel(oSession)
					};

			}
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAcSessionModel>
					{
						Code = (int)ErrorCodes.LogoutFailure
						, Message = string.Format("Following error occured on TerminateSession {0}"
						, oEx.Message)
					};
			}

			/** Return result. */
			return oResult;
		}

		//public IFnsAcAuthenticationModel SosAuthenticate(long lSessionId, long lDealerId, string szUsername, string szPassword)
		//{
		//    throw new NotImplementedException();
		//}

		public IFnsResult<IFnsAcGeneralAuthenticationModel> GeneralAuthentication(string sUsername, string sPassword, string sIPAddress, int timezoneOffset)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAcGeneralAuthenticationModel>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeneralAuthentication"
			};

			/** Validate. */
			if (string.IsNullOrWhiteSpace(sUsername))
			{
				return new FnsResult<IFnsAcGeneralAuthenticationModel>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeneralAuthentication is missing sUsername argument."
				};
			}
			if (string.IsNullOrWhiteSpace(sPassword))
			{
				return new FnsResult<IFnsAcGeneralAuthenticationModel>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeneralAuthentication is missing sPassword argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY

			try
			{
				/** Get accounts. */
				AC_UserGeneralAuthenticationViewCollection oResulItems =
					SosAuthControlDataContext.Instance.AC_UserGeneralAuthenticationViews.GetListOfAccountByUsername(sUsername, sPassword);

				/** Check that there was an authentication performed. */
				if (oResulItems == null || oResulItems.Count == 0)
				{
					oResult.Code = (int) ErrorCodes.LoginFailure;
					oResult.Message = "Authentication failed with the credentials passed.";
				}
				else
				{
					/** Find GPS Client first. */
					AC_UserGeneralAuthenticationView oItem = oResulItems.FirstOrDefault();
// ReSharper disable PossibleNullReferenceException
					IFnsAcSessionModel oSosResult = SosStart(oItem.ApplicationID, sIPAddress, timezoneOffset);
// ReSharper restore PossibleNullReferenceException
					var oAcAuthentication = new AC_Authentication
						{
							SessionId = oSosResult.SessionID,
							UserId = oItem.UserID,
							Username = sUsername,
							Password = sPassword
						};
					oAcAuthentication.Save();

					var oResultValue = new FnsAcGeneralAuthenticationModel();

					switch (oItem.ApplicationID)
					{
						case AC_Application.MetaData.SOSGPSClientID:
							oResultValue.SessionID = oSosResult.SessionID;
							oResultValue.AuthenticationToken = _getAuthenticationToken(oItem, sIPAddress, oSosResult.SessionID, oAcAuthentication.AuthenticationID);
							oResultValue.Url = oItem.WebUrl;
							oResult.Code = (int) ErrorCodes.Success;
							oResult.Message = "Success";
							oResult.Value = oResultValue;
							break;
						case AC_Application.MetaData.SOSCRMID:
							oResultValue.SessionID = oSosResult.SessionID;
							oResultValue.AuthenticationToken = _getAuthenticationToken(oItem, sIPAddress, oSosResult.SessionID, oAcAuthentication.AuthenticationID);
							oResultValue.Url = oItem.WebUrl;
							oResult.Code = (int) ErrorCodes.Success;
							oResult.Message = "Success";
							oResult.Value = oResultValue;
							break;
						default:
							oResult.Code = (int)ErrorCodes.LoginFailure;
							oResult.Message = "Login Failure";
							break;
					}
				}
			}

			#endregion TRY
			#region CATCH

			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAcGeneralAuthenticationModel>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at GeneralAuthentication: {0}", oEx.Message)
				};
			}

			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsAcGeneralAuthenticationModel> DecryptToken(string sTokenStream)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAcGeneralAuthenticationModel>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GeneralAuthentication"
			};

			/** Validate. */
			if (string.IsNullOrWhiteSpace(sTokenStream))
			{
				return new FnsResult<IFnsAcGeneralAuthenticationModel>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "GeneralAuthentication is missing sTokenStream argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				FnsAcGeneralAuthenticationModel oTokenRaw = _decryptAuthenticationToken(sTokenStream);

				/** Check what client we are talking about. */
				switch (oTokenRaw.ApplicationID)
				{
					case AC_Application.MetaData.SOSGPSClientID:

						break;
					case AC_Application.MetaData.SOSCRMID:
						break;
// ReSharper disable RedundantEmptyDefaultSwitchBranch
					default:
						break;
// ReSharper restore RedundantEmptyDefaultSwitchBranch
				}

				oResult.Code = (int) ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oTokenRaw;
			}
			#endregion TRY
			#region CATCH
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAcGeneralAuthenticationModel>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at GeneralAuthentication: {0}", oEx.Message)
				};
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		public IFnsResult<IFnsWiseCrmDealerUserModel> AuthenticationDealerViaToken(string sTokenStream)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsWiseCrmDealerUserModel>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing AuthenticationDealerViaToken"
			};

			/** Validate. */
			if (string.IsNullOrWhiteSpace(sTokenStream))
			{
				return new FnsResult<IFnsWiseCrmDealerUserModel>
				{
					Code = (int)ErrorCodes.GeneralMessage
					, Message = "AuthenticationDealerViaToken is missing sTokenStream argument."
				};
			}

			#endregion INITIALIZATION

			#region TRY
			try
			{
				/** Decrypt Token. */
				FnsAcGeneralAuthenticationModel oTokenRaw = _decryptAuthenticationToken(sTokenStream);

				/** Get Dealer information. */
				long lSessionId = oTokenRaw.SessionID;
				var nDealerUserID = (int) oTokenRaw.ID;
				AC_UsersDealerUsersAuthenticateView oAuthDealerUser = SosAuthControlDataContext.Instance.AC_UsersDealerUsersAuthenticateViews.AuthenticateViaToken(lSessionId, nDealerUserID, sTokenStream);
				/*AC_Authentication oAuth = */
				SosAuthControlDataContext.Instance.AC_Authentications.SaveEvent(
					oAuthDealerUser.SessionID, oAuthDealerUser.UserID, oAuthDealerUser.Username, oAuthDealerUser.Password);
				IFnsWiseCrmDealerUserModel oAuthResult = new FnsWiseCrmDealerUserModel(oAuthDealerUser);

				oResult.Code = (int)ErrorCodes.Success;
				oResult.Message = "Success";
				oResult.Value = oAuthResult;
			}
			#endregion TRY
			#region CATCH
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsWiseCrmDealerUserModel>
				{
					Code = (int)ErrorCodes.UnexpectedException
					, Message = string.Format("Exception thrown at AuthenticationDealerViaToken: {0}", oEx.Message)
				};
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}

		#region Private Member Functions
		private string _getAuthenticationToken(AC_UserGeneralAuthenticationView oItem, string sIPAddress, long lSessionID, long lAuthenticationID)
		{
			/** Initialize. */
			const string AUTH_TOKEN = "ID={0}|SessionID={1}|ApplicationID={2}|IPAddress={3}|AuthenticationID={4}";

			/** Build Token. */
			string sResult = string.Format(AUTH_TOKEN, oItem.ID, lSessionID, oItem.ApplicationID, sIPAddress, lAuthenticationID);
			sResult = Lib.Util.Cryptography.TripleDES.EncryptString(sResult, null);

			/** Return result. */
			return sResult;
		}

		private FnsAcGeneralAuthenticationModel _decryptAuthenticationToken(string sToken)
		{
			/** Inititialize. */
			string sResult = Lib.Util.Cryptography.TripleDES.DecryptString(sToken, null);
			string[] saTokenArray = sResult.Split('|');
			var oModel = new FnsAcGeneralAuthenticationModel();

			foreach (var sItem in saTokenArray)
			{
				/** Initialize. */
				var saItem = sItem.Split('=');
				switch (saItem[0])
				{
					case "ID":
						oModel.ID = Convert.ToInt64(saItem[1]);
						break;
					case "SessionID":
						oModel.SessionID = Convert.ToInt64(saItem[1]);
						break;
					case "ApplicationID":
						oModel.ApplicationID= saItem[1];
						break;
					case "IPAddress":
						oModel.IPAddress = saItem[1];
						break;
				}
			}

			/** Return result. */
			return oModel;
		}
		#endregion Private Member Functions

		#region GpsClient CRUD

		public IFnsResult<List<IFnsAeGpsClient>> GpsClientRead(long? customerMasterFileId, long? customerID)
		{
			/** Initialize. */
			#region INITIALIZATION

			var oResult = new FnsResult<List<IFnsAeGpsClient>>
			{
				Code = (int)ErrorCodes.GeneralMessage
				, Message = "Initializing GpsClientRead"
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				List<IFnsAeGpsClient> list = null;
				AE_GpsClientToCustomerMasterViewCollection oCol = SosCrmDataContext.Instance.AE_GpsClientToCustomerMasterViews.Read(customerMasterFileId, customerID);

				foreach (AE_GpsClientToCustomerMasterView oItem in oCol)
				{
					if (list == null) list = new List<IFnsAeGpsClient>();

					list.Add(new FnsAeGpsClient(oItem));
				}

				oResult.Code = (int) ErrorCodes.Success;
				oResult.Message = "Successful";
				oResult.Value = list;
			}
			#endregion TRY
			#region CATCH
			catch (Exception oEx)
			{
				oResult = new FnsResult<List<IFnsAeGpsClient>>
					{
						Code = (int)ErrorCodes.UnexpectedException
						, Message = string.Format("Exception thrown at the GpsClientRead: {0}", oEx.Message)
					};
			}
			#endregion CATCH

			/** Return result. */
			return oResult;
		}
		#endregion GpsClient CRUD

		public IFnsResult<IFnsAcCmsUser> AuthenticateCmsUser(string username, string password, long sessionID, string appToken)
		{
			#region INITIALIZATION

			var oResult = new FnsResult<IFnsAcCmsUser>
				{
					Code = (int) ErrorCodes.GeneralMessage
					, Message = "Initializing AuthenitcateCmsUser method."
				};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				var groups = GetGroupsForUser(username);
				AC_UsersAppAuthenticationView crmUser = SosAuthControlDataContext.Instance.AC_UsersAppAuthenticationViews.SigninToApp(username, password, sessionID, appToken, groups);

				if (crmUser != null && crmUser.IsLoaded)
				{
					var resultValue = new FnsAcCmsUser(crmUser);

					oResult.Code = (int) ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = resultValue;
				}
				else
				{
					oResult.Code = (int) ErrorCodes.LoginFailure;
					oResult.Message = "Login failed";
				}
			}

			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
				oResult = new FnsResult<IFnsAcCmsUser>
					{
						Code = (int)ErrorCodes.UnexpectedException
						, Message = string.Format("Exception thrown at the AuthenticateCmsUser: {0}", oEx.Message)
					};
			}

			#endregion CATCH

			// ** Return result
			return oResult;
		}
		private List<string> GetGroupsForUser(string username)
		{
			if (!MockADGroups)
			{
				return ADHelper.GetGroupsForUser(username);
			}
			else
			{
				var filepath = Path.Combine(System.Web.HttpContext.Current.Request.PhysicalApplicationPath, "mockadgroups.json");
				if (File.Exists(filepath))
				{
					var dict = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(File.ReadAllText(filepath));
					if (dict.ContainsKey(username))
					{
						return dict[username];
					}
				}
				// default
				return new List<string> { "Nexsense", };
			}
		}
		public IFnsResult<List<string>> SessionApps(long sessionID)
		{
			return new FnsResult<List<string>>
			{
				Value = SosAuthControlDataContext.Instance.AC_Applications.ForSession(sessionID).ConvertAll(a => a.ApplicationID.ToLower()),
			};
		}
		public IFnsResult<List<string>> SessionActions(long sessionID)
		{
			return new FnsResult<List<string>>
			{
				Value = SosAuthControlDataContext.Instance.AC_Actions.ForSession(sessionID).ConvertAll(a => a.ActionID.ToLower()),
			};
		}
		public IFnsResult<bool> HasPermission(long sessionID, string applicationID, string actionID)
		{
			return new FnsResult<bool>
			{
				Value = ((applicationID == null || SessionApps(sessionID).GetTValue().Contains(applicationID.ToLower())) && // applicationID must be null or in list
						 (actionID == null || SessionActions(sessionID).GetTValue().Contains(actionID.ToLower()))) // actionID must be null or in list
			};
		}

		public IFnsResult<IFnsRuUser> SalesRepRead(string companyID, string username)
		{
			#region INITIALIZATION

            var oResult = new FnsResult<IFnsRuUser>
				{
					Code = (int) ErrorCodes.GeneralMessage
					, Message = "Initializing SalesRepRead method."
				};

			#endregion INITIALIZATION

			#region TRY

			try
			{
                RU_User crmUser = HumanResourceDataContext.Instance.RU_Users.LoadBySalesRepId(companyID);
				
				if (crmUser.IsLoaded)
				{
					var resultValue = new FnsRuUser(crmUser);
					oResult.Code = (int) ErrorCodes.Success;
					oResult.Message = "Success";
					oResult.Value = resultValue;
				}
				else
				{
					oResult.Code = (int) ErrorCodes.LoginFailure;
					oResult.Message = "Login failed";
				}
			}

			#endregion TRY

			#region CATCH
			catch (Exception oEx)
			{
                oResult = new FnsResult<IFnsRuUser>
					{
						Code = (int)ErrorCodes.UnexpectedException
						, Message = string.Format("Exception thrown at the SalesRepRead: {0}", oEx.Message)
					};
			}

			#endregion CATCH

			// ** Return result
			return oResult;
		}

		public DateTime? GetLocalDateTime()
		{
			return SosAuthControlDataContext.Instance.AC_DateTimeViews.GetLocalDateTime();
		}

		public DateTime? GetUTCDateTime()
		{
			return SosAuthControlDataContext.Instance.AC_DateTimeViews.GetUTCDateTime();
		}

		#endregion Implementation of IAuthenticationService
	}
}
