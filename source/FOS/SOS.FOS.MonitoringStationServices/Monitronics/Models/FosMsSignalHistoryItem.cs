using System;
using NSE.FOS.Contracts.Models;
using SOS.FOS.MonitoringStationServices.AGSiteService;

namespace SOS.FOS.MonitoringStationServices.Monitronics.Models
{
	public class FosMsSignalHistoryItem : IFosSignalHistoryItem
	{
		#region .ctor

		public FosMsSignalHistoryItem(NXS.Logic.MonitoringStations.Schemas.GetEventHistories.TableRow historyItem)
		{
//			AlarmNum = historyItem.AlarmNum;
			AreaNum = historyItem.Iszone_idNull() ? null : historyItem.zone_id;
//			Comment = historyItem.Comment;
			EventCode = historyItem.Isevent_idNull() ? null : historyItem.event_id;
			//EventCodeDescription = historyItem.EventCodeDescription;
			//FullClearFlag = historyItem.FullClearFlag;
			HistoryDate = historyItem.Isevent_dateNull() ? (DateTime?)null : historyItem.event_date;
			//Latitude = historyItem.Latitude;
			//Longitude = historyItem.Longitude;
			//OpAct = historyItem.OpAct;
			//OpActDescription = historyItem.OpActDescription;
			//Phone = historyItem.Phone;
			Point = historyItem.Iszone_idNull() ? null : historyItem.zone_id;
			PointDescription = historyItem.IscomputedNull() ? null : historyItem.computed;
			//RawMessage = historyItem.RawMessage;
			SignalCode = historyItem.Iszonestate_idNull() ? null : historyItem.zonestate_id;
			//SiteName = historyItem.SiteName;
			//TestNum = historyItem.TestNum;
			TransmitterCode = historyItem.Iscs_noNull() ? null : historyItem.cs_no;
			//UserId = historyItem.UserId;
			//UserName = historyItem.UserName;
			UTCDate = historyItem.event_date;
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
