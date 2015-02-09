using System;
using System.Security.Cryptography;
using System.Text;

namespace SOS.Lib.Util.Cryptography
{
	/// <summary>
	/// SHA1 encryption algorythm is good for encrypting passwords and items that do not 
	/// need to be decrypted.  It is a good one-way 128 bitalgorythm.
	/// </summary>
	public class SHA1
	{
		/// <summary>
		/// Encodes a stream of bytes using SHA1 encryption.
		/// </summary>
		/// <param name="bytInput">byte[] that contains the value to encrypt.</param>
		/// <returns>byte[]</returns>
		public static byte[] EncyptByte(byte[] bytInput)
		{
			var oSha1 = new SHA1CryptoServiceProvider();
			return oSha1.ComputeHash(bytInput);
		}

		/// <summary>
		/// Encodes a string using SHA1 encryption
		/// </summary>
		/// <param name="strInput">string that contains the value to encrypt.</param>
		/// <returns>byte[]</returns>
		public static byte[] EncryptByte(string strInput)
		{
			return EncyptByte(Encoding.ASCII.GetBytes(strInput));
		}

		/// <summary>
		/// Encodes a string using SHA1 encryption
		/// </summary>
		/// <param name="strInput">string that contains the value to encrypt.</param>
		/// <returns>string</returns>
		public static string EncryptString(string strInput)
		{
			return Convert.ToBase64String(EncyptByte(Encoding.ASCII.GetBytes(strInput)));
		}
	}
}