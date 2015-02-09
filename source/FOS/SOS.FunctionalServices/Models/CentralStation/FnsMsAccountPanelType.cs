using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
    public class FnsMsAccountPanelType : IFnsMsAccountPanelType
    {
         #region .ctor

		public FnsMsAccountPanelType(MS_AccountPanelType panelType)
		{
            PanelTypeID = panelType.PanelTypeID;
            PanelTypeName = panelType.PanelTypeName;
			UIName = panelType.UIName;
            IsActive = panelType.IsActive;
            IsDeleted = panelType.IsDeleted;
            ModifiedOn = panelType.ModifiedOn;
            ModifiedBy = panelType.ModifiedBy;
            CreatedOn = panelType.CreatedOn;
            CreatedBy = panelType.CreatedBy;
            DexRowTs = panelType.DEX_ROW_TS;
            DexRowId = panelType.DEX_ROW_ID;
		}

		#endregion .ctor

        #region Properties
        public string PanelTypeID { get; set; }
        public string PanelTypeName { get; set; }
		public string UIName { get; set; }
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
