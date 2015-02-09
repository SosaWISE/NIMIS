using System;
using NSE.FOS.Contracts.Models;
using SOS.FOS.MonitoringStationServices.AGSiteService;

namespace SOS.FOS.MonitoringStationServices.AvantGuard.Models
{
	public class FosAgSignalHistoryItem : IFosSignalHistoryItem
	{
		#region .ctor

		public FosAgSignalHistoryItem(History historyItem)
		{
			AlarmNum = historyItem.AlarmNum;
			AreaNum = historyItem.AreaNum;
			Comment = historyItem.Comment;
			EventCode = historyItem.EventCode;
			EventCodeDescription = historyItem.EventCodeDescription;
			FullClearFlag = historyItem.FullClearFlag;
			HistoryDate = historyItem.HistoryDate;
			Latitude = historyItem.Latitude;
			Longitude = historyItem.Longitude;
			OpAct = historyItem.OpAct;
			OpActDescription = historyItem.OpActDescription;
			Phone = historyItem.Phone;
			Point = historyItem.Point;
			PointDescription = historyItem.PointDescription;
			RawMessage = historyItem.RawMessage;
			SignalCode = historyItem.SignalCode;
			SiteName = historyItem.SiteName;
			TestNum = historyItem.TestNum;
			TransmitterCode = historyItem.TransmitterCode;
			UserId = historyItem.UserId;
			UserName = historyItem.UserName;
			UTCDate = historyItem.UTCDate;
		}

		#endregion .ctor

		#region Properties
		
		public string AlarmNum { get; private set; }
		public string AreaNum { get; private set; }
		public string Comment { get; private set; }
		public string EventCode { get; private set; }
		public string EventCodeDescription { get; private set; }
		public string FullClearFlag { get; private set; }
		public DateTime? HistoryDate { get; private set; }
		public string Latitude { get; private set; }
		public string Longitude { get; private set; }
		public string OpAct { get; private set; }
		public string OpActDescription { get; private set; }
		public string Phone { get; private set; }
		public string Point { get; private set; }
		public string PointDescription { get; private set; }
		public string RawMessage { get; private set; }
		public string SignalCode { get; private set; }
		public string SiteName { get; private set; }
		public string TestNum { get; private set; }
		public string TransmitterCode { get; private set; }
		public string UserId { get; private set; }
		public string UserName { get; private set; }
		public DateTime? UTCDate { get; private set; }
		#endregion Properties

	}
}
