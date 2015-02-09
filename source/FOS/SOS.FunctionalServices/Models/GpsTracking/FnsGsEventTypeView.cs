using SOS.Data.GpsTracking;
using SOS.FunctionalServices.Contracts.Models.GpsTracking;

namespace SOS.FunctionalServices.Models.GpsTracking
{
	public class FnsGsEventTypeView : IFnsGsEventTypeView
	{
		#region .ctor

		public FnsGsEventTypeView(GS_EventTypesView oItem)
		{
			EventTypeID = oItem.EventTypeID;
			EventType = oItem.EventType;
		}

		#endregion .ctor

		#region Properties
		public string EventTypeID { get; set; }
		public string EventType { get; set; }
		#endregion Properties
	}
}
