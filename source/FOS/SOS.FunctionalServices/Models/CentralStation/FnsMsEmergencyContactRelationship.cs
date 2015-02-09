using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsEmergencyContactRelationship : IFnsMsEmergencyContactRelationship
	{
		#region .ctor

		public FnsMsEmergencyContactRelationship(MS_EmergencyContactRelationship relItem)
		{
			RelationshipID = relItem.RelationshipID;
			MonitoringStationOSId = relItem.MonitoringStationOSId;
			MsRelationshipId = relItem.MsRelationshipId;
			RelationshipDescription = relItem.RelationshipDescription;
			IsEVC = relItem.IsEVC;
			IsActive = relItem.IsActive;
			IsDeleted = relItem.IsDeleted;
			ModifiedOn = relItem.ModifiedOn;
			ModifiedBy = relItem.ModifiedBy;
			CreatedOn = relItem.CreatedOn;
			CreatedBy = relItem.CreatedBy;
		}

		#endregion .ctor

		#region Properties
		public int RelationshipID { get; private set; }
		public string MonitoringStationOSId { get; private set; }
		public string MsRelationshipId { get; private set; }
		public string RelationshipDescription { get; private set; }
		public bool IsEVC { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsDeleted { get; private set; }
		public DateTime ModifiedOn { get; private set; }
		public string ModifiedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }
		public string CreatedBy { get; private set; }
		#endregion Properties
	}
}
