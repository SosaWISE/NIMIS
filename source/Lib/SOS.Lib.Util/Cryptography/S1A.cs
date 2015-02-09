using System;
using System.Text;

namespace SOS.Lib.Util.Cryptography
{
	/// <summary>
	/// This static class will be used to create a sofisticated 1 way encryption algorythm.
	/// This algorythm is good for encrypting keys for a 2 way encryption algorythm and also
	/// a asymetrical and symetrical algorythms.
	/// </summary>
	public class S1A
	{
		/// <summary>
		/// Encodes a stream of bytes using this sofisticated 1 way algorythm.
		/// </summary>
		/// <param name="bytInput">byte[] that contains the value to encrypt.</param>
		/// <returns>byte[]</returns>
		public static byte[] EncyptByte(byte[] bytInput)
		{
			// Local vars
			int i;

			// Begin the hashing
			for (i = 0; i < 20; i++)
			{
				bytInput = (i%2) == 0 ? MD5.EncyptByte(bytInput) : SHA1.EncyptByte(bytInput);
			}
			return bytInput;
			// Return result
		}

		/// <summary>
		/// Encodes a stream of bytes using this sofisticated 1 way algorythm.
		/// </summary>
		/// <param name="strInput">string that contains the value to encrypt.</param>
		/// <returns>byte[]</returns>
		public static byte[] EncryptByte(string strInput)
		{
			return EncyptByte(Encoding.ASCII.GetBytes(strInput));
		}

		/// <summary>
		/// Encodes a stream of bytes using this sofisticated 1 way algorythm.
		/// </summary>
		/// <param name="strInput">string that contains the value to encrypt.</param>
		/// <returns>string</returns>
		public static string EncryptString(string strInput)
		{
			return Convert.ToBase64String(EncyptByte(Encoding.ASCII.GetBytes(strInput)));
		}
	}
}