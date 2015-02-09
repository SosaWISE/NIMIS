using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsEmergencyContactRelationship : IMsEmergencyContactRelationship
	{
		#region Properties
		public int RelationshipID { get; set; }
		public string MonitoringStationOSId { get; set; }
		public string MsRelationshipId { get; set; }
		public string RelationshipDescription { get; set; }
		public bool IsEVC { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		#endregion Properties
	}

	public interface IMsEmergencyContactRelationship
	{
		[DataMember]
		int RelationshipID { get; set; }

		[DataMember]
		string MonitoringStationOSId { get; set; }

		[DataMember]
		string MsRelationshipId { get; set; }

		[DataMember]
		string RelationshipDescription { get; set; }

		[DataMember]
		bool IsEVC { get; set; }

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
