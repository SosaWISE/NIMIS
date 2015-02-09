using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.InventoryEngine;


namespace SOS.FunctionalServices.Contracts
{
	public interface IInventoryEngineService : IFunctionalService
	{

		IFnsResult<IFnsIePurchaseOrder> PurchaseOrderGetByGPPO(string gpPO, string gpEmployeeId);
        IFnsResult<List<IFnsIePurchaseOrder>> PurchaseOrderListGetByVendorID(string vendorId, string count);

        IFnsResult<IFnsIePurchaseOrder> PurchaseOrderGet(long id);
        IFnsResult<List<IFnsIePurchaseOrderItem>> PurchaseOrderItemsGet(long purchaseOrderID);
        IFnsResult<IFnsIePackingSlip> PackingSlipGetByPOID(long id);
        IFnsResult<List<IFnsIePackingSlip>> PackingSlipGetByGPPON(string gPPONumber);


        IFnsResult<IFnsIePackingSlip> PackingSlipCreate(IFnsIePackingSlip fnsHeader, string gpEmployeeID);
        IFnsResult<IFnsIeProductBarcode> ProductBarcodeGetByPBID(string id);

        IFnsResult<IFnsIeProductBarcode> ProductBarcodeCreate(IFnsIeProductBarcode fnsHeader, string gpEmployeeID);

        IFnsResult<IFnsIePackingSlipItem> PackingSlipItemCreate(IFnsIePackingSlipItem fnsHeader, string gpEmployeeID);

        IFnsResult<IFnsIeProductBarcodeTracking> ProductBarcodeTrackingCreate(IFnsIeProductBarcodeTracking fnsHeader, string gpEmployeeID);

        IFnsResult<IFnsIeProductBarcodeTracking> ProductBarcodeTrackingGetByPBID(long id);

        IFnsResult<IFnsIeProductBarcodeTrackingView> ProductBarcodeTrackingViewGetByPBTID(long id);

        IFnsResult<List<IFnsIeWarehouseSite>> WarehouseSiteListGet();

        IFnsResult<List<IFnsIeLocationType>> LocationTypeListGet();

        IFnsResult<List<IFnsIeVendor>> VendorListGet();

        IFnsResult<List<IFnsIeLocation>> LocationListGet(string locationTypeId);

        IFnsResult<List<IFnsIeProductBarcodeLocation>> ProductBarcodeLocationListGet(string locationId);

        IFnsResult<IFnsIeProductBarcodeLocation> ProductBarcodeLocationGetByPBID(string productBarcodeID);


	}
}