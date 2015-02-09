/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 01/17/12
 * Time: 23:06
 * 
 * Description:  Session Model information returned for a created instance of a handshake.
 *********************************************************************************************************************/

using System;
using SOS.Data.AuthenticationControl;
using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models
{
	public class FnsWiseCrmDealerUserModel : IFnsWiseCrmDealerUserModel
	{
		#region .ctor
		public FnsWiseCrmDealerUserModel(AC_UsersDealerUsersAuthenticateView oDealerUser)
		{
			SessionID = oDealerUser.SessionID;
			DealerUserID = oDealerUser.DealerUserID;
			DealerUserTypeId = oDealerUser.DealerUserTypeId;
			DealerUserType = oDealerUser.DealerUserType;
			DealerId = oDealerUser.DealerId;
			DealerName = oDealerUser.DealerName;
			TeamLocationId = 0;
			SeasonId = 0;
			AuthUserId = oDealerUser.UserID;
			UserId = oDealerUser.McUserId;
			SalesRepId = oDealerUser.GPEmployeeID;
			Fullname = oDealerUser.FullName;
			Firstname = oDealerUser.Firstname;
			Lastname = oDealerUser.Lastname;
			Email = oDealerUser.Email;
			PhoneWork = oDealerUser.PhoneWork;
			PhoneCell = oDealerUser.PhoneCell;
			ADUsername = oDealerUser.ADUsername;
			Username = oDealerUser.Username;
			Password = oDealerUser.Password;
			LastLoginOn = oDealerUser.LastLoginOn;
			AuIsActive = oDealerUser.AuIsActive;
			MduIsActive = oDealerUser.MduIsActive;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public FnsWiseCrmDealerUserModel() {}

		#endregion .ctor

		#region Methods

		public void SetSeasonAndTeamLocationId(int nSeasonId, int nTeamLocationId)
		{
			SeasonId = nSeasonId;
			TeamLocationId = nTeamLocationId;
		}

		#endregion Methods

		#region Implementation of IFnsWiseCrmDealerUserModel

		public long SessionID { get; private set; }

		public int DealerUserID { get; private set; }

		public short DealerUserTypeId { get; private set; }

		public string DealerUserType { get; private set; }

		public int DealerId { get; private set; }

		public string DealerName { get; private set; }

		public int TeamLocationId { get; private set; }

		public int SeasonId { get; private set; }

		public int AuthUserId { get; private set; }

		public string UserId { get; private set; }

		public string SalesRepId { get; private set; }

		public string Fullname { get; private set; }

		public string Firstname { get; private set; }

		public string Lastname { get; private set; }

		public string Email { get; private set; }

		public string PhoneWork { get; private set; }

		public string PhoneCell { get; private set; }

		public string ADUsername { get; private set; }

		public string Username { get; private set; }

		public string Password { get; private set; }

		public DateTime? LastLoginOn { get; private set; }

		public bool AuIsActive { get; private set; }

		public bool MduIsActive { get; private set; }

		#endregion
	}
}
