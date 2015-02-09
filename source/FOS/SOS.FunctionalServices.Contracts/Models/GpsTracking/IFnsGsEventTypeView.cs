using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.GpsTracking
{
	public interface IFnsGsEventTypeView
	{
		[DataMember]
		string EventTypeID { get; set; }

		[DataMember]
		string EventType { get; set; }
	}
}