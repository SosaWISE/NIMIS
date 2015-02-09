using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccount
	{
		#region Properties

		[DataMember]
		long AccountID { get; }

		[DataMember]
		long? IndustryAccountId { get; }

		[DataMember]
		string SystemTypeId { get; }

		[DataMember]
		string CellularTypeId { get; }

		[DataMember]
		string PanelTypeId { get; }

		[DataMember]
		short? DslSeizureId { get; }

		[DataMember]
		string PanelItemId { get; }

		[DataMember]
		string CellPackageItemId { get; }

		[DataMember]
		int? ContractId { get; }

		[DataMember]
		string TechId { get; }

		[DataMember]
		string AccountPassword { get; }

		[DataMember]
		string SimProductBarcodeId { get; }

		[DataMember]
		string DispatchMessage { get; }

		[DataMember]
		bool IsActive { get; }

		[DataMember]
		bool IsDeleted { get; }

		[DataMember]
		DateTime ModifiedOn { get; }

		[DataMember]
		string ModifiedBy { get; }

		[DataMember]
		DateTime CreatedOn { get; }

		[DataMember]
		string CreatedBy { get; }

	#endregion Properties
	}
}
