using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
    public class FnsMsAccountServiceType : IFnsMsAccountServiceType
    {
        #region .ctor

		public FnsMsAccountServiceType(MS_AccountSystemType systemType)
		{
            SystemTypeID = systemType.SystemTypeID;
            SystemTypeName = systemType.SystemTypeName;
            IsActive = systemType.IsActive;
            IsDeleted = systemType.IsDeleted;
            ModifiedOn = systemType.ModifiedOn;
            ModifiedBy = systemType.ModifiedBy;
            CreatedOn = systemType.CreatedOn;
            CreatedBy = systemType.CreatedBy;
            DexRowTs = systemType.DEX_ROW_TS;
            DexRowId = systemType.DEX_ROW_ID;
		}

		#endregion .ctor

        #region Properties
        public string SystemTypeID { get; set; }
        public string SystemTypeName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DexRowTs { get; set; }
        public int DexRowId { get; set; }

        #endregion Properties
    }
}
