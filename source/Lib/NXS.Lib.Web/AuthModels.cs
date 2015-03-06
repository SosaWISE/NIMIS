using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Lib.Web
{
	public class SystemUserIdentity : IUserIdentity
	{
		public bool UseSessionNumAsClaims;
		private string[] _groups;
		public IEnumerable<string> Claims
		{
			get
			{
				if (UseSessionNumAsClaims)
					return new string[] { SessionNumToString(SessionNum) };
				else
					return _groups;
			}
		}
		public byte[] SessionNum { get; private set; }
		public int UserID { get; private set; }
		public string UserName { get; private set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string GPEmployeeID { get; private set; }
		public int DealerId { get; private set; }

		public SystemUserIdentity(byte[] sessionNum, User user)
		{
			_groups = user.Groups;
			this.SessionNum = sessionNum;
			this.UserID = user.UserID;
			this.UserName = user.Username;
			this.FirstName = user.FirstName;
			this.LastName = user.LastName;
			this.GPEmployeeID = user.GPEmployeeID;
			this.DealerId = user.DealerId;
		}

		public static string SessionNumToString(byte[] sessionNum)
		{
			return Convert.ToBase64String(sessionNum);
		}
		public static byte[] SessionNumFromString(string sessionStr)
		{
			return Convert.FromBase64String(sessionStr);
		}
		public static byte[] SessionNumFromClaims(IEnumerable<string> claims)
		{
			var id = claims.FirstOrDefault();
			if (id == null) return null;
			return SystemUserIdentity.SessionNumFromString(id);
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
}
