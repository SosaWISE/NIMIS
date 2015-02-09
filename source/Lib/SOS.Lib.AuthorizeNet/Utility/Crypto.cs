using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SOS.Lib.AuthorizeNet.Utility
{
	public class Crypto
	{
		/// <summary>
		/// Generates the HMAC-encrypted hash to send along with the SIM form
		/// </summary>
		/// <param name="transactionKey">The merchant's transaction key</param>
		/// <param name="login">The merchant's Authorize.NET API Login</param>
		/// <param name="amount">The amount of the transaction</param>
		/// <param name="sequence">??? </param>
		/// <param name="timeStamp">??? </param>
		/// <returns></returns>
		public static string GenerateFingerprint(string transactionKey, string login, decimal amount, string sequence,
												 string timeStamp)
		{
			string keyString = string.Format("{0}^{1}^{2}^{3}^", login, sequence, timeStamp, amount.ToString(CultureInfo.InvariantCulture));
			string result = EncryptHMAC(transactionKey, keyString);
			return result;
		}

		/// <summary>
		/// Decrypts provided string parameter
		/// </summary>
		public static bool IsMatch(string key, string apiLogin, string transactionID, decimal amount, string expected)
		{
			string unencrypted = string.Format("{0}{1}{2}{3}", key, apiLogin, transactionID, amount.ToString(CultureInfo.InvariantCulture));

			var md5 = new MD5CryptoServiceProvider();
			string hashed = Regex.Replace(BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(unencrypted))), "-", "");

			// And return it
			return hashed.Equals(expected);
		}

		/// <summary>
		/// Encrypts the key/value pair supplied using HMAC-MD5
		/// </summary>
		public static string EncryptHMAC(string key, string value)
		{
			// The first two lines take the input values and convert them from strings to Byte arrays
			byte[] yHMACkey = (new ASCIIEncoding()).GetBytes(key);
			byte[] yHMACdata = (new ASCIIEncoding()).GetBytes(value);

			// create a HMACMD5 object with the key set
			var myhmacMD5 = new HMACMD5(yHMACkey);

			//calculate the hash (returns a byte array)
			byte[] yHMAChash = myhmacMD5.ComputeHash(yHMACdata);

			//loop through the byte array and add append each piece to a string to obtain a hash string
			string fingerprint = "";
			for (int i = 0; i < yHMAChash.Length; i++)
			{
				fingerprint += yHMAChash[i].ToString("x").PadLeft(2, '0');
			}

			return fingerprint;
		}

		/// <summary>
		/// Generates a 4-place sequence number randomly
		/// </summary>
		/// <returns></returns>
		public static string GenerateSequence()
		{
			var random = new Random();
			return (random.Next(0, 1000)).ToString(CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Generates a timestamp in seconds from 1970
		/// </summary>
		public static int GenerateTimestamp()
		{
			return ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds);
		}
	}
}