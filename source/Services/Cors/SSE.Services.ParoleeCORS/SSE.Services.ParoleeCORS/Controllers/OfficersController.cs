using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Parolee;
using SSE.Services.ParoleeCORS.Helpers;
using SSE.Services.ParoleeCORS.Models;

namespace SSE.Services.ParoleeCORS.Controllers
{
	public class OfficersController : ApiController
	{
		#region Officers CRUD

		#region Officer Create

		public ParoleeCORSResult<SosOfficer> OfficerCreate(CustomerParam officerInfo)
		{
			/** Initialize. */
			const string METHOD_NAME = "OfficerCreate";
			// ReSharper disable RedundantAssignment
			var oResult = new ParoleeCORSResult<SosOfficer>((int) SosResultCodes.Initializing,
				string.Format("Initializing method '{0}'.", METHOD_NAME)) {Value = null};
			// ReSharper restore RedundantAssignment

			#region Parameter Validation

			var aCORSArg = new List<CORSArg>
			{
				new CORSArg(officerInfo.OfficerID, (string.IsNullOrEmpty(officerInfo.OfficerID.ToString(CultureInfo.InvariantCulture))),
					"<li>'OfficerID' can not be blank.</li>"),
				new CORSArg(officerInfo.LocalizationId, (string.IsNullOrEmpty(officerInfo.LocalizationId)),
					"<li>'LocalizationId' can not be blank.</li>"),
				new CORSArg(officerInfo.UserName, (string.IsNullOrEmpty(officerInfo.UserName)),
					"<li>'UserName' can not be blank.</li>"),
				new CORSArg(officerInfo.Password, (string.IsNullOrEmpty(officerInfo.Password)),
					"<li>'Password' can not be blank.</li>"),
				new CORSArg(officerInfo.Title, (string.IsNullOrEmpty(officerInfo.Title)), "<li>'Title' can not be blank.</li>"),
				new CORSArg(officerInfo.Department, (string.IsNullOrEmpty(officerInfo.Department)),
					"<li>'Department' was not set.</li>"),
				new CORSArg(officerInfo.Salutation, (string.IsNullOrEmpty(officerInfo.Salutation)),
					"<li>'Salutation' can not be blank.</li>"),
				new CORSArg(officerInfo.FirstName, (string.IsNullOrEmpty(officerInfo.FirstName)),
					"<li>'FirstName' can not be blank.</li>"),
				new CORSArg(officerInfo.MiddleName, (string.IsNullOrEmpty(officerInfo.MiddleName)),
					"<li>'MiddleName' can not be blank.</li>"),
				new CORSArg(officerInfo.LastName, (string.IsNullOrEmpty(officerInfo.LastName)), "<li></li>"),
				new CORSArg(officerInfo.Suffix, (string.IsNullOrEmpty(officerInfo.Suffix)), "<li>'Suffix' can not be blank.</li>"),
				new CORSArg(officerInfo.OfficePhone, (string.IsNullOrEmpty(officerInfo.OfficePhone)),
					"<li>'OfficePhone' can not be blank.</li>"),
				new CORSArg(officerInfo.MobilePhone, (string.IsNullOrEmpty(officerInfo.MobilePhone)),
					"<li>'MobilePhone' was not set.</li>"),
				new CORSArg(officerInfo.HomePhone, (string.IsNullOrEmpty(officerInfo.HomePhone)),
					"<li>'HomePhone' can not be blank.</li>"),
				new CORSArg(officerInfo.Pager, (string.IsNullOrEmpty(officerInfo.Pager)), "<li>'Pager' can not be blank.</li>"),
				new CORSArg(officerInfo.Fax, (string.IsNullOrEmpty(officerInfo.Fax)), "<li>'Fax' can not be blank.</li>"),
				new CORSArg(officerInfo.Email1, (string.IsNullOrEmpty(officerInfo.Email1)), "<li>'Email1' can not be blank.</li>"),
				new CORSArg(officerInfo.Email2, (string.IsNullOrEmpty(officerInfo.Email2)),
					"<li>'OffenderPay' can not be blank.</li>"),
				new CORSArg(officerInfo.EmailPasswordReset, (string.IsNullOrEmpty(officerInfo.EmailPasswordReset)),
					"<li>'EmailPasswordReset' can not be blank.</li>"),
				new CORSArg(officerInfo.SmsGateway, (string.IsNullOrEmpty(officerInfo.SmsGateway)),
					"<li>'SmsGateway' can not be blank.</li>"),
				new CORSArg(officerInfo.SmsAddress, (string.IsNullOrEmpty(officerInfo.SmsAddress)),
					"<li>'SmsAddress' can not be blank.</li>"),
				new CORSArg(officerInfo.SessionTimeOut, (string.IsNullOrEmpty(officerInfo.SessionTimeOut)),
					"<li>'SessionTimeOut' can not be blank.</li>"),

			};
			if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;

			#endregion Parameter Validation

			#region Execute Try

			var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IParoleeEngineService>();
			var officerInfoArg = new FnsParoleeOfficer
			{

				OfficerID = officerInfo.OfficerID,
				LocalizationId = officerInfo.LocalizationId,
				UserName = officerInfo.UserName,
				Password = officerInfo.Password,
				Title = officerInfo.Title,
				Department = officerInfo.Department,
				Salutation = officerInfo.Salutation,
				FirstName = officerInfo.FirstName,
				MiddleName = officerInfo.MiddleName,
				LastName = officerInfo.LastName,
				Suffix = officerInfo.Suffix,
				OfficePhone = officerInfo.OfficePhone,
				MobilePhone = officerInfo.MobilePhone,
				HomePhone = officerInfo.HomePhone,
				Pager = officerInfo.Pager,
				Fax = officerInfo.Fax,
				Email1 = officerInfo.Email1,
				Email2 = officerInfo.Email2,
				EmailPasswordReset = officerInfo.EmailPasswordReset,
				SmsGateway = officerInfo.SmsGateway,
				SmsAddress = officerInfo.SmsAddress,
				SessionTimeOut = officerInfo.SessionTimeOut,


			};

			try
			{
				IFnsResult<IFnsParoleeOfficer> oFnsModel = oService.OfficerCreate(officerInfoArg);

				/** Check corsResult. */
				if (oFnsModel.Code != 0)
				{
					SessionCookie.DestroySessionCookie(System.Web.HttpContext.Current);
					oResult.Code = oFnsModel.Code;
					oResult.Message = oFnsModel.Message;
					return oResult;
				}

				/** Setup return corsResult. */
				var oMcOfficer = ConvertTo.CastFnsToMcOfficer((FnsParoleeOfficer)oFnsModel.GetValue());

				/** Save session cookie. */
				var oSosOfficer = new SosOfficer
				{

					OfficerID = oMcOfficer.OfficerID,
					LocalizationId = oMcOfficer.LocalizationId,
					UserName = oMcOfficer.UserName,
					Password = oMcOfficer.Password,
					Title = oMcOfficer.Title,
					Department = oMcOfficer.Department,
					Salutation = oMcOfficer.Salutation,
					FirstName = oMcOfficer.FirstName,
					MiddleName = oMcOfficer.MiddleName,
					LastName = oMcOfficer.LastName,
					Suffix = oMcOfficer.Suffix,
					OfficePhone = oMcOfficer.OfficePhone,
					MobilePhone = oMcOfficer.MobilePhone,
					HomePhone = oMcOfficer.HomePhone,
					Pager = oMcOfficer.Pager,
					Fax = oMcOfficer.Fax,
					Email1 = oMcOfficer.Email1,
					Email2 = oMcOfficer.Email2,
					EmailPasswordReset = oMcOfficer.EmailPasswordReset,
					SmsGateway = oMcOfficer.SmsGateway,
					SmsAddress = oMcOfficer.SmsAddress,
					SessionTimeOut = oMcOfficer.SessionTimeOut,

				};
				SessionCookie.SetSessionCookie(oSosOfficer, true, System.Web.HttpContext.Current);

				/** Save success results. */
				oResult.Code = (int) SosResultCodes.Success;
				oResult.SessionId = officerInfo.SessionID;
				oResult.Message = "Success";
				oResult.Value = oSosOfficer;
			}
				#endregion Execute Try

			#region Execute Catch

			catch (Exception oEx)
			{
				oResult.Code = (int) SosResultCodes.ExceptionThrown;
				oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
			}

			#endregion Execute Catch

			#region Return Result

			/** Return result. */
			return oResult;

			#endregion Return Result

		}

		#endregion Officers Create

		#region Officer Read

		public ParoleeCORSResult<SosOfficer> OfficerRead(CustomerParam officerInfo)
		{
			#region Initialize

			const string METHOD_NAME = "OfficerRead";
			var oResult = new ParoleeCORSResult<SosOfficer>((int)SosResultCodes.CookieInvalid, "Session has expired.") { Value = null };

			#endregion Initialize

			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, oOfficer =>
				{

					#region Try

					var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IParoleeEngineService>();
					IFnsResult<IFnsParoleeOfficer> oFnsResult = oService.OfficerRead(officerInfo.OfficerID);

					if (oFnsResult.Code != (int) ErrorCodes.Success)
					{
						oResult.Code = oFnsResult.Code;
						oResult.Message = oFnsResult.Message;
						oResult.Value = null;
						return oResult;
					}

					/** Setup return corsResult. */
					var oSosOfficer = ConvertTo.CastFnsToMcOfficer((IFnsParoleeOfficer)oFnsResult.GetValue());

					oResult.Code = oFnsResult.Code;
					oResult.Message = oFnsResult.Message;
					oResult.Value = oSosOfficer;

					#endregion Try

					#region Return result

					return oResult;

					#endregion Return result
				});
		}


		#endregion Officer Read

		#region Officer Update

		public ParoleeCORSResult<SosOfficer> OfficerUpdate(CustomerParam officerInfo)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Parolee Update";
			// ReSharper disable RedundantAssignment
			var oResult = new ParoleeCORSResult<SosOfficer>((int) SosResultCodes.Initializing,
				string.Format("Initializing method '{0}'.", METHOD_NAME)) {Value = null};
			// ReSharper restore RedundantAssignment

			#endregion Initialize

			/** Authenticate Session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, oOfficer =>
				{

					#region Parameter Validation

					var aCORSArg = new List<CORSArg>
					{
						new CORSArg(officerInfo.OfficerID, (string.IsNullOrEmpty(officerInfo.OfficerID.ToString(CultureInfo.InvariantCulture))),
							"<li>'OfficerID' can not be blank.</li>"),
						new CORSArg(officerInfo.LocalizationId, (string.IsNullOrEmpty(officerInfo.LocalizationId)),
							"<li>'LocalizationId' can not be blank.</li>"),
						new CORSArg(officerInfo.UserName, (string.IsNullOrEmpty(officerInfo.UserName)),
							"<li>'UserName' can not be blank.</li>"),
						new CORSArg(officerInfo.Password, (string.IsNullOrEmpty(officerInfo.Password)),
							"<li>'Password' can not be blank.</li>"),
						new CORSArg(officerInfo.Title, (string.IsNullOrEmpty(officerInfo.Title)), "<li>'Title' can not be blank.</li>"),
						new CORSArg(officerInfo.Department, (string.IsNullOrEmpty(officerInfo.Department)),
							"<li>'Department' was not set.</li>"),
						new CORSArg(officerInfo.Salutation, (string.IsNullOrEmpty(officerInfo.Salutation)),
							"<li>'Salutation' can not be blank.</li>"),
						new CORSArg(officerInfo.FirstName, (string.IsNullOrEmpty(officerInfo.FirstName)),
							"<li>'FirstName' can not be blank.</li>"),
						new CORSArg(officerInfo.MiddleName, (string.IsNullOrEmpty(officerInfo.MiddleName)),
							"<li>'MiddleName' can not be blank.</li>"),
						new CORSArg(officerInfo.LastName, (string.IsNullOrEmpty(officerInfo.LastName)), "<li></li>"),
						new CORSArg(officerInfo.Suffix, (string.IsNullOrEmpty(officerInfo.Suffix)),
							"<li>'Suffix' can not be blank.</li>"),
						new CORSArg(officerInfo.OfficePhone, (string.IsNullOrEmpty(officerInfo.OfficePhone)),
							"<li>'OfficePhone' can not be blank.</li>"),
						new CORSArg(officerInfo.MobilePhone, (string.IsNullOrEmpty(officerInfo.MobilePhone)),
							"<li>'MobilePhone' was not set.</li>"),
						new CORSArg(officerInfo.HomePhone, (string.IsNullOrEmpty(officerInfo.HomePhone)),
							"<li>'HomePhone' can not be blank.</li>"),
						new CORSArg(officerInfo.Pager, (string.IsNullOrEmpty(officerInfo.Pager)), "<li>'Pager' can not be blank.</li>"),
						new CORSArg(officerInfo.Fax, (string.IsNullOrEmpty(officerInfo.Fax)), "<li>'Fax' can not be blank.</li>"),
						new CORSArg(officerInfo.Email1, (string.IsNullOrEmpty(officerInfo.Email1)),
							"<li>'Email1' can not be blank.</li>"),
						new CORSArg(officerInfo.Email2, (string.IsNullOrEmpty(officerInfo.Email2)),
							"<li>'OffenderPay' can not be blank.</li>"),
						new CORSArg(officerInfo.EmailPasswordReset, (string.IsNullOrEmpty(officerInfo.EmailPasswordReset)),
							"<li>'EmailPasswordR	eset' can not be blank.</li>"),
						new CORSArg(officerInfo.SmsGateway, (string.IsNullOrEmpty(officerInfo.SmsGateway)),
							"<li>'SmsGateway' can not be blank.</li>"),
						new CORSArg(officerInfo.SmsAddress, (string.IsNullOrEmpty(officerInfo.SmsAddress)),
							"<li>'SmsAddress' can not be blank.</li>"),
						new CORSArg(officerInfo.SessionTimeOut, (string.IsNullOrEmpty(officerInfo.SessionTimeOut)),
							"<li>'SessionTimeOut' can not be blank.</li>"),
					};
					if (!CORSArg.ArgumentValidation(aCORSArg, out oResult)) return oResult;

					#endregion Parameter Validation

					#region Execute Try

					try
					{
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IParoleeEngineService>();
						var officerInfoArg = new FnsParoleeOfficer
						{
							OfficerID = officerInfo.OfficerID,
							LocalizationId = officerInfo.LocalizationId,
							UserName = officerInfo.UserName,
							Password = officerInfo.Password,
							Title = officerInfo.Title,
							Department = officerInfo.Department,
							Salutation = officerInfo.Salutation,
							FirstName = officerInfo.FirstName,
							MiddleName = officerInfo.MiddleName,
							LastName = officerInfo.LastName,
							Suffix = officerInfo.Suffix,
							OfficePhone = officerInfo.OfficePhone,
							MobilePhone = officerInfo.MobilePhone,
							HomePhone = officerInfo.HomePhone,
							Pager = officerInfo.Pager,
							Fax = officerInfo.Fax,
							Email1 = officerInfo.Email1,
							Email2 = officerInfo.Email2,
							EmailPasswordReset = officerInfo.EmailPasswordReset,
							SmsGateway = officerInfo.SmsGateway,
							SmsAddress = officerInfo.SmsAddress,
							SessionTimeOut = officerInfo.SessionTimeOut,

						};
						IFnsResult<IFnsParoleeOfficer> oFnsModel = oService.OfficerCreate(officerInfoArg);



						/** Setup return corsResult. */
						var oSosOfficer = ConvertTo.CastFnsToMcOfficer((FnsParoleeOfficer) oFnsModel.GetValue());

						/** Save success results. */
						oResult.Code = (int) SosResultCodes.Success;
						oResult.SessionId = officerInfo.SessionID;
						oResult.Message = "Success";
						oResult.Value = oSosOfficer;

					}

					#endregion Execute Try

					#region Execute Catch

					catch (Exception oEx)
					{
						oResult.Code = (int) SosResultCodes.ExceptionThrown;
						oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
					}

					#endregion Execute Catch

					#region Result

					return oResult;

					#endregion Result

				});
		}

		#endregion Officer Update

		#region Officer Delete

		public ParoleeCORSResult<bool> OfficerDelete(CustomerParam officerInfo)
			{
				#region Initialize

				const string METHOD_NAME = "OfficerDelete";
				var oResult = new ParoleeCORSResult<bool>((int) SosResultCodes.CookieInvalid, "Session has expired.") { Value = false };

				#endregion Initialize

				/** Authenticate Session first. */
				return CORSSecurity.AuthenticationWrapper(METHOD_NAME
					, oOfficer =>
					{
						#region Execute Try

						try
						{
							var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IParoleeEngineService>();
							IFnsResult<IFnsParoleeOfficer> oFnsResult =
								oService.OfficerDelete(officerInfo.OfficerID);

							oResult.Code = oFnsResult.Code;
							oResult.Message = oFnsResult.Message;
							oResult.Value = (oFnsResult.Code == (int) ErrorCodes.Success);
						}

							#endregion Execute Try

						#region Execute Catch

						catch (Exception oEx)
						{
							oResult.Code = (int) SosResultCodes.ExceptionThrown;
							oResult.Message = string.Format("The following exception was thrown: {0}", oEx.Message);
						}

						#endregion Execute Catch

						#region Return result

						return oResult;

						#endregion Return result

					});
			}


			#endregion Officer Delete

			#endregion Officer CRUD
		}
	}


	