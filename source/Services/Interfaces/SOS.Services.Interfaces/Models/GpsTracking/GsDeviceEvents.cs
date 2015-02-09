using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.GpsTracking
{

	public interface IGsDeviceEvents
	{
		#region Properties

		[DataMember]
		long EventID { get; set; }

		[DataMember]
		string EventTypeId { get; set; }

		[DataMember]
		string EventType { get; set; }

		[DataMember]
		string EventTypeUi { get; set; }

		[DataMember]
		string EventShortDesc { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		long CustomerId { get; set; }

		[DataMember]
		long CustomerMasterFileId { get; set; }

		[DataMember]
		string AccountName { get; set; }

		[DataMember]
		string EventName { get; set; }

		[DataMember]
		DateTime EventDate { get; set; }

		[DataMember]
		string Lattitude { get; set; }

		[DataMember]
		string Longitude { get; set; }

		#endregion Properties
	}

	public class GsDeviceEvents : IGsDeviceEvents
	{
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
