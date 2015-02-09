using System;
using System.Globalization;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Data.SosCrm;
using SOS.Lib.LaipacAPI.Helper;

namespace SOS.Lib.LaipacAPI.Commands
{
	public class AVRMCResponse : Response
	{
		#region .ctor
		public AVRMCResponse(string rawSentence, Func<string, decimal, decimal, string, string, bool?, bool> dispatchToCs = null) 
			: base(rawSentence, LP_CommandType.MetaData.RequestCommandsID, CommandDef.AVRMC)
		{
			CommandAVCFGRequest = null;
			_dispatchToCs = dispatchToCs;

			Parse();
		}
		#endregion .ctor

		#region Member Variables

		#region Sentence Components

		protected string UTCTime { get; private set; }
		protected string Status { get; private set; }
		protected string Latitude { get; private set; }
		protected decimal LatitudeDec
		{
			get { return GPSUnit.GetLatitudeFromLapacDevice(Latitude, NSIndicator); }
		}
		protected string NSIndicator { get; private set; }
		protected string Longitude { get; private set; }
		protected decimal LongitudeDec
		{
			get { return GPSUnit.GetLongitudeFromLaipacDevice(Longitude, EWIndicator); }
		}
		protected string EWIndicator { get; private set; }
		protected string Speed { get; private set; }
		protected string Course { get; private set; }
		protected string UTCDate { get; private set; }
		protected string EventCode { get; private set; }
		protected string BatteryVoltage { get; private set; }
		protected string CurrentMilage { get; private set; }
		protected string GPSStatus { get; private set; }
		protected string AnalogPort1 { get; private set; }
		protected string AnalogPort2 { get; private set; }

		#endregion Sentence Components

		protected LP_CommandMessageAVRMC CommandMessageAVRMC { get; private set; }
		protected AVCFGRequest CommandAVCFGRequest { get; private set; }
		protected EAVACKRequest CommandEAVACKRequest { get; private set; }
		private bool _suppressResponse;
		private GS_Account _msAccount;

		private readonly Func<string, decimal, decimal, string, string, bool?, bool> _dispatchToCs;

		protected enum AcknowledgeTypes
		{
			None,
			AVCFG,
			EAVACK
		}

		protected AcknowledgeTypes EAcknowledgeTypes { get; private set; }


		#endregion Member Variables

		#region Member Functions

		private void Parse()
		{
			/** Initialize. */
			string[] aSplitString = SentenceNet.Split(',');

			UnitIDSet(aSplitString[FieldsAVRMC.UnitID]);
			UTCTime = aSplitString[FieldsAVRMC.UTCTime];
			Status = aSplitString[FieldsAVRMC.Status];
			Latitude = aSplitString[FieldsAVRMC.Latitude];
			NSIndicator = aSplitString[FieldsAVRMC.NSIndicator];
			Longitude = aSplitString[FieldsAVRMC.Longitude];
			EWIndicator = aSplitString[FieldsAVRMC.EWIndicator];
			Speed = aSplitString[FieldsAVRMC.Speed];
			Course = aSplitString[FieldsAVRMC.Course];
			UTCDate = aSplitString[FieldsAVRMC.UTCDate];
			EventCode = aSplitString[FieldsAVRMC.EventCode];
			BatteryVoltage = aSplitString[FieldsAVRMC.BatteryVoltage];
			CurrentMilage = aSplitString[FieldsAVRMC.CurrentMilage];
			GPSStatus = aSplitString[FieldsAVRMC.GPSStatus];
			AnalogPort1 = aSplitString[FieldsAVRMC.AnalogPort1];
			AnalogPort2 = aSplitString[FieldsAVRMC.AnalogPort2];

		}

		public void SaveInfo(EndPoint remoteEndPoint, LP_CommandMessage commandMessage)
		{
			/** Initialize. */
			_msAccount = GPSUnit.GetMsAccount(UnitID.ToString(CultureInfo.InvariantCulture));
			var lpDevice = GpsTrackingDataContext.Instance.LP_Devices.LoadByPrimaryKey(UnitID) ??
						   new LP_Device { AccountID = _msAccount.AccountID, UnitID = UnitID };

			/** Bind Device Info to object. */
			BindDeviceInfo(remoteEndPoint, lpDevice);
			/** Bind base command information. */
			base.SaveInfo(remoteEndPoint, commandMessage);

			/** 
			 * Bind new data. 
			 * (Only if the sentence passed has info that could bind to LP_Devices.)
			 */

			/** Save it. */
			lpDevice.Save();

			/** Save Command information. */
			commandMessage.CommandTypeId = CommandTypeID;
			commandMessage.CommandNameId = LP_CommandName.MetaData.AVRMCID;
			commandMessage.UnitID = UnitID;
			commandMessage.CreatedOn = DateTime.Now;
			commandMessage.DEX_ROW_TS = DateTime.UtcNow;
			commandMessage.Save();

			/** Attach it to the base class. */
			CommandMessageSet(commandMessage);

			/** Save Command in proprietary table. */
			var avrmcCommand = new LP_CommandMessageAVRMC
			                   	{
			                   		CommandMessageID = CommandMessage.CommandMessageID,
			                   		UTCDateTime = SentenceParser.ConvertStringToDateTime(UTCDate, UTCTime),
			                   		DeviceStatusId = Status,
			                   		Latitude = ((double)0).TryParseWithNull(Latitude),
			                   		NSIndicator = NSIndicator,
			                   		Longitude = ((double)0).TryParseWithNull(Longitude),
			                   		EWIndicator = EWIndicator,
									Speed = ((decimal)0).TryParseWithNull(Speed),
									Course = ((decimal)0).TryParseWithNull(Course),
			                   		EventCodeId = EventCode,
			                   		BatteryVoltage = int.Parse(BatteryVoltage),
			                   		CurrentMilage = int.Parse(CurrentMilage),
									GPSStatus = ((bool?)true).TryParseWithNull(GPSStatus),
									AnalogPort1 = ((bool?)true).TryParseWithNull(AnalogPort1),
									AnalogPort2 = ((bool?)true).TryParseWithNull(AnalogPort2),
									CreatedOn = DateTime.Now,
									DEX_ROW_TS = DateTime.UtcNow
			                   	};
			avrmcCommand.Save();

			/** Save as property.  */
			CommandMessageAVRMC = avrmcCommand;
			
			/** Perform Action if necessary. */
			PerformAction();
		}

		private void PerformAction()
		{
			/** Initialize. */
			var sPassword = GPSUnit.GetPassword(UnitID.ToString(CultureInfo.InvariantCulture));
			LP_AVCFGCode.MetaDataEnum eAcknowledge = LP_AVCFGCode.MetaDataEnum.AcknowledgeSOSPanicAlertMessage;
			EAcknowledgeTypes = AcknowledgeTypes.None;

			/** Check to see if this is an emergency event. */
			switch (CommandMessageAVRMC.EventCodeId)
			{
				case LP_EventCode.MetaData.SOSbuttonpressedalertID:
				case LP_EventCode.MetaData.PanicSOSbuttonpressedalertID:
					_suppressResponse = !_dispatchToCs(CommandMessageAVRMC.EventCodeId, LatitudeDec, LongitudeDec, _msAccount.DispatchMessage,
					                                  _msAccount.IndustryAccount.Csid, MonitoringEnvironmentIsLive);
					eAcknowledge = LP_AVCFGCode.MetaDataEnum.AcknowledgeSOSPanicAlertMessage;
					EAcknowledgeTypes = AcknowledgeTypes.AVCFG;
					break;
				case LP_EventCode.MetaData.G_Sensoralert1ID:
					_suppressResponse = !_dispatchToCs(CommandMessageAVRMC.EventCodeId, LatitudeDec, LongitudeDec, _msAccount.DispatchMessage,
					                                  _msAccount.IndustryAccount.Csid, MonitoringEnvironmentIsLive);
					EAcknowledgeTypes = Char.IsLower(CommandMessageAVRMC.DeviceStatusId[0])
						? AcknowledgeTypes.EAVACK 
						: AcknowledgeTypes.None;
					break;
				case LP_EventCode.MetaData.Geo_fenceenteralertID:
				case LP_EventCode.MetaData.Geo_fenceexitsalertID:
					eAcknowledge = LP_AVCFGCode.MetaDataEnum.AcknowledgeGeoFenceAlertMessage;
					EAcknowledgeTypes = AcknowledgeTypes.AVCFG;
					break;
				case LP_EventCode.MetaData.LowbatteryalertID:
					eAcknowledge = LP_AVCFGCode.MetaDataEnum.AcknowledgeLowBatteryAlertMessage;
					EAcknowledgeTypes = AcknowledgeTypes.AVCFG;
					break;
				case LP_EventCode.MetaData.TamperdetectionswitchisclosealertID:
				case LP_EventCode.MetaData.TamperdetectionswitchisopenalertID:
					eAcknowledge = LP_AVCFGCode.MetaDataEnum.AcknowledgeTamperDetectionAlertMessage;
					EAcknowledgeTypes = AcknowledgeTypes.AVCFG;
					break;
				case LP_EventCode.MetaData.OverspeedalertID:
				//TODO:  We need to fire a DISPATCH Alert here.

				case LP_EventCode.MetaData.RegularreportID:
					/** Only acknowledge the lowercase regular reporting. */
					EAcknowledgeTypes = Char.IsLower(CommandMessageAVRMC.DeviceStatusId[0])
						? AcknowledgeTypes.EAVACK 
						: AcknowledgeTypes.None;
					break;
				default:
					EAcknowledgeTypes = AcknowledgeTypes.None;
					break;
			}

			/** Create Acknowledgement request. */
			switch (EAcknowledgeTypes)
			{
					case AcknowledgeTypes.AVCFG:
					CommandAVCFGRequest = new AVCFGRequest(sPassword, eAcknowledge, CommandMessageAVRMC);
					break;
					case AcknowledgeTypes.EAVACK:
						CommandEAVACKRequest = new EAVACKRequest(EventCode, CheckSum, CommandMessageAVRMC);
					break;
			}
		}

		public new string GetResponseBack()
		{
			/** Check of the response should be suppressed. */
			if (!_suppressResponse)
			{
				switch (EAcknowledgeTypes)
				{
					case AcknowledgeTypes.AVCFG:
						return CommandAVCFGRequest.GetRequest();
					case AcknowledgeTypes.EAVACK:
						return CommandEAVACKRequest.GetRequest();
				}
			}

			/** Default path of execution */
			return string.Empty;
		}

		#endregion Member Functions

	}
}