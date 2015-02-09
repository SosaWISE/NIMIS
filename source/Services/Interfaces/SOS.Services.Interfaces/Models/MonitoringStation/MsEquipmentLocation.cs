using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsEquipmentLocation : IMsEquipmentLocation
	{
		#region Properties
		public int EquipmentLocationID { get; set; }
		public string EquipmentLocationDesc { get; set; }
		public string MonitronicsCode { get; set; }
		public string CriticomCode { get; set; }
		public string AvantGuardCode { get; set; }
		public string LocationCode { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		#endregion Properties
	}

	public interface IMsEquipmentLocation
	{
		#region Properties

		[DataMember]
		int EquipmentLocationID { get; set; }

		[DataMember]
		string EquipmentLocationDesc { get; set; }

		[DataMember]
		string MonitronicsCode { get; set; }

		[DataMember]
		string CriticomCode { get; set; }

		[DataMember]
		string AvantGuardCode { get; set; }

		[DataMember]
		string LocationCode { get; set; }

		[DataMember]
		bool IsActive { get; set; }

		[DataMember]
		bool IsDeleted { get; set; }

		#endregion Properties
	}
}
