using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountSalesInformation : IFnsMsAccountSalesInformation
	{
		#region .ctor

		public FnsMsAccountSalesInformation(MS_AccountSalesInformationsView info)
		{
			AccountID = info.AccountID;
			PaymentTypeId = info.PaymentTypeId;
			BillingDay = info.BillingDay;
			CurrentMonitoringStation = info.CurrentMonitoringStation;
			PanelTypeId = info.PanelTypeId;
			PanelItemId = info.PanelItemId;
			IsTakeOver = info.IsTakeOver;
			IsOwner = info.IsOwner;
			CellPackageItemId = info.CellPackageItemId;
			CellularTypeId = info.CellularTypeId;
			CellularTypeName = info.CellularTypeName;
			CellServicePackage = info.CellServicePackage;
			CellularVendor = info.CellularVendor;
			SetupFee = info.SetupFee;
			SetupFee1StMonth = info.SetupFee1stMonth;
			MMR = info.MMR;
			Over3Months = info.Over3Months;
			ContractLength = info.ContractLength;
			ContractId = info.ContractId;
			ContractTemplateId = info.ContractTemplateId;
			Email = info.Email;
			IsMoni = info.IsMoni;
		}


		#endregion .ctor

		#region Properties
		public long AccountID { get; private set; }
		public string PaymentTypeId { get; private set; }
		public short? BillingDay { get; private set; }
		public string CurrentMonitoringStation { get; private set; }
		public string PanelTypeId { get; private set; }
		public string PanelItemId { get; private set; }
		public bool? IsTakeOver { get; private set; }
		public bool? IsOwner { get; private set; }
		public string CellPackageItemId { get; private set; }
		public string CellularTypeId { get; private set; }
		public string CellularTypeName { get; private set; }
		public string CellServicePackage { get; private set; }
		public string CellularVendor { get; set; }
		public decimal? SetupFee { get; private set; }
		public decimal? SetupFee1StMonth { get; private set; }
		public decimal? MMR { get; private set; }
		public bool? Over3Months { get; private set; }
		public short? ContractLength { get; private set; }
		public int? ContractId { get; private set; }
		public int? ContractTemplateId { get; private set; }
		public string Email { get; private set; }
		public bool? IsMoni { get; private set; }
		#endregion Properties
	}
}
