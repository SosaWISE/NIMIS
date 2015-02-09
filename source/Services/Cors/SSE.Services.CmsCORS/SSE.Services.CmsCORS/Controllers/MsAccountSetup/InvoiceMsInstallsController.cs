using System;
using System.Collections.Generic;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv")]
	public class InvoiceMsInstallsController : ApiController
    {
		// GET InvoiceMsIsntalls/{id}/AccountID
		[Route("InvoiceMsIsntalls/{id}/AccountID")]
		[HttpGet]
		public CmsCORSResult<AeInvoiceMsInstallInfo> GetByAccountId(long id)
        {
			#region Initialization

			const string METHOD_NAME = "GetByAccountId";

			#endregion Initialization

			#region Execute

			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(id, (id == 0), "<li>'AccountId' Has to be passed.</li>"),
				};
				CmsCORSResult<AeInvoiceMsInstallInfo> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation
				
				#region TRY

				try
				{
					// ** Create Service
					var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
					IFnsResult<IFnsAeInvoiceMsInstallInfo> fnsResult = mcService.InvoiceMsIsntallsGetByAccountId(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var fnsResultValue = (IFnsAeInvoiceMsInstallInfo)fnsResult.GetValue();
					if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
					{
						var resultValue = new AeInvoiceMsInstallInfo
						{
							InvoiceID = fnsResultValue.InvoiceID,
							AccountId = fnsResultValue.AccountId,
							ActivationFeeItemId = fnsResultValue.ActivationFeeItemId,
							ActivationFeeActual = fnsResultValue.ActivationFeeActual,
							ActivationFee = fnsResultValue.ActivationFee,
							MonthlyMonitoringRateItemId = fnsResultValue.MonthlyMonitoringRateItemId,
							MonthlyMonitoringRateActual = fnsResultValue.MonthlyMonitoringRateActual,
							MonthlyMonitoringRate = fnsResultValue.MonthlyMonitoringRate,
							AlarmComPackageId = fnsResultValue.AlarmComPackageId,
							Over3Months = fnsResultValue.Over3Months,
							CellularTypeId = fnsResultValue.CellularTypeId,
							PanelTypeId=fnsResultValue.PanelTypeId,
							ContractTemplateId = fnsResultValue.ContractId
						};

						// ** Get header 
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
				// Return result
				return result;
				#endregion Result
			});

			#endregion Execute
		}

		// GET InvoiceMsIsntalls/{id}/InvoiceID
		[Route("InvoiceMsIsntalls/{id}/InvoiceID")]
		[HttpGet]
		public CmsCORSResult<AeInvoiceMsInstallInfo> GetByInvoiceID(long id)
		{
			#region Initialization

			const string METHOD_NAME = "GetByInvoiceID";

			#endregion Initialization

			#region Execute

			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(id, (id == 0), "<li>'InvoiceID' Has to be passed.</li>"),
				};
				CmsCORSResult<AeInvoiceMsInstallInfo> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
					IFnsResult<IFnsAeInvoiceMsInstallInfo> fnsResult = mcService.InvoiceMsIsntallsGetByInvoiceID(id, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var fnsResultValue = (IFnsAeInvoiceMsInstallInfo)fnsResult.GetValue();
					if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
					{
						var resultValue = new AeInvoiceMsInstallInfo
						{
							InvoiceID = fnsResultValue.InvoiceID,
							AccountId = fnsResultValue.AccountId,
							ActivationFeeItemId = fnsResultValue.ActivationFeeItemId,
							ActivationFeeActual = fnsResultValue.ActivationFeeActual,
							ActivationFee = fnsResultValue.ActivationFee,
							MonthlyMonitoringRateItemId = fnsResultValue.MonthlyMonitoringRateItemId,
							MonthlyMonitoringRateActual = fnsResultValue.MonthlyMonitoringRateActual,
							MonthlyMonitoringRate = fnsResultValue.MonthlyMonitoringRate
						};

						// ** Get header 
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
				// Return result
				return result;
				#endregion Result
			});

			#endregion Execute
		}
	}
}
