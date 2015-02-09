using System;
using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	public class FnsGsDeviceEvents : IFnsGsDeviceEvents
	{
		#region .ctor
		public FnsGsDeviceEvents(GS_EventsView eventItem)
		{
			EventID = eventItem.EventID;
			EventTypeId = eventItem.EventTypeId;
			EventType = eventItem.EventType;
			EventTypeUi = eventItem.EventTypeUi;
			EventShortDesc = eventItem.EventShortDesc;
			AccountId = eventItem.AccountId;
			CustomerId = eventItem.CustomerID;
			CustomerMasterFileId = eventItem.CustomerMasterFileId;
			AccountName = eventItem.AccountName;
			EventName = eventItem.EventName;
			EventDate = eventItem.EventDate;
			Lattitude = eventItem.Lattitude;
			Longitude = eventItem.Longitude;


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
