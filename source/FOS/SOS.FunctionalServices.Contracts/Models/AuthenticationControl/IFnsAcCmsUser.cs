using System;

namespace SOS.FunctionalServices.Contracts.Models.AuthenticationControl
{
	public interface IFnsAcCmsUser
	{
		int UserID { get; set; }
		int DealerId { get; set; }
		Guid? Ssid { get; set; }
		int? HRUserId { get; set; }
		string GPEmployeeID { get; set; }
		string Username { get; set; }
		string Password { get; set; }
		string Firstname { get; set; }
		string Lastname { get; set; }
		long SessionId { get; set; }

        string UserEmployeeTypeID { get; set;}
        string UserEmployeeTypeName { get;set; }

        byte? SecurityLevel { get; set; }
	}
}