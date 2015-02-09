using System;

namespace SOS.Data.SosCrm
{
	public partial class AE_BillingCustomer
	{
		#region Properties

		public string CcNameOnRaw
		{
			get { return DecryptValueSafely(CcNameOn); }
			set { CcNameOn = EncryptValueSafely(value); }
		}

		public string CcNumberRaw
		{
			get { return DecryptValueSafely(CcNumber); }
			set { CcNumber = EncryptValueSafely(value); }
		}

		public string CcvRaw
		{
			get { return DecryptValueSafely(Ccv); }
			set { Ccv = EncryptValueSafely(value); }
		}

		public string BkRoutingNumberRaw
		{
			get { return DecryptValueSafely(BkRoutingNumber); }
			set { BkRoutingNumber = EncryptValueSafely(value); }
		}

		public string BkAccountNumberRaw
		{
			get { return DecryptValueSafely(BkAccountNumber); }
			set { BkAccountNumber = EncryptValueSafely(value); }
		}

		public string BkCheckNumberRaw
		{
			get { return DecryptValueSafely(BkCheckNumber); }
			set { BkCheckNumber = EncryptValueSafely(value); }
		}

		#endregion Properties

		#region Methods

		/// <summary>
		/// Given a value it will encrypt it.  If the value is null or an empty string it will pass it as a null.
		/// </summary>
		/// <param name="szValue"></param>
		/// <returns></returns>
		private string EncryptValueSafely(string szValue)
		{
			/** Init. */
			if (string.IsNullOrWhiteSpace(szValue)) return null;

			/** Return encryption. */
			return Lib.Util.Cryptography.TripleDES.EncryptString(szValue, null);
		}

		/// <summary>
		/// If the value passed is not decryped then it will return that value otherwise returns decrypted value.
		/// </summary>
		/// <param name="szValue">string</param>
		/// <returns>string</returns>
		private string DecryptValueSafely(string szValue)
		{
			/** Initialize. */
			string sResult = szValue;

			/** Validate input. */
			if (string.IsNullOrWhiteSpace(szValue)) return szValue;

			/** Check for a value that is not decrypted. */
			try
			{
				sResult = Lib.Util.Cryptography.TripleDES.DecryptString(szValue, null);
			}
			catch (Exception oEx)
			{
				System.Diagnostics.Debug.WriteLine(string.Format("The following error occrred when decrypting in AE_BillingCustomer: {0}"
					, oEx.Message));
			}

			/** return value. */
			return sResult;
		}

		#endregion Methods
	}
}
