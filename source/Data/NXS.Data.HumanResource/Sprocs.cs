using Dapper;
using NXS.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NXS.Data.HumanResource
{
	public partial class Sprocs
	{
		private readonly DBase db;
		public Sprocs(DBase db)
		{
			this.db = db;
		}

		public Task<IEnumerable<T>> DocLinkGetDocumentsByID<T>(int? DocumentID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@DocumentID", DocumentID);
			return db.QueryAsync<T>("custDocLinkGetDocumentsByID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> Report_CreditAndInstalls<T>(int? officeId,DateTime? startDate,DateTime? endDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@officeId", officeId);
			p.Add("@startDate", startDate);
			p.Add("@endDate", endDate);
			return db.QueryAsync<T>("custReport_CreditAndInstalls", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitGetAllTechsBySeasonID<T>(int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitGetAllTechsBySeasonID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitGetOfficeNameByGPID<T>(string GPEmployeeID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitGetOfficeNameByGPID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitingStructureGetManageableTeams<T>(int? SeasonID,int? RoleLocationID,int? RecruitID,int? TeamLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@RecruitID", RecruitID);
			p.Add("@TeamLocationID", TeamLocationID);
			return db.QueryAsync<T>("custRU_RecruitingStructureGetManageableTeams", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsActivateByGPIdAndSeasonId<T>(int? SeasonId,string GPID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonId", SeasonId);
			p.Add("@GPID", GPID);
			return db.QueryAsync<T>("custRU_RecruitsActivateByGPIdAndSeasonId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsCopyWithNewSeasonID<T>(int? RecruitId,int? SeasonId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitId", RecruitId);
			p.Add("@SeasonId", SeasonId);
			return db.QueryAsync<T>("custRU_RecruitsCopyWithNewSeasonID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsFindBy<T>(string SeasonIDList,string FirstName,string LastName)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonIDList", SeasonIDList);
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			return db.QueryAsync<T>("custRU_RecruitsFindBy", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsFindRecruitsInTeams<T>(string FirstName,string LastName,string TeamIDList,string DeletionStatus)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			p.Add("@TeamIDList", TeamIDList);
			p.Add("@DeletionStatus", DeletionStatus);
			return db.QueryAsync<T>("custRU_RecruitsFindRecruitsInTeams", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetActiveSeasonAccounts<T>(string GPEmployeeID,int? SeasonID,bool? IsCanceled,bool? IsDelinquent,bool? HasRepHold,bool? HasTechHold)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			p.Add("@SeasonID", SeasonID);
			p.Add("@IsCanceled", IsCanceled);
			p.Add("@IsDelinquent", IsDelinquent);
			p.Add("@HasRepHold", HasRepHold);
			p.Add("@HasTechHold", HasTechHold);
			return db.QueryAsync<T>("custRU_RecruitsGetActiveSeasonAccounts", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetAllSeasonRankings<T>(DateTime? StartDate,DateTime? EndDate,string SeasonIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			p.Add("@SeasonIDList", SeasonIDList);
			return db.QueryAsync<T>("custRU_RecruitsGetAllSeasonRankings", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetBuddyByGPEmpoyeeID<T>(string GPEmployeeID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetBuddyByGPEmpoyeeID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetByGPEmployeeId<T>(string GPEmployeeID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetByGPEmployeeId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetByGPEmployeeIdRaw<T>(string GPEmployeeID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetByGPEmployeeIdRaw", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetByGPEmployeeIdWithNoSeasonID<T>(string GPEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			return db.QueryAsync<T>("custRU_RecruitsGetByGPEmployeeIdWithNoSeasonID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw<T>(string GPEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			return db.QueryAsync<T>("custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetCompetitiveRankings<T>(int? UserID,DateTime? StartDate,DateTime? EndDate,string SeasonIDList,int? RoleLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			p.Add("@SeasonIDList", SeasonIDList);
			p.Add("@RoleLocationID", RoleLocationID);
			return db.QueryAsync<T>("custRU_RecruitsGetCompetitiveRankings", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetDateStats<T>(int? RecruitID,DateTime? WeekStartDate,DateTime? WeekEndDate,DateTime? MonthStartDate,DateTime? MonthEndDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitID", RecruitID);
			p.Add("@WeekStartDate", WeekStartDate);
			p.Add("@WeekEndDate", WeekEndDate);
			p.Add("@MonthStartDate", MonthStartDate);
			p.Add("@MonthEndDate", MonthEndDate);
			return db.QueryAsync<T>("custRU_RecruitsGetDateStats", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetManagersBySeasonId<T>(int? SeasonId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonId", SeasonId);
			return db.QueryAsync<T>("custRU_RecruitsGetManagersBySeasonId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetManagersRoster<T>(int? seasonID,int? oldSeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@seasonID", seasonID);
			p.Add("@oldSeasonID", oldSeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetManagersRoster", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetMigrationRecruits<T>(int? MigrateFromSeasonID,int? MigrateToSeasonID,int? UserTypeID,bool? ExcludeAlreadyInSeason)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@MigrateFromSeasonID", MigrateFromSeasonID);
			p.Add("@MigrateToSeasonID", MigrateToSeasonID);
			p.Add("@UserTypeID", UserTypeID);
			p.Add("@ExcludeAlreadyInSeason", ExcludeAlreadyInSeason);
			return db.QueryAsync<T>("custRU_RecruitsGetMigrationRecruits", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetNewestRecruit<T>(int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_RecruitsGetNewestRecruit", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetOAEmailByTeamLocationID<T>(int? TeamLocationID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetOAEmailByTeamLocationID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetOrphanRecruitsByManagersAndSeason<T>(int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetOrphanRecruitsByManagersAndSeason", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetPayrollStats<T>(int? RecruitID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitID", RecruitID);
			return db.QueryAsync<T>("custRU_RecruitsGetPayrollStats", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetPointBankBySeasonId<T>(string GPEmployeeID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetPointBankBySeasonId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetPossibleReportTos<T>(int? SeasonID,int? UserTypeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@UserTypeID", UserTypeID);
			return db.QueryAsync<T>("custRU_RecruitsGetPossibleReportTos", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRankingInTeam<T>(int? recruitID,int? roleLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@recruitID", recruitID);
			p.Add("@roleLocationID", roleLocationID);
			return db.QueryAsync<T>("custRU_RecruitsGetRankingInTeam", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRankings<T>(DateTime? StartDate,DateTime? EndDate,string SeasonIDList,int? RoleLocationID,int? MaxRankToReturn,int? UserID,bool? IncludeCancels)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			p.Add("@SeasonIDList", SeasonIDList);
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@MaxRankToReturn", MaxRankToReturn);
			p.Add("@UserID", UserID);
			p.Add("@IncludeCancels", IncludeCancels);
			return db.QueryAsync<T>("custRU_RecruitsGetRankings", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRankInSeason<T>(int? SeasonID,int? UserID,int? RoleLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@UserID", UserID);
			p.Add("@RoleLocationID", RoleLocationID);
			return db.QueryAsync<T>("custRU_RecruitsGetRankInSeason", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitAllInfoByRecruitId<T>(int? RecruitId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitId", RecruitId);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitAllInfoByRecruitId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitByEmailAndSeasonID<T>(string Email,string SeasonIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Email", Email);
			p.Add("@SeasonIDList", SeasonIDList);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitByEmailAndSeasonID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitByUserAndSeasonIDs<T>(int? UserID,string SeasonIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@SeasonIDList", SeasonIDList);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitByUserAndSeasonIDs", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitIDsForBrackets<T>(string BracketType)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@BracketType", BracketType);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitIDsForBrackets", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitIDsForBrackets2010<T>(string BracketType)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@BracketType", BracketType);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitIDsForBrackets2010", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitNames<T>(int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitNames", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitsByTeam<T>(int? TeamID,int? RoleLocationID,string DeletionStatus)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamID", TeamID);
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@DeletionStatus", DeletionStatus);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitsByTeam", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitsByTeamLocation<T>(int? TeamLocationID,int? RoleLocationID,string DeletionStatus)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@DeletionStatus", DeletionStatus);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitsByTeamLocation", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitsByTeamLocationAndUserTypeID<T>(string TeamLocationIDList,int? UserTypeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationIDList", TeamLocationIDList);
			p.Add("@UserTypeID", UserTypeID);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList<T>(string TeamIDList,int? UserTypeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamIDList", TeamIDList);
			p.Add("@UserTypeID", UserTypeID);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitSeasonSalesTotals<T>(string GPEmployeeID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitSeasonSalesTotals", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitSeasonsMaps<T>(int? FromSeasonID,int? ToSeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FromSeasonID", FromSeasonID);
			p.Add("@ToSeasonID", ToSeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitSeasonsMaps", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRecruitsToMigrate<T>(int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetRecruitsToMigrate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRegionalsBySeasonID<T>(int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetRegionalsBySeasonID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetReportingTree<T>(string Type,int? TypeID,bool? HasOwnTeam,int? TeamID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Type", Type);
			p.Add("@TypeID", TypeID);
			p.Add("@HasOwnTeam", HasOwnTeam);
			p.Add("@TeamID", TeamID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetReportingTree", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetRepSalesTotals<T>(DateTime? SnapShotDate,int? SeasonID,DateTime? ExactDate,DateTime? StopDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SnapShotDate", SnapShotDate);
			p.Add("@SeasonID", SeasonID);
			p.Add("@ExactDate", ExactDate);
			p.Add("@StopDate", StopDate);
			return db.QueryAsync<T>("custRU_RecruitsGetRepSalesTotals", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetSalesPayrollDeductionsReport<T>(int? ID,string IDType)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ID", ID);
			p.Add("@IDType", IDType);
			return db.QueryAsync<T>("custRU_RecruitsGetSalesPayrollDeductionsReport", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetSalesTotals<T>(int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_RecruitsGetSalesTotals", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetStats<T>(int? RecruitID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitID", RecruitID);
			return db.QueryAsync<T>("custRU_RecruitsGetStats", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsGetSummaryReport<T>(int? RecruitID,int? SeasonID,DateTime? StartDate,DateTime? EndDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitID", RecruitID);
			p.Add("@SeasonID", SeasonID);
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			return db.QueryAsync<T>("custRU_RecruitsGetSummaryReport", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsLoadByRawSQL<T>(string FirstName,string LastName,string PhoneCell,string PhoneHome,string SSN,string BirthDate,int? SeasonId,string Mode)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			p.Add("@PhoneCell", PhoneCell);
			p.Add("@PhoneHome", PhoneHome);
			p.Add("@SSN", SSN);
			p.Add("@BirthDate", BirthDate);
			p.Add("@SeasonId", SeasonId);
			p.Add("@Mode", Mode);
			return db.QueryAsync<T>("custRU_RecruitsLoadByRawSQL", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsNewRecruits<T>(DateTime? StartDate,DateTime? EndDate,string UserTypeIDList,string SeasonIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			p.Add("@UserTypeIDList", UserTypeIDList);
			p.Add("@SeasonIDList", SeasonIDList);
			return db.QueryAsync<T>("custRU_RecruitsNewRecruits", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_RecruitsShowSeasonStatusesByUserId<T>(int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_RecruitsShowSeasonStatusesByUserId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_SeasonGetActiveSeasonsForAUserByUserID<T>(int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_SeasonGetActiveSeasonsForAUserByUserID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags<T>(int? UserID,bool? IsCurrent,bool? IsVisibleToRecruits,bool? IsInsideSales,bool? IsPreseason,bool? IsSummer,bool? IsExtended,bool? IsYearRound)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@IsCurrent", IsCurrent);
			p.Add("@IsVisibleToRecruits", IsVisibleToRecruits);
			p.Add("@IsInsideSales", IsInsideSales);
			p.Add("@IsPreseason", IsPreseason);
			p.Add("@IsSummer", IsSummer);
			p.Add("@IsExtended", IsExtended);
			p.Add("@IsYearRound", IsYearRound);
			return db.QueryAsync<T>("custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_SeasonGetActiveUserSeasons<T>(int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_SeasonGetActiveUserSeasons", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_SeasonGetAllSeasonsForAUserByUserID<T>(int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_SeasonGetAllSeasonsForAUserByUserID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_SeasonGetCurrentOrVisibleToRecruit<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custRU_SeasonGetCurrentOrVisibleToRecruit", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_SeasonGetMissingSeasonsByUserID<T>(int? UserId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserId", UserId);
			return db.QueryAsync<T>("custRU_SeasonGetMissingSeasonsByUserID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_SeasonGetVisibleUserSeasons<T>(int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_SeasonGetVisibleUserSeasons", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_SeasonsGetNormalSalesSeasons<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custRU_SeasonsGetNormalSalesSeasons", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationGetManagersByTeamLocation<T>(int? TeamLocationID,int? RoleLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@RoleLocationID", RoleLocationID);
			return db.QueryAsync<T>("custRU_TeamLocationGetManagersByTeamLocation", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsCopyOfficeStateMappings<T>(int? FromTeamLocationID,int? ToTeamLocationID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FromTeamLocationID", FromTeamLocationID);
			p.Add("@ToTeamLocationID", ToTeamLocationID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_TeamLocationsCopyOfficeStateMappings", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsFindOffices<T>(int? Top,int? TeamLocationID,string OfficeName,int? SeasonID,string SeasonName,int? MarketID,string MarketName,string City,string StateAB,int? TimeZoneID,string TimeZoneName)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Top", Top);
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@OfficeName", OfficeName);
			p.Add("@SeasonID", SeasonID);
			p.Add("@SeasonName", SeasonName);
			p.Add("@MarketID", MarketID);
			p.Add("@MarketName", MarketName);
			p.Add("@City", City);
			p.Add("@StateAB", StateAB);
			p.Add("@TimeZoneID", TimeZoneID);
			p.Add("@TimeZoneName", TimeZoneName);
			return db.QueryAsync<T>("custRU_TeamLocationsFindOffices", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetAllThatCanMigrateToSeason<T>(int? MigrateToSeasonID,bool? ExcludeOfficesAlreadyInSeason)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@MigrateToSeasonID", MigrateToSeasonID);
			p.Add("@ExcludeOfficesAlreadyInSeason", ExcludeOfficesAlreadyInSeason);
			return db.QueryAsync<T>("custRU_TeamLocationsGetAllThatCanMigrateToSeason", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetBySeasonIdAndGPEmployeeID<T>(int? SeasonId,string GPEmployeeId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonId", SeasonId);
			p.Add("@GPEmployeeId", GPEmployeeId);
			return db.QueryAsync<T>("custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetByStateABAndSeasonID<T>(string StateAB,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StateAB", StateAB);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_TeamLocationsGetByStateABAndSeasonID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetByTeamIDs<T>(string TeamIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamIDList", TeamIDList);
			return db.QueryAsync<T>("custRU_TeamLocationsGetByTeamIDs", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetCreditsRanTotals<T>(int? TeamLocationID,bool? ExcludeSubs,int? WeekDateFirst,bool? ByWeek,bool? ShowOnlyActiveInRoster)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@ExcludeSubs", ExcludeSubs);
			p.Add("@WeekDateFirst", WeekDateFirst);
			p.Add("@ByWeek", ByWeek);
			p.Add("@ShowOnlyActiveInRoster", ShowOnlyActiveInRoster);
			return db.QueryAsync<T>("custRU_TeamLocationsGetCreditsRanTotals", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetDateStats<T>(int? TeamLocationID,bool? IsRepOrTech,DateTime? WeekStartDate,DateTime? WeekEndDate,DateTime? MonthStartDate,DateTime? MonthEndDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@IsRepOrTech", IsRepOrTech);
			p.Add("@WeekStartDate", WeekStartDate);
			p.Add("@WeekEndDate", WeekEndDate);
			p.Add("@MonthStartDate", MonthStartDate);
			p.Add("@MonthEndDate", MonthEndDate);
			return db.QueryAsync<T>("custRU_TeamLocationsGetDateStats", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetLocationsBySeasonID<T>(int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_TeamLocationsGetLocationsBySeasonID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetOfficeRosterDetailReport<T>(int? TeamLocationID,DateTime? SnapShotDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@SnapShotDate", SnapShotDate);
			return db.QueryAsync<T>("custRU_TeamLocationsGetOfficeRosterDetailReport", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetOfficeRosterReport<T>(int? SeasonID,DateTime? SnapShotDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@SnapShotDate", SnapShotDate);
			return db.QueryAsync<T>("custRU_TeamLocationsGetOfficeRosterReport", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetOfficesByStateIdAndSeasonId<T>(int? StateId,int? SeasonId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StateId", StateId);
			p.Add("@SeasonId", SeasonId);
			return db.QueryAsync<T>("custRU_TeamLocationsGetOfficesByStateIdAndSeasonId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetOfficeSeasonsMaps<T>(int? FromSeasonID,int? ToSeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FromSeasonID", FromSeasonID);
			p.Add("@ToSeasonID", ToSeasonID);
			return db.QueryAsync<T>("custRU_TeamLocationsGetOfficeSeasonsMaps", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetOfficesForActiveSeasons<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custRU_TeamLocationsGetOfficesForActiveSeasons", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetOfficesUnderRecruit<T>(int? RegionID,int? NationalRegionID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RegionID", RegionID);
			p.Add("@NationalRegionID", NationalRegionID);
			return db.QueryAsync<T>("custRU_TeamLocationsGetOfficesUnderRecruit", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetSummaryReport<T>(int? TeamLocationID,int? SeasonID,DateTime? StartDate,DateTime? EndDate)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@SeasonID", SeasonID);
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			return db.QueryAsync<T>("custRU_TeamLocationsGetSummaryReport", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetTimeSpanTotals<T>(int? SeasonID,bool? ExcludeSubs,int? WeekDateFirst,bool? ByWeek)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@ExcludeSubs", ExcludeSubs);
			p.Add("@WeekDateFirst", WeekDateFirst);
			p.Add("@ByWeek", ByWeek);
			return db.QueryAsync<T>("custRU_TeamLocationsGetTimeSpanTotals", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetTimeSpanTotalsDetail<T>(bool? TrueOffice_FalseSeason,int? ID,bool? ExcludeSubs,int? WeekDateFirst,bool? ByWeek)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TrueOffice_FalseSeason", TrueOffice_FalseSeason);
			p.Add("@ID", ID);
			p.Add("@ExcludeSubs", ExcludeSubs);
			p.Add("@WeekDateFirst", WeekDateFirst);
			p.Add("@ByWeek", ByWeek);
			return db.QueryAsync<T>("custRU_TeamLocationsGetTimeSpanTotalsDetail", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetTimeSpanTotalsDetailDetail<T>(bool? TrueOffice_FalseSeason,int? ID,bool? ExcludeSubs,int? WeekDateFirst,bool? ByWeek)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TrueOffice_FalseSeason", TrueOffice_FalseSeason);
			p.Add("@ID", ID);
			p.Add("@ExcludeSubs", ExcludeSubs);
			p.Add("@WeekDateFirst", WeekDateFirst);
			p.Add("@ByWeek", ByWeek);
			return db.QueryAsync<T>("custRU_TeamLocationsGetTimeSpanTotalsDetailDetail", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetTotalCreditsRanByRecruit<T>(DateTime? StartDate,DateTime? EndDate,int? TeamLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			p.Add("@TeamLocationID", TeamLocationID);
			return db.QueryAsync<T>("custRU_TeamLocationsGetTotalCreditsRanByRecruit", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsGetViewableTeamLocations<T>(int? UserID,string SeasonIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@SeasonIDList", SeasonIDList);
			return db.QueryAsync<T>("custRU_TeamLocationsGetViewableTeamLocations", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamLocationsValidateStateMappings<T>(int? TeamLocationID,string StateAB)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamLocationID", TeamLocationID);
			p.Add("@StateAB", StateAB);
			return db.QueryAsync<T>("custRU_TeamLocationsValidateStateMappings", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsFindTeams<T>(int? Top,int? TeamID,string TeamName,string OfficeName,int? SeasonID,string SeasonName,int? RoleLocationID,string City,string StateAB)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Top", Top);
			p.Add("@TeamID", TeamID);
			p.Add("@TeamName", TeamName);
			p.Add("@OfficeName", OfficeName);
			p.Add("@SeasonID", SeasonID);
			p.Add("@SeasonName", SeasonName);
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@City", City);
			p.Add("@StateAB", StateAB);
			return db.QueryAsync<T>("custRU_TeamsFindTeams", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetActiveAccountsSum<T>(string TeamIDList,int? SeasonID,bool? IsCanceled,bool? IsDelinquent,bool? HasRepHold,bool? HasTechHold)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamIDList", TeamIDList);
			p.Add("@SeasonID", SeasonID);
			p.Add("@IsCanceled", IsCanceled);
			p.Add("@IsDelinquent", IsDelinquent);
			p.Add("@HasRepHold", HasRepHold);
			p.Add("@HasTechHold", HasTechHold);
			return db.QueryAsync<T>("custRU_TeamsGetActiveAccountsSum", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetActiveAccountsSumForRecruits<T>(int? TeamID,bool? IsCanceled,bool? IsDelinquent,bool? HasRepHold,bool? HasTechHold)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamID", TeamID);
			p.Add("@IsCanceled", IsCanceled);
			p.Add("@IsDelinquent", IsDelinquent);
			p.Add("@HasRepHold", HasRepHold);
			p.Add("@HasTechHold", HasTechHold);
			return db.QueryAsync<T>("custRU_TeamsGetActiveAccountsSumForRecruits", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetAllForSeason<T>(int? SeasonID,int? RoleLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@RoleLocationID", RoleLocationID);
			return db.QueryAsync<T>("custRU_TeamsGetAllForSeason", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetAllThatCanMigrateToSeason<T>(int? PreviousSeasonID,int? MigrateToSeasonID,bool? ExcludeOfficesAlreadyInSeason)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@PreviousSeasonID", PreviousSeasonID);
			p.Add("@MigrateToSeasonID", MigrateToSeasonID);
			p.Add("@ExcludeOfficesAlreadyInSeason", ExcludeOfficesAlreadyInSeason);
			return db.QueryAsync<T>("custRU_TeamsGetAllThatCanMigrateToSeason", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetCertificationInfo<T>(int? TeamID,bool? HasOwnTeam,string DeletionStatus)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamID", TeamID);
			p.Add("@HasOwnTeam", HasOwnTeam);
			p.Add("@DeletionStatus", DeletionStatus);
			return db.QueryAsync<T>("custRU_TeamsGetCertificationInfo", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetManagingTeams<T>(int? UserID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_TeamsGetManagingTeams", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetMarchMadnessReport<T>(int? SeasonID,int? RoleLocationID,DateTime? StartDate,DateTime? EndDate,int? PPNum,int? PPNewRecruit)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@StartDate", StartDate);
			p.Add("@EndDate", EndDate);
			p.Add("@PPNum", PPNum);
			p.Add("@PPNewRecruit", PPNewRecruit);
			return db.QueryAsync<T>("custRU_TeamsGetMarchMadnessReport", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetServicePercent<T>(int? SeasonID,string TeamIDList,string RecruitIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@TeamIDList", TeamIDList);
			p.Add("@RecruitIDList", RecruitIDList);
			return db.QueryAsync<T>("custRU_TeamsGetServicePercent", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetStats<T>(int? TeamID,bool? IsDeleted)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamID", TeamID);
			p.Add("@IsDeleted", IsDeleted);
			return db.QueryAsync<T>("custRU_TeamsGetStats", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamInLocationAndSeason<T>(int? SeasonID,string TeamName,string TeamLocationName)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@TeamName", TeamName);
			p.Add("@TeamLocationName", TeamLocationName);
			return db.QueryAsync<T>("custRU_TeamsGetTeamInLocationAndSeason", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamMemberInfo<T>(int? TeamID,bool? IsDeleted)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamID", TeamID);
			p.Add("@IsDeleted", IsDeleted);
			return db.QueryAsync<T>("custRU_TeamsGetTeamMemberInfo", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamMemberNumsInfo<T>(int? TeamID,bool? HasOwnTeam,bool? IsDeleted)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamID", TeamID);
			p.Add("@HasOwnTeam", HasOwnTeam);
			p.Add("@IsDeleted", IsDeleted);
			return db.QueryAsync<T>("custRU_TeamsGetTeamMemberNumsInfo", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamNumbersReport_Sales<T>(int? SeasonID,bool? ExcludeCancels,int? CreditScore)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@ExcludeCancels", ExcludeCancels);
			p.Add("@CreditScore", CreditScore);
			return db.QueryAsync<T>("custRU_TeamsGetTeamNumbersReport_Sales", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamNumbersReport_SalesManagers<T>(string IDType,int? ID,bool? ExcludeCancels,int? CreditScore)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@IDType", IDType);
			p.Add("@ID", ID);
			p.Add("@ExcludeCancels", ExcludeCancels);
			p.Add("@CreditScore", CreditScore);
			return db.QueryAsync<T>("custRU_TeamsGetTeamNumbersReport_SalesManagers", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamSalesInfo<T>(int? TeamID,int? RoleLocationID,bool? HasOwnTeam,string DeletionStatus)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamID", TeamID);
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@HasOwnTeam", HasOwnTeam);
			p.Add("@DeletionStatus", DeletionStatus);
			return db.QueryAsync<T>("custRU_TeamsGetTeamSalesInfo", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamsByRecruitID<T>(int? RecruitID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitID", RecruitID);
			return db.QueryAsync<T>("custRU_TeamsGetTeamsByRecruitID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamSeasonsMaps<T>(int? FromSeasonID,int? ToSeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FromSeasonID", FromSeasonID);
			p.Add("@ToSeasonID", ToSeasonID);
			return db.QueryAsync<T>("custRU_TeamsGetTeamSeasonsMaps", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetTeamsInSeasonByDescription<T>(int? SeasonID,string Description)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@Description", Description);
			return db.QueryAsync<T>("custRU_TeamsGetTeamsInSeasonByDescription", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_TeamsGetViewableTeams<T>(int? UserID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_TeamsGetViewableTeams", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UserAuthenticationsAuthenticate<T>(string Username,string Password,string IPAddress)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Username", Username);
			p.Add("@Password", Password);
			p.Add("@IPAddress", IPAddress);
			return db.QueryAsync<T>("custRU_UserAuthenticationsAuthenticate", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UserGetManagerBySeasonId<T>(int? SeasonId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonId", SeasonId);
			return db.QueryAsync<T>("custRU_UserGetManagerBySeasonId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UserGetManagerByTeamId<T>(int? TeamId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@TeamId", TeamId);
			return db.QueryAsync<T>("custRU_UserGetManagerByTeamId", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UserSalesInfoConnextGetByUserID<T>(int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_UserSalesInfoConnextGetByUserID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersFindByEmailAndSSN<T>(string Email,string SSN,string SSNEncrypted)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Email", Email);
			p.Add("@SSN", SSN);
			p.Add("@SSNEncrypted", SSNEncrypted);
			return db.QueryAsync<T>("custRU_UsersFindByEmailAndSSN", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersFindUsers<T>(int? Top,string FirstName,string LastName,string CompanyID,string SSN,string PhoneCell,string PhoneHome,string Email,string UserName,int? UserID,string UserEmployeeTypeID,int? RecruitID,int? SeasonID,short? UserTypeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@Top", Top);
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			p.Add("@CompanyID", CompanyID);
			p.Add("@SSN", SSN);
			p.Add("@PhoneCell", PhoneCell);
			p.Add("@PhoneHome", PhoneHome);
			p.Add("@Email", Email);
			p.Add("@UserName", UserName);
			p.Add("@UserID", UserID);
			p.Add("@UserEmployeeTypeID", UserEmployeeTypeID);
			p.Add("@RecruitID", RecruitID);
			p.Add("@SeasonID", SeasonID);
			p.Add("@UserTypeID", UserTypeID);
			return db.QueryAsync<T>("custRU_UsersFindUsers", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersFindUsersInTeams<T>(string FirstName,string LastName,string DeletionStatus,string TeamIDList,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@FirstName", FirstName);
			p.Add("@LastName", LastName);
			p.Add("@DeletionStatus", DeletionStatus);
			p.Add("@TeamIDList", TeamIDList);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_UsersFindUsersInTeams", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetActivationWaives<T>(int? UserID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_UsersGetActivationWaives", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetActiveUsers<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custRU_UsersGetActiveUsers", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetAllRecruitsByGPEmployeeID<T>(string GPEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			return db.QueryAsync<T>("custRU_UsersGetAllRecruitsByGPEmployeeID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetAllUsersByRoleLocationID<T>(int? RoleLocationID,int? SeasonID,string DeletionStatus)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@SeasonID", SeasonID);
			p.Add("@DeletionStatus", DeletionStatus);
			return db.QueryAsync<T>("custRU_UsersGetAllUsersByRoleLocationID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetByAgemniInvoiceID<T>(int? AgemniInvoiceID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@AgemniInvoiceID", AgemniInvoiceID);
			return db.QueryAsync<T>("custRU_UsersGetByAgemniInvoiceID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetClothingSizes<T>(int? SeasonID,string SizeType,bool? IsSales,bool? IsTech,bool? IsFemale,bool? IsMale)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@SizeType", SizeType);
			p.Add("@IsSales", IsSales);
			p.Add("@IsTech", IsTech);
			p.Add("@IsFemale", IsFemale);
			p.Add("@IsMale", IsMale);
			return db.QueryAsync<T>("custRU_UsersGetClothingSizes", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetDocumentsByGPID<T>(string GPEmployeeID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@GPEmployeeID", GPEmployeeID);
			return db.QueryAsync<T>("custRU_UsersGetDocumentsByGPID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetExpiringRightToWork<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custRU_UsersGetExpiringRightToWork", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetManagersTeamIDForSeason<T>(int? UserID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_UsersGetManagersTeamIDForSeason", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetMessageCenterInfo<T>(string RecruitIDList,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitIDList", RecruitIDList);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_UsersGetMessageCenterInfo", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetOrphanedSalesUsers<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custRU_UsersGetOrphanedSalesUsers", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetOwners<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("custRU_UsersGetOwners", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetRecruitersBySeasonID<T>(int? SeasonId)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonId", SeasonId);
			return db.QueryAsync<T>("custRU_UsersGetRecruitersBySeasonID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetRecruits<T>(int? UserID,int? RoleLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@RoleLocationID", RoleLocationID);
			return db.QueryAsync<T>("custRU_UsersGetRecruits", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetRepsTeamIDForSeason<T>(int? UserID,int? SeasonID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			p.Add("@SeasonID", SeasonID);
			return db.QueryAsync<T>("custRU_UsersGetRepsTeamIDForSeason", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetSalesUsersByRecruitedById<T>(int? RecruitedById)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitedById", RecruitedById);
			return db.QueryAsync<T>("custRU_UsersGetSalesUsersByRecruitedById", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetUserInfo<T>(int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_UsersGetUserInfo", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetUsersByNameOrGPID<T>(string NameOrID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@NameOrID", NameOrID);
			return db.QueryAsync<T>("custRU_UsersGetUsersByNameOrGPID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetUsersByRecruitIDs<T>(string RecruitIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@RecruitIDList", RecruitIDList);
			return db.QueryAsync<T>("custRU_UsersGetUsersByRecruitIDs", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetUsersByUserTypeID<T>(int? UserTypeID,string IDList,string BreakDownType,string DeletionStatus)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserTypeID", UserTypeID);
			p.Add("@IDList", IDList);
			p.Add("@BreakDownType", BreakDownType);
			p.Add("@DeletionStatus", DeletionStatus);
			return db.QueryAsync<T>("custRU_UsersGetUsersByUserTypeID", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersGetViewableUsers<T>(int? ViewingUserID,string CompanyID,int? UserID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@ViewingUserID", ViewingUserID);
			p.Add("@CompanyID", CompanyID);
			p.Add("@UserID", UserID);
			return db.QueryAsync<T>("custRU_UsersGetViewableUsers", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> RU_UsersLoadByMultiplePrimaryKeys<T>(string UserIDList)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@UserIDList", UserIDList);
			return db.QueryAsync<T>("custRU_UsersLoadByMultiplePrimaryKeys", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> VW_RecruitingStructureGetManageableTeams<T>(int? SeasonID,int? RoleLocationID,int? RecruitID,int? TeamLocationID)
		{
			var p = new Dapper.DynamicParameters();
			p.Add("@SeasonID", SeasonID);
			p.Add("@RoleLocationID", RoleLocationID);
			p.Add("@RecruitID", RecruitID);
			p.Add("@TeamLocationID", TeamLocationID);
			return db.QueryAsync<T>("custVW_RecruitingStructureGetManageableTeams", p, commandType: System.Data.CommandType.StoredProcedure);
		}
		public Task<IEnumerable<T>> wiseSP_ExceptionsThrown<T>()
		{
			var p = new Dapper.DynamicParameters();
			return db.QueryAsync<T>("wiseSP_ExceptionsThrown", p, commandType: System.Data.CommandType.StoredProcedure);
		}
	}
}

