using AR = SOS.Data.SosCrm.IE_ProductBarcodeLocationView;
using ARCollection = SOS.Data.SosCrm.IE_ProductBarcodeLocationViewCollection;
using ARController = SOS.Data.SosCrm.IE_ProductBarcodeLocationViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class IE_ProductBarcodeLocationViewControllerExtensions
	{

		public static ARCollection GetProductBarcodeLocationList(this ARController oCntlr, string locationId)
		{
			/** Initialize. */
            var oQry = AR.Query().WHERE(AR.Columns.LocationID, locationId);

			/** Return result. */
			return oCntlr.LoadCollection(oQry);
		}


        public static AR GetProductBarcodeLocationByPBID(this ARController oCntlr, string productBarcodeID)
        {
            /** Initialize. */
            var oQry = AR.Query().WHERE(AR.Columns.ProductBarcodeId, productBarcodeID);

            /** Return result. */
            return oCntlr.LoadSingle(oQry);
        }
      
	}
}