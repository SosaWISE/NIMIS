using System;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsSignalHistoryItem : IMsSignalHistoryItem
	{
		#region Properties
		public string AlarmNum { get; set; }
		public string AreaNum { get; set; }
		public string Comment { get; set; }
		public string EventCode { get; set; }
		public string EventCodeDescription { get; set; }
		public string FullClearFlag { get; set; }
		public DateTime? HistoryDate { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public string OpAct { get; set; }
		public string OpActDescription { get; set; }
		public string Phone { get; set; }
		public string Point { get; set; }
		public string PointDescription { get; set; }
		public string RawMessage { get; set; }
		public string SignalCode { get; set; }
		public string SiteName { get; set; }
		public string TestNum { get; set; }
		public string TransmitterCode { get; set; }
		public string UserId { get; set; }
		public string UserName { get; set; }
		public DateTime? UTCDate { get; set; }
		#endregion Properties
	}

	public interface IMsSignalHistoryItem
	{
		string AlarmNum { get; set; }
		string AreaNum { get; set; }
		string Comment { get; set; }
		string EventCode { get; set; }
		string EventCodeDescription { get; set; }
		string FullClearFlag { get; set; }
		DateTime? HistoryDate { get; set; }
		string Latitude { get; set; }
		string Longitude { get; set; }
		string OpAct { get; set; }
		string OpActDescription { get; set; }
		string Phone { get; set; }
		string Point { get; set; }
		string PointDescription { get; set; }
		string RawMessage { get; set; }
		string SignalCode { get; set; }
		string SiteName { get; set; }
		string TestNum { get; set; }
		string TransmitterCode { get; set; }
		string UserId { get; set; }
		string UserName { get; set; }
		DateTime? UTCDate { get; set; }
	}
}
