using System.ServiceModel;
using System.ServiceModel.Web;
using SOS.Services.Interfaces.Models;

namespace SOS.Services.Interfaces
{
	[ServiceContract(Namespace = "SosServices")]
	//[ServiceUrl(ServiceUrls.VERSION, ServiceUrls.EXECUTE_SERVICE)]
	public interface IExecuteSvc
	{
		[OperationContract]
		[WebGet(UriTemplate = "Receive?ReceiverToken={szReceiverToken}", ResponseFormat = WebMessageFormat.Json)]
		string Receive(string szReceiverToken);

		[OperationContract]
		[WebInvoke(Method = "POST", UriTemplate = "TxtWireReceivePost", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml)]
		TxtWireResponse TxtWireReceivePost(TxtWirePostInfo oParams);

		[OperationContract]
		[WebGet(UriTemplate = "TxtWireReceive?title={szTitle}")]
		string TxtWireReceive(string szTitle /*, string code, string shortcode, string message, string phone
			, string carrier, string keyword, string group, string custom_ticket, string default_keyword, string user_name
			, string password, string api_key*/);
	}
}