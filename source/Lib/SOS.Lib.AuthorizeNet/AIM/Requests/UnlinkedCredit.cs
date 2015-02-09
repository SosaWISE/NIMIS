using System.Globalization;
using SOS.Lib.AuthorizeNet.Utility;

namespace SOS.Lib.AuthorizeNet.AIM.Requests
{
	public class UnlinkedCredit : GatewayRequest
	{

		public UnlinkedCredit(decimal amount, string cardNumber)
		{
			SetApiAction(RequestAction.UnlinkedCredit);
			Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
			Queue(ApiFields.CreditCardNumber, cardNumber);
		}
	}
}
