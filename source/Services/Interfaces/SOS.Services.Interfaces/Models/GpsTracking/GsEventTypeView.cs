using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.GpsTracking
{
	public interface IGsEventTypeView
	{
		[DataMember]
		string EventTypeID { get; set; }

		[DataMember]
		string EventType { get; set; }
	}

	public class GsEventTypeView : IGsEventTypeView
	{
		public string EventTypeID { get; set; }
		public string EventType { get; set; }
	}
}
