using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.IE_PackingSlipItem;
using ARCollection = SOS.Data.SosCrm.IE_PackingSlipItemCollection;
using ARController = SOS.Data.SosCrm.IE_PackingSlipItemController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_PackingSlipItemControllerExtensions
	{

        public static AR CreatePackingSlipItem(this ARController cntlr,int packingSlipId, string productSkwId, string itemId, int quantity,string gpEmployeeId)
        {
            return
                cntlr.LoadSingle(SosCrmDataStoredProcedureManager.IE_PackingSlipItemCreate(packingSlipId, productSkwId, itemId, quantity, gpEmployeeId));
        }

	}
}
