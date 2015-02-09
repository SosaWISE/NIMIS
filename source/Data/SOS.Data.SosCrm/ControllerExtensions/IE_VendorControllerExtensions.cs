using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.IE_Vendor;
using ARCollection = SOS.Data.SosCrm.IE_VendorCollection;
using ARController = SOS.Data.SosCrm.IE_VendorController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_VendorControllerExtensions
	{

        public static ARCollection GetVendorList(this ARController oCntlr)
        {
            /** Initialize. */
            var oQry = AR.Query().WHERE(AR.Columns.IsDeleted,false).ORDER_BY(AR.Columns.VendorName);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }
	}
}
