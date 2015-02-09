namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsDispatchAgencyView
	{
		int DispatchAgencyID { get; }
		short DispatchAgencyTypeId { get; }
		string MonitoringStationOSId { get; }
		int DispatchAgencyOsId { get; }
		string DispatchAgencyName { get; }
		string MsAgencyNumber { get; }
		string Address1 { get; }
		string Address2 { get; }
		string City { get; }
		string State { get; }
		string ZipCode { get; }
		string Phone1 { get; }
		string Phone2 { get; }
		string DispatchAgencyType { get; }
	}
}