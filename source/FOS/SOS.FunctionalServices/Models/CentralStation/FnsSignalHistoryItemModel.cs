using System;
using NSE.FOS.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsSignalHistoryItemModel : IFnsSignalHistoryItemModel
	{
		#region .ctor

		public FnsSignalHistoryItemModel(IFosSignalHistoryItem item)
		{
			AlarmNum = item.AlarmNum;
			AreaNum = item.AreaNum;
			Comment = item.Comment;
			EventCode = item.EventCode;
			EventCodeDescription = item.EventCodeDescription;
			FullClearFlag = item.FullClearFlag;
			HistoryDate = item.HistoryDate;
			Latitude = item.Latitude;
			Longitude = item.Longitude;
			OpAct = item.OpAct;
			OpActDescription = item.OpActDescription;
			Phone = item.Phone;
			Point = item.Point;
			PointDescription = item.PointDescription;
			RawMessage = item.RawMessage;
			SignalCode = item.SignalCode;
			SiteName = item.SiteName;
			TestNum = item.TestNum;
			TransmitterCode = item.TransmitterCode;
			UserId = item.UserId;
			UserName = item.UserName;
			UTCDate = item.UTCDate;
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
