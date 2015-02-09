using System.Collections.Generic;
using SubSonic;
using AR = SOS.Data.HumanResource.RU_UsersCallerIDView;
using ARCollection = SOS.Data.HumanResource.RU_UsersCallerIDViewCollection;
using ARController = SOS.Data.HumanResource.RU_UsersCallerIDViewController;


namespace SOS.Data.HumanResource.ControllerExtensions
{											 
// ReSharper disable once InconsistentNaming
	public static class RU_UsersCallerIDViewsControllerExtensions
	{
		public static IList<RU_UsersCallerIDView> LookupByTelephone(this RU_UsersCallerIDViewController controller, string callerID)
		{
			if (string.IsNullOrEmpty(callerID))
			{
				return null;
			}

			callerID = string.Join(null, System.Text.RegularExpressions.Regex.Split(callerID, "[^\\d]")); //exacts integers from caller id
			if (callerID.Length >= 7)//assumes number is a valid callerID is valid nanpa number and not an internal extension
			{
				//Remove prefix digit number from caller ID
				if (callerID.Substring(0, 1) == "1")
				{
					callerID = callerID.Substring(1, callerID.Length - 1);
				}
			}
			else
			{
				return null;
			}

			Query q = RU_UsersCallerIDView.Query().AND(RU_UsersCallerIDView.Columns.CallerID, callerID);

			IList<RU_UsersCallerIDView> ruUsersCallerIDViewCollection = controller.LoadCollection(q);
			return ruUsersCallerIDViewCollection;

		}
	}
}