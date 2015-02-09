/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 13:19
 * 
 * Description:  Describes the Authentication Service for SOS.
 *********************************************************************************************************************/

using System;
using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.AuthenticationControl;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Contracts
{
	public interface IAuthenticationService : IFunctionalService
	{
		IFnsAcSessionModel SosStart(string szAccessToken, string szIPAddress, int timezoneOffset);

		IFnsResult<IFnsAcSessionModel> TerminateSession(long lSessionId);

		//IFnsAcAuthenticationModel SosAuthenticate(long lSessionId, long lDealerId, string szUsername, string szPassword);

		IFnsResult<IFnsAcSessionModel> SessionInfoGet(long lSessionID, string szApplicationToken);

		IFnsResult<IFnsAcSessionModel> SessionValidate(long lSessionID, string szApplicationToken, int nMinutes);

		IFnsResult<IFnsAcGeneralAuthenticationModel> GeneralAuthentication(string sUsername, string sPassword, string sIPAddress, int timezoneOffset);

		IFnsResult<IFnsAcGeneralAuthenticationModel> DecryptToken(string sTokenStream);

		IFnsResult<IFnsWiseCrmDealerUserModel> AuthenticationDealerViaToken(string sTokenStream);

		IFnsResult<List<IFnsAeGpsClient>> GpsClientRead(long? customerMasterFileId, long? customerID);

		IFnsResult<IFnsAcCmsUser> AuthenticateCmsUser(string username, string password, long sessionID, string appToken);
		IFnsResult<List<string>> SessionApps(long sessionID);
		IFnsResult<List<string>> SessionActions(long sessionID);
		IFnsResult<bool> HasPermission(long sessionID, string applicationID, string actionID);

		IFnsResult<IFnsRuUser> SalesRepRead(string companyID, string username);

		DateTime? GetLocalDateTime();

		DateTime? GetUTCDateTime();
	}
}