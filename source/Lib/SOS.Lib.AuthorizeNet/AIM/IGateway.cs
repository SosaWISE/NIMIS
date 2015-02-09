using SOS.Lib.AuthorizeNet.AIM.Requests;
using SOS.Lib.AuthorizeNet.AIM.Responses;

namespace SOS.Lib.AuthorizeNet.AIM
{
	public interface IGateway
	{
		string ApiLogin { get; set; }
		string TransactionKey { get; set; }
		IGatewayResponse Send(IGatewayRequest request);
		IGatewayResponse Send(IGatewayRequest request, string description);
	}
}