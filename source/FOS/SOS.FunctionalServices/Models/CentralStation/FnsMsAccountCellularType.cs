using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountCellularType : IFnsMsAccountCellularType
	{
		#region .ctor

		public FnsMsAccountCellularType(MS_AccountCellularType cellType)
		{
			CellularTypeID = cellType.CellularTypeID;
			CellularTypeName = cellType.CellularTypeName;
			IsActive = cellType.IsActive;
			IsDeleted = cellType.IsDeleted;
			ModifiedOn = cellType.ModifiedOn;
			ModifiedBy = cellType.ModifiedBy;
			CreatedOn = cellType.CreatedOn;
			CreatedBy = cellType.CreatedBy;
			DexRowTs = cellType.DEX_ROW_TS;
			DexRowId = cellType.DEX_ROW_ID;
		}

		#endregion .ctor

		#region Properties
		public string CellularTypeID { get; set; }
		public string CellularTypeName { get; set; }
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
