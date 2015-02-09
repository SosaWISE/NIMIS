using System.Collections.Specialized;
using System.Text;
using System.Web;
using SOS.Lib.AuthorizeNet.Utility;

namespace SOS.Lib.AuthorizeNet.AIM.Responses
{
	public class SIMResponse : IGatewayResponse
	{
		private readonly NameValueCollection _post;
/*
		string _merchantHash;
*/

		public SIMResponse(NameValueCollection post)
		{
			_post = post;
		}


		public SIMResponse() : this(HttpContext.Current.Request.Form)
		{
		}

		public string MD5Hash
		{
			get { return FindKey("x_MD5_Hash"); }
		}

		public string CardType
		{
			get { return FindKey(ApiFields.CreditCardType); }
		}

		#region IGatewayResponse Members

		public string ResponseCode
		{
			get { return FindKey("x_response_code"); }
		}

		public string Message
		{
			get { return FindKey("x_response_reason_text"); }
		}

		public bool Approved
		{
			get { return ResponseCode == "1"; }
		}

		public string InvoiceNumber
		{
			get { return FindKey(ApiFields.InvoiceNumber); }
		}

		public decimal Amount
		{
			get
			{
				string sAmount = FindKey(ApiFields.Amount);
				decimal result;
				if (!decimal.TryParse(sAmount, out result))
					result = 0.00M;
				return result;
			}
		}

		public string TransactionID
		{
			get { return FindKey(ApiFields.TransactionID); }
		}

		public string AuthorizationCode
		{
			get { return FindKey(ApiFields.AuthorizationCode); }
		}

		public string CardNumber
		{
			get { return FindKey(ApiFields.CreditCardNumber); }
		}

		#endregion

		/// <summary>
		/// Validates that what was passed by Auth.net is valid
		/// </summary>
		public bool Validate(string merchantHash, string apiLogin)
		{
			return Crypto.IsMatch(merchantHash, apiLogin, TransactionID, Amount, MD5Hash);
		}

		public string GetValue(string name)
		{
			return FindKey(name);
		}

		private string FindKey(string key)
		{
			string result = null;

			if (_post[key] != null)
			{
				result = _post[key];
			}

			return result;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.AppendFormat("<li>Code = {0}", ResponseCode);
			sb.AppendFormat("<li>Auth = {0}", AuthorizationCode);
			sb.AppendFormat("<li>Message = {0}", Message);
			sb.AppendFormat("<li>TransID = {0}", TransactionID);
			sb.AppendFormat("<li>Approved = {0}", Approved);
			return sb.ToString();
		}
	}
}