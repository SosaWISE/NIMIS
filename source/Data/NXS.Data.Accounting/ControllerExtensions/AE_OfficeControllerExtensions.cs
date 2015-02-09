using AR = NXS.Data.Accounting.AE_Office;
using ARCollection = NXS.Data.Accounting.AE_OfficeCollection;
using ARController = NXS.Data.Accounting.AE_OfficeController;

namespace NXS.Data.Accounting.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class AE_OfficeControllerExtensions
	{
		public static ARCollection GetAllActive(this ARController oCntlr)
		{
			return oCntlr.LoadCollection(AR.Query().WHERE(AR.Columns.IsActive, true));
		}
	}
}
