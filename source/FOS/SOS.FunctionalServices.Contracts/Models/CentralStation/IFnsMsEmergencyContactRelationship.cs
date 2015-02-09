using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsEmergencyContactRelationship
	{
		[DataMember]
		int RelationshipID { get; }

		[DataMember]
		string MonitoringStationOSId { get; }

		[DataMember]
		string MsRelationshipId { get; }

		[DataMember]
		string RelationshipDescription { get; }

		[DataMember]
		bool IsEVC { get; }

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
	}
}