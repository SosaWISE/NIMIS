using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.IE_ProductBarcode;
using ARCollection = SOS.Data.SosCrm.IE_ProductBarcodeCollection;
using ARController = SOS.Data.SosCrm.IE_ProductBarcodeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_ProductBarcodeControllerExtensions
	{
        public static AR GetByPBID(this ARController oCntlr, string lProductBarcodeId)
        {
            /** Initialize. */
            var oQuery = AR.Query()
                .WHERE(AR.Columns.ProductBarcodeID, lProductBarcodeId)
                .WHERE(AR.Columns.IsDeleted, false);   //do not include deleted ProductBarcode
                

            /** Execute. */
            var oResult = oCntlr.LoadSingle(oQuery);

            /** REturn result. */
            return oResult;
        }


        public static AR CreateProductBarcode(this ARController cntlr,string productBarcodeID , long purchaseOrderItemId, string simGUID,string gpEmployeeId)
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.IE_ProductBarcodeCreate( productBarcodeID ,  purchaseOrderItemId, gpEmployeeId));
        }

	}
}
