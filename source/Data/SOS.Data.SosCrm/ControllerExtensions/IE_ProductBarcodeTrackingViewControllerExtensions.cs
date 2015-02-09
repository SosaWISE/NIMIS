using AR = SOS.Data.SosCrm.IE_ProductBarcodeTrackingView;
using ARCollection = SOS.Data.SosCrm.IE_ProductBarcodeTrackingViewCollection;
using ARController = SOS.Data.SosCrm.IE_ProductBarcodeTrackingViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
    public static class IE_ProductBarcodeTrackingViewControllerExtensions
    {
        public static AR GetByPBTID(this ARController cntlr, long productBarcodeTrackingId)
        {
            /** Initialize. */
            var oQry = AR.Query().WHERE(AR.Columns.ProductBarcodeTrackingID, productBarcodeTrackingId);

            /** Return result. */
            return cntlr.LoadSingle(oQry);
        }

    }
	
}
