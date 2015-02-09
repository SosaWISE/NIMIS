using Newtonsoft.Json;
using NXS.Lib.Web.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Lib.Web
{
	public class SecureCookie
	{
		public static readonly SecureCookie Instance;
		static SecureCookie()
		{
			//@TODO: get authKey, cryptKey, etc. from config...
			var authKey = AESHMAC.NewKey();
			var cryptKey = AESHMAC.NewKey();
			authKey = Convert.FromBase64String("+DFY//LK9kUdcb/t6X+A19Doh9ebMfaFfeAQR5XBywA=");
			cryptKey = Convert.FromBase64String("69hKuFuxPbWFAx9AVB15/MnIPU3I1CYbpliFr5ng9fQ=");
			Instance = new SecureCookie(authKey, cryptKey);
			//Instance.MaxAge = 
		}

		// in milliseconds
		public int MinAge { get; set; }
		public int MaxAge { get; set; }
		// length of encrypted string
		public int MaxLength { get; set; }

		byte[] _authKey;
		byte[] _cryptKey;

		public SecureCookie(byte[] authKey, byte[] cryptKey)
		{
			if (authKey == null)
				throw new Exception("authKey is null");
			if (cryptKey == null)
				throw new Exception("cryptKey is null");

			_authKey = authKey;
			_cryptKey = cryptKey;

			// set defaults
			MinAge = 1;
			MaxAge = 1000 * 60 * 30;
			MaxLength = 400;
		}

		public string Encode(string name, object value)
		{
			if (name.Contains('|'))
				throw new Exception("invalid name");

			// 1. Serialize.
			var str = Serialize(value);
			// 2. Join parts "name|timestamp|value".
			str = string.Format("{0}|{1:d20}|{2}", name, Timestamp(), str);
			// 3. Encrypt and HMAC
			str = AESHMAC.SimpleEncrypt(str, _cryptKey, _authKey);
			// 4. Check length.
			if (MaxLength != 0 && str.Length > MaxLength)
			{
				throw new Exception("securecookie: the value is too long");
			}
			// Done.
			return str;
		}
		public CT Decode<CT>(string name, string str)
		{
			// 1. Check length.
			if (MaxLength != 0 && str.Length > MaxLength)
			{
				return default(CT); //"securecookie: the value is too long"
			}
			// 2. Decrypt
			str = AESHMAC.SimpleDecrypt(str, _cryptKey, _authKey);
			if (str == null)
			{
				return default(CT); //"securecookie: decryption failed"
			}
			// 3. Verify parts "name|timestamp|value". (it is possible for value to contain seperator, so we can't use str.Split)
			if (str.Length < name.Length + 22) // 22 = 2(pipes) + 20(timestamp length)
			{
				return default(CT); //"securecookie: the value is not valid"
			}
			if (str.Substring(0, name.Length) != name)
			{
				return default(CT); //"securecookie: the value is not valid"
			}
			// 4. Verify date ranges.
			long t1;
			if (!long.TryParse(str.Substring(name.Length + 1, 20), out t1))
			{
				return default(CT); //"securecookie: invalid timestamp"
			}
			var t2 = this.Timestamp();
			if (MinAge != 0 && t2 - MinAge < t1)
			{
				return default(CT); //"securecookie: timestamp is too new"
			}
			if (MaxAge != 0 && t1 < t2 - MaxAge)
			{
				return default(CT); //"securecookie: expired timestamp"
			}
			// 5. Deserialize
			return Deserialize<CT>(str.Substring(name.Length + 22));
		}

		private string Serialize(object value)
		{
			return JsonConvert.SerializeObject(value);
		}
		private CT Deserialize<CT>(string value)
		{
			return JsonConvert.DeserializeObject<CT>(value);
		}

		private long Timestamp()
		{
			return ConvertToTimestamp(DateTime.UtcNow);
		}
		private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		private static long ConvertToTimestamp(DateTime value)
		{
			TimeSpan elapsedTime = value - Epoch;
			return (long)elapsedTime.TotalMilliseconds;
		}
	}
}
