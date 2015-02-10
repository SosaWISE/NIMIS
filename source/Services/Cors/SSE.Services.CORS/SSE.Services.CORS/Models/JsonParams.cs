using System;

namespace SSE.Services.CORS.Models
{
	public class JsonParamBase
	{
		public long SessionID { get; set; }
	}

	public class GetListJsonParamBase : JsonParamBase
	{
		public long UniqueID { get; set; }
	}

	public class GetDeviceDetailsJsonParamBase : JsonParamBase
	{
		public long CMFID { get; set; }
		public long AccountID { get; set; }
		public long CustomerID { get; set; }
	}

	public class GetDeviceEventJsonParamBase : GetDeviceDetailsJsonParamBase
	{
		public string StartDate { get; set; }
		public string EndDate { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
	}

	public class GeoObjectBase : JsonParamBase
	{
		public long GeoFenceID { get; set; }
		public long AccountId { get; set; }
		public long CustomerId { get; set; }
		public string GeoFenceName { get; set; }
		public string GeoFenceDescription { get; set; }
	}

	public class PointJsonParam : GeoObjectBase
	{
		public double Lattitude { get; set; }
		public double Longitude { get; set; }
	}

	public class RectangleJsonParam : GeoObjectBase
	{
		public string ItemId { get; set; }
		public string ReportMode { get; set; }
		public double MaxLattitude { get; set; }
		public double MinLongitude { get; set; }
		public double MaxLongitude { get; set; }
		public double MinLattitude { get; set; }
		public short ZoomLevel { get; set; }
	}

	public class CircleJsonParam : GeoObjectBase
	{
		public double Radius { get; set; }
		public double CenterLattitude { get; set; }
		public double CenterLongitude { get; set; }
	}

	public class ReportJsonParam : JsonParamBase
	{
		public int DeviceId { get; set; }
		public string EventType { get; set; }
		public long Location { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
	}

	#region Customer

	public class CustomerParam : JsonParamBase
	{
		public long CustomerID { get; set; }
		public int LeadSourceId { get; set; }
		public int LeadDispositionId { get; set; }
		public int DealerId { get; set; }
		public string SalesRepId { get; set; }
		public string LocalizationId { get; set; }
		public string Gender { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string PostFix { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }

	}

	#endregion Customer

	#region Device

	public class DeviceParam : JsonParamBase
	{
		public long AccountID { get; set; }
		public string AccountName { get; set; }
		public string AccountDesc { get; set; }
}
	#endregion Device

	#region Event Types

	public class EventTypeParam : JsonParamBase
	{
		public string EventTypeID { get; set; }
		public string EventType { get; set; }
	}

	#endregion Event Types

}