using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Lib
{
	public class TokenUserIdentity : IUserIdentity
	{
		public AuthInformation AuthInfo { get; private set; }
		public IEnumerable<string> Claims
		{
			get { return new string[] { SystemUserIdentity.AuthNumToString(this.AuthInfo.AuthNum), this.AuthInfo.AuthType }; }
		}
		public string UserName { get; private set; }

		public TokenUserIdentity(AuthInformation authInfo, string userName)
		{
			this.AuthInfo = authInfo;
			this.UserName = userName;
		}
	}

	public class SystemUserIdentity : IUserIdentity
	{
		public static class AuthTypes
		{
			public const string Session = "Session";
			public const string ActionRequest = "ActionRequest";
		}

		public IEnumerable<string> Claims { get; private set; }
		public IEnumerable<string> Applications { get; private set; }
		public IEnumerable<string> Actions { get; private set; }
		public AuthInformation AuthInfo { get; private set; }
		public int UserID { get; private set; }
		public string UserName { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string GPEmployeeID { get; private set; }
		public int DealerId { get; private set; }

		public SystemUserIdentity(string authType, byte[] authNum, User user, IEnumerable<string> applications, IEnumerable<string> actions)
		{
			this.AuthInfo = AuthInformation.Create(authType, authNum);
			this.Claims = user.Groups;
			this.Applications = applications;
			this.Actions = actions;
			this.UserID = user.UserID;
			this.UserName = user.Username;
			this.FirstName = user.FirstName;
			this.LastName = user.LastName;
			this.GPEmployeeID = user.GPEmployeeID;
			this.DealerId = user.DealerId;
		}
		public TokenUserIdentity ToTokenIdentity()
		{
			return new TokenUserIdentity(this.AuthInfo, this.UserName);
		}

		public static string AuthNumToString(byte[] authNum)
		{
			return Convert.ToBase64String(authNum);
		}
		public static byte[] AuthNumFromString(string authStr)
		{
			return Convert.FromBase64String(authStr);
		}
		public static AuthInformation AuthInfoFromClaims(IEnumerable<string> claims)
		{
			byte[] authNum = null;
			string authType = null;
			var index = 0;
			foreach (var str in claims)
			{
				if (index == 0)
					authNum = SystemUserIdentity.AuthNumFromString(str);
				else if (index == 1)
					authType = str;
				else
					break;
				index++;
			}
			if (authNum != null)
				return AuthInformation.Create(authType ?? AuthTypes.Session, authNum);
			return null;
		}

		//private SHA1Managed _sha1 = new SHA1Managed();
		public static string AuthNumToKey(byte[] authNum)
		{
			// SHA1Managed is not thread safe: http://stackoverflow.com/questions/12644257
			// having this as a member variable caused random/wrong sessionKeys
			var _sha1 = System.Security.Cryptography.SHA1.Create();
			return Convert.ToBase64String(_sha1.ComputeHash(authNum));
		}

		private static readonly System.Security.Cryptography.RandomNumberGenerator _rnd = System.Security.Cryptography.RandomNumberGenerator.Create();
		public static byte[] NewAuthNum()
		{
			var key = new byte[16]; // 128 / 8
			_rnd.GetBytes(key);
			return key;
		}
	}
	public class AuthInformation
	{
		public string AuthType { get; private set; }
		public byte[] AuthNum { get; private set; }

		public static AuthInformation Create(string authType, byte[] authNum)
		{
			if (authType == null) throw new ArgumentNullException("authType");
			if (authNum == null) throw new ArgumentNullException("authNum");

			var result = new AuthInformation();
			result.AuthType = authType;
			result.AuthNum = authNum;
			return result;
		}
	}

	public struct User
	{
		public int UserID;
		public string Username;
		public string[] Groups;
		public string Password;
		public string FirstName;
		public string LastName;

		public string GPEmployeeID;
		public int DealerId;

		public static bool operator ==(User a, User b)
		{
			return a.Username == b.Username;
		}
		public static bool operator !=(User a, User b)
		{
			return !(a == b);
		}
		public override bool Equals(object obj)
		{
			var sess = obj as Nullable<User>;
			return sess.HasValue && this == sess.Value;
		}
		public override int GetHashCode()
		{
			return (Username != null) ? Username.GetHashCode() : 0;
		}
	}
	public struct Session
	{
		public string SessionKey;
		public string Username;
		public string IPAddress;
		public DateTime CreatedOn;

		public static bool operator ==(Session a, Session b)
		{
			return a.SessionKey == b.SessionKey && a.Username == b.Username;
		}
		public static bool operator !=(Session a, Session b)
		{
			return !(a == b);
		}
		public override bool Equals(object obj)
		{
			var sess = obj as Session?;
			return sess.HasValue && this == sess.Value;
		}
		public override int GetHashCode()
		{
			return SessionKey.GetHashCode();
		}
	}

	public struct ActionRequest
	{
		public int UserId;
		public string ActionId;
		public string ApplicationId;
		public string Username;
	}
}
