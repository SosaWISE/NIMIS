using System.Globalization;

// ReSharper disable CheckNamespace
namespace AuthorizeNet
// ReSharper restore CheckNamespace
{
	/// <summary>
	/// Credits, or refunds, the amount back to the user
	/// </summary>
	public class CreditRequest : GatewayRequest
	{

		/// <summary>
		/// Initializes a new instance of the <see cref="CreditRequest"/> class.
		/// </summary>
		/// <param name="transactionId">The transaction id.</param>
		/// <param name="amount">The amount.</param>
		/// <param name="cardNumber">The card number.</param>
		public CreditRequest(string transactionId, decimal amount, string cardNumber)
		{

			SetApiAction(RequestAction.Credit);

			Queue(ApiFields.TransactionID, transactionId);
			Queue(ApiFields.Amount, amount.ToString(CultureInfo.InvariantCulture));
			Queue(ApiFields.CreditCardNumber, cardNumber);

		}
	}
}
