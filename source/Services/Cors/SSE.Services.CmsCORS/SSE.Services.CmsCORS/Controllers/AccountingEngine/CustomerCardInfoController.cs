//using System;
//using System.Collections.Generic;
//using System.Web.Http;
//using SOS.FunctionalServices;
//using SOS.FunctionalServices.Contracts;
//using SOS.FunctionalServices.Contracts.Helper;
//using SOS.FunctionalServices.Contracts.Models;
//using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
//using SOS.Services.Interfaces.Models.AccountingEngine;
//using SSE.Services.CmsCORS.Helpers;
//using SSE.Services.CmsCORS.Models;

//namespace SSE.Services.CmsCORS.Controllers.AccountingEngine
//{
//	[RoutePrefix("AccountingEngineSrv")]
//	public class CustomerCardInfoController : ApiController
//	{
//		[Route("CustomerCardInfos/{id}")]
//		[HttpGet]
//		public CmsCORSResult<AeCustomerCardInfo> Get(long id)
//		{
//			#region Initialize

//			/** Initialize. */
//			const string METHOD_NAME = "Customer Card Info Get";

//			#endregion Initialize

//			/** Authenticate session first. */
//			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
//				, user =>
//				{
//					#region Parameter Validation

//					var argArray = new List<CORSArg>
//					{
//						new CORSArg(id, (id == 0), "<li>'Customer Number' must be passed.</li>"),
//					};
//					CmsCORSResult<AeCustomerCardInfo> result;
//					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

//					#endregion Parameter Validation

//					#region TRY

//					try
//					{
//						// ** Create Service
//						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
//						IFnsResult<IFnsAeCustomerCardInfo> fnsResult = mcService.CustomerCardInfoGet(id, user.GPEmployeeID);

//						// ** Save result
//						result.Code = fnsResult.Code;
//						result.SessionId = user.SessionID;
//						result.Message = fnsResult.Message;

//						if (result.Code == (int) ErrorCodes.Success)
//						{
//							var resultValue = ConvertTo.CastFnsToAeCustomerCardInfo((IFnsAeCustomerCardInfo) fnsResult.GetValue());
//							result.Value = resultValue;
//						}
//					}
//					#endregion TRY

//					#region CATCH

//					catch (Exception ex)
//					{
//						result.Code = (int)CmsResultCodes.ExceptionThrown;
//						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
//							ex.Message);
//					}

//					#endregion CATCH

//					#region Result

//					return result;

//					#endregion Result
//				});
//		}

//		[Route("CustomerCardInfos/{id}/Customer")]
//		[HttpGet]
//		public CmsCORSResult<AeCustomerCardInfo> GetCustomerByAccountId(long id)
//		{
//			#region Initialize

//			/** Initialize. */
//			const string METHOD_NAME = "Customer Card Info Get Customer by AccountId";

//			#endregion Initialize

//			/** Authenticate session first. */
//			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
//				, user =>
//				{
//					#region Parameter Validation

//					var argArray = new List<CORSArg>
//					{
//						new CORSArg(id, (id == 0), "<li>'Account ID' must be passed.</li>"),
//					};
//					CmsCORSResult<AeCustomerCardInfo> result;
//					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

//					#endregion Parameter Validation

//					#region TRY

//					try
//					{
//						// ** Create Service
//						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
//						IFnsResult<IFnsAeCustomerCardInfo> fnsResult = mcService.CustomerCardInfoGetByAccountId(id, user.GPEmployeeID);

//						// ** Save result
//						result.Code = fnsResult.Code;
//						result.SessionId = user.SessionID;
//						result.Message = fnsResult.Message;

//						if (result.Code == (int)ErrorCodes.Success)
//						{
//							var resultValue = ConvertTo.CastFnsToAeCustomerCardInfo((IFnsAeCustomerCardInfo)fnsResult.GetValue());
//							result.Value = resultValue;
//						}
//					}
//					#endregion TRY

//					#region CATCH

//					catch (Exception ex)
//					{
//						result.Code = (int)CmsResultCodes.ExceptionThrown;
//						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
//							ex.Message);
//					}

//					#endregion CATCH

//					#region Result

//					return result;

//					#endregion Result
//				});
//		}
//	}
//}