using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsEmergencyContactPhoneType : IFnsMsEmergencyContactPhoneType
	{
		#region .ctor

		public FnsMsEmergencyContactPhoneType(MS_EmergencyContactPhoneType phoneType)
		{
			PhoneTypeID = phoneType.PhoneTypeID;
			MonitoringStationOSId = phoneType.MonitoringStationOSId;
			MsPhoneTypeId = phoneType.MsPhoneTypeId;
			PhoneTypeDescription = phoneType.PhoneTypeDescription;
			IsActive = phoneType.IsActive;
			IsDeleted = phoneType.IsDeleted;
			ModifiedOn = phoneType.ModifiedOn;
			ModifiedBy = phoneType.ModifiedBy;
			CreatedOn = phoneType.CreatedOn;
			CreatedBy = phoneType.CreatedBy;
			DexRowTs = phoneType.DEX_ROW_TS;
		}

		#endregion .ctor

		#region Properties
		public int PhoneTypeID { get; set; }
		public string MonitoringStationOSId { get; set; }
		public string MsPhoneTypeId { get; set; }
		public string PhoneTypeDescription { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DexRowTs { get; set; }
		#endregion Properties
	}
}
