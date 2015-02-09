using SOS.Lib.Util;
using AR = SOS.Data.SosCrm.IE_LocationType;
using ARCollection = SOS.Data.SosCrm.IE_LocationTypeCollection;
using ARController = SOS.Data.SosCrm.IE_LocationTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
    public static class IE_LocationTypeControllerExtensions
	{

        public static ARCollection GetLocationTypeList(this ARController oCntlr)
        {
            /** Initialize. */
            var oQry = AR.Query().ORDER_BY(AR.Columns.LocationTypeName);

            /** Return result. */
            return oCntlr.LoadCollection(oQry);
        }
	}
}
