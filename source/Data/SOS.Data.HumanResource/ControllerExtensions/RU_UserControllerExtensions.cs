using System;
using SOS.Data.Extensions;
using SOS.Lib.Util;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_User;
using ARCollection = SOS.Data.HumanResource.RU_UserCollection;
using ARController = SOS.Data.HumanResource.RU_UserController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_UserControllerExtensions
	{
		public static AR LoadBySalesRepId(this ARController oCntlr, string szSalesRepId)
		{
			/** Initialize. */
			var oQry = AR.Query();
			oQry.WHERE(AR.Columns.GPEmployeeId, szSalesRepId);
			AR oResult = oCntlr.LoadSingle(oQry);

			/** Return rsult. */
			return oResult;
		}

		public static AR LoadByTechId(this ARController oCntlr, string szTechId)
		{
			/** Initialize. */
			var oQry = AR.Query();
			oQry.WHERE(AR.Columns.GPEmployeeId, szTechId);
			AR oResult = oCntlr.LoadSingle(oQry);

			/** Return rsult. */
			return oResult;
		}
		
		public static ARCollection FindUsers(this ARController cntrl,
			bool SearchLike,
			int? Top,
			//
			int? UserID,
			int? RecruitID,
			int? SeasonID,
			short? UserTypeID,
			//
			string FirstName,
			string LastName,
			string CompanyID,
			string SSN,
			string CellPhone,
			string HomePhone,
			string Email,
			string UserName,
			string UserEmployeeTypeID
			)
		{
			return cntrl.LoadCollection(HumanResourceDataStoredProcedureManager.RU_UsersFindUsers(
				Top
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(FirstName), SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(LastName), SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(CompanyID), SearchLike)
				, StringUtility.NullIfWhiteSpace(SSN)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(CellPhone), SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(HomePhone), SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(Email), SearchLike)
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(UserName), SearchLike)
				, UserID
				, StringUtility.ReplaceWildCard(StringUtility.NullIfWhiteSpace(UserEmployeeTypeID), SearchLike)
				, RecruitID
				, SeasonID
				, UserTypeID
			));
		}

		public static ARCollection LoadAllActive(this ARController controller)
		{
			Query qry = AR.Query()
				.Active()
				.ORDER_BY(AR.Columns.FullName);

			return controller.LoadCollection(qry);
		}

		public static int? GetUserIDForCompanyID(this ARController controller, string companyID)
		{
			SqlQuery qry = new Select(AR.Query().Provider, AR.Columns.UserID).From<RU_User>()
				.Where(AR.Columns.GPEmployeeId).IsEqualTo(companyID);

			object obj = qry.ExecuteScalar();
			return (obj == null || Convert.IsDBNull(obj)) ? (int?)null : (int)obj;
		}
		public static int? GetUserIDForUsername(this ARController controller, string username)
		{
			SqlQuery qry = new Select(AR.Query().Provider, AR.Columns.UserID).From<RU_User>()
				.Where(AR.Columns.UserName).IsEqualTo(username);

			object obj = qry.ExecuteScalar();
			return (obj == null || Convert.IsDBNull(obj)) ? (int?)null : (int)obj;
		}
		public static ARCollection GetExpiringRightToWork(this ARController controller, DateTime beforeDate)
		{
			Query qry = AR.Query()
				.Active()
				.AND(AR.Columns.RightToWorkExpirationDate, Comparison.LessOrEquals, beforeDate);

			return controller.LoadCollection(qry);
		}

	}
}
