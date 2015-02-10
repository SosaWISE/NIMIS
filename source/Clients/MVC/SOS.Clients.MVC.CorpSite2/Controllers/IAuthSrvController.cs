using System.ServiceModel;
using System.ServiceModel.Web;
using SOS.Framework.Mvc.ActionResults;

namespace SOS.Clients.MVC.CorpSite2.Controllers
{

	public interface IAuthSrvController
	{
		[OperationContract]
		[WebGet(UriTemplate = "SosStart?ApplicationToken={szApplicationToken}", ResponseFormat = WebMessageFormat.Json)]
		JsonpResult SosStart(string szApplication);

		[OperationContract]
		[WebGet(UriTemplate = "GeneralAuthentication?username={sUsername}&password={sPassword}", ResponseFormat = WebMessageFormat.Json)]
		JsonpResult GeneralAuthentication(string sUsername, string sPassword);

		[OperationContract]
		[WebGet(UriTemplate = "TokenAuthentication?token={sToken}", ResponseFormat = WebMessageFormat.Json)]
		JsonpResult TokenAuthentication(string sToken);

		[OperationContract]
		[WebGet(UriTemplate = "TerminateSession", ResponseFormat = WebMessageFormat.Json)]
		JsonpResult TerminateSession();
	}
}