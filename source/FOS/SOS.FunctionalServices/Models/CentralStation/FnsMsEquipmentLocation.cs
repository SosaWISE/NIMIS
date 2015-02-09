
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsEquipmentLocation : IFnsMsEquipmentLocation
	{
		#region .ctor

		public FnsMsEquipmentLocation(MS_EquipmentLocationsView item)
		{
			EquipmentLocationID = item.EquipmentLocationID;
			EquipmentLocationDesc = item.EquipmentLocationDesc;
			MonitronicsCode = item.MonitronicsCode;
			CriticomCode = item.CriticomCode;
			AvantGuardCode = item.AvantGuardCode;
			LocationCode = item.LocationCode;
			IsActive = item.IsActive;
			IsDeleted = item.IsDeleted;
		}

		#endregion .ctor

		#region Properties
		public int EquipmentLocationID { get; private set; }
		public string EquipmentLocationDesc { get; private set; }
		public string MonitronicsCode { get; private set; }
		public string CriticomCode { get; private set; }
		public string AvantGuardCode { get; private set; }
		public string LocationCode { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsDeleted { get; private set; }
		#endregion Properties
	}
}
