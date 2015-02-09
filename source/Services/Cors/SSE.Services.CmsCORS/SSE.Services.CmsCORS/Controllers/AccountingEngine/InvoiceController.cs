using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SOS.FunctionalServices;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Models.AccountingEngine;
using SOS.Services.Interfaces.Models.AccountingEngine;
using SSE.Services.CmsCORS.Helpers;
using SSE.Services.CmsCORS.Models;

namespace SSE.Services.CmsCORS.Controllers.AccountingEngine
{
	[RoutePrefix("InvoiceSrv")]
	public class InvoiceController : ApiController
	{
		// GET InvoiceSrv/invoice/5
		[Route("Invoices/{id}")]
		[HttpGet]
		public CmsCORSResult<AeInvoice> Get(long id)
		{
			#region Initialization

			const string METHOD_NAME = "Get Invloice";

			#endregion Initialization

			#region Execute

			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(id, (id == 0), "<li>'id' Has to be passed.</li>")
				};
				CmsCORSResult<AeInvoice> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
					IFnsResult<IFnsAeInvoice> fnsResult = mcService.InvoiceGet(id, user.GPEmployeeID);

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
							ItemSKU = item.ItemSKU,
							ItemDesc = item.ItemDesc,
							TaxOptionId = item.TaxOptionId,
							Qty = item.Qty,
							Cost = item.Cost,
							RetailPrice = item.RetailPrice,
							PriceWithTax = item.PriceWithTax,
							SystemPoints = item.SystemPoints,
							SalesmanID = item.SalesmanID,
							TechnicianID = item.TechnicianID,
							ModifiedOn = item.ModifiedOn,
							ModifiedBy = item.ModifiedBy,
							CreatedOn = item.CreatedOn,
							CreatedBy = item.CreatedBy,
						}).ToList();

						// ** Get SalesInformation
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<IFnsMsAccountSalesInformation> oFnsModel = oService.SalesInformationRead(fnsResultValue.Header.AccountId, user.GPEmployeeID);
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

		// POST InvoiceSrv/invoice
		[Route("Invoices/")]
		[HttpPost]
		public CmsCORSResult<AeInvoiceHeader> Post([FromBody]AeInvoiceHeader value)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "Get Department";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (value == null)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
				}
				else
				{
					argArray.Add(new CORSArg(value.AccountId, (value.AccountId == 0), "<li>'AccountId' was not passed.</li>"));
					argArray.Add(new CORSArg(value.InvoiceTypeId, (string.IsNullOrEmpty(value.InvoiceTypeId)), "<li>'InvoiceTypeId' was not passed.</li>"));
				}
				CmsCORSResult<AeInvoiceHeader> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();

					// ** Prepare arguents
					// ReSharper disable once PossibleNullReferenceException
					var fnsHeader = new FnsAeInvoiceHeader(value.AccountId, value.InvoiceTypeId);
					IFnsResult<IFnsAeInvoiceHeader> fnsResult = mcService.InvoiceCreate(fnsHeader, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var fnsResultValue = (IFnsAeInvoiceHeader)fnsResult.GetValue();
					if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
					{
						var resultValue = new AeInvoiceHeader
						{
							InvoiceID = fnsResultValue.InvoiceID,
							AccountId = fnsResultValue.AccountId,
							InvoiceTypeId = fnsResultValue.InvoiceTypeId,
							ContractId = fnsResultValue.ContractId,
							TaxScheduleId = fnsResultValue.TaxScheduleId,
							PaymentTermId = fnsResultValue.PaymentTermId,
							DocDate = fnsResultValue.DocDate,
							PostedDate = fnsResultValue.PostedDate,
							DueDate = fnsResultValue.DueDate,
							GLPostDate = fnsResultValue.GLPostDate,
							CurrentTransactionAmount = fnsResultValue.CurrentTransactionAmount,
							SalesAmount = fnsResultValue.SalesAmount,
							OriginalTransactionAmount = fnsResultValue.OriginalTransactionAmount,
							CostAmount = fnsResultValue.CostAmount,
							TaxAmount = fnsResultValue.TaxAmount
						};
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

		// POST InvoiceSrv/InvoiceItem
		[Route("InvoiceItems/{invoiceItemId}")]
		[HttpDelete]
		public CmsCORSResult<bool> InvoiceItemDelete(long invoiceItemId)
		{
			#region Initialization

			const string METHOD_NAME = "InvoiceItemDelete";

			#endregion Initialization

			#region Execute

			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>
				{
					new CORSArg(invoiceItemId, (invoiceItemId == 0), "<li>'invoiceItemId' Has to be passed.</li>")
				};
				CmsCORSResult<bool> result;
				if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
					IFnsResult fnsResult = mcService.InvoiceItemDelete(invoiceItemId, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;
					result.Value = true;
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

		// POST InvoiceSrv/InvoiceItem
		[Route("InvoiceItems/")]
		[HttpPost]
		public CmsCORSResult<AeInvoiceItem> InvoiceItemCreate([FromBody] AeInvoiceItem value)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "InvoiceItemCreate";
			var result = new CmsCORSResult<AeInvoiceItem>((int) ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
			, user =>
			{
				#region Parameter Validation

				var argArray = new List<CORSArg>();
				if (value == null)
				{
					argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
				}
				else
				{
					argArray.Add(new CORSArg(value.ItemId, (string.IsNullOrEmpty(value.ItemId)), "<li>'InvoiceItemID' was not passed.</li>"));
					argArray.Add(new CORSArg(value.InvoiceId, (value.InvoiceId == 0), "<li>'InvoiceId' was not passed.</li>"));
					argArray.Add(new CORSArg(value.Qty, (value.Qty == 0), "<li>'Qty' was not passed.</li>"));
					argArray.Add(new CORSArg(value.SalesmanID, (string.IsNullOrEmpty(value.SalesmanID) && string.IsNullOrEmpty(value.TechnicianID)), "<li>'SalesmanId' or 'TechnicianID' must be passed.</li>"));

					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;
				}

				#endregion Parameter Validation

				#region TRY

				try
				{
					// ** Create Service
					var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();

					// ** Prepare arguents
					// ReSharper disable once PossibleNullReferenceException
					var fnsAeInvoiceItem = new FnsAeInvoiceItemView(value.InvoiceId, value.ItemId, value.Qty, value.SalesmanID, value.TechnicianID);
					IFnsResult<IFnsAeInvoiceItemView> fnsResult = mcService.InvoiceItemCreate(fnsAeInvoiceItem, user.GPEmployeeID);

					// ** Save result
					result.Code = fnsResult.Code;
					result.SessionId = user.SessionID;
					result.Message = fnsResult.Message;

					// ** Get Values
					var fnsResultValue = (IFnsAeInvoiceItemView) fnsResult.GetValue();
					if (result.Code == (int) CmsResultCodes.Success && fnsResultValue != null)
					{
						var resultValue = new AeInvoiceItem
						{
							InvoiceItemID = fnsResultValue.InvoiceItemID,
							InvoiceId = fnsResultValue.InvoiceId,
							ItemId = fnsResultValue.ItemId,
							ItemSKU = fnsResultValue.ItemSKU,
							ItemDesc = fnsResultValue.ItemDesc,
							TaxOptionId = fnsResultValue.TaxOptionId,
							Qty = fnsResultValue.Qty,
							Cost = fnsResultValue.Cost,
							RetailPrice = fnsResultValue.RetailPrice,
							PriceWithTax = fnsResultValue.PriceWithTax,
							SystemPoints = fnsResultValue.SystemPoints,
							SalesmanID = fnsResultValue.SalesmanID,
							TechnicianID = fnsResultValue.TechnicianID,
							ModifiedBy = fnsResultValue.ModifiedBy,
							ModifiedOn = fnsResultValue.ModifiedOn,
							CreatedBy = fnsResultValue.CreatedBy,
							CreatedOn = fnsResultValue.CreatedOn

						};
						result.Value = resultValue;
					}
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

		// POST InvoiceSrv/AddByPartNumber
		[Route("InvoiceItems/{invoiceId}/AddByPartNumber")]
		[HttpPost]
		public CmsCORSResult<AeInvoice> AddByPartNumber(long invoiceId, [FromBody] ArgsAddBySku value)
		{
			#region Initialize

			/** Initialize. */
			const string METHOD_NAME = "AddByPartNumber";

			#endregion Initialize

			/** Authenticate session first. */
			return CORSSecurity.AuthenticationWrapper(METHOD_NAME
				, user =>
				{
					#region Parameter Validation

					var argArray = new List<CORSArg>();
					if (value == null)
					{
						argArray.Add(new CORSArg(null, (true), "<li>No values were passed.</li>"));
					}
					else
					{
						argArray.Add(new CORSArg(invoiceId, (invoiceId == 0), "<li>'InvoiceID' was not passed.</li>"));
						argArray.Add(new CORSArg(value.ItemSku, (string.IsNullOrEmpty(value.ItemSku)), "<li>'ItemSku' was not passed.</li>"));
						argArray.Add(new CORSArg(value.Qty, (value.Qty == 0), "<li>'Qty' was not passed.</li>"));
						argArray.Add(new CORSArg(value.SalesmanID, (string.IsNullOrEmpty(value.SalesmanID) && string.IsNullOrEmpty(value.TechnicianID)), "<li>'SalesmanId' or 'TechnicianID' must be passed.</li>"));
					}

					CmsCORSResult<AeInvoice> result;
					if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

					#endregion Parameter Validation

					#region TRY

					try
					{
						// ** Create Service
						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();

						// ** Prepare arguents
						// ReSharper disable once PossibleNullReferenceException
						IFnsResult<IFnsAeInvoice> fnsResult = mcService.InvoiceAddByPartNumber(invoiceId, value.ItemSku, value.Qty, value.SalesmanID, value.TechnicianID, user.GPEmployeeID);

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
								ItemSKU = item.ItemSKU,
								ItemDesc = item.ItemDesc,
								TaxOptionId = item.TaxOptionId,
								Qty = item.Qty,
								Cost = item.Cost,
								RetailPrice = item.RetailPrice,
								PriceWithTax = item.PriceWithTax,
								SystemPoints = item.SystemPoints,
								SalesmanID = item.SalesmanID,
								TechnicianID = item.TechnicianID,
								ModifiedOn = item.ModifiedOn,
								ModifiedBy = item.ModifiedBy,
								CreatedOn = item.CreatedOn,
								CreatedBy = item.CreatedBy
							}).ToList();

							// ** Get SalesInformation
							var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
							IFnsResult<IFnsMsAccountSalesInformation> oFnsModel = oService.SalesInformationRead(fnsResultValue.Header.AccountId, user.GPEmployeeID);
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

		[Route("InvoiceItems/{invoiceId}/AddByBarcode")]
		[HttpPost]
		public CmsCORSResult<AeInvoice> AddByBarcode(long invoiceId, [FromBody] ArgsAddBySku value)
	    {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "AddByBarcode";

            #endregion Initialize

            /** Authenticate session first. */
	        return CORSSecurity.AuthenticationWrapper(METHOD_NAME
	        , user =>
	        {
                #region Parameter Validation

                var argArray = new List<CORSArg>();
                if (value == null)
                {
                    argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
                }
                else
                {
					argArray.Add(new CORSArg(invoiceId, (invoiceId == 0), "<li>'InvoiceID' was not passed.</li>"));
                    argArray.Add(new CORSArg(value.BarcodeId, (string.IsNullOrEmpty(value.BarcodeId)), "<li>'BarcodeId' was not passed.</li>"));
					argArray.Add(new CORSArg(value.SalesmanID, (string.IsNullOrEmpty(value.SalesmanID) && string.IsNullOrEmpty(value.TechnicianID)), "<li>'SalesmanId' or 'TechnicianID' must be passed.</li>"));
                }

                CmsCORSResult<AeInvoice> result;
                if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                #endregion Parameter Validation

                #region TRY

                try
                {
                    // ** Create Service
                    var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();

                    // ** Prepare arguents
                    // ReSharper disable once PossibleNullReferenceException
                    IFnsResult<IFnsAeInvoice> fnsResult = mcService.InvoiceAddByBarcode(value.InvoiceID, value.BarcodeId, value.SalesmanID, value.TechnicianID, user.GPEmployeeID);

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
		                    ProductBarcodeId = item.ProductBarcodeId,
		                    ItemSKU = item.ItemSKU,
		                    ItemDesc = item.ItemDesc,
		                    TaxOptionId = item.TaxOptionId,
		                    Qty = item.Qty,
		                    Cost = item.Cost,
		                    RetailPrice = item.RetailPrice,
		                    PriceWithTax = item.PriceWithTax,
							SystemPoints = item.SystemPoints,
		                    SalesmanID = item.SalesmanID,
		                    TechnicianID = item.TechnicianID,
		                    ModifiedOn = item.ModifiedOn,
		                    ModifiedBy = item.ModifiedBy,
		                    CreatedOn = item.CreatedOn,
		                    CreatedBy = item.CreatedBy
	                    }).ToList();

						// ** Get SalesInformation
						var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
						IFnsResult<IFnsMsAccountSalesInformation> oFnsModel = oService.SalesInformationRead(fnsResultValue.Header.AccountId, user.GPEmployeeID);
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
							//BarcodeId = fnsResultValue.Header.BarcodeId,
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

		[Route("InvoiceItems/{invoiceId}/AddExistingEquipment")]
		[HttpPost]
		public CmsCORSResult<AeInvoice> AddExistingEquipment(long invoiceId, [FromBody] ArgsAddBySku value)
        {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "AddExistingEquipment";

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>();
                    if (value == null)
                    {
                        argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
                    }
                    else
                    {
						argArray.Add(new CORSArg(invoiceId, (invoiceId == 0), "<li>'InvoiceID' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.ItemSku, (string.IsNullOrEmpty(value.ItemSku)), "<li>'ItemSku' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.Qty, (value.Qty == 0), "<li>'Qty' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.SalesmanID, (string.IsNullOrEmpty(value.SalesmanID) && string.IsNullOrEmpty(value.TechnicianID)), "<li>'SalesmanId' or 'TechnicianID' must be passed.</li>"));
                    }

                    CmsCORSResult<AeInvoice> result;
                    if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;

                    #endregion Parameter Validation

                    #region TRY

					try
					{
						// ** Create Service
						var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();

						// ** Prepare arguents
						// ReSharper disable once PossibleNullReferenceException
						IFnsResult<IFnsAeInvoice> fnsResult = mcService.AddExistingEquipment(invoiceId, value.ItemSku, value.Qty, value.SalesmanID, value.TechnicianID, user.GPEmployeeID);

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
								ProductBarcodeId = item.ProductBarcodeId,
								ItemSKU = item.ItemSKU,
								ItemDesc = item.ItemDesc,
								TaxOptionId = item.TaxOptionId,
								Qty = item.Qty,
								Cost = item.Cost,
								RetailPrice = item.RetailPrice,
								PriceWithTax = item.PriceWithTax,
								SystemPoints = item.SystemPoints,
								SalesmanID = item.SalesmanID,
								TechnicianID = item.TechnicianID,
								ModifiedOn = item.ModifiedOn,
								ModifiedBy = item.ModifiedBy,
								CreatedOn = item.CreatedOn,
								CreatedBy = item.CreatedBy
							}).ToList();

							// ** Get SalesInformation
							var oService = SosServiceEngine.Instance.FunctionalServices.Instance<IMonitoringStationService>();
							IFnsResult<IFnsMsAccountSalesInformation> oFnsModel = oService.SalesInformationRead(fnsResultValue.Header.AccountId, user.GPEmployeeID);
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

                    return result;

                    #endregion Result

                });
        }

		[Route("InvoiceItems/{invoiceItemId}/UpdateEquipment")]
		[HttpPost]
		public CmsCORSResult<AeInvoiceItem> UpdateEquipment(long invoiceItemId, [FromBody] AeInvoiceItem value)
	    {
            #region Initialize

            /** Initialize. */
            const string METHOD_NAME = "UpdateEquipment";
            var result = new CmsCORSResult<AeInvoiceItem>((int)ErrorCodes.Initializing, string.Format("Initializing '{0}'.", METHOD_NAME));

            #endregion Initialize

            /** Authenticate session first. */
            return CORSSecurity.AuthenticationWrapper(METHOD_NAME
                , user =>
                {
                    #region Parameter Validation

                    var argArray = new List<CORSArg>();
                    if (value == null)
                    {
                        argArray.Add(new CORSArg(null, (true), "<li>No values where passed.</li>"));
                    }
                    else
                    {
						argArray.Add(new CORSArg(invoiceItemId, (invoiceItemId == 0), "<li>'InvoiceItemID' was not passed.</li>"));
						argArray.Add(new CORSArg(value.RetailPrice, (value.RetailPrice == 0), "<li>RetailPrice was not passed.</li>"));
						argArray.Add(new CORSArg(value.SystemPoints, (value.SystemPoints == 0), "<li>SystemPoints was not passed.</li>"));
                        argArray.Add(new CORSArg(value.Qty, (value.Qty == 0), "<li>'Qty' was not passed.</li>"));
                        argArray.Add(new CORSArg(value.SalesmanID, (string.IsNullOrEmpty(value.SalesmanID) && string.IsNullOrEmpty(value.TechnicianID)), "<li>'SalesmanId' or 'TechnicianID' must be passed.</li>"));

                        if (!CORSArg.ArgumentValidation(argArray, out result, METHOD_NAME)) return result;
                    }

                    #endregion Parameter Validation

                    #region TRY

                    try
                    {
                        // ** Create Service
                        var mcService = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();

                        // ** Prepare arguents
                        // ReSharper disable once PossibleNullReferenceException
                        var fnsAeInvoiceItem = new FnsAeInvoiceItemView(invoiceItemId, value.ItemId, value.Qty, value.RetailPrice, value.SystemPoints, value.SalesmanID, value.TechnicianID);
                        IFnsResult<IFnsAeInvoiceItemView> fnsResult = mcService.UpdateEquipment(fnsAeInvoiceItem, user.GPEmployeeID);

                        // ** Save result
                        result.Code = fnsResult.Code;
                        result.SessionId = user.SessionID;
                        result.Message = fnsResult.Message;

                        // ** Get Values
                        var fnsResultValue = (IFnsAeInvoiceItemView)fnsResult.GetValue();
                        if (result.Code == (int)CmsResultCodes.Success && fnsResultValue != null)
                        {
                            var resultValue = new AeInvoiceItem
                            {
                                InvoiceItemID = fnsResultValue.InvoiceItemID,
                                InvoiceId = fnsResultValue.InvoiceId,
                                ItemId = fnsResultValue.ItemId,
                                ItemSKU = fnsResultValue.ItemSKU,
                                ItemDesc = fnsResultValue.ItemDesc,
                                TaxOptionId = fnsResultValue.TaxOptionId,
                                Qty = fnsResultValue.Qty,
                                Cost = fnsResultValue.Cost,
                                RetailPrice = fnsResultValue.RetailPrice,
                                PriceWithTax = fnsResultValue.PriceWithTax,
								SystemPoints = fnsResultValue.SystemPoints,
                                SalesmanID = fnsResultValue.SalesmanID,
                                TechnicianID = fnsResultValue.TechnicianID,
                                ModifiedBy = fnsResultValue.ModifiedBy,
                                ModifiedOn = fnsResultValue.ModifiedOn,
                                CreatedBy = fnsResultValue.CreatedBy,
                                CreatedOn = fnsResultValue.CreatedOn

                            };
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

		//[Route("Items/{id}/ByPartNumber")]
		//[HttpGet]
		//public CmsCORSResult<AeItem> ItemByPartNumber(string id)
		//{
		//	return CORSSecurity.AuthenticationWrapper("ItemByPartNumber", user =>
		//	{
		//		var service = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
		//		var fnsResult = service.ItemByPartNumber(id);
		//		var value = fnsResult.GetTValue();
		//		return new CmsCORSResult<AeItem>
		//		{
		//			Code = fnsResult.Code,
		//			Message = fnsResult.Message,
		//			Value = (value == null) ? null : ConvertTo.CastFnsToAeItem(value),
		//		};
		//	});
		//}
		//[Route("Items/{id}/ByBarcode")]
		//[HttpGet]
		//public CmsCORSResult<AeItem> ItemByBarcode(string id)
		//{
		//	return CORSSecurity.AuthenticationWrapper("ItemByBarcode", user =>
		//	{
		//		var service = SosServiceEngine.Instance.FunctionalServices.Instance<IAccountingEngineService>();
		//		var fnsResult = service.ItemByBarcode(id);
		//		var value = fnsResult.GetTValue();
		//		return new CmsCORSResult<AeItem>
		//		{
		//			Code = fnsResult.Code,
		//			Message = fnsResult.Message,
		//			Value = (value == null) ? null : ConvertTo.CastFnsToAeItem(value),
		//		};
		//	});
		//}
	}
}