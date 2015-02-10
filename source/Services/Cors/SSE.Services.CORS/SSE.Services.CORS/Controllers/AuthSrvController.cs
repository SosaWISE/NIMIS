using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Models;
using SOS.Services.Interfaces.Models;
using SOS.Services.Interfaces.Models.Authentication;
using SSE.Services.CORS.Helpers;
using SSE.Services.CORS.Models;

namespace SSE.Services.CORS.Controllers
{

	/// <summary>
	/// AuthSrv Controller is what allows us to authenticate to the client.
	/// </summary>
	public class AuthSrvController : ApiController
	{
		#region Public Members
		public class AppInfo
		{
			public string AppToken { get; set; }
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<ISosSessionInfo> SessionStart(AppInfo appInfo)
		{
			/** Initialize. */
			var oResult = new SosCORSResult<ISosSessionInfo>((int)SosCORSResult.MessageCodes.GeneralWarning, "Initializing", typeof(SosSessionInfo).ToString());
			var szIPAddress = IPAddressUtil.ClientIPAddress();
			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();

			try
			{
				/** Check to see if the session has a Authenticated Customer. */
				SosCustomer oSosCustomer;
				if (SessionCookie.ValidateSessionCookie(System.Web.HttpContext.Current, true, CORSSecurity.ValidateSession, appInfo.AppToken, out oSosCustomer))
				{
					/** Check to see if this user has a valid SessionID. */
					IFnsResult<IFnsAcSessionModel> oResultModel = oService.SessionInfoGet(oSosCustomer.SessionID, appInfo.AppToken);
					// ** Check 
					if (oResultModel.Code == (int)SosCORSResult.MessageCodes.Success)
					{
						/** Initialize. */
						var oValue = (IFnsAcSessionModel)oResultModel.GetValue();
						oResult.SessionId = oSosCustomer.SessionID;
						oResult.Code = (int)SosCORSResult.MessageCodes.Success;
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

						/** Return corsResult. */
						//return Json(oCORSResult, JsonRequestBehavior.DenyGet);
						return oResult;
					}
				}

				/** Get new Session from database. */
				var oSrvSession = oService.SosStart(appInfo.AppToken, szIPAddress);


				oResult.SessionId = oSrvSession.SessionID;
				oResult.Code = (int)SosCORSResult.MessageCodes.Success;
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
				oResult.Code = (int)SosCORSResult.MessageCodes.GeneralException;
				oResult.Message = string.Format("The following error occurred: {0}", oEx.Message);
			}

			/** Return corsResult. */
			//Response.Write("Successfully submitted your information.");
			//return Json(oCORSResult, JsonRequestBehavior.DenyGet);
			return oResult;
		}

		public class UserAuthInfo
		{
			public string Username { get; set; }
			public string Password { get; set; }
			public long SessionID { get; set; }
			public bool RememberMe { get; set; }
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer> CustomerAuth(UserAuthInfo userAuthInfo)
		{
			/** Inititalizing. */
			SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer> oResult;

			#region Parameter Validation

			var aCORSArg = new List<CORSArg>
			{
				new CORSArg(userAuthInfo.Username, (string.IsNullOrEmpty(userAuthInfo.Username)), "<li>'Username' can not be blank.</li>"),
				new CORSArg(userAuthInfo.Password, (string.IsNullOrEmpty(userAuthInfo.Password)), "<li>'Password' can not be blank.</li>"),
				new CORSArg(userAuthInfo.SessionID, (userAuthInfo.SessionID == 0), "<li>'SessionID' was not passed or is 0.</li>"),
			};
			if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;

			#endregion Parameter Validation

			#region Execute Authentication
			#region TRY
			/** Execute authentication. */
			try
			{
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				IFnsResult<IFnsAeCustomerGpsClientsViewModel> oFnsModel = oService.AeCustomerAuthenticate(userAuthInfo.Username, userAuthInfo.Password);

				/** Check corsResult. */
				if (oFnsModel.Code != 0)
				{
					SessionCookie.DestroySessionCookie(System.Web.HttpContext.Current);
					oResult.Code = oFnsModel.Code;
					oResult.Message = oFnsModel.Message;
					return oResult;
				}

				/** Setup return corsResult. */
				var oAeCustomer = ConvertTo.CastFnsToAeCustomer((IFnsAeCustomerGpsClientsViewModel)oFnsModel.GetValue());

				/** Save session cookie. */
				var oSosCustomer = new SosCustomer
					{
						CustomerID = oAeCustomer.CustomerID
						, SessionID = userAuthInfo.SessionID
						, CustomerMasterFileId = oAeCustomer.CustomerMasterFileId
						, CustomerTypeId = oAeCustomer.CustomerTypeId
						, DealerId = oAeCustomer.DealerId
						, DealerName = string.Empty
						, LocalizationId = oAeCustomer.LocalizationId
						, LocalizationName = string.Empty
						, Prefix = oAeCustomer.Prefix
						, Firstname = oAeCustomer.FirstName
						, MiddleName = oAeCustomer.MiddleName
						, Lastname = oAeCustomer.LastName
						, Postfix = oAeCustomer.Postfix
						, Gender = oAeCustomer.Gender
						, PhoneHome = oAeCustomer.PhoneHome
						, PhoneWork = oAeCustomer.PhoneWork
						, PhoneCell = oAeCustomer.PhoneMobile
						, Email = oAeCustomer.Email
						, DOB = oAeCustomer.DOB
						, SSN = oAeCustomer.SSN
						, Username = oAeCustomer.Username
					};
				SessionCookie.SetSessionCookie(oSosCustomer, true, System.Web.HttpContext.Current);

				/** Add RememberMe Cookie. */
				if (userAuthInfo.RememberMe) SessionCookie.SetRememberMeCookie(oSosCustomer, System.Web.HttpContext.Current);

				oResult.Code = (int)SosResultCodes.Success;
				oResult.SessionId = userAuthInfo.SessionID;
				oResult.Message = "Success";
				oResult.Value = oAeCustomer;

			}
			#endregion TRY
			#region CATCH
			catch (Exception oEx)
			{
				oResult = new SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer>((int)SosResultCodes.GeneralError
				, string.Format("The following exception was thrown:\r\n{0}"
							, oEx.Message)
				, typeof(SOS.Services.Interfaces.Models.CmsModels.AeCustomer).ToString());
			}
			#endregion CATCH
			#endregion Execute Authentication

			#region Return results

			return oResult;

			#endregion Return results
		}


		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer> CustomerUpdate(CustomerParam customerInfo)
		{
			/** Authenticate. */
			const string METHOD_NAME = "AeCustomerUpdate";
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{
				/** Inititalizing. */
				SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer> oResult;

				#region Parameter Validation

				var aCORSArg = new List<CORSArg>
			{
				new CORSArg(customerInfo.CustomerID, (customerInfo.CustomerID == 0), "<li>'CustomerID' can not be blank.</li>"),
				new CORSArg(customerInfo.LocalizationId, (string.IsNullOrEmpty(customerInfo.LocalizationId)), "<li>'LocalizationId' can not be blank.</li>"),
				new CORSArg(customerInfo.FirstName, (string.IsNullOrEmpty(customerInfo.FirstName)), "<li>'FirstName' can not be blank.</li>"),
				new CORSArg(customerInfo.LastName, (string.IsNullOrEmpty(customerInfo.LastName)), "<li>'LastName' can not be blank.</li>"),
				new CORSArg(customerInfo.PhoneHome, (string.IsNullOrEmpty(customerInfo.PhoneHome)
					&& string.IsNullOrEmpty(customerInfo.PhoneWork)
					&& string.IsNullOrEmpty(customerInfo.PhoneMobile)), "<li>'Phone Numbers' can not be empty.  Either the home, work, or mobile must be present.</li>")
			};
				if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;

				#endregion Parameter Validation

				#region Execute Authentication
				#region TRY
				/** Execute authentication. */
				try
				{
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
					var customerInfoArg = new FnsAeCustomerGpsClientsViewModel
						{
							CustomerID = customerInfo.CustomerID,
							LocalizationId = customerInfo.LocalizationId,
							Prefix = customerInfo.Prefix,
							FirstName = customerInfo.FirstName,
							MiddleName = customerInfo.MiddleName,
							LastName = customerInfo.LastName,
							Postfix = customerInfo.PostFix,
							PhoneHome = customerInfo.PhoneHome,
							PhoneWork = customerInfo.PhoneWork,
							PhoneMobile = customerInfo.PhoneMobile
						};
					IFnsResult<IFnsAeCustomerGpsClientsViewModel> oFnsModel = oService.AeCustomerUpdate(customerInfoArg, oUser.Username);

					/** Check corsResult. */
					if (oFnsModel.Code != 0)
					{
						SessionCookie.DestroySessionCookie(System.Web.HttpContext.Current);
						oResult.Code = oFnsModel.Code;
						oResult.Message = oFnsModel.Message;
						return oResult;
					}

					/** Setup return corsResult. */
					var oAeCustomer = ConvertTo.CastFnsToAeCustomer((IFnsAeCustomerGpsClientsViewModel)oFnsModel.GetValue());

					/** Save session cookie. */
					var oSosCustomer = new SosCustomer
						{
							CustomerID = oAeCustomer.CustomerID
							,
							SessionID = oUser.SessionID
							,
							CustomerMasterFileId = oAeCustomer.CustomerMasterFileId
							,
							CustomerTypeId = oAeCustomer.CustomerTypeId
							,
							DealerId = oAeCustomer.DealerId
							,
							LocalizationId = oAeCustomer.LocalizationId
							,
							Prefix = oAeCustomer.Prefix
							,
							Firstname = oAeCustomer.FirstName
							,
							MiddleName = oAeCustomer.MiddleName
							,
							Lastname = oAeCustomer.LastName
							,
							Postfix = oAeCustomer.Postfix
							,
							Gender = oAeCustomer.Gender
							,
							PhoneHome = oAeCustomer.PhoneHome
							,
							PhoneWork = oAeCustomer.PhoneWork
							,
							PhoneCell = oAeCustomer.PhoneMobile
							,
							Email = oAeCustomer.Email
							,
							DOB = oAeCustomer.DOB
							,
							SSN = oAeCustomer.SSN
							,
							Username = oAeCustomer.Username
						};
					SessionCookie.SetSessionCookie(oSosCustomer, true, System.Web.HttpContext.Current);

					/** Save success results. */
					oResult.Code = (int)SosResultCodes.Success;
					oResult.SessionId = oUser.SessionID;
					oResult.Message = "Success";
					oResult.Value = oAeCustomer;

				}
				#endregion TRY
				#region CATCH
				catch (Exception oEx)
				{
					oResult = new SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer>((int)SosResultCodes.GeneralError
					, string.Format("The following exception was thrown:\r\n{0}"
								, oEx.Message));
				}
				#endregion CATCH
				#endregion Execute Authentication

				#region Return results

				return oResult;

				#endregion Return results
			});
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<bool> SessionTerminate()
		{
			/** Initialize. */
			const string METHOD_NAME = "SessionTerminate";
			var oResult = new SosCORSResult<bool>((int)SosResultCodes.CookieInvalid
											  , "Validating Authentication Failed", "bool") { Value = false };

			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{
				#region Execute Try
				try
				{
					/** Initialize. */
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
					IFnsResult<IFnsAcSessionModel> oFnsResult = oService.TerminateSession(oUser.SessionID);

					/** Check corsResult. */
					oResult.Code = oFnsResult.Code;
					oResult.Message = oFnsResult.Message;
					oResult.Value = true;

				}
				#endregion Execute Try

				#region Execute Catch
				catch (Exception oEx)
				{
					oResult = new SosCORSResult<bool>((int)SosResultCodes.ExceptionThrown
						, string.Format("The following exception was thrown: {0}", oEx.Message), null);
				}
				#endregion Execute Catch

				#region Return Result

				/** Return corsResult. */
				return oResult;

				#endregion Return Result
			});
		}


		#region New Users CRUD

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<List<GpsClient>> UsersRead()
		{
			/** Initialize. */
			const string METHOD_NAME = "UsersRead";
			var oResult = new SosCORSResult<List<GpsClient>>((int)SosResultCodes.CookieInvalid, "Session has expired.") { Value = null };

			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{
				#region Execute Try
				try
				{
					/** Initialize. */
					var list = new List<GpsClient>();
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAuthenticationService>();
					IFnsResult<List<IFnsAeGpsClient>> oFnsResult = oService.GpsClientRead(oUser.CustomerMasterFileId, oUser.CustomerID);

					/** Check result. */
					oResult.Code = oFnsResult.Code;
					oResult.Message = oFnsResult.Message;
					if (oFnsResult.GetValue() != null)
					{
						list.AddRange(from gpsClient in (List<IFnsAeGpsClient>)oFnsResult.GetValue()
									  select new GpsClient
										  {
											  CustomerID = gpsClient.CustomerID,
											  IsCurrent = gpsClient.IsCurrent,
											  CustomerMasterFileId = gpsClient.CustomerMasterFileId,
											  CustomerTypeId = gpsClient.CustomerTypeId,
											  CustomerTypeUi = gpsClient.CustomerTypeUi,
											  DealerId = gpsClient.DealerId,
											  DealerName = gpsClient.DealerName,
											  AddressId = gpsClient.AddressId,
											  LeadId = gpsClient.LeadId,
											  LocalizationId = gpsClient.LocalizationId,
											  LocalizationName = gpsClient.LocalizationName,
											  Prefix = gpsClient.Prefix,
											  FirstName = gpsClient.FirstName,
											  MiddleName = gpsClient.MiddleName,
											  LastName = gpsClient.LastName,
											  Postfix = gpsClient.Postfix,
											  Gender = gpsClient.Gender,
											  PhoneHome = gpsClient.PhoneHome,
											  PhoneMobile = gpsClient.PhoneMobile,
											  PhoneWork = gpsClient.PhoneWork,
											  Email = gpsClient.Email,
											  DOB = gpsClient.DOB,
											  SSN = gpsClient.SSN,
											  Username = gpsClient.Username,
											  Password = gpsClient.Password,
											  LastLoginOn = gpsClient.LastLoginOn,
											  IsActive = gpsClient.IsActive,
											  IsDeleted = gpsClient.IsDeleted,
											  ModifiedOn = gpsClient.ModifiedOn,
											  ModifiedBy = gpsClient.ModifiedBy,
											  CreatedOn = gpsClient.CreatedOn,
											  CreatedBy = gpsClient.CreatedBy,
											  DexRowTs = gpsClient.DexRowTs
										  });
						oResult.Value = list;
					}
				}
				#endregion Execute Try

				#region Execute Catch
				catch (Exception oEx)
				{
					oResult.Code = (int)SosResultCodes.ExceptionThrown;
					oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
				}
				#endregion Execute Catch

				#region Return Result

				return oResult;

				#endregion Return Result

			});
		}

		[System.Web.Mvc.HttpPost]
		[System.Web.Mvc.HttpOptions]
		public SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer> UserSignUp(CustomerParam customerInfo)
		{
			/** Initialize. */
			const string METHOD_NAME = "UserSignup";
			// ReSharper disable RedundantAssignment
			var oResult = new SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer>((int)SosResultCodes.Initializing, string.Format("Initializing method '{0}'.", METHOD_NAME)) { Value = null };
			// ReSharper restore RedundantAssignment

			#region Parameter Validation

			var aCORSArg = new List<CORSArg>
			{
				new CORSArg(customerInfo.LocalizationId, (string.IsNullOrEmpty(customerInfo.LocalizationId)), "<li>'LocalizationId' can not be blank.</li>"),
				new CORSArg(customerInfo.FirstName, (string.IsNullOrEmpty(customerInfo.FirstName)), "<li>'FirstName' can not be blank.</li>"),
				new CORSArg(customerInfo.LastName, (string.IsNullOrEmpty(customerInfo.LastName)), "<li>'LastName' can not be blank.</li>"),
				new CORSArg(customerInfo.PhoneHome, (string.IsNullOrEmpty(customerInfo.PhoneHome)
					&& string.IsNullOrEmpty(customerInfo.PhoneWork)
					&& string.IsNullOrEmpty(customerInfo.PhoneMobile)), "<li>'Phone Numbers' can not be empty.  Either the home, work, or mobile must be present.</li>"),
				new CORSArg(customerInfo.Email, (string.IsNullOrEmpty(customerInfo.Email)), "<li>'Email' can not be blank.</li>"),
				new CORSArg(customerInfo.Password, (string.IsNullOrEmpty(customerInfo.Password)), "<li>'Password' can not be blank.</li>"),
				new CORSArg(customerInfo.LeadSourceId, (customerInfo.LeadSourceId == 0), "<li>'Lead Source' is missing.</li>"),
				new CORSArg(customerInfo.LeadDispositionId, (customerInfo.LeadDispositionId == 0), "<li>'Lead Disposition' is missing.</li>"),
				new CORSArg(customerInfo.Gender, (string.IsNullOrEmpty(customerInfo.Gender)), "<li>'Gender' was not set.</li>")
			};
			if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;

			#endregion Parameter Validation

			#region Execute Try

			try
			{
				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				var customerInfoArg = new FnsAeCustomerGpsClientsViewModel
				{
					DealerId = customerInfo.DealerId,
					SalesRepId = customerInfo.SalesRepId,
					LocalizationId = customerInfo.LocalizationId,
					LeadSourceId = customerInfo.LeadSourceId,
					LeadDispositionId = customerInfo.LeadDispositionId,
					Gender = customerInfo.Gender,
					Prefix = customerInfo.Prefix,
					FirstName = customerInfo.FirstName,
					MiddleName = customerInfo.MiddleName,
					LastName = customerInfo.LastName,
					Postfix = customerInfo.PostFix,
					PhoneHome = customerInfo.PhoneHome,
					PhoneWork = customerInfo.PhoneWork,
					PhoneMobile = customerInfo.PhoneMobile,
					Email = customerInfo.Email
				};
				IFnsResult<IFnsAeCustomerGpsClientsViewModel> oFnsModel = oService.CustomerSignup(customerInfoArg);

				/** Check corsResult. */
				if (oFnsModel.Code != 0)
				{
					SessionCookie.DestroySessionCookie(System.Web.HttpContext.Current);
					oResult.Code = oFnsModel.Code;
					oResult.Message = oFnsModel.Message;
					return oResult;
				}

				/** Setup return corsResult. */
				var oAeCustomer = ConvertTo.CastFnsToAeCustomer((IFnsAeCustomerGpsClientsViewModel)oFnsModel.GetValue());

				/** Save session cookie. */
				var oSosCustomer = new SosCustomer
					{
						CustomerID = oAeCustomer.CustomerID
						, SessionID = customerInfo.SessionID
						, CustomerMasterFileId = oAeCustomer.CustomerMasterFileId
						, CustomerTypeId = oAeCustomer.CustomerTypeId
						, DealerId = oAeCustomer.DealerId
						, LocalizationId = oAeCustomer.LocalizationId
						, Prefix = oAeCustomer.Prefix
						, Firstname = oAeCustomer.FirstName
						, MiddleName = oAeCustomer.MiddleName
						, Lastname = oAeCustomer.LastName
						, Postfix = oAeCustomer.Postfix
						, Gender = oAeCustomer.Gender
						, PhoneHome = oAeCustomer.PhoneHome
						, PhoneWork = oAeCustomer.PhoneWork
						, PhoneCell = oAeCustomer.PhoneMobile
						, Email = oAeCustomer.Email
						, DOB = oAeCustomer.DOB
						, SSN = oAeCustomer.SSN
						, Username = oAeCustomer.Username
					};
				SessionCookie.SetSessionCookie(oSosCustomer, true, System.Web.HttpContext.Current);

				/** Save success results. */
				oResult.Code = (int)SosResultCodes.Success;
				oResult.SessionId = customerInfo.SessionID;
				oResult.Message = "Success";
				oResult.Value = oAeCustomer;

			}

			#endregion Execute Try

			#region Execute Catch

			catch (Exception oEx)
			{
				oResult.Code = (int)SosResultCodes.ExceptionThrown;
				oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
			}

			#endregion Execute Catch

			#region Result

			return oResult;

			#endregion Result
		}

		public SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer> UserUpdate(CustomerParam customerInfo)
		{
			#region Initialize
			/** Initialize. */
			const string METHOD_NAME = "CustomerUpdate";
			// ReSharper disable RedundantAssignment
			var oResult = new SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer>((int)SosResultCodes.Initializing, string.Format("Initializing method '{0}'.", METHOD_NAME)) { Value = null };
			// ReSharper restore RedundantAssignment
			#endregion Initialize

			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{
				#region Parameter Validation

				var aCORSArg = new List<CORSArg>
				{
					new CORSArg(customerInfo.LocalizationId, (string.IsNullOrEmpty(customerInfo.LocalizationId)), "<li>'LocalizationId' can not be blank.</li>"),
					new CORSArg(customerInfo.FirstName, (string.IsNullOrEmpty(customerInfo.FirstName)), "<li>'FirstName' can not be blank.</li>"),
					new CORSArg(customerInfo.LastName, (string.IsNullOrEmpty(customerInfo.LastName)), "<li>'LastName' can not be blank.</li>"),
					new CORSArg(customerInfo.PhoneHome, (string.IsNullOrEmpty(customerInfo.PhoneHome)
						&& string.IsNullOrEmpty(customerInfo.PhoneWork)
						&& string.IsNullOrEmpty(customerInfo.PhoneMobile)), "<li>'Phone Numbers' can not be empty.  Either the home, work, or mobile must be present.</li>"),
					new CORSArg(customerInfo.Email, (string.IsNullOrEmpty(customerInfo.Email)), "<li>'Email' can not be blank.</li>"),
					new CORSArg(customerInfo.Password, (string.IsNullOrEmpty(customerInfo.Password)), "<li>'Password' can not be blank.</li>"),
					new CORSArg(customerInfo.LeadSourceId, (customerInfo.LeadSourceId == 0), "<li>'Lead Source' is missing.</li>"),
					new CORSArg(customerInfo.LeadDispositionId, (customerInfo.LeadDispositionId == 0), "<li>'Lead Disposition' is missing.</li>"),
					new CORSArg(customerInfo.Gender, (string.IsNullOrEmpty(customerInfo.Gender)), "<li>'Gender' was not set.</li>")
				};
				if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;

				#endregion Parameter Validation

				#region Execute Try

				try
				{
					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
					var customerInfoArg = new FnsAeCustomerGpsClientsViewModel
					{
						CustomerID = customerInfo.CustomerID,
						LocalizationId = customerInfo.LocalizationId,
						Gender = customerInfo.Gender,
						Prefix = customerInfo.Prefix,
						FirstName = customerInfo.FirstName,
						MiddleName = customerInfo.MiddleName,
						LastName = customerInfo.LastName,
						Postfix = customerInfo.PostFix,
						PhoneHome = customerInfo.PhoneHome,
						PhoneWork = customerInfo.PhoneWork,
						PhoneMobile = customerInfo.PhoneMobile,
						Email = customerInfo.Email,

					};
					IFnsResult<IFnsAeCustomerGpsClientsViewModel> oFnsModel = oService.CustomerSignup(customerInfoArg);



					/** Setup return corsResult. */
					var oAeCustomer = ConvertTo.CastFnsToAeCustomer((IFnsAeCustomerGpsClientsViewModel)oFnsModel.GetValue());

					/** Save success results. */
					oResult.Code = (int)SosResultCodes.Success;
					oResult.SessionId = customerInfo.SessionID;
					oResult.Message = "Success";
					oResult.Value = oAeCustomer;

				}

				#endregion Execute Try

				#region Execute Catch

				catch (Exception oEx)
				{
					oResult.Code = (int)SosResultCodes.ExceptionThrown;
					oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
				}

				#endregion Execute Catch

				#region Result

				return oResult;

				#endregion Result
			});
		}

		public SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer> UserRead(CustomerParam customerInfo)
		{
			#region Initialize

			const string METHOD_NAME = "CustomerRead";
			var oResult = new SosCORSResult<SOS.Services.Interfaces.Models.CmsModels.AeCustomer>((int)SosResultCodes.CookieInvalid, "Session has expired.") { Value = null };

			#endregion Initialize

			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, oUser =>
			{

				#region Try

				var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
				IFnsResult<IFnsAeCustomerGpsClientsViewModel> oFnsResult = oService.CustomerRead(customerInfo.CustomerID);

				if (oFnsResult.Code != (int)ErrorCodes.Success)
				{
					oResult.Code = oFnsResult.Code;
					oResult.Message = oFnsResult.Message;
					oResult.Value = null;
					return oResult;
				}

				/** Setup return corsResult. */
				var oAeCustomer = ConvertTo.CastFnsToAeCustomer((IFnsAeCustomerGpsClientsViewModel)oFnsResult.GetValue());

				oResult.Code = oFnsResult.Code;
				oResult.Message = oFnsResult.Message;
				oResult.Value = oAeCustomer;

				#endregion Try

				#region Return result

				return oResult;

				#endregion Return result
			});
		}
		
		public SosCORSResult<bool> UserDelete(CustomerParam customerInfo)
		{
			#region Initialize

			const string METHOD_NAME = "CustomerDelete";
			var oResult = new SosCORSResult<bool>((int)SosResultCodes.CookieInvalid, "Session has expired.") { Value = false };

			#endregion Initialize
			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, oUser =>
				{
					#region Execute Try

					try
					{
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IWiseCrmService>();
						IFnsResult<IFnsAeCustomerGpsClientsViewModel> oFnsResult = oService.CustomerDelete(customerInfo.CustomerID);

						oResult.Code = oFnsResult.Code;
						oResult.Message = oFnsResult.Message;
						oResult.Value = (oFnsResult.Code == (int)ErrorCodes.Success);
					}
					#endregion Execute Try

					#region Execute Catch
					catch (Exception oEx)
					{
						oResult.Code = (int)SosResultCodes.ExceptionThrown;
						oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
					}
					#endregion Execute Catch

					#region Return result

					return oResult;

					#endregion Return result
				});
		}

		#endregion New Users CRUD

		#endregion Public Members
	}
}