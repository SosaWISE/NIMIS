/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 01/17/12
 * Time: 07:43
 * 
 * Description:  Describes the Authentication Service for SOS.
 *********************************************************************************************************************/
using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsWiseCrmDealerUserModel
	{
		#region Properties

		[DataMember]
		long SessionID { get; }

		[DataMember]
		int DealerUserID { get; }

		[DataMember]
		short DealerUserTypeId { get; }

		[DataMember]
		string DealerUserType { get; }

		[DataMember]
		int DealerId { get; }

		[DataMember]
		string DealerName { get; }

		[DataMember]
		int TeamLocationId { get; }

		[DataMember]
		int SeasonId { get; }

		[DataMember]
		int AuthUserId { get; }

		[DataMember]
		string UserId { get; }

		[DataMember]
		string SalesRepId { get;  }

		[DataMember]
		string Fullname { get; }

		[DataMember]
		string Firstname { get; }

		[DataMember]
		string Lastname { get; }

		[DataMember]
		string Email { get; }

		[DataMember]
		string PhoneWork { get; }

		[DataMember]
		string PhoneCell { get; }

		[DataMember]
		string ADUsername { get; }

		[DataMember]
		string Username { get; }

		[DataMember]
		string Password { get; }

		[DataMember]
		DateTime? LastLoginOn { get; }

		[DataMember]
		bool AuIsActive { get; }

		[DataMember]
		bool MduIsActive { get; }

		#endregion Properties
	}
}