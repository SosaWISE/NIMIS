using System;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsSignalHistoryItemModel
	{
		string AlarmNum { get; }
		string AreaNum { get; }
		string Comment { get; }
		string EventCode { get; }
		string EventCodeDescription { get; }
		string FullClearFlag { get; }
		DateTime? HistoryDate { get; }
		string Latitude { get; }
		string Longitude { get; }
		string OpAct { get; }
		string OpActDescription { get; }
		string Phone { get; }
		string Point { get; }
		string PointDescription { get; }
		string RawMessage { get; }
		string SignalCode { get; }
		string SiteName { get; }
		string TestNum { get; }
		string TransmitterCode { get; }
		string UserId { get; }
		string UserName { get; }
		DateTime? UTCDate { get; }
	}
}