using SubSonic;
using AR = SOS.Data.HumanResource.RU_Season;
using ARCollection = SOS.Data.HumanResource.RU_SeasonCollection;
using ARController = SOS.Data.HumanResource.RU_SeasonController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_SeasonControllerExtensions
	{
		public static ARCollection GetAllSeasonsByUserID(this ARController cntlr, int userID)
		{
			return cntlr.LoadCollection(HumanResourceDataStoredProcedureManager.RU_SeasonGetActiveSeasonsForAUserByUserID(userID));
		}
		public static ARCollection LoadAll(this ARController controller)
		{
			SqlQuery qry = Select.AllColumnsFrom<RU_Season>();
			qry.OrderBys.Add(string.Format("{0} DESC", AR.StartDateColumn.QualifiedName));

			return controller.LoadCollection(qry);
		}

		public static AR LoadCurrentSeason(this ARController controller)
		{
			Query qry = AR.Query().WHERE(AR.Columns.IsCurrent, true);

			return controller.LoadSingle(qry);
		}
		public static ARCollection LoadExistingForUser(this ARController controller, int userID, bool? canShowInHiringManager)
		{
			SqlQuery qry = Select.AllColumnsFrom<RU_Season>()
				.Where(AR.Columns.SeasonID).In(RU_RecruitControllerExtensions.ActiveRecruitSeasonIDs(userID));

			if (canShowInHiringManager.HasValue)
			{
				qry.And(AR.Columns.ShowInHiringManager).IsEqualTo(canShowInHiringManager.Value);
			}

			qry.OrderBys.Add(string.Format("{0} DESC", AR.StartDateColumn.QualifiedName));

			return controller.LoadCollection(qry);
		}
		public static ARCollection LoadNonExistingForUser(this ARController controller, int userID, bool? canShowInHiringManager)
		{
			SqlQuery qry = Select.AllColumnsFrom<RU_Season>()
				.Where(AR.Columns.SeasonID).NotIn(RU_RecruitControllerExtensions.ActiveRecruitSeasonIDs(userID));

			if (canShowInHiringManager.HasValue)
			{
				qry.And(AR.Columns.ShowInHiringManager).IsEqualTo(canShowInHiringManager.Value);
			}

			qry.OrderBys.Add(string.Format("{0} DESC", AR.StartDateColumn.QualifiedName));

			return controller.LoadCollection(qry);
		}

		private static RU_Season _currentOrVisible;
		private static readonly object _syncCurrentOrVisible = new object();
		public static AR GetCurrentOrVisible(this ARController controller)
		{
			lock (_syncCurrentOrVisible)
			{
				if (_currentOrVisible == null)
				{
					_currentOrVisible = controller.LoadSingle(HumanResourceDataStoredProcedureManager.RU_SeasonGetCurrentOrVisibleToRecruit());
				}
			}
			return _currentOrVisible;
		}


		public static ARCollection GetAllActiveAndVisibleInHiringManager(this ARController controller)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.IsDeleted, false)
				.AND(AR.Columns.ShowInHiringManager, true)
				.ORDER_BY(AR.Columns.SeasonID);

			return controller.LoadCollection(qry);
		}

		public static string GetScoreStatusBySeasonID(this ARController oCntlr, short sScore, int nSeasonId)
		{
			// Locals
			const string SZ_RESULT = "FAILED";
			var oSeason = oCntlr.LoadByPrimaryKey(nSeasonId);

			if (sScore >= oSeason.ExcellentCreditScoreThreshold)
				return "EXCELLENT";

			if (sScore >= oSeason.PassCreditScoreThreshold)
				return "PASS";

			if (sScore >= oSeason.SubCreditScoreThreshold)
				return "SUB";

			// Return result
			return SZ_RESULT;
		}
	}
}
