using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SOS.Data.GpsTracking;
using SOS.Lib.LaipacAPI.ExceptionHandling;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class EAVGOFRequest : Requests
	{
		#region .ctor

		/// <summary>
		/// 31. Change Report Mode of all valid Geo-fences setting items
		/// </summary>
		/// <param name="sPassword">string</param>
		/// <param name="unitID">long</param>
		/// <param name="reportMode">char</param>
		public EAVGOFRequest(string sPassword, long unitID, char reportMode)
			: base(sPassword)
		{
			CommandName = LP_CommandName.MetaData.EAVGOF2ID;
			UnitID = unitID;
			ReportModeI = reportMode;

			CreateCommandMessage();
		}

		/// <summary>
		/// 32. Set one or more Geo-fences items (not more than 5 items/ per setting)
		/// </summary>
		/// <param name="sPassword">string</param>
		/// <param name="unitID">long</param>
		/// <param name="oItemsList">List of FenceItem</param>
		public EAVGOFRequest(string sPassword, long unitID, List<FenceItem> oItemsList)
			: base(sPassword)
		{
			CommandName = LP_CommandName.MetaData.EAVGOF3ID;
			UnitID = unitID;
			Total = oItemsList.Count.ToString(CultureInfo.InvariantCulture);
			ItemsList = oItemsList;

			CreateCommandMessage();
		}

		/// <summary>
		/// 33. Get one Geo-fence item’s setting
		/// </summary>
		/// <param name="sPassword">string</param>
		/// <param name="unitID">long</param>
		/// <param name="geoFenceI">short</param>
		public EAVGOFRequest(string sPassword, long unitID, short geoFenceI)
			: base(sPassword)
		{
			CommandName = LP_CommandName.MetaData.EAVGOF4ID;
			UnitID = unitID;
			GeoFenceI = geoFenceI;

			CreateCommandMessage();
		}

		/// <summary>
		/// 34. Change Report Mode of several valid Geo-fences setting items
		/// </summary>
		/// <param name="sPassword">string</param>
		/// <param name="unitID">long</param>
		/// <param name="sTotal">string</param>
		/// <param name="geoFenceIList">List of short</param>
		/// <param name="cReportMode">char</param>
		public EAVGOFRequest(string sPassword, long unitID, string sTotal, List<short> geoFenceIList, char cReportMode)
			: base(sPassword)
		{
			CommandName = LP_CommandName.MetaData.EAVGOF5ID;
			UnitID = unitID;
			Total = sTotal;
			GeoFenceIList = geoFenceIList;
			ReportModeI = cReportMode;
		}

		/// <summary>
		/// 35. Upload one item of Geo-fence setting
		/// </summary>
		/// <param name="sPassword">string</param>
		/// <param name="unitID">long</param>
		/// <param name="geoFenceI">short</param>
		/// <param name="reportModeI">char</param>
		/// <param name="lattitudeI1">float</param>
		/// <param name="longitudeI1">float</param>
		/// <param name="lattitudeI2">float</param>
		/// <param name="longitudeI2">float</param>
		public EAVGOFRequest(string sPassword, long unitID, short geoFenceI, char reportModeI, float lattitudeI1, float longitudeI1
			, float lattitudeI2, float longitudeI2) : base(sPassword)
		{
			CommandName = LP_CommandName.MetaData.EAVGOF6ID;
			UnitID = unitID;
			GeoFenceI = geoFenceI;
			ReportModeI = reportModeI;
			LatitudeI1 = lattitudeI1;
			LongtitudeI1 = longitudeI1;
			LatitudeI2 = lattitudeI2;
			LongtitudeI2 = longitudeI2;
		}

		#endregion .ctor

		#region Member Variables

		public LP_CommandMessage CommandMessage { get; private set; }
		public long UnitID { get; private set; }
		public string CommandName { get; private set; }
		public string Total { get; private set; }
		public short GeoFenceI { get; private set; }
		public char ReportModeI { get; private set; }
		public float LatitudeI1 { get; private set; }
		public float LongtitudeI1 { get; private set; }
		public float LatitudeI2 { get; private set; }
		public float LongtitudeI2 { get; private set; }

		public List<FenceItem> ItemsList { get; private set; }
		public List<short> GeoFenceIList { get; private set; } 

		public string Sentence
		{
			get { return GetRequestWrapper(GetRequest()); }
		}

		#endregion Member Variables

		#region Member Functions

		public void CreateCommandMessage()
		{
			/** Create Base Command Message. */
			CommandMessage = new LP_CommandMessage
			{
				UnitID = UnitID,
				CommandTypeId = LP_CommandType.MetaData.RequestCommandsID,
				CommandNameId = CommandName,
				MessageDate = DateTime.Now,
				Sentence = Sentence,
				CreatedOn = DateTime.Now,
				DEX_ROW_TS = DateTime.UtcNow
			};
			CommandMessage.Save();
		}


		public string GetRequest()
		{
			/** Initialize. */
			string requestSentence;
			var sb = new StringBuilder();

			switch(CommandName)
			{
				case LP_CommandName.MetaData.EAVGOF2ID:
					requestSentence = REQ_EAVGOF_GEO2_COMMAND
						.Replace("{PWD}", Password)
						.Replace("{Report Mode}", ReportModeI.ToString(CultureInfo.InvariantCulture));
					break;
				case LP_CommandName.MetaData.EAVGOF3ID:
					foreach (FenceItem fenceItem in ItemsList)
					{
						sb.Append(GEOFENCE_ITEM
							.Replace("{Geo-fencei}", fenceItem.GeoFence.ToString(CultureInfo.InvariantCulture))
							.Replace("{Report Modei}", fenceItem.ReportMode.ToString(CultureInfo.InvariantCulture))
							.Replace("{Latitudei1}", Helper.GPSUnit.ConvertFromGoogleMapsToLaipacLatitude(fenceItem.Latitude1))
							.Replace("{Longtitudei1}", Helper.GPSUnit.ConvertFromGoogleMapsToLaipacLongitude(fenceItem.Longtitude1))
							.Replace("{Latitudei2}", Helper.GPSUnit.ConvertFromGoogleMapsToLaipacLatitude(fenceItem.Latitude2))
							.Replace("{Longtitudei2}", Helper.GPSUnit.ConvertFromGoogleMapsToLaipacLongitude(fenceItem.Longtitude2))
							);
					}
					requestSentence = REQ_EAVGOF_GEO3_COMMAND
						.Replace("{PWD}", Password)
						.Replace("{Total}", Total.ToString(CultureInfo.InvariantCulture))
						.Replace("{GEOFENCE_ITEM}", sb.ToString());
					break;
				case LP_CommandName.MetaData.EAVGOF4ID:
					requestSentence = REQ_EAVGOF_GEO4_COMMAND
						.Replace("{PWD}", Password)
						.Replace("{Geo-fencei}", GeoFenceI.ToString(CultureInfo.InvariantCulture));
					break;
				case LP_CommandName.MetaData.EAVGOF5ID:
					foreach (var geoFenceI in GeoFenceIList)
					{
						sb.Append(Convert.ToChar(",{0}"), geoFenceI);
					}
					requestSentence = REQ_EAVGOF_GEO6_COMMAND
						.Replace("{PWD}", Password)
						.Replace("{Total}", Total.ToString(CultureInfo.InvariantCulture))
						.Replace("{,Geo-fencei1,…,Geo-fenceik}", sb.ToString())
						.Replace("{Report Mode}", ReportModeI.ToString(CultureInfo.InvariantCulture));
					break;
				case LP_CommandName.MetaData.EAVGOF6ID:
					requestSentence = REQ_EAVGOF_GEO6_COMMAND
						.Replace("{PWD}", Password)
						.Replace("{Geo-fencei}", GeoFenceI.ToString(CultureInfo.InvariantCulture))
						.Replace("{Report Mode}", ReportModeI.ToString(CultureInfo.InvariantCulture))
						.Replace("{Latitudei1}", LatitudeI1.ToString(CultureInfo.InvariantCulture))
						.Replace("{Longtitudei1}", LongtitudeI1.ToString(CultureInfo.InvariantCulture))
						.Replace("{Latitudei2}", LatitudeI2.ToString(CultureInfo.InvariantCulture))
						.Replace("{Longtitudei2}", LongtitudeI2.ToString(CultureInfo.InvariantCulture));
					break;
				default:
					throw new LaipacCommandNotSupported(string.Format("The Command {0} is not supported as a EAVGOFRequest.", CommandName), CommandName);
			}

			/** Return sentence. */
			return requestSentence;
		}

		public LP_Request QueueRequest(long lAccountID)
		{
			/** Initialize. */
			var lpRequest = new LP_Request
			{
				CommandMessageId = CommandMessage.CommandMessageID,
				RequestNameId = CommandName,
				AccountId = lAccountID,
				UnitID = UnitID,
				Sentence = Sentence,
				Attempts = 0,
				CreatedOn = DateTime.UtcNow
			};
			lpRequest.Save();

			/** Bind to command and bind request to command. */
			CommandMessage.RequestId = lpRequest.RequestID;
			CommandMessage.Save();

			/** Return request. */
			return lpRequest;
		}

		#endregion Member Functions

		#region Supporting structures

		public struct FenceItem
		{
			public short GeoFence { get; set; }
			public char ReportMode { get; set; }
			public double Latitude1 { get; set; }
			public double Longtitude1 { get; set; }
			public double Latitude2 { get; set; }
			public double Longtitude2 { get; set; }
		}

		#endregion Supporting structures
	}
}
