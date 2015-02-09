using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsGsEventsView
	{
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
	}
}