/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/16/11
 * Time: 08:14
 * 
 * Description:  Describes for the services interfaces will be.
 *********************************************************************************************************************/

using System.ServiceModel;
using System.ServiceModel.Web;
using SOS.Services.Interfaces.Models;

namespace SOS.Services.Interfaces
{
	[ServiceContract(Namespace="SosServices")]
	public interface IAuthSvc
	{
		[OperationContract]
		[WebGet(UriTemplate = "SosStart?ApplicationToken={szApplicationToken}", ResponseFormat = WebMessageFormat.Json)]
		SosResult<SosSessionInfo> SosStart(string szApplicationToken);

		[OperationContract]
		[WebGet(UriTemplate = "CheckSessionStatus", ResponseFormat = WebMessageFormat.Json)]
		SosResult<SosUser> CheckSessionStatus();

		[OperationContract]
		[WebGet(UriTemplate = "SosAuthenticate?SessionId={lSessionId}&DealerId={lDealerId}&Username={szUsername}&Password={szPassword}", ResponseFormat = WebMessageFormat.Json)]
		SosResult<SosUser> SosWiseCrmAuthenticate(long lSessionId, long lDealerId, string szUsername, string szPassword);

		[OperationContract]
		[WebGet(UriTemplate = "TokenAuthentication?token={sToken}", ResponseFormat = WebMessageFormat.Json)]
		SosResult<SosUser> TokenAuthentication(string sToken);

		[OperationContract]
		[WebGet(UriTemplate = "TerminateCurrentSession", ResponseFormat = WebMessageFormat.Json)]
		SosResult<SosSessionInfo> TerminateCurrentSession();
	}
}
