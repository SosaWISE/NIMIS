﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace SOS.Lib.Util.Cryptography
{
	/// <summary>
	/// TirpleDES is a two way encryption algorythm that implememts 128 bit encryption and is good
	/// for items that have to be decrypted.
	/// </summary>
	public class TripleDES : MarshalByRefObject
	{
		private const string _ENCRYPTIONKEY = "טּטּטּﻃﺸﯜﯟﯤﯼ ﻪﻫﻚﺽﭻ ﺅﺄﷲﯓﮠﮍ  ﯝﮥﮗﮜﺾﺣﺷﯙﮎﻼﻸﻶﺀﭛשׂגּהּטּ";
//		private const string _ENCRYPTIONKEY = "ÆëAÓA17ª®¿Êîßéð¶¹æì£ Ø";
        private const string _OLDENCRYPTIONKEY = "ďťŤǾǼżΨΧΥΫάΪВЕДЉЊξЖЗץבڜڛڬګڪڨڀ۞۝";

		/// <summary>
		/// Encodes a stream of bytes using DES encryption with a pass key. Lowest level method that 
		/// handles all work.
		/// </summary>
		/// <param name="bytInputString"></param>
		/// <param name="strKey"></param>
		/// <returns>byte</returns>
		public static byte[] EncryptBytes(byte[] bytInputString, string strKey)
		{
			if (strKey == null) strKey = _ENCRYPTIONKEY;

			var o3Des = new TripleDESCryptoServiceProvider();
			var oHashmd5 = new MD5CryptoServiceProvider();

			o3Des.Key = oHashmd5.ComputeHash(Encoding.ASCII.GetBytes(strKey));
			o3Des.Mode = CipherMode.ECB;

			ICryptoTransform oTrans = o3Des.CreateEncryptor();

			byte[] oBuffer = bytInputString;
			return oTrans.TransformFinalBlock(oBuffer, 0, oBuffer.Length);
		}

		/// <summary>
		/// Encrypts a string into bytes using DES encryption with a Passkey. 
		/// </summary>
		/// <param name="strInputString"></param>
		/// <param name="strKey"></param>
		/// <returns>byte</returns>
		public static byte[] EncryptBytes(string strInputString, string strKey)
		{
			return EncryptBytes(Encoding.ASCII.GetBytes(strInputString), strKey);
		}

		/// <summary>
		/// Encrypts bytes into a string using Triple DES encryption with a Passkey
		/// </summary>
		/// <param name="bytInputString"></param>
		/// <param name="strKey"></param>
		/// <returns></returns>
		public static string EncryptString(byte[] bytInputString, string strKey)
		{
			return Convert.ToBase64String(EncryptBytes(bytInputString, strKey));
		}

		/// <summary>
		/// Encrypts a string using Triple DES encryption with a two way encryption key.String is returned as Base64 encoded value
		/// rather than binary.
		/// </summary>
		/// <param name="strInputString"></param>
		/// <param name="strKey"></param>
		/// <returns></returns>
		public static string EncryptString(string strInputString, string strKey)
		{
			return Convert.ToBase64String(EncryptBytes(Encoding.ASCII.GetBytes(strInputString), strKey));
		}

		/// <summary>
		/// Decrypts a Byte array from DES with an Encryption Key.
		/// </summary>
		/// <param name="bytDecryptBuffer"></param>
		/// <param name="strKey"></param>
		/// <returns>byte</returns>
		public static byte[] DecryptBytes(byte[] bytDecryptBuffer, string strKey)
		{
			if (strKey == null) strKey = _ENCRYPTIONKEY;

			var o3Des = new TripleDESCryptoServiceProvider();
			var oHashmd5 = new MD5CryptoServiceProvider();

			o3Des.Key = oHashmd5.ComputeHash(Encoding.ASCII.GetBytes(strKey));
			o3Des.Mode = CipherMode.ECB;

			ICryptoTransform oTransform = o3Des.CreateDecryptor();

			return oTransform.TransformFinalBlock(bytDecryptBuffer, 0, bytDecryptBuffer.Length);
		}

		/// <summary>
		/// Decrypts a string using DES encryption and a pass key that was used for 
		/// encryption.
		/// </summary>
		/// <param name="strDecryptString"></param>
		/// <param name="strKey"></param>
		/// <returns>byte</returns>
		public static byte[] DecryptBytes(string strDecryptString, string strKey)
		{
			return DecryptBytes(Convert.FromBase64String(strDecryptString), strKey);
		}

		/// <summary>
		/// Decrypts a string using DES encryption and a pass key that was used for 
		/// encryption.
		/// </summary>
		/// <param name="strDecryptString"></param>
		/// <param name="strKey"></param>
		/// <returns>String</returns>
		public static string DecryptString(string strDecryptString, string strKey)
		{
			try
			{
				return Encoding.ASCII.GetString(DecryptBytes(Convert.FromBase64String(strDecryptString), strKey));
			}
			catch (Exception oEx)
			{
				return "Error: " + oEx.Message;
			} // Probably not encoded
		}

        /// <summary>
        /// Attempts to decrypt using the OLD encryption key.
        /// If unsuccessful it falls back to using the current encryption key.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DecryptString(string str) {
            /* try { */
                // try current key
            var result = Encoding.ASCII.GetString(DecryptBytes(Convert.FromBase64String(str), null));
                // if there are zero or one questions mark then we assume the key is valid
                // 0 question marks - indices should be -1 and equal
                // 1 question marks - indices should be equal
                // 2 question marks - indices should be different
                if (result.IndexOf('?') == result.LastIndexOf('?')) {
                    return result;
                }
            /*} catch (Exception ex) {
                // errors are expected
				 Console.WriteLine("Decryption failed: {0}", ex.Message);
	            
            }*/

            // fallback to OLDkey
            return DecryptString(str, _OLDENCRYPTIONKEY);
        }

		public static bool DecryptStringTry(string str, out string result)
		{
			/** Init. */
			result = null;

			try
			{
				result = DecryptString(str);
				return true;
			}
			catch (Exception ex)
			{
				// errors are expected
				Console.WriteLine("Decryption failed: {0}", ex.Message);
			}

			// Return default path
			return false;
		}
	}
}