using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace NXS.Data.GreatPlains
{
	public partial class CustomerInvoicesView
	{
		#region Properties

		#region Private

		private static readonly List<string> _paymentPrefixes;

		#endregion Private

		#region Public

		public bool IsPayment
		{
			get
			{
				if (string.IsNullOrEmpty(InvoiceNumber))
				{
					return false;
				}
				foreach (string currPreifix in _paymentPrefixes)
				{
					if (InvoiceNumber.ToUpper().StartsWith(currPreifix))
						return true;
				}
				return false;
			}
		}

		#endregion Public

		#endregion Properties

		#region Constructors

		static CustomerInvoicesView()
		{
			_paymentPrefixes = new List<string>();
			_paymentPrefixes.Add("PYMT-"); // Payment
			_paymentPrefixes.Add("CR-"); // Credit
			_paymentPrefixes.Add("RET-"); // Return invoice
		}

		#endregion Constructors
	}
}