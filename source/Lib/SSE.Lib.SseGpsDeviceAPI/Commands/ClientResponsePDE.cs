using SOS.Lib.Core.ExceptionHandling;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.Helper;
using System;

namespace SSE.Lib.SseGpsDeviceAPI.Commands
{
	public class ClientResponsePDE : Response, IResponsePDE
	{
		#region .ctor
		public ClientResponsePDE(string sentence) : base(sentence, CommandDef.PDE)
		{
			Init();
		}

		protected ClientResponsePDE(string sentence, CommandDef commandDef) : base (sentence, commandDef)
		{
			Init();
		}

		#endregion .ctor

		#region Constants

		private const string _SENTENCE_RAW = "{DeviceID},{UTCTime},{Lattitude}{NSIndicator},{Longitude}{EWIndicator},{HDOP},{Altitude},{Fix},{COG},{SpKm},{SpKn},{UTCDate},{NSat},{GForce},{Battery},{CellStrength},{GpsStrength},{MessageState}";
		private const string _SENTENCE = "PDE,[DATA_ENVELOP]";

		protected const int DEVICEID_LENGTH = 8;

		#endregion Constants

		#region Member Properties

		public string DeviceID { get; set; }

		public string Lattitude { get; set; }

		public GPSIndicator NSIndicator { get; set; }

		public string Longitude { get; set; }

		public GPSIndicator EWIndicator { get; set; }

		public string HDOP { get; set; }

		public string Altitude { get; set; }

		public string Fix { get; set; }

		public string COG { get; set; }

		public string SpKm { get; set; }

		public string SpKn { get; set; }

		public DateTime UTCEventTime { get; set; }

		public string NSat { get; set; }

		public string GForce { get; set; }

		public string Battery { get; set; }

		public string CellStrength { get; set; }

		public string GpsStrength { get; set; }

		public MessageState MessageState { get; set; }

		public string CheckSum { get; private set; }

		protected void SetCheckSum(string ckSum)
		{
			CheckSum = ckSum;
		}

		#endregion Member Properties

		#region Member Functions

		private void Init()
		{
			DateTime utcDateTime = DateTime.UtcNow;

			HDOP = "0.0";
			Altitude = "0.0";
			Fix = "3";
			COG = "0.0";
			SpKm = "0.00";
			SpKn = "0.00";
			UTCEventTime = utcDateTime; // 240613
			NSat = "00";
			Battery = "0";
			CellStrength = "0";
			GpsStrength = "0";
			MessageState = MessageState.DataLogged;
		}

		private void ValidateProperties()
		{
			if (string.IsNullOrEmpty(DeviceID)) throw new ResponsePDEMissingProperty("DeviceID");
			if (DeviceID.Length != 8) throw new ClientRequestLengthProperty("DeviceID", DEVICEID_LENGTH, DeviceID.Length);

			if (string.IsNullOrEmpty(Lattitude)) throw new ResponsePDEMissingProperty("Lattitude");
			if (!GPSValidator.Lattitude(Lattitude)) throw new GPSValidator.GPSValidationException(Lattitude, NSIndicator.Value);

			if (string.IsNullOrEmpty(Longitude)) throw new ResponsePDEMissingProperty("Longitude");
			if (!GPSValidator.Longitude(Longitude)) throw new GPSValidator.GPSValidationException(Longitude, EWIndicator.Value);

		}

		public string GetSentence()
		{
			/** Validate properties. */
			ValidateProperties();

			/** Build result. */
			var rawSentence = _SENTENCE.Replace("[DATA_ENVELOP]", GetSentenceRaw());

			CheckSum = SentenceParser.GetCheckSum(rawSentence);

			/** Return result. */
			return string.Format("${0}*{1}", rawSentence, CheckSum);
		}

		protected string GetSentenceRaw()
		{
			/** Initialize. */
			var result = _SENTENCE_RAW.Replace("{DeviceID}", DeviceID);

			/** Build sentence raw. */
			result = result.Replace("{UTCTime}", GetUTCTime());
			result = result.Replace("{Lattitude}", Lattitude);
			result = result.Replace("{NSIndicator}", NSIndicator.Value);
			result = result.Replace("{Longitude}", Longitude);
			result = result.Replace("{EWIndicator}", EWIndicator.Value);
			result = result.Replace("{HDOP}", HDOP);
			result = result.Replace("{Altitude}", Altitude);
			result = result.Replace("{Fix}", Fix);
			result = result.Replace("{COG}", COG);
			result = result.Replace("{SpKm}", SpKm);
			result = result.Replace("{SpKn}", SpKn);
			result = result.Replace("{UTCDate}", GetUTCDate());
			result = result.Replace("{NSat}", NSat);
			result = result.Replace("{GForce}", GForce);
			result = result.Replace("{Battery}", Battery);
			result = result.Replace("{CellStrength}", CellStrength);
			result = result.Replace("{GpsStrength}", GpsStrength);
			result = result.Replace("{MessageState}", MessageState.Value);

			/** Return result. */
			return result;
		}

		public string GetUTCTime()
		{
			return string.Format("{0:D2}{1:D2}{2:D2}{3:D3}", UTCEventTime.Hour, UTCEventTime.Minute, UTCEventTime.Second,
			                     UTCEventTime.Millisecond);
		}

		public string GetUTCDate()
		{
			return string.Format("{0:D2}{1:D2}{2:D4}", UTCEventTime.Month, UTCEventTime.Day, UTCEventTime.Year);
		}

		#endregion Member Functions
	}

	#region Exceptions
	
	public class ResponsePDEBaseException : BaseException
	{
		public ResponsePDEBaseException(string propertyName, string message) : base(message)
		{
			PropertyName = propertyName;
		}
		public string PropertyName { get; private set; }
	}

	public class ResponsePDEMissingProperty : ResponsePDEBaseException
	{
		public ResponsePDEMissingProperty(string propertyName) : base(propertyName, string.Format("The property '{0}' did not have a valid value.", propertyName))
		{
		}
	}

	public class ResponsePDELengthProperty : ResponsePDEBaseException
	{
		public ResponsePDELengthProperty(string propertyName, int requiredLength, int actualLength)
			: base(propertyName, string.Format("The property '{0}' has a value of length '{1}' when it should be '{2}'."
			                     , propertyName, actualLength, requiredLength))
		{
			RequiredLength = requiredLength;
			ActualLength = actualLength;
		}

		protected int RequiredLength { get; private set; }
		protected int ActualLength { get; private set; }
	}

	#endregion Exceptions

}
