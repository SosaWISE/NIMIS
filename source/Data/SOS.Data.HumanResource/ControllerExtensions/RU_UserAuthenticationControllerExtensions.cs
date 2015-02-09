using AR = SOS.Data.HumanResource.RU_UserAuthentication;
using ARCollection = SOS.Data.HumanResource.RU_UserAuthenticationCollection;
using ARController = SOS.Data.HumanResource.RU_UserAuthenticationController;

namespace SOS.Data.HumanResource.ControllerExtensions
{
// ReSharper disable once InconsistentNaming
	public static class RU_UserAuthenticationControllerExtensions
	{
		public static ARCollection Authenticate(this ARController oCntlr, string szUid, string szPwd, string szIP)
		{
			/** Initialization. */
			return
				oCntlr.LoadCollection(HumanResourceDataStoredProcedureManager.RU_UserAuthenticationsAuthenticate(szUid, szPwd, szIP));
		}
	}
}
