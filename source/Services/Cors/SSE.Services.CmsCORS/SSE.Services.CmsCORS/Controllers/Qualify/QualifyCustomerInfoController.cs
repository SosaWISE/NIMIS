using System;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.Services.Interfaces.Models.QualifyLead;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.Qualify
{
	[RoutePrefix("QualifySrv")]
	public class QualifyCustomerInfoController : ApiController
    {
        // GET api/qualifycustomerinfo
		[Route("QualifyCustomerInfos/{id}/Lead")]
		[HttpGet]
		public CmsCORSResult<QlQualifyCustomerInfo> GetByLeadId(long id)
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "QlQualifyCustomerInfo Get";
			var result = new CmsCORSResult<QlQualifyCustomerInfo>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IQualifyLeadServices>();
						IFnsResult<IFnsQlQualifyCustomerInfo> oFnsModel = oService.QlQualifyCustomerInfoReadByLeadId(id, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oQlQualifyCustomerInfo = ConvertTo.CastFnsToQlQualifyCustomerInfo((IFnsQlQualifyCustomerInfo)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oQlQualifyCustomerInfo;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}

		[Route("QualifyCustomerInfos/{id}/Customer")]
		[HttpGet]
		public CmsCORSResult<QlQualifyCustomerInfo> GetByCustomerId(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "QlQualifyCustomerInfo Get";
			var result = new CmsCORSResult<QlQualifyCustomerInfo>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IQualifyLeadServices>();
						IFnsResult<IFnsQlQualifyCustomerInfo> oFnsModel = oService.QlQualifyCustomerInfoReadByCustomerId(id, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oQlQualifyCustomerInfo = ConvertTo.CastFnsToQlQualifyCustomerInfo((IFnsQlQualifyCustomerInfo)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oQlQualifyCustomerInfo;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}

		[Route("QualifyCustomerInfos/{id}/Account")]
		[HttpGet]
		public CmsCORSResult<QlQualifyCustomerInfo> GetByAccountId(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "QlQualifyCustomerInfo Get by AccountId";
			var result = new CmsCORSResult<QlQualifyCustomerInfo>((int)CmsResultCodes.Initializing
				, string.Format("Initializing {0}.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region TRY

					try
					{
						// ** Create Service
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IQualifyLeadServices>();
						IFnsResult<IFnsQlQualifyCustomerInfo> oFnsModel = oService.QlQualifyCustomerInfoReadByAccountId(id, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oQlQualifyCustomerInfo = ConvertTo.CastFnsToQlQualifyCustomerInfo((IFnsQlQualifyCustomerInfo)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oQlQualifyCustomerInfo;
					}
					#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int)CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}

	}
}
