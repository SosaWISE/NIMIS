using System.Web;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.SmsGateways;

namespace SOS.FunctionalServices.Contracts
{
	public interface IReceiverEngineService : IFunctionalService
	{
		IFnsREResult SaveRequest(HttpContext oContext);

		IFnsREResult SaveTxtWireSignal(string szTitle, string szCode, string szShortCode, string szMessage, string szPhone,
						string szCarrier, string szKeyword, string szGroupName, string szCustomTicket, string szDefaultKeyword,
						string szUsername, string szPassword, string szApiKey);

		IFnsResult ExecuteSignalToAvantGuard(IFnsSmsGwyTxtEnvModel oModelEnv);
	}
}