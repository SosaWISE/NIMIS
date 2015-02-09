using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Lib.Web
{
	public class UserSession
	{
		public string Username { get; set; }
		public byte[] SessionNum { get; set; }
	}

	public struct User
	{
		public int UserID;
		public string Username;
		public string Password;
		public string FirstName;
		public string LastName;
		public string GPEmployeeID;
		public string[] Groups;

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
			var sess = obj as User?;
			return sess.HasValue && this == sess.Value;
		}
		public override int GetHashCode()
		{
			return (Username != null) ? Username.GetHashCode() : 0;
		}
	}
	public struct Session
	{
		public int ID;
		public string SessionKey;
		public string Username;
		public string IPAddress;
		public DateTime LastAccessedOn;

		public static bool operator ==(Session a, Session b)
		{
			return a.ID == b.ID;
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
			return ID.GetHashCode();
		}
	}
}
