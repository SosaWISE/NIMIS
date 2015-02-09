using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountLeadInfo
	{
		[DataMember]
		long AccountID { get; }
		[DataMember]
		long LeadId { get; }
		[DataMember]
		long CustomerId { get; }
		[DataMember]
		long CustomerMasterFileId { get; }
		[DataMember]
		long? IndustryAccountId { get; }
		[DataMember]
		string SystemTypeId { get; }
		[DataMember]
		string CellularTypeId { get; }
		[DataMember]
		string PanelTypeId { get; }
		[DataMember]
		string PanelItemId { get; }
		[DataMember]
		string CellPackageItemId { get; }
		[DataMember]
		int? ContractId { get; }

	}
}