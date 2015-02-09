using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.Data.SosCrm;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.MonitoringStation;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.MsAccountSetup
{
	[RoutePrefix("MsAccountSetupSrv")]
    public class SalesSummaryController : ApiController
    {
		// GET MsAccountSetupSrv/PointSystems
		[Route("PointSystems")]
		[HttpGet]
		public CmsCORSResult<List<AeInvoiceTemplate>> PointSystems()
        {
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get PointSystems";
			var result = new CmsCORSResult<List<AeInvoiceTemplate>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsAeInvoiceTemplate>> oFnsModel = oService.PointSystemsGet(user.UserID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oAeInvoiceTemplateList = ConvertTo.CastFnsToAeInvoiceTemplateList((List<IFnsAeInvoiceTemplate>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oAeInvoiceTemplateList;
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

		[Route("ActivationFees")]
		[HttpGet]
		public CmsCORSResult<List<AeItem>> ActivationFees()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get ActivationFees";
			var result = new CmsCORSResult<List<AeItem>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsAeItem>> oFnsModel = oService.ActivationFeesGet(user.UserID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oAeItemList = ConvertTo.CastFnsToAeItemList((List<IFnsAeItem>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oAeItemList;
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

		[Route("CellularTypes")]
		[HttpGet]
		public CmsCORSResult<List<MsAccountCellularType>> CellularTypes()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get CellularTypes";
			var result = new CmsCORSResult<List<MsAccountCellularType>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsMsAccountCellularType>> oFnsModel = oService.CellularTypesGet(user.UserID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oMsCellularTypeList = ConvertTo.CastFnsToMsCellularTypeList((List<IFnsMsAccountCellularType>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oMsCellularTypeList;
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

		[Route("VendorAlarmComPacakges")]
		[HttpGet]
		public CmsCORSResult<List<MsVendorAlarmComPackage>> VendorAlarmComPacakges()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get CellularTypes";
			var result = new CmsCORSResult<List<MsVendorAlarmComPackage>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsMsVendorAlarmComPackage>> oFnsModel = oService.VendorAlarmComPackagesGet(user.UserID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oMsCellularTypeList = ConvertTo.CastFnsToMsVendorAlarmComPackageList((List<IFnsMsVendorAlarmComPackage>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oMsCellularTypeList;
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

		[Route("EquipmentByPointsGet/{id}")]
		[HttpGet]
		public CmsCORSResult<List<AeItem>> EquipmentByPointsGet(long id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Equipment Type";
			var result = new CmsCORSResult<List<AeItem>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsAeItem>> oFnsModel = oService.EquipmentByPointsGet(id, user.UserID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oAeItemList = ConvertTo.CastFnsToAeItemList((List<IFnsAeItem>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int) CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oAeItemList;
					}
						#endregion TRY

					#region CATCH

					catch (Exception ex)
					{
						result.Code = (int) CmsResultCodes.ExceptionThrown;
						result.Message = string.Format("The following exception was thrown from '{0}' method: {1}", METHOD_NAME,
							ex.Message);
					}

					#endregion CATCH

					#region Result

					return result;

					#endregion Result
				});
		}

		[Route("ContractLengthsGet/{id}")]
		[HttpGet]
		public CmsCORSResult<List<AE_ContractTemplate>> ContractLengthsGet(int id)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Equipment Type";
			var result = new CmsCORSResult<List<AE_ContractTemplate>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsAeContractTemplate>> oFnsModel = oService.ContractLengthsGet(id, user.UserID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oAeContractTemplateList = ConvertTo.CastFnsToAeContractTemplateList((List<IFnsAeContractTemplate>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int) CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oAeContractTemplateList;
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

		[Route("FrequentlyInstalledEquipmentGet")]
		[HttpGet]
		public CmsCORSResult<List<AeItem>> FrequentlyInstalledEquipmentGet()
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "FrequentlyInstalledEquipmentGet";
			var result = new CmsCORSResult<List<AeItem>>((int)CmsResultCodes.Initializing
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
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
						IFnsResult<List<IFnsMsEquipmentsView>> oFnsModel = oService.FrequentlyInstalledEquipmentGet(user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}

						/** Setup return corsResult. */
						var oAeItemList = ConvertTo.CastFnsToAeItemList((List<IFnsMsEquipmentsView>)oFnsModel.GetValue());


						/** Save success results. */
						result.Code = (int)CmsResultCodes.Success;
						result.SessionId = user.SessionID;
						result.Message = "Success";
						result.Value = oAeItemList;
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

		[Route("InvoiceRefresh")]
		[HttpPost]
		public CmsCORSResult<AeInvoice> InvoiceRefresh([FromBody] SalesSummaryProperties props)
		{
			#region Initialization

			const string METHOD_NAME = "InvoiceRefresh";

			#endregion Initialization

			#region Execute

			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(props.AccountId, (props.AccountId == 0), "<li>'AccountId' Has to be passed.</li>"),
					new CORSArg(props.ActivationFeeItemId, (string.IsNullOrEmpty(props.ActivationFeeItemId)), "<li>'ActivationFeeItemId' Has to be passed.</li>"),
					new CORSArg(props.ActivationFeeActual, (props.ActivationFeeActual < 0), "<li>'ActivationFeeActual' Can not be less than zero.</li>"),
					new CORSArg(props.MonthlyMonitoringRateItemId, (string.IsNullOrEmpty(props.MonthlyMonitoringRateItemId)), "<li>'MonthlyMonitoringRateItemId' Has to be passed.</li>"),
					new CORSArg(props.MonthlyMonitoringRateActual, (props.MonthlyMonitoringRateActual <= 0), "<li>'MonthlyMonitoringRateActual' Has to be passed, and can not be zero or less than zero.</li>"),
				};
				CmsCORSResult<AeInvoice> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{

					// ** Create Service
					var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();

					// ** Create props argument
					var fnsProps = new FnsSalesSummaryProperties(props.InvoiceID, props.AccountId, props.BillingDay, props.CurrentMonitoringStation,
						props.ActivationFeeItemId, props.ActivationFeeActual, props.MonthlyMonitoringRateItemId, props.MonthlyMonitoringRateActual,
						props.AlarmComPackageId, props.PanelTypeId, props.CellTypeId, props.CellPackageItemId, props.CellServicePackage, props.Over3Months,
						props.Email, props.PaymentTypeId, props.IsTakeover, props.IsOwner, props.IsMoni, props.ContractTemplateId, props.ContractId, props.ContractLength);
					IFnsResult<IFnsAeInvoice> fnsResult = mcService.RefreshMsAccountInstall(fnsProps, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var fnsResultValue = (IFnsAeInvoice)fnsResult.GetValue();
					if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
					{
						var itemsList = fnsResultValue.Items.Select(item => new AeInvoiceItem
						{
							InvoiceItemID = item.InvoiceItemID,
							InvoiceId = item.InvoiceId,
							ItemId = item.ItemId,
							ItemDesc = item.ItemDesc,
							TaxOptionId = item.TaxOptionId,
							Qty = item.Qty,
							Cost = item.Cost,
							RetailPrice = item.RetailPrice,
							PriceWithTax = item.PriceWithTax,
							SystemPoints = item.SystemPoints
						}).ToList();

						// ** Get SalesInformation
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<IFnsMsAccountSalesInformation> oFnsModel = oService.SalesInformationRead(props.AccountId, user.GPEmployeeID);
						/** Check corsResult. */
						if (oFnsModel.Code != 0)
						{
							result.Code = oFnsModel.Code;
							result.Message = oFnsModel.Message;
							return result;
						}
						/** Setup return corsResult. */
						var msAccountSalesInformation = ConvertTo.CastFnsToMsAccountSalesInformation((IFnsMsAccountSalesInformation)oFnsModel.GetValue());

						var resultValue = new AeInvoice(new AeInvoiceHeader
						{
							InvoiceID = fnsResultValue.Header.InvoiceID,
							AccountId = fnsResultValue.Header.AccountId,
							InvoiceTypeId = fnsResultValue.Header.InvoiceTypeId,
							ContractId = fnsResultValue.Header.ContractId,
							TaxScheduleId = fnsResultValue.Header.TaxScheduleId,
							PaymentTermId = fnsResultValue.Header.PaymentTermId,
							DocDate = fnsResultValue.Header.DocDate,
							PostedDate = fnsResultValue.Header.PostedDate,
							DueDate = fnsResultValue.Header.DueDate,
							GLPostDate = fnsResultValue.Header.GLPostDate,
							CurrentTransactionAmount = fnsResultValue.Header.CurrentTransactionAmount,
							SalesAmount = fnsResultValue.Header.SalesAmount,
							OriginalTransactionAmount = fnsResultValue.Header.OriginalTransactionAmount,
							CostAmount = fnsResultValue.Header.CostAmount,
							TaxAmount = fnsResultValue.Header.TaxAmount
						}, itemsList, msAccountSalesInformation);

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
