
using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccount : IFnsMsAccount
	{
		#region .ctor

		public FnsMsAccount(long accountID)
		{
			AccountID = accountID;
		}

		public FnsMsAccount(MS_Account account)
		{
			var msAccountSlI = SosCrmDataContext.Instance.MS_AccountSalesInformations.LoadByPrimaryKey(account.AccountID);
			AccountID = account.AccountID;
			IndustryAccountId = account.IndustryAccountId;
			SystemTypeId = account.SystemTypeId;
			CellularTypeId = account.CellularTypeId;
			PanelTypeId = account.PanelTypeId;
			DslSeizureId = account.DslSeizureId;
			PanelItemId = account.PanelItemId;
			CellPackageItemId = account.CellPackageItemId;
			ContractId = account.ContractId;
			TechId = msAccountSlI.TechId;
			AccountPassword = account.AccountPassword;
			SimProductBarcodeId = account.SimProductBarcodeId;
			DispatchMessage = account.DispatchMessage;
			IsActive = account.IsActive;
			IsDeleted = account.IsDeleted;
			ModifiedOn = account.ModifiedOn;
			ModifiedBy = account.ModifiedBy;
			CreatedOn = account.CreatedOn;
			CreatedBy = account.CreatedBy;
		}

		#endregion .ctor

		#region Properties
		public long AccountID { get; private set; }
		public long? IndustryAccountId { get; private set; }
		public string SystemTypeId { get; set; }
		public string CellularTypeId { get; set; }
		public string PanelTypeId { get; set; }
		public short? DslSeizureId { get; set; }
		public string PanelItemId { get; private set; }
		public string CellPackageItemId { get; private set; }
		public int? ContractId { get; private set; }
		public string TechId { get; private set; }
		public string AccountPassword { get; set; }
		public string SimProductBarcodeId { get; private set; }
		public string DispatchMessage { get; private set; }
		public bool IsActive { get; private set; }
		public bool IsDeleted { get; private set; }
		public DateTime ModifiedOn { get; private set; }
		public string ModifiedBy { get; private set; }
		public DateTime CreatedOn { get; private set; }
		public string CreatedBy { get; private set; }
		#endregion Properties

	}
}