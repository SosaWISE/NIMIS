using AR = SOS.Data.SosCrm.MS_EmergencyContact;
using ARCollection = SOS.Data.SosCrm.MS_EmergencyContactCollection;
using ARController = SOS.Data.SosCrm.MS_EmergencyContactController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class MS_EmergencyContactControllerExtensions
	{
		public static AR LoadByPrimaryKeySafe(this ARController cntlr, long id)
		{
			/** Initialize. */
			var emc = cntlr.LoadByPrimaryKey(id);

			if (emc == null) return null;

			return emc.IsDeleted ? null : emc;
		}

		public static ARCollection ByAccountId(this ARController cntlr, long accountId)
		{
			var qry = AR.Query()
				.WHERE(AR.Columns.AccountId, accountId)
				.WHERE(AR.Columns.IsActive, true)
				.WHERE(AR.Columns.IsDeleted, false);

			return cntlr.LoadCollection(qry);
		}
	}
}
