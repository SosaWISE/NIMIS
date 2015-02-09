using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountDetails : IFnsMsAccountDetails
	{
		#region .ctor
		public FnsMsAccountDetails(MS_AccountMonitorInformationsView account)
		{
			AccountID = account.AccountID;
			IndustryAccountId = account.IndustryAccountId;
			MonitoringStationOsId = account.MonitoringStationOSId;
			IndustryAccount2Id = account.IndustryAccount2Id;
			SystemTypeId = account.SystemTypeId;
			CellularTypeId = account.CellularTypeId;
			PanelTypeId = account.PanelTypeId;
			DslSeizureId = account.DslSeizureId;
			PanelItemId = account.PanelItemId;
			CellPackageItemId = account.CellPackageItemId;
			ContractId = account.ContractId;
			TechId = account.TechId;
			TechFullName = account.TechFullName;
			SalesRepId = account.SalesRepId;
			SalesRepFullName = account.SalesFullName;
			AccountPassword = account.AccountPassword;

			var msAccount = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(account.AccountID);

			if (msAccount.IndustryAccount2Id != null)
			{
				Csid2 = account.Csid2;
				ReceiverLine2Id = account.ReceiverLine2Id;
			}

			if (account.IndustryAccountId != null)
			{
				Csid = account.Csid;
				ReceiverLineId = account.ReceiverLineId;
			}
			if (msAccount.SystemTypeId != null)
			{

				SystemTypeName = msAccount.SystemType.SystemTypeName;
			}
			if (msAccount.CellularType != null)
			{
				CellularTypeName = msAccount.CellularType.CellularTypeName;
			}
			if (msAccount.PanelType != null)
			{
				PanelTypeName = msAccount.PanelType.PanelTypeName;
			}
			if (msAccount.DslSeizure != null)
			{
				DslSeizure = msAccount.DslSeizure.DslSeizure;
			}
		}
		#endregion .ctor

		#region Properties
		public long AccountID { get; private set; }
		public string MonitoringStationOsId { get; private set; }
		public long? IndustryAccountId { get; private set; }
		public long? IndustryAccount2Id { get; private set; }
		public string SystemTypeId { get; private set; }
		public string CellularTypeId { get; private set; }
		public string PanelTypeId { get; private set; }
		public short? DslSeizureId { get; private set; }
		public string PanelItemId { get; private set; }
		public string CellPackageItemId { get; private set; }
		public int? ContractId { get; private set; }
		public string TechId { get; private set; }
		public string TechFullName { get; private set; }
		public string SalesRepId { get; private set; }
		public string SalesRepFullName { get; private set; }
		public string AccountPassword { get; private set; }
		public string Csid { get; private set; }
		public string Csid2 { get; private set; }
		public string ReceiverLineId { get; private set; }
		public string ReceiverLine2Id { get; private set; }
		public string SystemTypeName { get; private set; }
		public string CellularTypeName { get; private set; }
		public string PanelTypeName { get; private set; }
		public string DslSeizure { get; private set; }

		#endregion Properties
	}
}