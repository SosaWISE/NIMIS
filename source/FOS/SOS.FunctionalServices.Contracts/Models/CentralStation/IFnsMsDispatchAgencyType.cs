namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsDispatchAgencyType
	{
		#region Properties
		short DispatchAgencyTypeID { get; }
		string MonitoringStationOSId { get; }
		string DispatchAgencyType { get; }
		string MsAgencyTypeNo { get; }
		#endregion Properties
	}
}
