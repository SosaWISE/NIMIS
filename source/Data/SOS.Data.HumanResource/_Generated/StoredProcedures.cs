


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace SOS.Data.HumanResource {
	public partial class HumanResourceDataStoredProcedureManager {
		public static StoredProcedure DocLinkGetDocumentsByID(int? DocumentID) {
			StoredProcedure sp = new StoredProcedure("custDocLinkGetDocumentsByID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@DocumentID", DocumentID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitGetAllTechsBySeasonID(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitGetAllTechsBySeasonID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitGetOfficeNameByGPID(string GPEmployeeID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitGetOfficeNameByGPID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitingStructureGetManageableTeams(int? SeasonID,int? RoleLocationID,int? RecruitID,int? TeamLocationID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitingStructureGetManageableTeams" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsActivateByGPIdAndSeasonId(int? SeasonId,string GPID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsActivateByGPIdAndSeasonId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			sp.Command.AddParameter("@GPID", GPID, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsCopyWithNewSeasonID(int? RecruitId,int? SeasonId) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsCopyWithNewSeasonID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitId", RecruitId, DbType.Int32);
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsFindBy(string SeasonIDList,string FirstName,string LastName) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsFindBy" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonIDList", SeasonIDList, DbType.String);
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsFindRecruitsInTeams(string FirstName,string LastName,string TeamIDList,string DeletionStatus) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsFindRecruitsInTeams" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@TeamIDList", TeamIDList, DbType.String);
			sp.Command.AddParameter("@DeletionStatus", DeletionStatus, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetActiveSeasonAccounts(string GPEmployeeID,int? SeasonID,bool? IsCanceled,bool? IsDelinquent,bool? HasRepHold,bool? HasTechHold) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetActiveSeasonAccounts" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@IsCanceled", IsCanceled, DbType.Boolean);
			sp.Command.AddParameter("@IsDelinquent", IsDelinquent, DbType.Boolean);
			sp.Command.AddParameter("@HasRepHold", HasRepHold, DbType.Boolean);
			sp.Command.AddParameter("@HasTechHold", HasTechHold, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetAllSeasonRankings(DateTime? StartDate,DateTime? EndDate,string SeasonIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetAllSeasonRankings" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@SeasonIDList", SeasonIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetBuddyByGPEmpoyeeID(string GPEmployeeID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetBuddyByGPEmpoyeeID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetByGPEmployeeId(string GPEmployeeID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetByGPEmployeeId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetByGPEmployeeIdRaw(string GPEmployeeID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetByGPEmployeeIdRaw" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetByGPEmployeeIdWithNoSeasonID(string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetByGPEmployeeIdWithNoSeasonID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw(string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetByGPEmployeeIdWithNoSeasonIDRaw" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetCompetitiveRankings(int? UserID,DateTime? StartDate,DateTime? EndDate,string SeasonIDList,int? RoleLocationID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetCompetitiveRankings" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@SeasonIDList", SeasonIDList, DbType.String);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetDateStats(int? RecruitID,DateTime? WeekStartDate,DateTime? WeekEndDate,DateTime? MonthStartDate,DateTime? MonthEndDate) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetDateStats" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			sp.Command.AddParameter("@WeekStartDate", WeekStartDate, DbType.DateTime);
			sp.Command.AddParameter("@WeekEndDate", WeekEndDate, DbType.DateTime);
			sp.Command.AddParameter("@MonthStartDate", MonthStartDate, DbType.DateTime);
			sp.Command.AddParameter("@MonthEndDate", MonthEndDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetManagersBySeasonId(int? SeasonId) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetManagersBySeasonId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetManagersRoster(int? seasonID,int? oldSeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetManagersRoster" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@seasonID", seasonID, DbType.Int32);
			sp.Command.AddParameter("@oldSeasonID", oldSeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetMigrationRecruits(int? MigrateFromSeasonID,int? MigrateToSeasonID,int? UserTypeID,bool? ExcludeAlreadyInSeason) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetMigrationRecruits" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@MigrateFromSeasonID", MigrateFromSeasonID, DbType.Int32);
			sp.Command.AddParameter("@MigrateToSeasonID", MigrateToSeasonID, DbType.Int32);
			sp.Command.AddParameter("@UserTypeID", UserTypeID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeAlreadyInSeason", ExcludeAlreadyInSeason, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetNewestRecruit(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetNewestRecruit" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetOAEmailByTeamLocationID(int? TeamLocationID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetOAEmailByTeamLocationID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetOrphanRecruitsByManagersAndSeason(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetOrphanRecruitsByManagersAndSeason" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetPayrollStats(int? RecruitID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetPayrollStats" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetPointBankBySeasonId(string GPEmployeeID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetPointBankBySeasonId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetPossibleReportTos(int? SeasonID,int? UserTypeID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetPossibleReportTos" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@UserTypeID", UserTypeID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRankingInTeam(int? recruitID,int? roleLocationID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRankingInTeam" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@recruitID", recruitID, DbType.Int32);
			sp.Command.AddParameter("@roleLocationID", roleLocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRankings(DateTime? StartDate,DateTime? EndDate,string SeasonIDList,int? RoleLocationID,int? MaxRankToReturn,int? UserID,bool? IncludeCancels) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRankings" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@SeasonIDList", SeasonIDList, DbType.String);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@MaxRankToReturn", MaxRankToReturn, DbType.Int32);
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@IncludeCancels", IncludeCancels, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRankInSeason(int? SeasonID,int? UserID,int? RoleLocationID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRankInSeason" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitAllInfoByRecruitId(int? RecruitId) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitAllInfoByRecruitId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitId", RecruitId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitByEmailAndSeasonID(string Email,string SeasonIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitByEmailAndSeasonID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@SeasonIDList", SeasonIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitByUserAndSeasonIDs(int? UserID,string SeasonIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitByUserAndSeasonIDs" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@SeasonIDList", SeasonIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitIDsForBrackets(string BracketType) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitIDsForBrackets" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@BracketType", BracketType, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitIDsForBrackets2010(string BracketType) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitIDsForBrackets2010" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@BracketType", BracketType, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitNames(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitNames" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitsByTeam(int? TeamID,int? RoleLocationID,string DeletionStatus) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitsByTeam" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@DeletionStatus", DeletionStatus, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitsByTeamLocation(int? TeamLocationID,int? RoleLocationID,string DeletionStatus) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitsByTeamLocation" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@DeletionStatus", DeletionStatus, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitsByTeamLocationAndUserTypeID(string TeamLocationIDList,int? UserTypeID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitsByTeamLocationAndUserTypeID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationIDList", TeamLocationIDList, DbType.String);
			sp.Command.AddParameter("@UserTypeID", UserTypeID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList(string TeamIDList,int? UserTypeID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitsByUserTypeIDAndTeamIDList" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamIDList", TeamIDList, DbType.String);
			sp.Command.AddParameter("@UserTypeID", UserTypeID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitSeasonSalesTotals(string GPEmployeeID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitSeasonSalesTotals" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitSeasonsMaps(int? FromSeasonID,int? ToSeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitSeasonsMaps" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@FromSeasonID", FromSeasonID, DbType.Int32);
			sp.Command.AddParameter("@ToSeasonID", ToSeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRecruitsToMigrate(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRecruitsToMigrate" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRegionalsBySeasonID(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRegionalsBySeasonID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetReportingTree(string Type,int? TypeID,bool? HasOwnTeam,int? TeamID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetReportingTree" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@Type", Type, DbType.String);
			sp.Command.AddParameter("@TypeID", TypeID, DbType.Int32);
			sp.Command.AddParameter("@HasOwnTeam", HasOwnTeam, DbType.Boolean);
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetRepSalesTotals(DateTime? SnapShotDate,int? SeasonID,DateTime? ExactDate,DateTime? StopDate) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetRepSalesTotals" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SnapShotDate", SnapShotDate, DbType.DateTime);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@ExactDate", ExactDate, DbType.DateTime);
			sp.Command.AddParameter("@StopDate", StopDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetSalesPayrollDeductionsReport(int? ID,string IDType) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetSalesPayrollDeductionsReport" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@ID", ID, DbType.Int32);
			sp.Command.AddParameter("@IDType", IDType, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetSalesTotals(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetSalesTotals" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetStats(int? RecruitID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetStats" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_RecruitsGetSummaryReport(int? RecruitID,int? SeasonID,DateTime? StartDate,DateTime? EndDate) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsGetSummaryReport" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure RU_RecruitsLoadByRawSQL(string FirstName,string LastName,string PhoneCell,string PhoneHome,string SSN,string BirthDate,int? SeasonId,string Mode) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsLoadByRawSQL" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@PhoneCell", PhoneCell, DbType.String);
			sp.Command.AddParameter("@PhoneHome", PhoneHome, DbType.String);
			sp.Command.AddParameter("@SSN", SSN, DbType.String);
			sp.Command.AddParameter("@BirthDate", BirthDate, DbType.AnsiString);
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			sp.Command.AddParameter("@Mode", Mode, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure RU_RecruitsNewRecruits(DateTime? StartDate,DateTime? EndDate,string UserTypeIDList,string SeasonIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsNewRecruits" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@UserTypeIDList", UserTypeIDList, DbType.String);
			sp.Command.AddParameter("@SeasonIDList", SeasonIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_RecruitsShowSeasonStatusesByUserId(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_RecruitsShowSeasonStatusesByUserId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_SeasonGetActiveSeasonsForAUserByUserID(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_SeasonGetActiveSeasonsForAUserByUserID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags(int? UserID,bool? IsCurrent,bool? IsVisibleToRecruits,bool? IsInsideSales,bool? IsPreseason,bool? IsSummer,bool? IsExtended,bool? IsYearRound) {
			StoredProcedure sp = new StoredProcedure("custRU_SeasonGetActiveSeasonsForAUserByUserIDAndFlags" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@IsCurrent", IsCurrent, DbType.Boolean);
			sp.Command.AddParameter("@IsVisibleToRecruits", IsVisibleToRecruits, DbType.Boolean);
			sp.Command.AddParameter("@IsInsideSales", IsInsideSales, DbType.Boolean);
			sp.Command.AddParameter("@IsPreseason", IsPreseason, DbType.Boolean);
			sp.Command.AddParameter("@IsSummer", IsSummer, DbType.Boolean);
			sp.Command.AddParameter("@IsExtended", IsExtended, DbType.Boolean);
			sp.Command.AddParameter("@IsYearRound", IsYearRound, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_SeasonGetActiveUserSeasons(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_SeasonGetActiveUserSeasons" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_SeasonGetAllSeasonsForAUserByUserID(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_SeasonGetAllSeasonsForAUserByUserID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_SeasonGetCurrentOrVisibleToRecruit() {
			StoredProcedure sp = new StoredProcedure("custRU_SeasonGetCurrentOrVisibleToRecruit" ,DataService.GetInstance("SosHumanResourceProvider"));
			return sp;
		}
		public static StoredProcedure RU_SeasonGetMissingSeasonsByUserID(int? UserId) {
			StoredProcedure sp = new StoredProcedure("custRU_SeasonGetMissingSeasonsByUserID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserId", UserId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_SeasonGetVisibleUserSeasons(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_SeasonGetVisibleUserSeasons" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_SeasonsGetNormalSalesSeasons() {
			StoredProcedure sp = new StoredProcedure("custRU_SeasonsGetNormalSalesSeasons" ,DataService.GetInstance("SosHumanResourceProvider"));
			return sp;
		}
		public static StoredProcedure RU_TeamLocationGetManagersByTeamLocation(int? TeamLocationID,int? RoleLocationID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationGetManagersByTeamLocation" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsCopyOfficeStateMappings(int? FromTeamLocationID,int? ToTeamLocationID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsCopyOfficeStateMappings" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@FromTeamLocationID", FromTeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@ToTeamLocationID", ToTeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsFindOffices(int? Top,int? TeamLocationID,string OfficeName,int? SeasonID,string SeasonName,int? MarketID,string MarketName,string City,string StateAB,int? TimeZoneID,string TimeZoneName) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsFindOffices" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@Top", Top, DbType.Int32);
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@OfficeName", OfficeName, DbType.AnsiString);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@SeasonName", SeasonName, DbType.String);
			sp.Command.AddParameter("@MarketID", MarketID, DbType.Int32);
			sp.Command.AddParameter("@MarketName", MarketName, DbType.String);
			sp.Command.AddParameter("@City", City, DbType.AnsiString);
			sp.Command.AddParameter("@StateAB", StateAB, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@TimeZoneID", TimeZoneID, DbType.Int32);
			sp.Command.AddParameter("@TimeZoneName", TimeZoneName, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetAllThatCanMigrateToSeason(int? MigrateToSeasonID,bool? ExcludeOfficesAlreadyInSeason) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetAllThatCanMigrateToSeason" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@MigrateToSeasonID", MigrateToSeasonID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeOfficesAlreadyInSeason", ExcludeOfficesAlreadyInSeason, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetBySeasonIdAndGPEmployeeID(int? SeasonId,string GPEmployeeId) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetBySeasonIdAndGPEmployeeID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			sp.Command.AddParameter("@GPEmployeeId", GPEmployeeId, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetByStateABAndSeasonID(string StateAB,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetByStateABAndSeasonID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@StateAB", StateAB, DbType.AnsiStringFixedLength);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetByTeamIDs(string TeamIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetByTeamIDs" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamIDList", TeamIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetCreditsRanTotals(int? TeamLocationID,bool? ExcludeSubs,int? WeekDateFirst,bool? ByWeek,bool? ShowOnlyActiveInRoster) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetCreditsRanTotals" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeSubs", ExcludeSubs, DbType.Boolean);
			sp.Command.AddParameter("@WeekDateFirst", WeekDateFirst, DbType.Int32);
			sp.Command.AddParameter("@ByWeek", ByWeek, DbType.Boolean);
			sp.Command.AddParameter("@ShowOnlyActiveInRoster", ShowOnlyActiveInRoster, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetDateStats(int? TeamLocationID,bool? IsRepOrTech,DateTime? WeekStartDate,DateTime? WeekEndDate,DateTime? MonthStartDate,DateTime? MonthEndDate) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetDateStats" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@IsRepOrTech", IsRepOrTech, DbType.Boolean);
			sp.Command.AddParameter("@WeekStartDate", WeekStartDate, DbType.DateTime);
			sp.Command.AddParameter("@WeekEndDate", WeekEndDate, DbType.DateTime);
			sp.Command.AddParameter("@MonthStartDate", MonthStartDate, DbType.DateTime);
			sp.Command.AddParameter("@MonthEndDate", MonthEndDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetLocationsBySeasonID(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetLocationsBySeasonID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetOfficeRosterDetailReport(int? TeamLocationID,DateTime? SnapShotDate) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetOfficeRosterDetailReport" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@SnapShotDate", SnapShotDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetOfficeRosterReport(int? SeasonID,DateTime? SnapShotDate) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetOfficeRosterReport" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@SnapShotDate", SnapShotDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetOfficesByStateIdAndSeasonId(int? StateId,int? SeasonId) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetOfficesByStateIdAndSeasonId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@StateId", StateId, DbType.Int32);
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetOfficeSeasonsMaps(int? FromSeasonID,int? ToSeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetOfficeSeasonsMaps" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@FromSeasonID", FromSeasonID, DbType.Int32);
			sp.Command.AddParameter("@ToSeasonID", ToSeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetOfficesForActiveSeasons() {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetOfficesForActiveSeasons" ,DataService.GetInstance("SosHumanResourceProvider"));
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetOfficesUnderRecruit(int? RegionID,int? NationalRegionID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetOfficesUnderRecruit" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RegionID", RegionID, DbType.Int32);
			sp.Command.AddParameter("@NationalRegionID", NationalRegionID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetSummaryReport(int? TeamLocationID,int? SeasonID,DateTime? StartDate,DateTime? EndDate) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetSummaryReport" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetTimeSpanTotals(int? SeasonID,bool? ExcludeSubs,int? WeekDateFirst,bool? ByWeek) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetTimeSpanTotals" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeSubs", ExcludeSubs, DbType.Boolean);
			sp.Command.AddParameter("@WeekDateFirst", WeekDateFirst, DbType.Int32);
			sp.Command.AddParameter("@ByWeek", ByWeek, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetTimeSpanTotalsDetail(bool? TrueOffice_FalseSeason,int? ID,bool? ExcludeSubs,int? WeekDateFirst,bool? ByWeek) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetTimeSpanTotalsDetail" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TrueOffice_FalseSeason", TrueOffice_FalseSeason, DbType.Boolean);
			sp.Command.AddParameter("@ID", ID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeSubs", ExcludeSubs, DbType.Boolean);
			sp.Command.AddParameter("@WeekDateFirst", WeekDateFirst, DbType.Int32);
			sp.Command.AddParameter("@ByWeek", ByWeek, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetTimeSpanTotalsDetailDetail(bool? TrueOffice_FalseSeason,int? ID,bool? ExcludeSubs,int? WeekDateFirst,bool? ByWeek) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetTimeSpanTotalsDetailDetail" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TrueOffice_FalseSeason", TrueOffice_FalseSeason, DbType.Boolean);
			sp.Command.AddParameter("@ID", ID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeSubs", ExcludeSubs, DbType.Boolean);
			sp.Command.AddParameter("@WeekDateFirst", WeekDateFirst, DbType.Int32);
			sp.Command.AddParameter("@ByWeek", ByWeek, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetTotalCreditsRanByRecruit(DateTime? StartDate,DateTime? EndDate,int? TeamLocationID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetTotalCreditsRanByRecruit" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsGetViewableTeamLocations(int? UserID,string SeasonIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsGetViewableTeamLocations" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@SeasonIDList", SeasonIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_TeamLocationsValidateStateMappings(int? TeamLocationID,string StateAB) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamLocationsValidateStateMappings" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			sp.Command.AddParameter("@StateAB", StateAB, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure RU_TeamsFindTeams(int? Top,int? TeamID,string TeamName,string OfficeName,int? SeasonID,string SeasonName,int? RoleLocationID,string City,string StateAB) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsFindTeams" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@Top", Top, DbType.Int32);
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@TeamName", TeamName, DbType.AnsiString);
			sp.Command.AddParameter("@OfficeName", OfficeName, DbType.AnsiString);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@SeasonName", SeasonName, DbType.String);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@City", City, DbType.AnsiString);
			sp.Command.AddParameter("@StateAB", StateAB, DbType.AnsiStringFixedLength);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetActiveAccountsSum(string TeamIDList,int? SeasonID,bool? IsCanceled,bool? IsDelinquent,bool? HasRepHold,bool? HasTechHold) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetActiveAccountsSum" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamIDList", TeamIDList, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@IsCanceled", IsCanceled, DbType.Boolean);
			sp.Command.AddParameter("@IsDelinquent", IsDelinquent, DbType.Boolean);
			sp.Command.AddParameter("@HasRepHold", HasRepHold, DbType.Boolean);
			sp.Command.AddParameter("@HasTechHold", HasTechHold, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetActiveAccountsSumForRecruits(int? TeamID,bool? IsCanceled,bool? IsDelinquent,bool? HasRepHold,bool? HasTechHold) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetActiveAccountsSumForRecruits" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@IsCanceled", IsCanceled, DbType.Boolean);
			sp.Command.AddParameter("@IsDelinquent", IsDelinquent, DbType.Boolean);
			sp.Command.AddParameter("@HasRepHold", HasRepHold, DbType.Boolean);
			sp.Command.AddParameter("@HasTechHold", HasTechHold, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetAllForSeason(int? SeasonID,int? RoleLocationID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetAllForSeason" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetAllThatCanMigrateToSeason(int? PreviousSeasonID,int? MigrateToSeasonID,bool? ExcludeOfficesAlreadyInSeason) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetAllThatCanMigrateToSeason" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@PreviousSeasonID", PreviousSeasonID, DbType.Int32);
			sp.Command.AddParameter("@MigrateToSeasonID", MigrateToSeasonID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeOfficesAlreadyInSeason", ExcludeOfficesAlreadyInSeason, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetCertificationInfo(int? TeamID,bool? HasOwnTeam,string DeletionStatus) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetCertificationInfo" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@HasOwnTeam", HasOwnTeam, DbType.Boolean);
			sp.Command.AddParameter("@DeletionStatus", DeletionStatus, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetManagingTeams(int? UserID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetManagingTeams" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetMarchMadnessReport(int? SeasonID,int? RoleLocationID,DateTime? StartDate,DateTime? EndDate,int? PPNum,int? PPNewRecruit) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetMarchMadnessReport" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@StartDate", StartDate, DbType.DateTime);
			sp.Command.AddParameter("@EndDate", EndDate, DbType.DateTime);
			sp.Command.AddParameter("@PPNum", PPNum, DbType.Int32);
			sp.Command.AddParameter("@PPNewRecruit", PPNewRecruit, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetServicePercent(int? SeasonID,string TeamIDList,string RecruitIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetServicePercent" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@TeamIDList", TeamIDList, DbType.String);
			sp.Command.AddParameter("@RecruitIDList", RecruitIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetStats(int? TeamID,bool? IsDeleted) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetStats" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@IsDeleted", IsDeleted, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamInLocationAndSeason(int? SeasonID,string TeamName,string TeamLocationName) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamInLocationAndSeason" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@TeamName", TeamName, DbType.String);
			sp.Command.AddParameter("@TeamLocationName", TeamLocationName, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamMemberInfo(int? TeamID,bool? IsDeleted) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamMemberInfo" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@IsDeleted", IsDeleted, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamMemberNumsInfo(int? TeamID,bool? HasOwnTeam,bool? IsDeleted) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamMemberNumsInfo" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@HasOwnTeam", HasOwnTeam, DbType.Boolean);
			sp.Command.AddParameter("@IsDeleted", IsDeleted, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamNumbersReport_Sales(int? SeasonID,bool? ExcludeCancels,int? CreditScore) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamNumbersReport_Sales" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeCancels", ExcludeCancels, DbType.Boolean);
			sp.Command.AddParameter("@CreditScore", CreditScore, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamNumbersReport_SalesManagers(string IDType,int? ID,bool? ExcludeCancels,int? CreditScore) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamNumbersReport_SalesManagers" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@IDType", IDType, DbType.String);
			sp.Command.AddParameter("@ID", ID, DbType.Int32);
			sp.Command.AddParameter("@ExcludeCancels", ExcludeCancels, DbType.Boolean);
			sp.Command.AddParameter("@CreditScore", CreditScore, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamSalesInfo(int? TeamID,int? RoleLocationID,bool? HasOwnTeam,string DeletionStatus) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamSalesInfo" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamID", TeamID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@HasOwnTeam", HasOwnTeam, DbType.Boolean);
			sp.Command.AddParameter("@DeletionStatus", DeletionStatus, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamsByRecruitID(int? RecruitID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamsByRecruitID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamSeasonsMaps(int? FromSeasonID,int? ToSeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamSeasonsMaps" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@FromSeasonID", FromSeasonID, DbType.Int32);
			sp.Command.AddParameter("@ToSeasonID", ToSeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetTeamsInSeasonByDescription(int? SeasonID,string Description) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetTeamsInSeasonByDescription" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@Description", Description, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_TeamsGetViewableTeams(int? UserID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_TeamsGetViewableTeams" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UserAuthenticationsAuthenticate(string Username,string Password,string IPAddress) {
			StoredProcedure sp = new StoredProcedure("custRU_UserAuthenticationsAuthenticate" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@Username", Username, DbType.String);
			sp.Command.AddParameter("@Password", Password, DbType.String);
			sp.Command.AddParameter("@IPAddress", IPAddress, DbType.AnsiString);
			return sp;
		}
		public static StoredProcedure RU_UserGetManagerBySeasonId(int? SeasonId) {
			StoredProcedure sp = new StoredProcedure("custRU_UserGetManagerBySeasonId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UserGetManagerByTeamId(int? TeamId) {
			StoredProcedure sp = new StoredProcedure("custRU_UserGetManagerByTeamId" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@TeamId", TeamId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UserSalesInfoConnextGetByUserID(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_UserSalesInfoConnextGetByUserID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersFindByEmailAndSSN(string Email,string SSN,string SSNEncrypted) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersFindByEmailAndSSN" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@SSN", SSN, DbType.String);
			sp.Command.AddParameter("@SSNEncrypted", SSNEncrypted, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_UsersFindUsers(int? Top,string FirstName,string LastName,string CompanyID,string SSN,string PhoneCell,string PhoneHome,string Email,string UserName,int? UserID,string UserEmployeeTypeID,int? RecruitID,int? SeasonID,short? UserTypeID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersFindUsers" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@Top", Top, DbType.Int32);
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@CompanyID", CompanyID, DbType.String);
			sp.Command.AddParameter("@SSN", SSN, DbType.String);
			sp.Command.AddParameter("@PhoneCell", PhoneCell, DbType.String);
			sp.Command.AddParameter("@PhoneHome", PhoneHome, DbType.String);
			sp.Command.AddParameter("@Email", Email, DbType.String);
			sp.Command.AddParameter("@UserName", UserName, DbType.String);
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@UserEmployeeTypeID", UserEmployeeTypeID, DbType.String);
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@UserTypeID", UserTypeID, DbType.Int16);
			return sp;
		}
		public static StoredProcedure RU_UsersFindUsersInTeams(string FirstName,string LastName,string DeletionStatus,string TeamIDList,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersFindUsersInTeams" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@FirstName", FirstName, DbType.String);
			sp.Command.AddParameter("@LastName", LastName, DbType.String);
			sp.Command.AddParameter("@DeletionStatus", DeletionStatus, DbType.String);
			sp.Command.AddParameter("@TeamIDList", TeamIDList, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetActivationWaives(int? UserID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetActivationWaives" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetActiveUsers() {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetActiveUsers" ,DataService.GetInstance("SosHumanResourceProvider"));
			return sp;
		}
		public static StoredProcedure RU_UsersGetAllRecruitsByGPEmployeeID(string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetAllRecruitsByGPEmployeeID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_UsersGetAllUsersByRoleLocationID(int? RoleLocationID,int? SeasonID,string DeletionStatus) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetAllUsersByRoleLocationID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@DeletionStatus", DeletionStatus, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_UsersGetByAgemniInvoiceID(int? AgemniInvoiceID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetByAgemniInvoiceID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@AgemniInvoiceID", AgemniInvoiceID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetClothingSizes(int? SeasonID,string SizeType,bool? IsSales,bool? IsTech,bool? IsFemale,bool? IsMale) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetClothingSizes" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@SizeType", SizeType, DbType.String);
			sp.Command.AddParameter("@IsSales", IsSales, DbType.Boolean);
			sp.Command.AddParameter("@IsTech", IsTech, DbType.Boolean);
			sp.Command.AddParameter("@IsFemale", IsFemale, DbType.Boolean);
			sp.Command.AddParameter("@IsMale", IsMale, DbType.Boolean);
			return sp;
		}
		public static StoredProcedure RU_UsersGetDocumentsByGPID(string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetDocumentsByGPID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_UsersGetExpiringRightToWork() {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetExpiringRightToWork" ,DataService.GetInstance("SosHumanResourceProvider"));
			return sp;
		}
		public static StoredProcedure RU_UsersGetManagersTeamIDForSeason(int? UserID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetManagersTeamIDForSeason" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetMessageCenterInfo(string RecruitIDList,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetMessageCenterInfo" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitIDList", RecruitIDList, DbType.String);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetOrphanedSalesUsers() {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetOrphanedSalesUsers" ,DataService.GetInstance("SosHumanResourceProvider"));
			return sp;
		}
		public static StoredProcedure RU_UsersGetOwners() {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetOwners" ,DataService.GetInstance("SosHumanResourceProvider"));
			return sp;
		}
		public static StoredProcedure RU_UsersGetRecruitersBySeasonID(int? SeasonId) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetRecruitersBySeasonID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonId", SeasonId, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetRecruits(int? UserID,int? RoleLocationID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetRecruits" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetRepsTeamIDForSeason(int? UserID,int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetRepsTeamIDForSeason" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetSalesUsersByRecruitedById(int? RecruitedById) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetSalesUsersByRecruitedById" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitedById", RecruitedById, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetUserInfo(int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetUserInfo" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersGetUsersByNameOrGPID(string NameOrID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetUsersByNameOrGPID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@NameOrID", NameOrID, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_UsersGetUsersByRecruitIDs(string RecruitIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetUsersByRecruitIDs" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@RecruitIDList", RecruitIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_UsersGetUsersByUserTypeID(int? UserTypeID,string IDList,string BreakDownType,string DeletionStatus) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetUsersByUserTypeID" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserTypeID", UserTypeID, DbType.Int32);
			sp.Command.AddParameter("@IDList", IDList, DbType.String);
			sp.Command.AddParameter("@BreakDownType", BreakDownType, DbType.String);
			sp.Command.AddParameter("@DeletionStatus", DeletionStatus, DbType.String);
			return sp;
		}
		public static StoredProcedure RU_UsersGetViewableUsers(int? ViewingUserID,string CompanyID,int? UserID) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersGetViewableUsers" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@ViewingUserID", ViewingUserID, DbType.Int32);
			sp.Command.AddParameter("@CompanyID", CompanyID, DbType.String);
			sp.Command.AddParameter("@UserID", UserID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure RU_UsersLoadByMultiplePrimaryKeys(string UserIDList) {
			StoredProcedure sp = new StoredProcedure("custRU_UsersLoadByMultiplePrimaryKeys" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@UserIDList", UserIDList, DbType.String);
			return sp;
		}
		public static StoredProcedure VW_RecruitingStructureGetManageableTeams(int? SeasonID,int? RoleLocationID,int? RecruitID,int? TeamLocationID) {
			StoredProcedure sp = new StoredProcedure("custVW_RecruitingStructureGetManageableTeams" ,DataService.GetInstance("SosHumanResourceProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			sp.Command.AddParameter("@RoleLocationID", RoleLocationID, DbType.Int32);
			sp.Command.AddParameter("@RecruitID", RecruitID, DbType.Int32);
			sp.Command.AddParameter("@TeamLocationID", TeamLocationID, DbType.Int32);
			return sp;
		}
	}
}
 
