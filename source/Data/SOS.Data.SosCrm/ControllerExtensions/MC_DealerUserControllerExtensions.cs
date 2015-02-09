using AR = SOS.Data.SosCrm.MC_DealerUser;
using ARCollection = SOS.Data.SosCrm.MC_DealerUserCollection;
using ARController = SOS.Data.SosCrm.MC_DealerUserController;

namespace SOS.Data.SosCrm.ControllerExtensions
{
// ReSharper disable InconsistentNaming
	public static class MC_DealerUserControllerExtensions
// ReSharper restore InconsistentNaming
	{
		#region Member functions

		public static AR Authenticate(this ARController oCntlr, long lSeasonId, long lDealerId, string szUsername, string szPassword)
		{
			return
				oCntlr.LoadSingle(SosCrmDataStoredProcedureManager.MC_DealerUsersAuthenticate(lSeasonId, lDealerId, szUsername,
				                                                                              szPassword));
		}

		public static ARCollection GetByDealerID(this ARController oCntlr, int nDealerID)
		{
			/** Initialize. */
			return oCntlr.LoadCollection(SosCrmDataStoredProcedureManager.MC_DealerUserGetByDealerID(nDealerID));
		}

		#endregion Member functions
	}
}
