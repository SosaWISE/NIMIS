using System.Globalization;
using SOS.Lib.AuthorizeNet.Utility;

namespace SOS.Lib.AuthorizeNet.AIM.Requests
{
	/// <summary>
	/// A request representing a Capture - the final transfer of funds that happens after an auth.
	/// </summary>
	public class CaptureRequest : GatewayRequest
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="CaptureRequest"/> class.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="transactionId">The transaction id.</param>
		/// <param name="authCode">The auth code.</param>
		public CaptureRequest(decimal amount, string transactionId, string authCode)
		{
			SetApiAction(RequestAction.Capture);
			Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
			Queue(ApiFields.TransactionID, transactionId);
			Queue(ApiFields.AuthorizationCode, authCode);
		}

	}
}
