using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsEmergencyContact
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