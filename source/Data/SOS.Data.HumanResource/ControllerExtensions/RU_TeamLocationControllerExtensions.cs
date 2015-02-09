using SOS.Data.Extensions;
using SOS.Data.HumanResource.Models;
using SOS.Lib.Util;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_TeamLocation;
using ARCollection = SOS.Data.HumanResource.RU_TeamLocationCollection;
using ARController = SOS.Data.HumanResource.RU_TeamLocationController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_TeamLocationControllerExtensions
	{
		public static AR GetBySeasonIdAndGPEmployeeID(this ARController cntlr, int seasonId, string gpEmployeeId)
		{
			return
				cntlr.LoadSingle(HumanResourceDataStoredProcedureManager.RU_TeamLocationsGetBySeasonIdAndGPEmployeeID(seasonId,
					gpEmployeeId));
		}

        public static ARCollection GetRuTeamLocationList(this ARController cntlr)
        {
            var qry = AR.Query();
                //.WHERE(AR.Columns.IsDeleted, false);

            return cntlr.LoadCollection(qry);
        }
		public static ARCollection LoadAll(this ARController controller)
		{
			var qry = ReadOnlyRecord<RU_TeamLocation>.Query();
			return controller.LoadCollection(qry);
		}

		public static ARCollection LoadAllOfficesUnderRegional(this ARController controller, int regionID)
		{
			return controller.LoadCollection(
				HumanResourceDataStoredProcedureManager.RU_TeamLocationsGetOfficesUnderRecruit(regionID, null)
			);
		}
		public static ARCollection LoadAllOfficesUnderNationalRegional(this ARController controller, int nationalRegionID)
		{
			return controller.LoadCollection(
				HumanResourceDataStoredProcedureManager.RU_TeamLocationsGetOfficesUnderRecruit(null, nationalRegionID)
			);
		}
		public static ARCollection LoadAllActiveOfficesInSeason(this ARController controller, int seasonID)
		{
			var qry = ReadOnlyRecord<RU_TeamLocation>.Query()
				.WHERE(AR.Columns.SeasonID, seasonID)
				.WHERE(AR.Columns.IsActive, true)
				.WHERE(AR.Columns.IsDeleted, false)
				.ORDER_BY(AR.Columns.Description, "ASC");

			return controller.LoadCollection(qry);
		}

		public static ARCollection GetAllThatCanMigrateToSeason(this ARController controller, int seasonID, bool excludeOfficesAlreadyInSeason)
		{
			return controller.LoadCollection(
				HumanResourceDataStoredProcedureManager.RU_TeamLocationsGetAllThatCanMigrateToSeason(seasonID, excludeOfficesAlreadyInSeason)
			);
		}
		public static void CopyOfficeStateMappings(this ARController controller, int fromTeamLocationID, int toTeamLocationID, int seasonID)
		{
			HumanResourceDataStoredProcedureManager.RU_TeamLocationsCopyOfficeStateMappings(fromTeamLocationID, toTeamLocationID, seasonID).ExecuteScalar();
		}

		public static AR GetNextOffice(this ARController controller, int previousSeasonTeamLocationID, int seasonID)
		{
			var qry = ReadOnlyRecord<RU_TeamLocation>.Query()
				.WHERE(AR.Columns.CreatedFromTeamLocationID, previousSeasonTeamLocationID)
				.AND(AR.Columns.SeasonID, seasonID);

			return controller.LoadSingle(qry);
		}

		public static bool ValidateState(this ARController controller, int nTeamLocationId, string szStateAB)
		{
			// Locals
			var bResult = false;

			var oDs =
				HumanResourceDataStoredProcedureManager.RU_TeamLocationsValidateStateMappings(nTeamLocationId, szStateAB).GetDataSet();

			if (oDs != null
				&& oDs.Tables.Count > 0
				&& oDs.Tables[0].Rows.Count > 0)
				bResult = true;

			// Return result
			return bResult;
		}

		public static AR GetByStateABAndSeason(this ARController controller, string szStateAB, int nSeasonID)
		{
			return controller.LoadSingle(HumanResourceDataStoredProcedureManager.RU_TeamLocationsGetByStateABAndSeasonID(szStateAB, nSeasonID));
		}

		public static RU_TeamLocationCollection GetAllBySeason(this ARController controller, int seasonID)
		{
			return controller.GetAllBySeason(seasonID, null, null);
		}
		public static RU_TeamLocationCollection GetAllBySeason(this ARController controller, int seasonID, bool? isActive, bool? isDeleted)
		{
			Query qry = AR.Query()
				.WHERE(AR.Columns.SeasonID, seasonID);

			if (isActive.HasValue)
				qry.WHERE(AR.Columns.IsActive, isActive.Value);

			if (isDeleted.HasValue)
				qry.WHERE(AR.Columns.IsDeleted, isDeleted.Value);

			qry.ORDER_BY(AR.Columns.Description);

			return controller.LoadCollection(qry);
		}

		public static RU_TeamLocationViewCollection FindByOfficeInfo(this ARController oCntlr, OfficeSearchInfo oOfficeInfo)
		{
			// Locals
			var oResult = HumanResourceDataStoredProcedureManager.RU_TeamLocationsFindOffices(
				oOfficeInfo.Top
				, oOfficeInfo.TeamLocationID
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oOfficeInfo.OfficeName), oOfficeInfo.SearchLike)
				, oOfficeInfo.SeasonID
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oOfficeInfo.SeasonName), oOfficeInfo.SearchLike)
				, oOfficeInfo.MarketID
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oOfficeInfo.MarketName), oOfficeInfo.SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oOfficeInfo.City), oOfficeInfo.SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oOfficeInfo.StateAB), oOfficeInfo.SearchLike)
				, oOfficeInfo.TimeZoneID
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(oOfficeInfo.TimeZoneName), oOfficeInfo.SearchLike)
				).ToCollectionView<RU_TeamLocationView, RU_TeamLocationViewCollection>();

			// Return result
			return oResult;
		}

	}
}
