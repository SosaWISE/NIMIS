using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountLeadInfo : IMsAccountLeadInfo
	{
		#region Properties
		public long AccountID { get; set; }
		public long LeadId { get; set; }
		public long CustomerId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public long? IndustryAccountId { get; set; }
		public string SystemTypeId { get; set; }
		public string CellularTypeId { get; set; }
		public string PanelTypeId { get; set; }
		public string PanelItemId { get; set; }
		public string CellPackageItemId { get; set; }
		public int? ContractTemplateId { get; set; }
		#endregion Properties
	}

	public interface IMsAccountLeadInfo
	{
		#region Properties
		[DataMember]
		long AccountID { get; set; }
		[DataMember]
		long LeadId { get; set; }
		[DataMember]
		long CustomerId { get; set; }
		[DataMember]
		long CustomerMasterFileId { get; set; }
		[DataMember]
		long? IndustryAccountId { get; set; }
		[DataMember]
		string SystemTypeId { get; set; }
		[DataMember]
		string CellularTypeId { get; set; }
		[DataMember]
		string PanelTypeId { get; set; }
		[DataMember]
		string PanelItemId { get; set; }
		[DataMember]
		string CellPackageItemId { get; set; }
		[DataMember]
		int? ContractTemplateId { get; set; }
		#endregion Properties
	}
}
