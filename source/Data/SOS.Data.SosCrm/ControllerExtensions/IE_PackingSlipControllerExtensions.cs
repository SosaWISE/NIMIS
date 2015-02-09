using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.IE_PackingSlip;
using ARCollection = SOS.Data.SosCrm.IE_PackingSlipCollection;
using ARController = SOS.Data.SosCrm.IE_PackingSlipController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_PackingSlipControllerExtensions
	{


        public static AR GetByPOID(this ARController oCntlr, long lPurchaseOrderId)
		{
			/** Initialize. */
            var oQuery = AR.Query()
                .WHERE(AR.Columns.PurchaseOrderId, lPurchaseOrderId);

			/** Execute. */
			var oResult = oCntlr.LoadSingle(oQuery);

			/** REturn result. */
			return oResult;
		}

        //public static AR GetByGPPON(this ARController oCntlr, string gPPONumber)
        //{
        //    /** Initialize. */
        //    var oQuery = AR.Query()
        //        .WHERE(AR.Columns.g, gPPONumber);

        //    /** Execute. */
        //    var oResult = oCntlr.LoadSingle(oQuery);

        //    /** REturn result. */
        //    return oResult;
        //}



        public static AR CreatePackingSlip(this ARController cntlr, string packingSlipNumber, long purchaseOrderId,string gpEmployeeId)
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.IE_PackingSlipCreate(packingSlipNumber, purchaseOrderId, gpEmployeeId));
        }

	}
}
