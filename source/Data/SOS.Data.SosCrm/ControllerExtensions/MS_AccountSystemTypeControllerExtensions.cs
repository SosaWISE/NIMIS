using AR = SOS.Data.SosCrm.MS_AccountSystemType;
using ARCollection = SOS.Data.SosCrm.MS_AccountSystemTypeCollection;
using ARController = SOS.Data.SosCrm.MS_AccountSystemTypeController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_AccountSystemTypeControllerExtensions
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
