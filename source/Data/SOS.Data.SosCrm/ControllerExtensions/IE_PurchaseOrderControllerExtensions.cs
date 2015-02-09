using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.IE_PurchaseOrder;
using ARCollection = SOS.Data.SosCrm.IE_PurchaseOrderCollection;
using ARController = SOS.Data.SosCrm.IE_PurchaseOrderController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_PurchaseOrderControllerExtensions
	{
		

		public static AR LoadByPrimaryKey( this ARController oCntlr, long lPurchaseOrderID)
		{
			/** Initialize. */
            var oQuery = AR.Query()
                .WHERE(AR.Columns.PurchaseOrderID, lPurchaseOrderID);

			/** Execute. */
			var oResult = oCntlr.LoadSingle(oQuery);

			/** REturn result. */
			return oResult;
		}

	    public static AR LoadByGPPurchaseOrderNumber(this ARController cntlr, string gpPO, string gpEmployeeId)
	    {
		    return cntlr.LoadSingle(SosCrmDataStoredProcedureManager.IE_PurchaseOrdersGet(gpPO, gpEmployeeId));
	    }


        public static ARCollection LoadByVendorId(this ARController oCntlr, string vendorId, string top)
        {
            /** Initialize. */
            var oQuery = AR.Query()

                .WHERE(AR.Columns.VendorId, vendorId).ORDER_BY(AR.Columns.ModifiedOn).SetTop(top);


            /** Execute. */
            var oResult = oCntlr.LoadCollection(oQuery);

            /** REturn result. */
            return oResult;
        }

	}
}
