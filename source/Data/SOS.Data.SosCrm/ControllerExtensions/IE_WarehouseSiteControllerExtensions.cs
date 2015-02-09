using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.IE_WarehouseSite;
using ARCollection = SOS.Data.SosCrm.IE_WarehouseSiteCollection;
using ARController = SOS.Data.SosCrm.IE_WarehouseSiteController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_WarehouseSiteControllerExtensions
	{

        public static ARCollection GetWarehouseSiteList(this ARController oCntlr)
        {
            /** Initialize. */
            var oQry = AR.Query().ORDER_BY(AR.Columns.WarehouseSiteName);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }
	}
}
