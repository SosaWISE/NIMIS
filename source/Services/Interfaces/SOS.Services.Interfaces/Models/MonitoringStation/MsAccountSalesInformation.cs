using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountSalesInformation : IMsAccountSalesInformation
	{
		#region Properties
		public long AccountID { get; set; }
		public string PaymentTypeId { get; set; }
		public short? BillingDay { get; set; }
		public string CurrentMonitoringStation { get; set; }
		public string PanelTypeId { get; set; }
		public string PanelItemId { get; set; }
		public bool? IsTakeOver { get; set; }
		public bool? IsOwner { get; set; }
		public string CellPackageItemId { get; set; }
		public string CellServicePackage { get; set; }
		public string CellularTypeId { get; set; }
		public string CellularTypeName { get; set; }
		public string CellularVendor { get; set; }
		public decimal? SetupFee { get; set; }
		public decimal? Setup1StMonth { get; set; }
		public decimal? MMR { get; set; }
		public bool? Over3Months { get; set; }
		public short? ContractLength { get; set; }
		public int? ContractId { get; set; }
		public int? ContractTemplateId { get; set; }
		public string Email { get; set; }
		public bool? IsMoni { get; set; }

		#endregion Properties
	}

	public interface IMsAccountSalesInformation
	{
		[DataMember]
		long AccountID { get; set; }

		[DataMember]
		string PaymentTypeId { get; set; }

		[DataMember]
		short? BillingDay { get; set; }

		[DataMember]
		string CurrentMonitoringStation { get; set; }

		[DataMember]
		string PanelTypeId { get; set; }

		[DataMember]
		string PanelItemId { get; set; }

		[DataMember]
		bool? IsTakeOver { get; set; }

		[DataMember]
		bool? IsOwner { get; set; }

		[DataMember]
		string CellPackageItemId { get; set; }

		[DataMember]
		string CellularTypeId { get; set; }

		[DataMember]
		string CellularTypeName { get; set; }

		[DataMember]
		string CellularVendor { get; set; }

		[DataMember]
		string CellServicePackage { get; set; }

		[DataMember]
		decimal? SetupFee { get; set; }

		[DataMember]
		decimal? Setup1StMonth { get; set; }

		[DataMember]
		decimal? MMR { get; set; }

		[DataMember]
		bool? Over3Months { get; set; }

		[DataMember]
		short? ContractLength { get; set; }

		[DataMember]
		int? ContractId { get; set; }

		[DataMember]
		int? ContractTemplateId { get; set; }

		[DataMember]
		string Email { get; set; }

		[DataMember]
		bool? IsMoni { get; set; }
	}
}
