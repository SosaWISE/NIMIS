using AR = SOS.Data.SosCrm.MS_AccountPanelType;
using ARCollection = SOS.Data.SosCrm.MS_AccountPanelTypeCollection;
using ARController = SOS.Data.SosCrm.MS_AccountPanelTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
	// ReSharper disable once InconsistentNaming
	public static class MS_AccountPanelTypeControllerExtensions
	{
		public static ARCollection GetAll(this ARController cntlr)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.IsActive, true)
				.WHERE(AR.Columns.IsDeleted, false);

			return cntlr.LoadCollection(qry);
		}
	}
}
