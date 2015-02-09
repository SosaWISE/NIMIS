using SSE.Lib.SseGpsDeviceAPI.Helper;

namespace SSE.Lib.SseGpsDeviceAPI.Processor
{
	public class Device
	{
		#region .ctor

		public Device(long accountID, string imei, string sim, short ppm, double lowBatteryAlert, string password, double speedAlert, short gForceAlert)
		{
			AccountID = accountID;
			IMEI = imei;
			SIM = sim;
			PPM = ppm;
			LowBatteryAlert = lowBatteryAlert;
			Password = password;
			SpeedAlert = speedAlert;
			GForceAlert = gForceAlert;

			InitDefaultValues();
		}

		#endregion .ctor

		#region Member Variables

		#region Fixed Members
		public long AccountID { get; private set; }
		public string IMEI { get; private set; }
		public string SIM { get; private set; }
		/// <summary>
		/// Pings Per Minute
		/// </summary>
		public short PPM { get; private set; }
		public string Password { get; private set; }
		public double LowBatteryAlert { get; private set; }
		public double SpeedAlert { get; private set; }
		public short GForceAlert { get; private set; }
		#endregion Fixed Members

		#region Changable Variables
		
		public string Lattitude { get; set; }
		public GPSIndicator NSIndicator { get; set; }
		public string Longitude { get; set; }
		public GPSIndicator EWIndicator { get; set; }
		public double Speed { get; set; }
		public double Direction { get; set; }
		public short Altitude { get; set; }
		public string BatteryLevel { get; set; }
		public string GForce { get; set; }
		public string CellStrength { get; set; }
		public string GPSStrength { get; set; }

		#endregion Changable Variables

		#endregion Member Variables

		#region Member Functions

		private void InitDefaultValues()
		{
			Lattitude = "4542.8106";
			NSIndicator = new GPSIndicator(GPSIndicator.IndicatorTypes.N);
			Longitude = "01344.2720";
			EWIndicator = new GPSIndicator(GPSIndicator.IndicatorTypes.W);
			Speed = 84.23;
			Direction = 125.23;
			Altitude = 2135;
			BatteryLevel = "99.8";
			GForce = "3";
			CellStrength = "98.3";
			GPSStrength = "89.76";
		}

		#endregion Member Functions
	}
}