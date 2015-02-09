using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsMcDealerUser
	{
		[DataMember]
		int DealerUserID { get; set; }

		[DataMember]
		byte DealerUserTypeId { get; set; }

		[DataMember]
		string DealerUserType { get; set; }

		[DataMember]
		int DealerId { get; set; }

		[DataMember]
		int? AuthUserId { get; set; }

		[DataMember]
		string UserID { get; set; }

		[DataMember]
		string Firstname { get; set; }

		[DataMember]
		string Middlename { get; set; }

		[DataMember]
		string Lastname { get; set; }

		[DataMember]
		string FullName { get; set; }

		[DataMember]
		string Email { get; set; }

		[DataMember]
		string PhoneWork { get; set; }

		[DataMember]
		string PhoneCell { get; set; }

		[DataMember]
		string ADUsername { get; set; }

		[DataMember]
		string Username { get; set; }

		[DataMember]
		string Password { get; set; }

		[DataMember]
		DateTime? LastLoginOn { get; set; }

		[DataMember]
		bool IsActive { get; set; }

		[DataMember]
		bool IsDeleted { get; set; }

		[DataMember]
		DateTime ModifiedOn { get; set; }

		[DataMember]
		string ModifiedBy { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		[DataMember]
		string CreatedBy { get; set; }
	}
}
