using SubSonic;
using AR = SOS.Data.HumanResource.RU_Recruit;
using ARCollection = SOS.Data.HumanResource.RU_RecruitCollection;
using ARController = SOS.Data.HumanResource.RU_RecruitController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_RecruitControllerExtensions
	{
		public static ARCollection ForUser(this ARController controller, int userID)
		{
			SqlQuery qry = Select.AllColumnsFrom<AR>()
				.InnerJoin("RU_Season", RU_Season.Columns.SeasonID, "RU_Recruits", AR.Columns.SeasonId)
				.Where(AR.Columns.UserId).IsEqualTo(userID);

			qry.OrderBys.Add(string.Format("{0} DESC", RU_Season.StartDateColumn.QualifiedName));
			qry.OrderBys.Add(string.Format("{0} ASC", AR.IsDeletedColumn.QualifiedName));

			return controller.LoadCollection(qry);
		}

		public static ARCollection LastByUserAndSeason(this ARController controller, int userID, int seasonID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.UserId, Comparison.Equals, userID)
				.WHERE(AR.Columns.SeasonId, Comparison.Equals, seasonID)
				.ORDER_BY(AR.Columns.IsDeleted);

			return controller.LoadCollection(qry);
		}

		public static AR GetActiveRecruitForSeason(this ARController controller, int userID, int seasonID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.UserId, userID)
				.WHERE(AR.Columns.SeasonId, seasonID)
				.WHERE(AR.Columns.IsDeleted, false);

			return controller.LoadSingle(qry);
		}
		public static ARCollection LastByUserSeasonAndUserType(this ARController controller, int userID, int seasonID, short userTypeID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.UserId, Comparison.Equals, userID)
				.WHERE(AR.Columns.SeasonId, Comparison.Equals, seasonID)
				.WHERE(AR.Columns.UserTypeId, Comparison.Equals, userTypeID)
				.ORDER_BY(AR.Columns.IsDeleted);

			return controller.LoadCollection(qry);
		}

		public static bool HasUndeletedRecruitInSameSeason(this ARController controller, RU_Recruit recruit)
		{
			if (recruit.UserId == 5799)
				return false;

			Query qry = AR.Query()
				.WHERE(AR.Columns.UserId, recruit.UserId)
				.WHERE(AR.Columns.SeasonId, recruit.SeasonId)
				.NotDeleted();

			ARCollection list = controller.LoadCollection(qry);
			bool hasUndeletedRecruit = false;
			foreach (var item in list)
			{
				if (item.RecruitID != recruit.RecruitID)
				{
					hasUndeletedRecruit = true;
					break;
				}
			}
			return hasUndeletedRecruit;
		}
		public static ARCollection GetMigrationRecruits(this ARController controller, int fromSeasonID, int toSeasonID, short userTypeID, bool excludeAlreadyInSeason)
		{
			return controller.LoadCollection(
				HumanResourceDataStoredProcedureManager.RU_RecruitsGetMigrationRecruits(fromSeasonID, toSeasonID, userTypeID, excludeAlreadyInSeason)
			);
		}

		public static AR GetByGPEmployeeID(this ARController controller, string gpEmployeeID, int seasonID)
		{
			return controller.LoadSingle(HumanResourceDataStoredProcedureManager.RU_RecruitsGetByGPEmployeeId(gpEmployeeID, seasonID));
		}

		#region Private Helper Extensions

		private static Query NotDeleted(this Query qry)
		{
			qry.WHERE(AR.Columns.IsDeleted, false);
			return qry;
		}

		#endregion //Private Helper Methods

		#region Public Queries

		public static SqlQuery ActiveRecruitSeasonIDs(int userID)
		{
			return new Select(AR.Columns.SeasonId).From(AR.Schema)
				.Where(AR.Columns.UserId).IsEqualTo(userID)
				.And(AR.Columns.IsDeleted).IsEqualTo(false);
		}

		#endregion //Public Queries
	}
}
