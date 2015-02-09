using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FunctionalServices.Contracts;
using SOS.FunctionalServices.Contracts.Helper;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;
using SOS.FunctionalServices.Models;
using SOS.FunctionalServices.Models.InventoryEngine;
using SOS.Lib.Util;

namespace SOS.FunctionalServices
{
	[ServiceBehavior(IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
	public class InventoryEngineService : IInventoryEngineService
	{
		public IFnsResult<IFnsIePurchaseOrder> PurchaseOrderGetByGPPO(string gpPO, string gpEmployeeId)
		{
			var result = new FnsResult<IFnsIePurchaseOrder>();
			IE_PurchaseOrder purchaseOrderHeader = SosCrmDataContext.Instance.IE_PurchaseOrders.LoadByGPPurchaseOrderNumber(gpPO, gpEmployeeId);
			if (purchaseOrderHeader == null)
			{
				result.Code = (int)ErrorCodes.SqlItemNotFound;
				result.Message = "PO# not found";
				return result;
			}

			result.Value = new FnsIePurchaseOrder(purchaseOrderHeader);
			return result;
		}

		public IFnsResult<IFnsIePurchaseOrder> PurchaseOrderGet(long id)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "PurchaseOrderGet";
            var result = new FnsResult<IFnsIePurchaseOrder>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
               // IE_PurchaseOrderCollection ieList = SosCrmDataContext.Instance.IE_PurchaseOrders
                //var resultList = aeList.Select(item => new FnsAeInvoiceItemView(item)).Cast<IFnsAeInvoiceItemView>().ToList();
                IE_PurchaseOrder purchaseOrderHeader = SosCrmDataContext.Instance.IE_PurchaseOrders.LoadByPrimaryKey(id);

                // ** Build result
                var resultValue = new FnsIePurchaseOrder(purchaseOrderHeader);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsIePurchaseOrder>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }

        public IFnsResult<List<IFnsIePurchaseOrderItem>> PurchaseOrderItemsGet(long purchaseOrderID)
		{
			#region INITIALIZATION

			// ** Initialize 
			const string METHOD_NAME = "PurchaseOrderItemsGet";
			var result = new FnsResult<List<IFnsIePurchaseOrderItem>>
			{
				Code = (int)ErrorCodes.GeneralMessage,
				Message = string.Format("Initializing {0}", METHOD_NAME),
			};

			#endregion INITIALIZATION

			#region TRY
			try
			{

				// ** retrieve list of purchase order items
                IE_PurchaseOrderItemsViewCollection ieList = SosCrmDataContext.Instance.IE_PurchaseOrderItemsViews.GetPurchaseOrderItemsList(purchaseOrderID);

				var resultList = ieList.Select(item => new FnsIePurchaseOrderItem(item)).Cast<IFnsIePurchaseOrderItem>().ToList();
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
				result = new FnsResult<List<IFnsIePurchaseOrderItem>>
				{
					Code = (int)ErrorCodes.UnexpectedException,
					Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
				};
			}
			#endregion CATCH
			// ** Return result

			return result;
		}


        public IFnsResult<IFnsIePackingSlip> PackingSlipGetByPOID(long id)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "PackingSlipGetByPOID";
            var result = new FnsResult<IFnsIePackingSlip>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                IE_PackingSlip packingSlip = SosCrmDataContext.Instance.IE_PackingSlips.GetByPOID(id);

                // ** Build result
                var resultValue = new FnsIePackingSlip(packingSlip);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsIePackingSlip>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }



        public IFnsResult<List<IFnsIePackingSlip>> PackingSlipGetByGPPON(string gPPONumber)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "PackingSlipGetByGPPON";
            var result = new FnsResult<List<IFnsIePackingSlip>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                IE_PackingSlipViewCollection packingSlipList = SosCrmDataContext.Instance.IE_PackingSlipViews.GetByGPPON(gPPONumber);

                // ** Build result
                var resultList = packingSlipList.Select(item => new FnsIePackingSlip(item)).Cast<IFnsIePackingSlip>().ToList();
            

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultList;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<List<IFnsIePackingSlip>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }



        public IFnsResult<IFnsIePackingSlip> PackingSlipCreate(IFnsIePackingSlip fnsHeader, string gpEmployeeID)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "PackingSlipCreate";
            var result = new FnsResult<IFnsIePackingSlip>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the ie service.
                var packingSlip = SosCrmDataContext.Instance.IE_PackingSlips.CreatePackingSlip(fnsHeader.PackingSlipNumber, fnsHeader.PurchaseOrderId, gpEmployeeID);

                // ** Build result
                var resultValue = new FnsIePackingSlip(packingSlip);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsIePackingSlip>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }


        public IFnsResult<IFnsIeProductBarcode> ProductBarcodeGetByPBID(string id)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ProductBarcodeGetByPBID";
            var result = new FnsResult<IFnsIeProductBarcode>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                IE_ProductBarcode productBarcode = SosCrmDataContext.Instance.IE_ProductBarcodes.GetByPBID(id);

                // ** Build result
                var resultValue = new FnsIeProductBarcode(productBarcode);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsIeProductBarcode>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }
        public IFnsResult<IFnsIeProductBarcode> ProductBarcodeCreate(IFnsIeProductBarcode fnsHeader, string gpEmployeeID)
        {
        #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ProductBarcodeCreate";
            var result = new FnsResult<IFnsIeProductBarcode>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY

	        try
	        {
		        // ** Get instance of the ie service.
		        var productBarcode =
			        SosCrmDataContext.Instance.IE_ProductBarcodes.CreateProductBarcode(fnsHeader.ProductBarcodeID,
				        fnsHeader.PurchaseOrderItemId, fnsHeader.SimGUID, gpEmployeeID);

		        // ** Build result
		        var resultValue = new FnsIeProductBarcode(productBarcode);

		        // ** Save result information
		        result.Code = (int) ErrorCodes.Success;
		        result.Message = "Success";
		        result.Value = resultValue;
	        }
		        #endregion TRY

		        #region CATCH

	        catch (SqlException sqlEx)
	        {
		        var sqlUtil = MsSqlExceptionUtil.Parse(sqlEx.Message);
				result.Code = sqlUtil.MessageID;
			    result.Message = sqlUtil.ErrorMessage;
	        }
	        catch (Exception ex)
	        {
		        result = new FnsResult<IFnsIeProductBarcode>
		        {
			        Code = (int) ErrorCodes.UnexpectedException,
			        Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
		        };
	        }

	        #endregion CATCH

            // ** Return result
            return result;
        }


        public IFnsResult<IFnsIePackingSlipItem> PackingSlipItemCreate(IFnsIePackingSlipItem fnsHeader, string gpEmployeeID)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "PackingSlipItemCreate";
            var result = new FnsResult<IFnsIePackingSlipItem>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the ie service.
                var packingSlipItem = SosCrmDataContext.Instance.IE_PackingSlipItems.CreatePackingSlipItem(fnsHeader.PackingSlipId, fnsHeader.ProductSkwId,fnsHeader.ItemId, 
                    fnsHeader.Quantity, gpEmployeeID);

                // ** Build result
                var resultValue = new FnsIePackingSlipItem(packingSlipItem);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsIePackingSlipItem>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }


        public IFnsResult<IFnsIeProductBarcodeTracking> ProductBarcodeTrackingCreate(IFnsIeProductBarcodeTracking fnsHeader, string gpEmployeeID)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ProductBarcodeTrackingCreate";
            var result = new FnsResult<IFnsIeProductBarcodeTracking>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                // ** Get instance of the ie service.
                var productBarcodeTracking = SosCrmDataContext.Instance.IE_ProductBarcodeTrackings.CreateProductBarcodeTracking(
                    fnsHeader.ProductBarcodeTrackingTypeId, 
                    fnsHeader.ProductBarcodeId, 
                    fnsHeader.LocationTypeID,
                    fnsHeader.LocationID,
                  /*  fnsHeader.TransferToWarehouseSiteId,
                    fnsHeader.ReturnToVendorId,
                    fnsHeader.AssignedToCustomerId,
                    fnsHeader.AssignedToDealerId,
                    fnsHeader.RtmaNumberId,*/
                    fnsHeader.Comment,
                    gpEmployeeID);

                // ** Build result
                var resultValue = new FnsIeProductBarcodeTracking(productBarcodeTracking);

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
                result = new FnsResult<IFnsIeProductBarcodeTracking>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }

        public IFnsResult<IFnsIeProductBarcodeTracking> ProductBarcodeTrackingGetByPBID(long id)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ProductBarcodeTrackingGetByPBID";
            var result = new FnsResult<IFnsIeProductBarcodeTracking>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                IE_ProductBarcodeTracking productBarcodeTracking = SosCrmDataContext.Instance.IE_ProductBarcodeTrackings.GetByPBID(id);

                // ** Build result
                var resultValue = new FnsIeProductBarcodeTracking(productBarcodeTracking);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsIeProductBarcodeTracking>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }

        public IFnsResult<IFnsIeProductBarcodeTrackingView> ProductBarcodeTrackingViewGetByPBTID(long id)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ProductBarcodeTrackingGetByPBTID";
            var result = new FnsResult<IFnsIeProductBarcodeTrackingView>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {
                IE_ProductBarcodeTrackingView productBarcodeTrackingView = SosCrmDataContext.Instance.IE_ProductBarcodeTrackingViews.GetByPBTID(id);

                // ** Build result
                var resultValue = new FnsIeProductBarcodeTrackingView(productBarcodeTrackingView);

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsIeProductBarcodeTrackingView>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH

            // ** Return result
            return result;
        }



        public IFnsResult<List<IFnsIeWarehouseSite>> WarehouseSiteListGet()
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "WarehouseSiteListGet";
            var result = new FnsResult<List<IFnsIeWarehouseSite>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {

                // ** retrieve list of purchase order items
                IE_WarehouseSiteCollection ieList = SosCrmDataContext.Instance.IE_WarehouseSites.GetWarehouseSiteList();

                var resultList = ieList.Select(item => new FnsIeWarehouseSite(item)).Cast<IFnsIeWarehouseSite>().ToList();
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
                result = new FnsResult<List<IFnsIeWarehouseSite>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }



        public IFnsResult<List<IFnsIeLocationType>> LocationTypeListGet()
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "LocationTypeListGet";
            var result = new FnsResult<List<IFnsIeLocationType>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                IE_LocationTypeCollection ieList = SosCrmDataContext.Instance.IE_LocationTypes.GetLocationTypeList();

                var resultList = ieList.Select(item => new FnsIeLocationType(item)).Cast<IFnsIeLocationType>().ToList();
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
                result = new FnsResult<List<IFnsIeLocationType>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<List<IFnsIeLocation>> LocationListGet(string locationTypeId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "LocationListGet";
            var result = new FnsResult<List<IFnsIeLocation>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {

                // ** retrieve list of purchase order items
                IE_LocationViewCollection ieList = SosCrmDataContext.Instance.IE_LocationViews.LocationGetByLocationTypeID(locationTypeId);

                var resultList = ieList.Select(item => new FnsIeLocation(item)).Cast<IFnsIeLocation>().ToList();
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
                result = new FnsResult<List<IFnsIeLocation>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }



        public IFnsResult<List<IFnsIeVendor>> VendorListGet()
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "VendorListGet";
            var result = new FnsResult<List<IFnsIeVendor>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {


                IE_VendorCollection ieList = SosCrmDataContext.Instance.IE_Vendors.GetVendorList();

                var resultList = ieList.Select(item => new FnsIeVendor(item)).Cast<IFnsIeVendor>().ToList();
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
                result = new FnsResult<List<IFnsIeVendor>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }

        public IFnsResult<List<IFnsIeProductBarcodeLocation>> ProductBarcodeLocationListGet(string locationId)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ProductBarcodeLocationListGet";
            var result = new FnsResult<List<IFnsIeProductBarcodeLocation>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {

                // ** retrieve list of purchase order items
                IE_ProductBarcodeLocationViewCollection ieList = SosCrmDataContext.Instance.IE_ProductBarcodeLocationViews.GetProductBarcodeLocationList(locationId);

                var resultList = ieList.Select(item => new FnsIeProductBarcodeLocation(item)).Cast<IFnsIeProductBarcodeLocation>().ToList();
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
                result = new FnsResult<List<IFnsIeProductBarcodeLocation>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }



        public IFnsResult<IFnsIeProductBarcodeLocation> ProductBarcodeLocationGetByPBID(string productBarcodeID)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "ProductBarcodeLocationGetByPBID";
            var result = new FnsResult<IFnsIeProductBarcodeLocation>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {

                // ** retrieve list of purchase order items
                IE_ProductBarcodeLocationView iePBLV = SosCrmDataContext.Instance.IE_ProductBarcodeLocationViews.GetProductBarcodeLocationByPBID(productBarcodeID);

                var resultValue = new FnsIeProductBarcodeLocation(iePBLV);
                // ** Build result

                // ** Save result information
                result.Code = (int)ErrorCodes.Success;
                result.Message = "Success";
                result.Value = resultValue;
            }
            #endregion TRY

            #region CATCH
            catch (Exception ex)
            {
                result = new FnsResult<IFnsIeProductBarcodeLocation>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }


        public IFnsResult<List<IFnsIePurchaseOrder>> PurchaseOrderListGetByVendorID(string vendorId, string count)
        {
            #region INITIALIZATION

            // ** Initialize 
            const string METHOD_NAME = "PurchaseOrderListGetByVendorID";
            var result = new FnsResult<List<IFnsIePurchaseOrder>>
            {
                Code = (int)ErrorCodes.GeneralMessage,
                Message = string.Format("Initializing {0}", METHOD_NAME),
            };

            #endregion INITIALIZATION

            #region TRY
            try
            {

                // ** retrieve list of purchase order items
                IE_PurchaseOrderCollection ieList = SosCrmDataContext.Instance.IE_PurchaseOrders.LoadByVendorId(vendorId, count);

                var resultList = ieList.Select(item => new FnsIePurchaseOrder(item)).Cast<IFnsIePurchaseOrder>().ToList();
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
                result = new FnsResult<List<IFnsIePurchaseOrder>>
                {
                    Code = (int)ErrorCodes.UnexpectedException,
                    Message = string.Format("Exception thrown at {0}: {1}", METHOD_NAME, ex.Message),
                };
            }
            #endregion CATCH
            // ** Return result

            return result;
        }


      }
    
}
