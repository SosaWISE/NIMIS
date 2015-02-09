using System;
using SOS.Data.AuthenticationControl;
using SOS.FunctionalServices.Contracts.Models.AuthenticationControl;

namespace SOS.FunctionalServices.Models.AuthenticationControl
{
	public class FnsAcCmsUser : IFnsAcCmsUser
	{
		#region .ctor
		public FnsAcCmsUser(AC_UsersAppAuthenticationView user)
		{
			UserID = user.UserID;
			DealerId = user.DealerId;
			Ssid = user.SSID;
			HRUserId = user.HRUserId;
			Username = user.Username;
			Password = user.Password;
			Firstname = user.FirstName;
			Lastname = user.LastName;
			GPEmployeeID = user.GPEmployeeID;
			if (user.SessionId != null) SessionId = (long) user.SessionId;
            UserEmployeeTypeID=user.UserEmployeeTypeID;
            UserEmployeeTypeName =user.UserEmployeeTypeName;
            SecurityLevel = user.SecurityLevel;



		}


		#endregion .ctor

		#region Properties
		public int UserID { get; set; }
		public int DealerId { get; set; }
		public Guid? Ssid { get; set; }
		public int? HRUserId { get; set; }
		public string GPEmployeeID { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public long SessionId { get; set; }

        public string UserEmployeeTypeID { get; set; }

        public string UserEmployeeTypeName { get;set; }

        public byte? SecurityLevel { get; set; }
		#endregion Properties
	}
}
