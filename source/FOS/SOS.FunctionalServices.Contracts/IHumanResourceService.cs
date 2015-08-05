using System;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using System.Collections.Generic;
using IFnsRuUser = SOS.FunctionalServices.Contracts.Models.Data.IFnsRuUser;

namespace SOS.FunctionalServices.Contracts
{
	public interface IHumanResourceService : IFunctionalService
    {
        #region HumanResources
        IFnsResult<IFnsSalesRepInfo> SalesRepInfoGet(string companyId);
		IFnsResult<IFnsTechInfo> TechInfoGet(string companyId);
		IFnsResult<IFnsTechInfo> TechInfoSave(string companyId, long msAccountId, string gpEmployeeId);

		IFnsResult<IFnsSalesRepInfo> AccountSalesRep(long accountId);
		IFnsResult<IFnsTechInfo> AccountTechnician(long accountId);

        IFnsResult<List<IFnsRuTeamLocation>> GetRuTeamLocationList();

        IFnsResult<List<IFnsRuTechnician>> GetRuTechnicianList();
        IFnsResult<IFnsRuTechnician> RuTechnicianGetByTechnicianId(string technicianId);

		IFnsResult<List<IFnsRuSalesRep>> GetRuSalesRepList();

		IFnsResult<IFnsRuTeam> TeamGet(int teamid);
		IFnsResult<IFnsRuTeam> TeamSave(IFnsRuTeam fnsTeam, string gpEmployeeId);
		IFnsResult<object> TeamsSearch(object teamSearchInfo);

		IFnsResult<IFnsRuUser> UserGet(int userid);
		IFnsResult<IFnsRuUser> UserSave(IFnsRuUser fnsUser, string gpEmployeeId, int userid);
		IFnsResult<bool> UserPhotoSave(int userID, byte[] photoFile, string mimeType, string gpEmployeeId);
		IFnsResult<IFnsRuUserPhoto> UserPhotoGet(int userID);
		IFnsResult<object> UsersSearch(object userSearchInfo);
		IFnsResult<List<IFnsRuRecruit>> UserRecruits(int userid);

		IFnsResult<IFnsRuRecruit> RecruitGet(int recruitid);
		IFnsResult<IFnsRuRecruit> RecruitSave(IFnsRuRecruit fnsRecruit, string gpEmployeeId);

		IFnsResult<object> UserEmployeeTypes();
		IFnsResult<object> PhoneCellCarriers();
		IFnsResult<object> Seasons();
		IFnsResult<object> Payscales();
		IFnsResult<object> RoleLocations();
		IFnsResult<object> Schools();
		IFnsResult<object> UserTypes();
		IFnsResult<object> Teams();
		IFnsResult<object> TeamLocations();
        #endregion HumanResources

        #region Connext
		//IFnsResult<List<IFnsConnextAccountList>> ConnextAccountList(int userId, DateTime beginDate, DateTime endDate, bool isActive);
		//IFnsResult<IFnsConnextCombinedMonthlySalesDetails> ConnextCombinedMonthlySalesDetails(int userID, int salesMonth, int salesYear);
		//IFnsResult<IFnsConnextCustomerInfo> ConnextCustomerInfo(long customerMasterFileID);
		//IFnsResult<List<IFnsConnextSalesRanking>> ConnextSalesRanking(int userId, string resultType, string rankingGroup, string rankingPeriod, int rows);
		//IFnsResult<IFnsConnextSalesRepExtendedInfo> ConnextSalesRepInfo(int userId, bool isExtended);
		//IFnsResult<IFnsSalesSalespersonMonthlyCommissions> SalespersonMonthlyCommissions(int userID, int salesMonth,
		//	int salesYear);
		//IFnsResult<List<IFnsSalesSalespersonMonthlyHolds>> SalespersonMonthlyHolds(int userID, int salesMonth, int salesYear);
		//IFnsResult<IFnsSalesSalespersonMonthlyEarningsSummary> SalespersonMonthlyEarningsSummary(int userID, int salesMonth,
		//	int salesYear);
        #endregion Connext
    }
}
