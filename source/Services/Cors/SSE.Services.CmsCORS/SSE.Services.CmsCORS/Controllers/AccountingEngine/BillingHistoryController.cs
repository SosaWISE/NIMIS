using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.AccountingEngine
{
	[RoutePrefix("AccountingEngineSrv")]
	public class BillingHistoryController : ApiController
    {

        // GET api/billinghistory/5
        //[Route("BillingHistory/{id}/AccountID")]
        //[HttpGet]
        //public CmsCORSResult<List<SaeBillingHistory>> GetByAccountID(long id)
        //{

        //    #region Initialize

        //    /** Initialize. */
        //    const string METHOD_NAME = "GetByAccountID";

        //    #endregion Initialize
			
        //    /** Authenticate session first. */
        //    return CORSSecurity.AuthenticationWrapper(METHOD_NAME
        //        , user =>
        //        {
        //            #region Parameter Validation

        //            var argArray = new List<CORSArg>
        //            {
        //                new CORSArg(id,
        //                    (id == 0), "<li>'AccountID' must be passed.</li>"),
        //            };
        //            CmsCORSResult<List<SaeBillingHistory>> result;
        //            if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

        //            #endregion Parameter Validation

        //            #region TRY

        //            try
        //            {
        //                // ** Create Service
        //                var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
        //                IFnsResult<List<IFnsSaeBillingHistory>> fnsResult = mcService.BillingHistoryGetByAccountID(id, user.GPEmployeeID);

        //                // ** Save result
        //                result.Code = fnsResult.Code;
        //                result.SessionId = user.SessionID;
        //                result.Message = fnsResult.Message;

        //                // ** Get Values
        //                var fnsResultValue = (List<IFnsSaeBillingHistory>)fnsResult.GetValue();
        //                if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
        //                {
        //                    var resultValue = (from fnsToken in fnsResultValue
        //                                       select new SaeBillingHistory
        //                                       {
        //                                           //BillingHistoryID = fnsToken.BillingHistoryID,
        //                                           CustomerMasterFileId = fnsToken.CustomerMasterFileId,
        //                                           BillingType = fnsToken.BillingType,
        //                                           BillingDate = fnsToken.BillingDate,
        //                                           BillingNumber = fnsToken.BillingNumber,
        //                                           BillingAmount = fnsToken.BillingAmount
        //                                           //AccountId = fnsToken.AccountId,
        //                                           //BillingDate = fnsToken.BillingDate,
        //                                           //InvoiceNumber = fnsToken.InvoiceNumber,
        //                                           //Amount = fnsToken.Amount,
        //                                           //PaymentDate = fnsToken.PaymentDate,
        //                                           //PaymentNumber = fnsToken.PaymentNumber,
        //                                           //PaymentAmount = fnsToken.PaymentAmount,
        //                                           //AmountRemain = fnsToken.AmountRemain

        //                                       }).ToList();

        //                    result.Value = resultValue;
        //                }
        //            }
        //            #endregion TRY

        //            #region CATCH

        //            catch (Exception ex)
        //            {
        //                result.Code = (int)CmsResultCodes.ExceptionThrown;
        //                result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
        //                    ex.Message);
        //            }

        //            #endregion CATCH

        //            #region Result

        //            return result;

        //            #endregion Result

        //        });
			
        //}
	
		// GET api/billinghistory/5
		[Route("BillingHistory/{id}/CMFID")]
		[HttpGet]
		public CmsCORSResult<List<SaeBillingHistory>> GetByCMFID(long id)
		{

			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "GetByCMFID";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>
					{
						new CORSArg(id,
							(id == 0), "<li>'CMFID' must be passed.</li>"),
					};
					CmsCORSResult<List<SaeBillingHistory>> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY

					try
					{
						// ** Create Service
						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsSaeBillingHistory>> fnsResult = mcService.BillingHistoryGetByCMFID(id, user.GPEmployeeID);

						// ** Save result
						result.Code = fnsResult.Code;
						result.SessionId = user.SessionID;
						result.Message = fnsResult.Message;

						// ** Get Values
						var fnsResultValue = (List<IFnsSaeBillingHistory>)fnsResult.GetValue();
						if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
						{
							var resultValue = (from fnsToken in fnsResultValue
											   select new SaeBillingHistory
											   {
                                                   //BillingHistoryID = fnsToken.BillingHistoryID,
                                                   CustomerMasterFileId = fnsToken.CustomerMasterFileId,
                                                   BillingType = fnsToken.BillingType,
                                                   BillingDate = fnsToken.BillingDate,
                                                   BillingNumber = fnsToken.BillingNumber,
                                                   BillingAmount = fnsToken.BillingAmount
                                                   //AccountId = fnsToken.AccountId,
                                                   //BillingDate = fnsToken.BillingDate,
                                                   //InvoiceNumber = fnsToken.InvoiceNumber,
                                                   //Amount = fnsToken.Amount,
                                                   //PaymentDate = fnsToken.PaymentDate,
                                                   //PaymentNumber = fnsToken.PaymentNumber,
                                                   //PaymentAmount = fnsToken.PaymentAmount,
                                                   //AmountRemain = fnsToken.AmountRemain
											   }).ToList();

							result.Value = resultValue;
						}
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
