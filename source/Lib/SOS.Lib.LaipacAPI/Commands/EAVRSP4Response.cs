using System;
using System.Globalization;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Lib.LaipacAPI.Helper;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class EAVRSP4Response : EAVRSPResponse
	{
		#region .ctor

		public EAVRSP4Response(string rawSentence) : base(rawSentence)
		{
			Parse();
		}

		#endregion .ctor

		#region Member Variables

		protected short GeoFence { get; private set; }
		protected short ReportMode { get; private set; }
		protected decimal Latitude1 { get; private set; }
		protected decimal Longitude1 { get; private set; }
		protected decimal Latitude2 { get; private set; }
		protected decimal Longitude2 { get; private set; }

		protected LP_CommandMessageEAVRSP4 CommandMessageEAVRSP4 { get; private set; }

		#endregion Member Variables

		#region Member Functions
		private void Parse()
		{
			/** Initialize. */
			string[] aSplitString = SentenceNet.Split(',');

			UnitIDSet(aSplitString[FieldsEAVRSP.UnitID]);
			GeoFence = Convert.ToInt16(aSplitString[FieldsEAVRSP4.GeoFence]);
			ReportMode = Convert.ToInt16(aSplitString[FieldsEAVRSP4.ReportMode]);
			Latitude1 = GPSUnit.GetLatitudeFromLapacDevice(aSplitString[FieldsEAVRSP4.Latitude1]);
			Longitude1 = GPSUnit.GetLongitudeFromLaipacDevice(aSplitString[FieldsEAVRSP4.Longitude1]);
			Latitude2 = GPSUnit.GetLatitudeFromLapacDevice(aSplitString[FieldsEAVRSP4.Latitude2]);
			Longitude2 = GPSUnit.GetLongitudeFromLaipacDevice(aSplitString[FieldsEAVRSP4.Longitude2]);
		}

		public new void SaveInfo(EndPoint remoteEndPoint, LP_CommandMessage commandMessage)
		{
			/** Call base first. */
			base.SaveInfo(remoteEndPoint, commandMessage);

			/** Save this command to the database. */
			CommandMessageEAVRSP4 = new LP_CommandMessageEAVRSP4
			    {
					CommandMessageID = commandMessage.CommandMessageID,
					// UnitID = (long) commandMessage.UnitID,
					GeoFenceI = (byte) GeoFence,
					ReportModeI = ReportMode.ToString(CultureInfo.InvariantCulture),
					LattitudeI1 = (double) Latitude1,
					LongitudeI1 = (double) Longitude1,
					LattitudeI2 = (double) Latitude2,
					LongitudeI2 = (double) Longitude2,
					CreatedOn = DateTime.Now,
					DEX_ROW_TS = DateTime.UtcNow
			    };
			if (commandMessage.UnitID != null) CommandMessageEAVRSP4.UnitID = (long) commandMessage.UnitID;
			CommandMessageEAVRSP4.Save();
		}

		#endregion Member Functions
	}
}