using System;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	public class FnsGsEventsView : IFnsGsEventsView
	{
		#region .ctor

		public FnsGsEventsView (GS_EventsView oItem)
		{
			EventID = oItem.EventID;
			EventTypeId = oItem.EventTypeId;
			EventType = oItem.EventType;
			EventTypeUi = oItem.EventTypeUi;
			EventShortDesc = oItem.EventShortDesc;
			AccountId = oItem.AccountId;
			CustomerId = oItem.CustomerID;
			CustomerMasterFileId = oItem.CustomerMasterFileId;
			AccountName = oItem.AccountName;
			EventName = oItem.EventName;
			EventDate = oItem.EventDate;
			Lattitude = oItem.Lattitude;
			Longitude = oItem.Longitude;
		}

		#endregion .ctor

		#region Properties
		public long EventID { get; set; }
		public string EventTypeId { get; set; }
		public string EventType { get; set; }
		public string EventTypeUi { get; set; }
		public string EventShortDesc { get; set; }
		public long AccountId { get; set; }
		public long CustomerId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public string AccountName { get; set; }
		public string EventName { get; set; }
		public DateTime EventDate { get; set; }
		public string Lattitude { get; set; }
		public string Longitude { get; set; }
		#endregion Properties
	}
}
