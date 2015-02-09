using SubSonic;
using AR = NXS.Data.Inventory.IV_Office;
using ARCollection = NXS.Data.Inventory.IV_OfficeCollection;
using ARController = NXS.Data.Inventory.IV_OfficeController;

namespace NXS.Data.Inventory.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class IV_OfficeControllerExtentions
	{
		public static ARCollection GetAllActive(this ARController oCntrl)
		{
			var oQry = AR.Query();

			oQry.WHERE(AR.Columns.IsActive, Comparison.Equals, true)
				.WHERE(AR.Columns.IsDeleted, Comparison.Equals, false)
				.ORDER_BY(AR.Columns.OfficeName);

			var oList = oCntrl.LoadCollection(oQry);
			return oList;
		}
	}
}
