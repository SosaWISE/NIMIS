using SOS.Data.Extensions;
using SOS.Data.HumanResource.Models;
using SOS.Lib.Util;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_Team;
using ARCollection = SOS.Data.HumanResource.RU_TeamCollection;
using ARController = SOS.Data.HumanResource.RU_TeamController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_TeamControllerExtensions
	{
		public static ARCollection LoadAllInOfficeWithRoleLocation(this ARController controller, int teamLocationID, int roleLocationID)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.TeamLocationId, teamLocationID)
				.WHERE(AR.Columns.RoleLocationId, roleLocationID)
				.Order();

			return controller.LoadCollection(qry);
		}
		public static ARCollection GetManageableTeams(this ARController controller, int seasonID, int roleLocationID, int recruitID, int? teamLocationID)
		{
			return controller.LoadCollection(HumanResourceDataStoredProcedureManager.VW_RecruitingStructureGetManageableTeams(seasonID, roleLocationID, recruitID, teamLocationID));
		}

		public static ARCollection GetAllThatCanMigrateToSeason(this ARController controller, int previousSeasonID, int currentSeasonID, bool excludeThoseAlreadyInSeason)
		{
			return controller.LoadCollection(
				HumanResourceDataStoredProcedureManager.RU_TeamsGetAllThatCanMigrateToSeason(previousSeasonID, currentSeasonID, excludeThoseAlreadyInSeason)
			);
		}

		public static TeamsViewCollection FindByTeamInfo(this ARController controller, TeamSearchInfo oTeamInfo)
		{
			// Locals
			var oResult = HumanResourceDataStoredProcedureManager.RU_TeamsFindTeams(
				oTeamInfo.Top
				, oTeamInfo.TeamID
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oTeamInfo.TeamName), oTeamInfo.SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oTeamInfo.OfficeName), oTeamInfo.SearchLike)
				, oTeamInfo.SeasonID
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oTeamInfo.SeasonName), oTeamInfo.SearchLike)
				, oTeamInfo.RoleLocationID
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oTeamInfo.City), oTeamInfo.SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oTeamInfo.StateAB), oTeamInfo.SearchLike)
				).ToCollectionView<TeamsView, TeamsViewCollection>();

			// Return result
			return oResult;
		}

		#region Private Helper Extensions

		private static Query Order(this Query qry)
		{
			qry.ORDER_BY(string.Format("{0} ASC", AR.Columns.Description));
			return qry;
		}

		#endregion //Private Helper Methods
	}
}
