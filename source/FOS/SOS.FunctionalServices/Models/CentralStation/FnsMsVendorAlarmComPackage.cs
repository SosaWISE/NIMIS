using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsVendorAlarmComPackage : IFnsMsVendorAlarmComPackage
	{
		#region .ctor

		public FnsMsVendorAlarmComPackage(MS_VendorAlarmComPackage package)
		{
			AlarmComPackageID = package.AlarmComPackageID;
			AlarmComAccountId = package.AlarmComAccountId;
			PackageName = package.PackageName;
			DefaultValue = package.DefaultValue;
			IsActive = package.IsActive;
			IsDeleted = package.IsDeleted;
			ModifiedOn = package.ModifiedOn;
			ModifiedBy = package.ModifiedBy;
			CreatedOn = package.CreatedOn;
			CreatedBy = package.CreatedBy;
			DexRowTs = package.DEX_ROW_TS;

		}
		#endregion .ctor

		#region Properties
		public string AlarmComPackageID { get; set; }
		public int AlarmComAccountId { get; set; }
		public string PackageName { get; set; }
		public bool DefaultValue { get; set; }
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
