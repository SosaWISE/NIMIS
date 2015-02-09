namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountDetails
	{
		#region Properties
		long AccountID { get; }
		string MonitoringStationOsId { get; }
		long? IndustryAccountId { get; }
		long? IndustryAccount2Id { get; }
		string SystemTypeId { get; }
		string CellularTypeId { get; }
		string PanelTypeId { get; }
		short? DslSeizureId { get; }
		string PanelItemId { get; }
		string CellPackageItemId { get; }
		int? ContractId { get; }
		string TechId { get; }
		string TechFullName { get; }
		string SalesRepId { get; }
		string SalesRepFullName { get; }
		string AccountPassword { get; }
		string Csid { get; }
		string Csid2 { get; }
		string ReceiverLineId { get; }
		string ReceiverLine2Id { get; }
		string SystemTypeName { get; }
		string CellularTypeName { get; }
		string PanelTypeName { get; }
		string DslSeizure { get; }

		#endregion Properties
 
	}
}