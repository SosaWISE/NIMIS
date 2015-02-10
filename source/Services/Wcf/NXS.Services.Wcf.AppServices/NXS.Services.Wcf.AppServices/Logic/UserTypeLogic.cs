using System.Collections.Generic;
using System.Linq;
using SOS.Data.HumanResource;
using SOS.Data.HumanResource.ControllerExtensions;

namespace NXS.Services.Wcf.AppServices.Logic
{
	public static class UserTypeLogic
	{
		public static IList<RU_UserType> AllThatMustManageTeam(HumanResourceDataContext recruitDB)
		{
			List<RU_UserType> list = recruitDB.RU_UserTypes.LoadAllCached();

			list = list.Where(item => MustManageTeam(recruitDB, item)).ToList();

			return list;
		}

		//public static bool CanManageTeam(HumanResourceDataStoredProcedureManager recruitDB, short userTypeID)
		//{
		//    RU_UserType userType = recruitDB.RU_UserTypes.LoadAllCached().FirstOrDefault(a => a.UserTypeID == userTypeID);
		//    return CanManageTeam(recruitDB, userType);
		//}
		public static bool CanManageTeam(HumanResourceDataContext recruitDB, RU_UserType userType)
		{
			bool result = false;
			if (userType != null)
			{
				result = FallsUnderTeamType(recruitDB, RU_UserTypeTeamType.UserTypeTeamTypeEnum.Team_Manager, userType.UserTypeTeamTypeID);
			}
			return result;
		}

		//public static bool MustManageTeam(HumanResourceDataStoredProcedureManager recruitDB, short userTypeID)
		//{
		//    RU_UserType userType = recruitDB.RU_UserTypes.LoadAllCached().FirstOrDefault(a => a.UserTypeID == userTypeID);
		//    return MustManageTeam(recruitDB, userType);
		//}
		public static bool MustManageTeam(HumanResourceDataContext recruitDB, RU_UserType userType)
		{
			bool result = false;
			if (userType != null)
			{

				if (CanManageTeam(recruitDB, userType))
				{

					//REVIEW: there is probably a better way to tell if someone has to manage a team
					result = userType.ReportingLevel == 2;
				}
			}
			return result;
		}

		public static bool FallsUnderTeamType(HumanResourceDataContext recruitDB, RU_UserTypeTeamType.UserTypeTeamTypeEnum parentUserTeamTypeEnum, int teamTypeID)
		{
			var nParentID = (int)parentUserTeamTypeEnum;

			if (nParentID == teamTypeID) return true;

			List<RU_UserTypeTeamType> list = recruitDB.RU_UserTypeTeamTypes.LoadAllCached();
			foreach (var item in list)
			{
				//find the correct teamType
				if (teamTypeID == item.UserTypeTeamTypeID)
				{
					//check if parent matches parentID
					return FallsUnderTeamTypeHelper(item, nParentID);
				}
			}
			return false;
		}

		private static bool FallsUnderTeamTypeHelper(RU_UserTypeTeamType teamType, int parentID)
		{
			if (teamType.ParentID == null)
			{
				return false;
			}

			if (teamType.ParentID == parentID)
			{
				return true;
			}

			// Default execution path
			return FallsUnderTeamTypeHelper(teamType.Parent, parentID);
		}
	}
}
