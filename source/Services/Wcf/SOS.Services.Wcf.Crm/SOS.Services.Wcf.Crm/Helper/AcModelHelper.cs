using SOS.FunctionalServices.Contracts.Models;
using SOS.Services.Interfaces.Models;

namespace SOS.Services.Wcf.Crm.Helper
{
	public static class AcModelHelper
	{
		public static SosSessionInfo CastToSosSessionInfo(IFnsAcSessionModel oSession)
		{
			var oResult = new SosSessionInfo
			{
				SessionId = oSession.SessionID
				, ApplicationId =  oSession.ApplicationId
				, UserId =  oSession.UserId
				, IPAddress =  oSession.IPAddress
				, LastAccessedOn =  oSession.LastAccessedOn
				, SessionTerminated =  oSession.SessionTerminated
				, CreatedOn =  oSession.CreatedOn
			};

			/** Return result. */
			return oResult;
		}
	}
}