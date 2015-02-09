using SubSonic;
using AR = SOS.Data.HumanResource.RecruitUserView;
using ARCollection = SOS.Data.HumanResource.RecruitUserViewCollection;
using ARController = SOS.Data.HumanResource.RecruitUserViewController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
	public static class RecruitUserViewControllerExtensions
	{
		public static AR LoadByRecruitID(this ARController controller, int recruitID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.RecruitID, recruitID);

			return controller.LoadSingle(qry);
		}
		public static AR LoadByUserAndSeason(this ARController controller, int userID, int seasonID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.UserID, userID)
				.WHERE(AR.Columns.SeasonID, seasonID)
				.ORDER_BY(string.Format("{0} ASC", AR.Columns.IsDeletedRecruit));

			return controller.LoadSingle(qry);
		}
		public static ARCollection LoadByReportingLevelAndRoleLocation(this ARController controller, int reportingLevel, int roleLocationID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.ReportingLevel, reportingLevel)
				.WHERE(AR.Columns.RoleLocationID, roleLocationID)
				.RecruitNotDeleted()
				.Order();

			return controller.LoadCollection(qry);
		}
		public static ARCollection LoadByReportsToID(this ARController controller, int reportsToID)
		{
			return controller.LoadByReportsToID(reportsToID, null);
		}
		public static ARCollection LoadByReportsToID(this ARController controller, int reportsToID, int? reportingLevel)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.ReportsToID, reportsToID);

			if (reportingLevel.HasValue)
			{
				qry.WHERE(AR.Columns.ReportingLevel, reportingLevel.Value);
			}

			qry
				.RecruitNotDeleted()
				.Order();

			return controller.LoadCollection(qry);
		}
		public static ARCollection LoadTeamManagers(this ARController controller, int teamID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.TeamID, teamID)
				.RecruitNotDeleted()
				.Order();

			return controller.LoadCollection(qry);
		}

		#region Private Helper Extensions

		private static Query RecruitNotDeleted(this Query qry)
		{
			qry.WHERE(AR.Columns.IsDeletedRecruit, false);
			return qry;
		}
		private static Query Order(this Query qry)
		{
			qry.ORDER_BY(string.Format("{0} ASC", AR.Columns.FullName));
			return qry;
		}

		#endregion //Private Helper Methods
	}
}
