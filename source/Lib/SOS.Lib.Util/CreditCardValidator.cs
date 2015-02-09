namespace SOS.Lib.Util
{
	public class CreditCardValidator
	{
		// Mark default constructor as private since the class will provide only static methods
		private CreditCardValidator()
		{
		}

		/// <summary>
		/// Tests whether the given credit card number is valid according to the Lunh Algorithm.
		/// </summary>
		/// <param name="cardNumber">The card number to test.</param>
		/// <returns>True if the card number passes validation, false if not.</returns>
		public static bool IsValidCreditCardNumber(string cardNumber)
		{
			var number = new byte[16]; // number to validate

			// Remove non-digits
			int nDigits = 0;
			for (int i = 0; i < cardNumber.Length; i++)
			{
				if (char.IsDigit(cardNumber, i))
				{
					if (nDigits == 16)
						return false; // number has too many digits
					number[nDigits++] = byte.Parse(cardNumber.Substring(i, 1));
				}
			}

			if (nDigits < 15)
				return false; // Gotta have at least 15 digits

			switch (number[0])
			{
				case 3: // American express
					if (nDigits != 15) // Amex has 15 digits
						return false;
					break;
				case 4: // Visa
				case 5: // Mastercard
				case 6: // Discover
					if (nDigits != 16)
						return false; // These card types have 16 digits
					break;
				default:
					return false; // Not a recognized type
			}

			// Validate with the Luhn Algorithm
			int sum = 0;
			for (int i = nDigits - 1; i >= 0; i--)
			{
				if (i % 2 == nDigits % 2)
				{
					int n = number[i] * 2;
					sum += (n / 10) + (n % 10);
				}
				else
				{
					sum += number[i];
				}
			}
			return (sum % 10 == 0);
		}
	}
}