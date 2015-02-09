using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsEquipmentLocation
	{
		[DataMember]
		int EquipmentLocationID { get; }

		[DataMember]
		string EquipmentLocationDesc { get; }

		[DataMember]
		string MonitronicsCode { get; }

		[DataMember]
		string CriticomCode { get; }

		[DataMember]
		string AvantGuardCode { get; }

		[DataMember]
		string LocationCode { get; }

		[DataMember]
		bool IsActive { get; }

		[DataMember]
		bool IsDeleted { get; }
	}
}