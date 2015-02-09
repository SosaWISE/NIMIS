using AR = SOS.Data.SosCrm.IE_ProductBarcodeTracking;
using ARCollection = SOS.Data.SosCrm.IE_ProductBarcodeTrackingCollection;
using ARController = SOS.Data.SosCrm.IE_ProductBarcodeTrackingController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_ProductBarcodeTrackingControllerExtensions
	{


        public static AR GetByPBID(this ARController oCntlr, long lProductBarcodeId)
		{
			/** Initialize. */
            var oQuery = AR.Query()
                .WHERE(AR.Columns.ProductBarcodeId, lProductBarcodeId);

			/** Execute. */
			var oResult = oCntlr.LoadSingle(oQuery);

			/** REturn result. */
			return oResult;
		}

        public static AR CreateProductBarcodeTracking(this ARController cntlr, 
            string productBarcodeTrackingTypeId, 
            string productBarcodeId, 
            string locationTypeID,
            string locationID,
            string comment,
            string gpEmployeeId 
            )
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.IE_ProductBarcodeTrackingCreate(
                productBarcodeTrackingTypeId,
                productBarcodeId, 
                locationTypeID,
                locationID,
                comment,
                gpEmployeeId));
        }

	}
}
