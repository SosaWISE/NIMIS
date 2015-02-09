using System.Collections.Specialized;
using System.Globalization;
using SOS.Lib.AuthorizeNet.Utility;

namespace SOS.Lib.AuthorizeNet.AIM.Requests
{
	/// <summary>
	/// A request that authorizes a transaction, no capture
	/// </summary>
	public sealed class AuthorizationRequest : GatewayRequest
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationRequest"/> class.
		/// </summary>
		/// <param name="cardNumber">The card number.</param>
		/// <param name="expirationMonthAndYear">The expiration month and year.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="description">The description.</param>
		public AuthorizationRequest(string cardNumber, string expirationMonthAndYear, decimal amount, string description)
		{
			SetApiAction(RequestAction.AuthorizeAndCapture);
			SetQueue(cardNumber, expirationMonthAndYear, amount, description);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuthorizationRequest"/> class.
		/// </summary>
		/// <param name="cardNumber">The card number.</param>
		/// <param name="expirationMonthAndYear">The expiration month and year.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="description">The description.</param>
		/// <param name="includeCapture">if set to <c>true</c> [include capture].</param>
		public AuthorizationRequest(string cardNumber, string expirationMonthAndYear, decimal amount, string description, bool includeCapture)
		{
			SetApiAction(includeCapture
				? RequestAction.AuthorizeAndCapture
				: RequestAction.Authorize);
			SetQueue(cardNumber, expirationMonthAndYear, amount, description);
		}

		/// <summary>
		/// Loader for use with form POSTS from web
		/// </summary>
		/// <param name="post"></param>
		public AuthorizationRequest(NameValueCollection post)
		{
			SetApiAction(RequestAction.AuthorizeAndCapture);
			Queue(ApiFields.CreditCardNumber, post[ApiFields.CreditCardNumber]);
			Queue(ApiFields.CreditCardExpiration, post[ApiFields.CreditCardExpiration]);
			Queue(ApiFields.Amount, post[ApiFields.Amount]);
		}

		private void SetQueue(string cardNumber, string expirationMonthAndYear, decimal amount, string description)
		{
			Queue(ApiFields.CreditCardNumber, cardNumber);
			Queue(ApiFields.CreditCardExpiration, expirationMonthAndYear);
			Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
			Queue(ApiFields.Description, description);
		}
	}
}
