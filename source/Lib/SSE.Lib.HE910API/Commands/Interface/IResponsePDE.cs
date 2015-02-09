using System;
using SSE.Lib.HE910API.Helper;

namespace SSE.Lib.HE910API.Commands.Interface
{
	public interface IResponsePDE
	{
		string DeviceID { get; set; }

		string Lattitude { get; set; }

		GPSIndicator NSIndicator { get; set; }

		string Longitude { get; set; }

		GPSIndicator EWIndicator { get; set; }

		string HDOP { get; set; }

		string Altitude { get; set; }

		string Fix { get; set; }

		string COG { get; set; }

		string SpKm { get; set; }

		string SpKn { get; set; }

		DateTime UTCEventTime { get; set; }

		string NSat { get; set; }

		string GForce { get; set; }

		string Battery { get; set; }

		string CellStrength { get; set; }

		string GpsStrength { get; set; }

		MessageState MessageState { get; set; }
		 
	}
}