using System.Data.SqlClient;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.FunctionalServices.Contracts.Models.CentralStation;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.AccountingEngine;
using SOS.FunctionalServices.Models.CentralStation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.Lib.Util;
using SOS.Data.HumanResource;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class AccountingEngineService : IAccountingEngineService
	{
		#region Search Customers

		public IFnsResult<List<IFnsCustomerMasterFileGeneral>> CustomerGeneralSearch(int dealerId, string city, string stateId, string postalCode, string email, string firstName, string lastName, string phoneNumber, int pageSize, int pageNumber, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "Search Customer Master File General";
			var result = new FnsResult<List<IFnsCustomerMasterFileGeneral>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				// ** Get instance of the AddressVerification service.
				AE_CustomerMasterFileGeneralViewCollection aeList = SosCrmDataContext.Instance.AE_CustomerMasterFileGeneralViews.Search(dealerId, city, stateId, postalCode, email, firstName, lastName, phoneNumber, pageSize, pageNumber);
				var resultList = aeList.Select(item => new FnsCustomerMasterFileGeneral(item)).Cast<IFnsCustomerMasterFileGeneral>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsCustomerMasterFileGeneral>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		//public IFnsResult<IFnsAeCustomerCardInfo> CustomerCardInfoGet(long cmfid, string gpEmployeeId)
		//{
		//	#region INITIALIZATION
		//
		//	// ** Initialize 
		//	const string METHOD_NAME = "Get CustomerCardInfo";
		//	var result = new FnsResult<IFnsAeCustomerCardInfo>
		//	{
		//		Code = (int)ErrorCodes.GeneralMessage,
		//		Message = string.Format("Initializing {0}", METHOD_NAME),
		//	};
		//
		//	#endregion INITIALIZATION
		//
		//	#region TRY
		//	try
		//	{
		//		// ** Get instance of the AddressVerification service.
		//		AE_CustomerCardInfoView customerCardInfo = SosCrmDataContext.Instance.AE_CustomerCardInfoViews.ByCMFID(cmfid, gpEmployeeId);
		//		QL_CreditReport creditReport = null;
		//		RU_Season season = null;
		//		if (customerCardInfo != null)
		//		{
		//			creditReport = SosCrmDataContext.Instance.QL_CreditReports.MaxScoreByCmfID(customerCardInfo.CustomerMasterFileId);
		//		}
		//		if (creditReport != null)
		//		{
		//			season = HumanResourceDataContext.Instance.RU_Seasons.LoadByPrimaryKey(creditReport.SeasonId);
		//		}
		//
		//		// ** Build result
		//		var fnsCustomerCardInfo = new FnsAeCustomerCardInfo(customerCardInfo, creditReport, season);
		//		// ** Save result information
		//		result.Code = (int)ErrorCodes.Success;
		//		result.Message = "Success";
		//		result.Value = fnsCustomerCardInfo;
		//	}
		//	#endregion TRY
		//
		//	#region CATCH
		//	catch (Exception ex)
		//	{
		//		result = new FnsResult<IFnsAeCustomerCardInfo>
		//		{
		//			Code = (int)ErrorCodes.UnexpectedException,
		//			Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
		//		};
		//	}
		//	#endregion CATCH
		//
		//	// ** Return result
		//	return result;
		//}
		//
		//public IFnsResult<IFnsAeCustomerCardInfo> CustomerCardInfoGetByAccountId(long accountId, string gpEmployeeId)
		//{
		//	#region INITIALIZATION
		//
		//	// ** Initialize 
		//	const string METHOD_NAME = "Get CustomerCardInfo";
		//	var result = new FnsResult<IFnsAeCustomerCardInfo>
		//	{
		//		Code = (int)ErrorCodes.GeneralMessage,
		//		Message = string.Format("Initializing {0}", METHOD_NAME),
		//	};
		//
		//	#endregion INITIALIZATION
		//
		//	#region TRY
		//	try
		//	{
		//
		//		// ** Get instance of the AddressVerification service.
		//		AE_CustomerCardInfoView customerCardInfo = SosCrmDataContext.Instance.AE_CustomerCardInfoViews.ByAccountId(accountId, gpEmployeeId);
		//		QL_CreditReport creditReport = null;
		//		RU_Season season = null;
		//		if (customerCardInfo != null)
		//		{
		//			creditReport = SosCrmDataContext.Instance.QL_CreditReports.MaxScoreByCmfID(customerCardInfo.CustomerMasterFileId);
		//		}
		//		if (creditReport != null)
		//		{
		//			season = HumanResourceDataContext.Instance.RU_Seasons.LoadByPrimaryKey(creditReport.SeasonId);
		//		}
		//
		//		// ** Build result
		//		var fnsCustomerCardInfo = new FnsAeCustomerCardInfo(customerCardInfo, creditReport, season);
		//		// ** Save result information
		//		result.Code = (int)ErrorCodes.Success;
		//		result.Message = "Success";
		//		result.Value = fnsCustomerCardInfo;
		//	}
		//	#endregion TRY
		//
		//	#region CATCH
		//	catch (Exception ex)
		//	{
		//		result = new FnsResult<IFnsAeCustomerCardInfo>
		//		{
		//			Code = (int)ErrorCodes.UnexpectedException,
		//			Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
		//		};
		//	}
		//	#endregion CATCH
		//
		//	// ** Return result
		//	return result;
		//}

		public IFnsResult<object> Customer(long accountId, string customerTypeId)
		{
			return new FnsResult<object>
			{
				Code = 0,
				Message = "",
				Value = SosCrmDataContext.Instance.AE_Customers.GetByAccountID(accountId, customerTypeId),
			};
		}
		public IFnsResult<object> CustomerAddress(long customerId, string customerAddressTypeId)
		{
			MC_Address address = null;
			var custAddress = SosCrmDataContext.Instance.AE_CustomerAddresses.ByCustomerID(customerId, customerAddressTypeId);
			if (custAddress != null)
			{
				address = custAddress.Address;
			}
			else if (string.Compare(customerAddressTypeId, "PREM", StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				// customer addresses don't seem to be created so if we're looking for the premise address find it on the actual customer????
				var customer = SosCrmDataContext.Instance.AE_Customers.LoadByPrimaryKey(customerId);
				if (customer != null)
				{
					address = customer.Address;
				}
			}

			if (address == null)
			{
				return new FnsResult<object>
				{
					Code = (int)ErrorCodes.SqlItemNotFound,
					Message = "No '" + customerAddressTypeId + "' address for customer " + customerId,
				};
			}
			return new FnsResult<object>
			{
				Code = 0,
				Message = "",
				Value = address,
			};
		}

		#endregion Search Customers

		public IFnsResult<List<IFnsAeInvoiceTemplate>> PointSystemsGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsAeInvoiceTemplate>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing PointSystemsGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				AE_InvoiceTemplateCollection aeList = SosCrmDataContext.Instance.AE_InvoiceTemplates.LoadAll();
				var resultList = aeList.Select(item => new FnsAeInvoiceTemplate(item)).Cast<IFnsAeInvoiceTemplate>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsAeInvoiceTemplate>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at PointSystemsGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsAeItem>> ActivationFeesGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsAeItem>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing ActivationFeesGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				AE_ItemCollection aeList = SosCrmDataContext.Instance.AE_Items.ActivationFeesGet();
				var resultList = aeList.Select(item => new FnsAeItem(item)).Cast<IFnsAeItem>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsAeItem>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at ActivationFeesGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountCellularType>> CellularTypesGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsMsAccountCellularType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing CellularTypesGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_AccountCellularTypeCollection aeList = SosCrmDataContext.Instance.MS_AccountCellularTypes.GetAll();
				var resultList = aeList.Select(item => new FnsMsAccountCellularType(item)).Cast<IFnsMsAccountCellularType>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountCellularType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at ActivationFeesGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountServiceType>> ServiceTypesGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsMsAccountServiceType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing ServiceTypesGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_AccountSystemTypeCollection aeList = SosCrmDataContext.Instance.MS_AccountSystemTypes.GetAll();
				var resultList = aeList.Select(item => new FnsMsAccountServiceType(item)).Cast<IFnsMsAccountServiceType>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountServiceType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at ActivationFeesGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsAccountPanelType>> PanelTypesGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsMsAccountPanelType>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing PanelTypesGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_AccountPanelTypeCollection aeList = SosCrmDataContext.Instance.MS_AccountPanelTypes.GetAll();
				var resultList = aeList.Select(item => new FnsMsAccountPanelType(item)).Cast<IFnsMsAccountPanelType>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsAccountPanelType>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at ActivationFeesGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsMsVendorAlarmComPackage>> VendorAlarmComPackagesGet(int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsMsVendorAlarmComPackage>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing VendorAlarmComPackagesGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_VendorAlarmComPackageCollection aeList = SosCrmDataContext.Instance.MS_VendorAlarmComPackages.LoadAll();
				var resultList = aeList.Select(item => new FnsMsVendorAlarmComPackage(item)).Cast<IFnsMsVendorAlarmComPackage>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsVendorAlarmComPackage>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at VendorAlarmComPackagesGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsAeItem>> EquipmentByPointsGet(long inventoryTemplateId, int userID)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsAeItem>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing ActivationFeesGet",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				// ** Get instance of the AddressVerification service.
				AE_ItemCollection aeList = SosCrmDataContext.Instance.AE_Items.EquipmentByPointsGet(inventoryTemplateId);
				var resultList = aeList.Select(item => new FnsAeItem(item)).Cast<IFnsAeItem>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsAeItem>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at EquipmentByPointsGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsAeContractTemplate>> ContractLengthsGet(int invoiceTemplateId, int userId)
		{
			#region INITIALIZATION

			// ** Initialize 
			var result = new FnsResult<List<IFnsAeContractTemplate>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = "Initializing Contract Lengths Get",
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				// ** Get instance of the AddressVerification service.
				AE_ContractTemplateCollection aeList = SosCrmDataContext.Instance.AE_ContractTemplates.ContractLengthsGet(invoiceTemplateId);
				var resultList = aeList.Select(item => new FnsAeContractTemplate(item)).Cast<IFnsAeContractTemplate>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsAeContractTemplate>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at EquipmentByPointsGet: {0}", ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsAeAging>> AgingGetByCMFID(long cmfid, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "AgingGetByCMFID";
			var result = new FnsResult<List<IFnsAeAging>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				// ** Get instance of the AddressVerification service.
				AE_AgingViewCollection aeList = SosCrmDataContext.Instance.AE_AgingViews.GetByCMFID(cmfid);
				var resultList = aeList.Select(item => new FnsAeAging(item)).Cast<IFnsAeAging>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsAeAging>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#region Billing Info Summary
		public IFnsResult<List<IFnsSaeBillingInfoSummary>> BillingInfoSummaryGetByCMFID(long cmfid, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "BillingInfoSummaryGetByCMFID";
			var result = new FnsResult<List<IFnsSaeBillingInfoSummary>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				// ** Get instance of the AddressVerification service.
				SAE_BillingInfoSummaryViewCollection aeList = SosCrmDataContext.Instance.SAE_BillingInfoSummaryViews.GetByCMFID(cmfid);

				// ** Build result
				var resultList = aeList.Select(item => new FnsSaeBillingInfoSummary(item)).Cast<IFnsSaeBillingInfoSummary>().ToList();

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSaeBillingInfoSummary>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<List<IFnsSaeBillingInfoSummary>> BillingInfoSummaryGetByAccountID(long accountId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "BillingInfoSummaryGetByAccountID";
			var result = new FnsResult<List<IFnsSaeBillingInfoSummary>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				// ** Get instance of the AddressVerification service.
				SAE_BillingInfoSummaryViewCollection aeList = SosCrmDataContext.Instance.SAE_BillingInfoSummaryViews.GetByAccountId(accountId);
				var resultList = aeList.Select(item => new FnsSaeBillingInfoSummary(item)).Cast<IFnsSaeBillingInfoSummary>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSaeBillingInfoSummary>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		//public IFnsResult<List<IFnsSaeBillingHistory>> BillingHistoryGetByAccountID(long accountId, string gpEmployeeId)
		//{
		//    #region INITIALIZATION

		//    // ** Initialize 
		//    const string METHOD_NAME = "BillingHistoryGetByAccountID";
		//    var result = new FnsResult<List<IFnsSaeBillingHistory>>
		//    {
		//        Code = (int)ErrorCodes.GeneralMessage,
		//        Message = string.Format("Initializing {0}", METHOD_NAME),
		//    };

		//    #endregion INITIALIZATION

		//    #region TRY
		//    try
		//    {

		//        // ** Get instance of the AddressVerification service.
		//        var aeList = SosCrmDataContext.Instance.SAE_BillingHistoryViews.GetByCMFID(cmfid:);
		//        var resultList = aeList.Select(item => new FnsSaeBillingHistory(item)).Cast<IFnsSaeBillingHistory>().ToList();
		//        // ** Build result

		//        // ** Save result information
		//        result.Code = (int)ErrorCodes.Success;
		//        result.Message = "Success";
		//        result.Value = resultList;
		//    }
		//    #endregion TRY

		//    #region CATCH
		//    catch (Exception ex)
		//    {
		//        result = new FnsResult<List<IFnsSaeBillingHistory>>
		//        {
		//            Code = (int)ErrorCodes.UnexpectedException,
		//            Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
		//        };
		//    }
		//    #endregion CATCH

		//    // ** Return result
		//    return result;
		//}

		public IFnsResult<List<IFnsSaeBillingHistory>> BillingHistoryGetByCMFID(long cmfid, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "BillingHistoryGetByCMFID";
			var result = new FnsResult<List<IFnsSaeBillingHistory>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				// ** Get instance of the AddressVerification service.
				var aeList = SosCrmDataContext.Instance.SAE_BillingHistoryViews.GetByCMFID(cmfid);
				var resultList = aeList.Select(item => new FnsSaeBillingHistory(item)).Cast<IFnsSaeBillingHistory>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsSaeBillingHistory>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}
		#endregion Billing Info Summary

		#region Inventory Items

		public IFnsResult<List<IFnsMsEquipmentsView>> FrequentlyInstalledEquipmentGet(string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "FrequentlyInstalledEquipmentGet";
			var result = new FnsResult<List<IFnsMsEquipmentsView>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				MS_EquipmentsViewCollection aeList = SosCrmDataContext.Instance.MS_EquipmentsViews.FrequentlyInstalledEquipmentGet();
				var resultList = aeList.Select(item => new FnsMsEquipmentsView(item)).Cast<IFnsMsEquipmentsView>().ToList();
				// ** Build result

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultList;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<List<IFnsMsEquipmentsView>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		#endregion Inventory Items

		#region Invoicing

		public IFnsResult<IFnsAeInvoice> InvoiceGet(long invoiceId, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "InvoiceGet";
			var result = new FnsResult<IFnsAeInvoice>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				AE_InvoiceItemsViewCollection aeList = SosCrmDataContext.Instance.AE_InvoiceItemsViews.ByInvoiceId(invoiceId);
				var resultList = aeList
					.Where(item => !item.IsDeleted && item.IsActive)
					.Select(item => (IFnsAeInvoiceItemView)new FnsAeInvoiceItemView(item))
					.ToList();
				AE_Invoice invoiceHeader = SosCrmDataContext.Instance.AE_Invoices.LoadByPrimaryKey(invoiceId);

				// ** Build result
				var resultValue = new FnsAeInvoice(new FnsAeInvoiceHeader(invoiceHeader), resultList);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoice>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoiceHeader> InvoiceCreate(IFnsAeInvoiceHeader fnsHeader, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "InvoiceCreate";
			var result = new FnsResult<IFnsAeInvoiceHeader>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				var invoice = SosCrmDataContext.Instance.AE_Invoices.CreateInvoiceMin(fnsHeader.AccountId, fnsHeader.InvoiceTypeId,
					gpEmployeeID);

				// ** Build result
				var resultValue = new FnsAeInvoiceHeader(invoice);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoiceHeader>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoiceItemView> InvoiceItemCreate(IFnsAeInvoiceItemView item, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "InvoiceItemCreate";
			var result = new FnsResult<IFnsAeInvoiceItemView>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get AE_Item first.
				//var aeItem = SosCrmDataContext.Instance.AE_Items.LoadByPrimaryKey(item.ItemId);

				var invoiceItem = SosCrmDataContext.Instance.AE_InvoiceItemsViews.Create(item.InvoiceId, item.ItemId, item.Qty, item.SalesmanID, item.TechnicianID, gpEmployeeId);

				// ** Build result
				var resultValue = new FnsAeInvoiceItemView(invoiceItem);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoiceItemView>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoiceItemView> UpdateEquipment(IFnsAeInvoiceItemView item, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "UpdateEquipment";
			var result = new FnsResult<IFnsAeInvoiceItemView>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get AE_Item first.
				//var aeItem = SosCrmDataContext.Instance.AE_Items.LoadByPrimaryKey(item.ItemId);

				var invoiceItem = SosCrmDataContext.Instance.AE_InvoiceItemsViews.Update(item.InvoiceItemID, item.Qty, item.RetailPrice, item.SystemPoints, item.SalesmanID, item.TechnicianID, gpEmployeeId);

				// ** Build result
				var resultValue = new FnsAeInvoiceItemView(invoiceItem);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoiceItemView>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		/// <summary>
		/// This is a logical delete
		/// </summary>
		/// <param name="invoiceItemID"></param>
		/// <param name="gpEmployeeId"></param>
		/// <returns>IFnsResult</returns>
		public IFnsResult InvoiceItemDelete(long invoiceItemID, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "InvoiceItemDelete";
			var result = new FnsResult<IFnsAeInvoiceHeader>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				var invoiceItem = SosCrmDataContext.Instance.AE_InvoiceItems.LoadByPrimaryKey(invoiceItemID);

				// ** Make the logical delete
				invoiceItem.IsDeleted = true;
				invoiceItem.Save(gpEmployeeId);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoiceHeader>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoice> RefreshMsAccountInstall(IFnsSalesSummaryProperties props, string gpEmployeeId)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "RefreshMsAccountInstall(IFnsSalesSummaryProperties)";
			var result = new FnsResult<IFnsAeInvoice>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY

			try
			{
				// ** Get instance of the Refresh Install Invoice service.
				AE_InvoiceItemsViewCollection aeList = SosCrmDataContext.Instance.AE_InvoiceItemsViews.RefreshMsAccountInstall001(
					props.InvoiceID,
					props.AccountId,
					props.BillingDay,
					props.CurrentMonitoringStation,
					props.Email,
					props.ActivationFeeItemId,
					props.ActivationFeeActual,
					props.MonthlyMonitoringRateItemId,
					props.MonthlyMonitoringRateActual,
					props.PanelTypeId,
					props.CellTypeId,
					props.CellPackageItemId,
					props.Over3Months,
					props.AlarmComPackage,
					props.PaymentTypeId,
					props.IsTakeover,
					props.IsOwner,
					props.IsMoni,
					props.ContractTemplateId,
					props.ContractLength,
					props.DealerId,
					props.SalesmanID,
					props.TechnicianID,
					gpEmployeeId);
				var resultList = aeList.Select(item => new FnsAeInvoiceItemView(item)).Cast<IFnsAeInvoiceItemView>().ToList();
				AE_Invoice invoiceHeader = SosCrmDataContext.Instance.AE_Invoices.LoadByPrimaryKey(props.InvoiceID);

				// ** Build result
				var resultValue = new FnsAeInvoice(new FnsAeInvoiceHeader(invoiceHeader), resultList);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH

			catch (SqlException sqlEx)
			{
				var sqlUtil = MsSqlExceptionUtil.Parse(sqlEx.Message);
				result.Code = sqlUtil.MessageID;
				result.Message = string.Format("SQL Exception thrown at {0}: {1}", METHOD_NAME, sqlUtil.ErrorMessage);
			}
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoice>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}

			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoice> RefreshMsAccountInstall(long invoiceID, long accountId, string activationFeeItemId, decimal activationFeeActual,
			string monthlyMonitoringRateItemId, decimal monthlyMonitoringRateActual, string panelTypeId, string cellTypeId, bool over3Months,
			string alarmComPackageId, int dealerId, string salesmanID, string technicianID, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "RefreshMsAccountInstall";
			var result = new FnsResult<IFnsAeInvoice>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				AE_InvoiceItemsViewCollection aeList = SosCrmDataContext.Instance.AE_InvoiceItemsViews.RefreshMsAccountInstall(
					invoiceID,
					accountId,
					activationFeeItemId,
					activationFeeActual,
					monthlyMonitoringRateItemId,
					monthlyMonitoringRateActual,
					panelTypeId,
					cellTypeId,
					over3Months,
					alarmComPackageId,
					dealerId,
					salesmanID,
					technicianID,
					gpEmployeeID);
				var resultList = aeList.Select(item => new FnsAeInvoiceItemView(item)).Cast<IFnsAeInvoiceItemView>().ToList();
				AE_Invoice invoiceHeader = SosCrmDataContext.Instance.AE_Invoices.LoadByPrimaryKey(invoiceID);

				// ** Build result
				var resultValue = new FnsAeInvoice(new FnsAeInvoiceHeader(invoiceHeader), resultList);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoice>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoice> InvoiceAddByPartNumber(long invoiceID, string itemSku, int qty,
			string salesmanID, string technicianID, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "InvoiceAddByPartNumber";
			var result = new FnsResult<IFnsAeInvoice>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				AE_InvoiceItemsViewCollection aeList = SosCrmDataContext.Instance.AE_InvoiceItemsViews.AddByPartNumber(invoiceID, itemSku, qty, salesmanID, technicianID, gpEmployeeID);
				var resultList = aeList.Select(item => new FnsAeInvoiceItemView(item)).Cast<IFnsAeInvoiceItemView>().ToList();
				AE_Invoice invoiceHeader = SosCrmDataContext.Instance.AE_Invoices.LoadByPrimaryKey(invoiceID);

				// ** Build result
				var resultValue = new FnsAeInvoice(new FnsAeInvoiceHeader(invoiceHeader), resultList);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoice>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoice> InvoiceAddByBarcode(long invoiceID, string barcodeId,
			string salesmanID, string technicianID, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "AddByBarcode";
			var result = new FnsResult<IFnsAeInvoice>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				AE_InvoiceItemsViewCollection aeList = SosCrmDataContext.Instance.AE_InvoiceItemsViews.AddByBarcode(invoiceID, barcodeId, salesmanID, technicianID, gpEmployeeID);
				var resultList = aeList.Select(item => new FnsAeInvoiceItemView(item)).Cast<IFnsAeInvoiceItemView>().ToList();
				AE_Invoice invoiceHeader = SosCrmDataContext.Instance.AE_Invoices.LoadByPrimaryKey(invoiceID);

				// ** Build result
				var resultValue = new FnsAeInvoice(new FnsAeInvoiceHeader(invoiceHeader), resultList);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoice>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}




		public IFnsResult<IFnsAeInvoiceMsInstallInfo> InvoiceMsIsntallsGetByAccountId(long accountId, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "InvoiceMsIsntallsGetByAccountId";
			var result = new FnsResult<IFnsAeInvoiceMsInstallInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				AE_InvoiceMsInstallInfoView item = SosCrmDataContext.Instance.AE_InvoiceMsInstallInfoViews.GetByIDs(null, accountId, gpEmployeeID);
				var resultValue = new FnsAeInvoiceMsInstallInfo(item);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoiceMsInstallInfo>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoiceMsInstallInfo> InvoiceMsIsntallsGetByInvoiceID(long invoiceID, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "InvoiceMsIsntallsGetByInvoiceID";
			var result = new FnsResult<IFnsAeInvoiceMsInstallInfo>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				AE_InvoiceMsInstallInfoView item = SosCrmDataContext.Instance.AE_InvoiceMsInstallInfoViews.GetByIDs(invoiceID, null, gpEmployeeID);
				var resultValue = new FnsAeInvoiceMsInstallInfo(item);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoiceMsInstallInfo>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}

		public IFnsResult<IFnsAeInvoice> AddExistingEquipment(long invoiceID, string itemSku, int qty,
			string salesmanID, string technicianID, string gpEmployeeID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "InvoiceAddExistingEquipment";
			var result = new FnsResult<IFnsAeInvoice>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{
				// ** Get instance of the AddressVerification service.
				AE_InvoiceItemsViewCollection aeList = SosCrmDataContext.Instance.AE_InvoiceItemsViews.AddExistingEquipment(invoiceID, itemSku, qty, salesmanID, technicianID, gpEmployeeID);
				var resultList = aeList.Select(item => new FnsAeInvoiceItemView(item)).Cast<IFnsAeInvoiceItemView>().ToList();
				AE_Invoice invoiceHeader = SosCrmDataContext.Instance.AE_Invoices.LoadByPrimaryKey(invoiceID);

				// ** Build result
				var resultValue = new FnsAeInvoice(new FnsAeInvoiceHeader(invoiceHeader), resultList);

				// ** Save result information
				result.Code = (int)ErrorCodes.Success;
				result.Message = "Success";
				result.Value = resultValue;
			}
			#endregion TRY

			#region CATCH
			catch (Exception ex)
			{
				result = new FnsResult<IFnsAeInvoice>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH

			// ** Return result
			return result;
		}


		#endregion Invoicing

		//public IFnsResult<IFnsAeItem> ItemByPartNumber(string partNumber)
		//{
		//	var item = SosCrmDataContext.Instance.AE_Items.ByPartNumber(partNumber);
		//	return new FnsResult<IFnsAeItem>
		//	{
		//		Code = (int)ErrorCodes.Success,
		//		Message = (item == null) ? "No items matching part# " + partNumber : "",
		//		Value = (item == null) ? null : new FnsAeItem(item),
		//	};
		//}
		//public IFnsResult<IFnsAeItem> ItemByBarcode(string barcode)
		//{
		//	var item = SosCrmDataContext.Instance.AE_Items.ByBarcode(barcode);
		//	return new FnsResult<IFnsAeItem>
		//	{
		//		Code = (int)ErrorCodes.Success,
		//		Message = (item == null) ? "No items matching barcode " + barcode : "",
		//		Value = (item == null) ? null : new FnsAeItem(item),
		//	};
		//}
	}
}
