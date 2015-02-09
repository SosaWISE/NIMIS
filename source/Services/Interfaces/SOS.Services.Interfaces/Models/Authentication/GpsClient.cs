using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Authentication
{
	public interface IGpsClient
	{
		#region Properties

		[DataMember]
		long CustomerID { get; }

		[DataMember]
		bool? IsCurrent { get; }

		[DataMember]
		long CustomerMasterFileId { get; }

		[DataMember]
		string CustomerTypeId { get; }

		[DataMember]
		string CustomerTypeUi { get; }

		[DataMember]
		int DealerId { get; }

		[DataMember]
		string DealerName { get; }

		[DataMember]
		long AddressId { get; }

		[DataMember]
		long LeadId { get; }

		[DataMember]
		string LocalizationId { get; }

		[DataMember]
		string LocalizationName { get; }

		[DataMember]
		string Prefix { get; }

		[DataMember]
		string FirstName { get; }

		[DataMember]
		string MiddleName { get; }

		[DataMember]
		string LastName { get; }

		[DataMember]
		string Postfix { get; }

		[DataMember]
		string Gender { get; }

		[DataMember]
		string PhoneHome { get; }

		[DataMember]
		string PhoneWork { get; }

		[DataMember]
		string PhoneMobile { get; }

		[DataMember]
		string Email { get; }

		[DataMember]
		DateTime? DOB { get; }

		[DataMember]
		string SSN { get; }

		[DataMember]
		string Username { get; }

		[DataMember]
		string Password { get; }

		[DataMember]
		DateTime? LastLoginOn { get; }

		[DataMember]
		bool IsActive { get; }

		[DataMember]
		bool IsDeleted { get; }

		[DataMember]
		DateTime ModifiedOn { get; }

		[DataMember]
		string ModifiedBy { get; }

		[DataMember]
		DateTime CreatedOn { get; }

		[DataMember]
		string CreatedBy { get; }

		[DataMember]
		DateTime DexRowTs { get; }

		#endregion Properties
		
	}

	public class GpsClient : IGpsClient
	{

		public long CustomerID { get; set; }
		public bool? IsCurrent { get; set; }
		public long CustomerMasterFileId { get; set; }
		public string CustomerTypeId { get; set; }
		public string CustomerTypeUi { get; set; }
		public int DealerId { get; set; }
		public string DealerName { get; set; }
		public long AddressId { get; set; }
		public long LeadId { get; set; }
		public string LocalizationId { get; set; }
		public string LocalizationName { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Postfix { get; set; }
		public string Gender { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string Email { get; set; }
		public DateTime? DOB { get; set; }
		public string SSN { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime? LastLoginOn { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DexRowTs { get; set; }
	}
}
