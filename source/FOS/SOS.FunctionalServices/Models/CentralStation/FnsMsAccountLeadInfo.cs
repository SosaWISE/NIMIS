using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountLeadInfo : IFnsMsAccountLeadInfo
	{
		#region .ctor

		public FnsMsAccountLeadInfo(MS_AccountAndLeadInfoView item)
		{
			AccountID = item.AccountID;
			LeadId = item.LeadId;
			CustomerId = item.CustomerId;
			CustomerMasterFileId = item.CustomerMasterFileId;
			IndustryAccountId = item.IndustryAccountId;
			SystemTypeId = item.SystemTypeId;
			CellularTypeId = item.CellularTypeId;
			PanelTypeId = item.PanelTypeId;
			PanelItemId = item.PanelItemId;
			CellPackageItemId = item.CellPackageItemId;
			ContractId = item.ContractId;

		}

		#endregion .ctor

		#region Properties
		public long AccountID { get; private set; }
		public long LeadId { get; private set; }
		public long CustomerId { get; private set; }
		public long CustomerMasterFileId { get; private set; }
		public long? IndustryAccountId { get; private set; }
		public string SystemTypeId { get; private set; }
		public string CellularTypeId { get; private set; }
		public string PanelTypeId { get; private set; }
		public string PanelItemId { get; private set; }
		public string CellPackageItemId { get; private set; }
		public int? ContractId { get; private set; }
		#endregion Properties
	}
}
