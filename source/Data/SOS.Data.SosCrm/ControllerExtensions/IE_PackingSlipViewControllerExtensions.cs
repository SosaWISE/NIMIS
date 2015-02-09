using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.IE_PackingSlipView;
using ARCollection = SOS.Data.SosCrm.IE_PackingSlipViewCollection;
using ARController = SOS.Data.SosCrm.IE_PackingSlipViewController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_PackingSlipViewControllerExtensions
	{

        public static ARCollection GetByGPPON(this ARController oCntlr, string gPPONumber)
        {
            /** Initialize. */
            var oQuery = AR.Query()
                .WHERE(AR.Columns.GPPONumber, gPPONumber);

            /** Execute. */
            var oResult = oCntlr.LoadCollection(oQuery);

            /** REturn result. */
            return oResult;
        }

	}
}
