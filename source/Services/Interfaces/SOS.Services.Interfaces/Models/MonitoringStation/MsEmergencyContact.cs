using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsEmergencyContact : IMsEmergencyContact
	{
		#region Properties
		public long EmergencyContactID { get; set; }
		public int EmergencyContactTypeId { get; set; }
		public long? CustomerId { get; set; }
		public long AccountId { get; set; }
		public int RelationshipId { get; set; }
		public int AuthorityId { get; set; }
		public short OrderNumber { get; set; }
		public string Allergies { get; set; }
		public string MedicalConditions { get; set; }
		public bool HasKey { get; set; }
		public DateTime? DOB { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Postfix { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Phone1 { get; set; }
		public int Phone1TypeId { get; set; }
		public string Phone2 { get; set; }
		public int? Phone2TypeId { get; set; }
		public string Phone3 { get; set; }
		public int? Phone3TypeId { get; set; }
		public string Comment1 { get; set; }
		#endregion Properties
	}

	public interface IMsEmergencyContact
	{
        #region Properties

		[DataMember]
		long EmergencyContactID { get; set; }

		[DataMember]
		int EmergencyContactTypeId { get; set; }

		[DataMember]
		long? CustomerId { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		int RelationshipId { get; set; }

		[DataMember]
		int AuthorityId { get; set; }

		[DataMember]
		short OrderNumber { get; set; }

		[DataMember]
		string Allergies { get; set; }

		[DataMember]
		string MedicalConditions { get; set; }

		[DataMember]
		bool HasKey { get; set; }

		[DataMember]
		DateTime? DOB { get; set; }

		[DataMember]
		string Prefix { get; set; }

		[DataMember]
		string FirstName { get; set; }

		[DataMember]
		string MiddleName { get; set; }

		[DataMember]
		string LastName { get; set; }

		[DataMember]
		string Postfix { get; set; }

		[DataMember]
		string Email { get; set; }

		[DataMember]
		string Password { get; set; }

		[DataMember]
		string Phone1 { get; set; }

		[DataMember]
		int Phone1TypeId { get; set; }

		[DataMember]
		string Phone2 { get; set; }

		[DataMember]
		int? Phone2TypeId { get; set; }

		[DataMember]
		string Phone3 { get; set; }

		[DataMember]
		int? Phone3TypeId { get; set; }

		[DataMember]
		string Comment1 { get; set; }

        #endregion Properties

    }


}