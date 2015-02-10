using SOS.FunctionalServices.Contracts.Models;
using SOS.Services.Interfaces.Models;

namespace SOS.Services.Wcf.Crm.Helper
{
	public static class McModelHelper
	{
		public static McModels.DealerUser CastMcDealerUserFromFns(IFnsMcDealerUser oFnsItem)
		{
			/** Initialize. */
			var oResult = new McModels.DealerUser
				{
					DealerUserID = oFnsItem.DealerUserID
					, DealerUserTypeId = oFnsItem.DealerUserTypeId
					, DealerUserType = oFnsItem.DealerUserType
					, DealerId = oFnsItem.DealerId
					, AuthUserId = oFnsItem.AuthUserId
					, UserID = oFnsItem.UserID
					, Firstname = oFnsItem.Firstname
					, Middlename = oFnsItem.Middlename
					, Lastname = oFnsItem.Lastname
					, FullName = oFnsItem.FullName
					, Email = oFnsItem.Email
					, PhoneWork = oFnsItem.PhoneWork
					, PhoneCell = oFnsItem.PhoneCell
					, ADUsername = oFnsItem.ADUsername
					, Username = oFnsItem.Username
					, Password = oFnsItem.Password
					, LastLoginOn = oFnsItem.LastLoginOn
					, IsActive = oFnsItem.IsActive
					, IsDeleted = oFnsItem.IsDeleted
					, ModifiedOn = oFnsItem.ModifiedOn
					, ModifiedBy = oFnsItem.ModifiedBy
					, CreatedOn = oFnsItem.CreatedOn
					, CreatedBy = oFnsItem.CreatedBy
				};

			/** Return result. */
			return oResult;
		}
	}
}