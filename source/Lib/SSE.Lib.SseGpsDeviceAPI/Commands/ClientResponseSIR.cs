using System;
using System.Globalization;
using System.Net;
using SOS.Data.GpsTracking;
using SOS.Lib.Core.ExceptionHandling;
using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.ExceptionHandling;
using SSE.Lib.SseGpsDeviceAPI.Helper;
using SOS.Lib.Util.Extensions;

namespace SSE.Lib.SseGpsDeviceAPI.Commands
{
	public class ClientResponseSIR : Response
	{
		#region .ctor
		
		public ClientResponseSIR(long accountID, string imei, string sim, short ppm, string password, double lowBatteryAlert, double speedAlert, short gForceAlert) : base(string.Empty, CommandDef.SIR)
		{
			AccountID = accountID;
			IMEI = imei;
			SIM = sim;
			PPM = ppm;
			Password = password;
			LowBatteryAlert = lowBatteryAlert;
			SpeedAlert = speedAlert;
			GForceAlert = gForceAlert;

		}

		public ClientResponseSIR(string sentenceRaw) : base(sentenceRaw, CommandDef.SIR)
		{
			// ** Inititalize
			RawSentence = sentenceRaw;
			string[] splitSentence = sentenceRaw.Replace("$", "").Split('*');

			// ** Validate CheckSum
			SentenceParser.CheckSumValidation(splitSentence[_POS_SENTENCE], splitSentence[_POS_CHECKSUM]);

			// ** Parse Sentence
			ParseSentence(splitSentence[_POS_SENTENCE]);
		}

		#endregion .ctor

		#region Contants

		private const string _SENTENCE_RAW = "{AccountID},{IMEI},{SIM},{PPM},{LBA},{SPA},{GFA},{UTCDate},{UTCTime}";
		private const string _SENTENCE = "SIR,[DATA_ENVELOP]";
		private const int _ACCOUNTID_LENGTH = 10;
		private const int _MIN_ACCOUNT_ID = 100000;
		private const int _POS_SENTENCE = 0;
		private const int _POS_CHECKSUM = 1;

		/** Field Positions */
		private const int _POS_ACCOUNTID = 0;
		private const int _POS_IMEI = _POS_ACCOUNTID + 1;
		private const int _POS_SIM = _POS_ACCOUNTID + 2;
		private const int _POS_PPM = _POS_ACCOUNTID + 3;
		private const int _POS_LBA = _POS_ACCOUNTID + 4;
		private const int _POS_SPA = _POS_ACCOUNTID + 5;
		private const int _POS_GFA = _POS_ACCOUNTID + 6;
		private const int _POS_DATE = _POS_ACCOUNTID + 7;
		private const int _POS_TIME = _POS_ACCOUNTID + 8;

		#endregion Contants

		#region Member Variables

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
		public DateTime ExecuteDateTime { get; private set; }

		public string CheckSum { get; private set; }


		public new SS_CommandMessage CommandMessage
		{
			get { return base.CommandMessage; }
		}

//		public SS_CommandMessage CommandMessage { get; private set; }
		public new string RawSentence { get; private set; }

		#endregion Member Variables

		#region Member Functions

		private void ParseSentence(string sentence)
		{
			// ** Initialize. 
			var parsedSentence = sentence.Split(',');
			long accountId;
			if (!long.TryParse(parsedSentence[_POS_ACCOUNTID], out accountId))
				throw new SseInvalidAccountIDException(parsedSentence[_POS_ACCOUNTID], "ClientResponseSIR ParseSentence method.");
			short ppm;
			if (!short.TryParse(parsedSentence[_POS_PPM], out ppm))
				throw new SseInvalidPPMException(parsedSentence[_POS_PPM], "ClientResponseSIR, ParseSentence method.");
			double lowBatteryAlert;
			if (!double.TryParse(parsedSentence[_POS_LBA], out lowBatteryAlert))
				throw new SseInvalidLBAException(parsedSentence[_POS_LBA], "ClientResponseSIR, ParseSentence method.");
			double speedAlert;
			if (!double.TryParse(parsedSentence[_POS_SPA], out speedAlert))
				throw new SseInvalidSPAException(parsedSentence[_POS_SPA], "ClientResponseSIR, ParseSentence method.");
			short gForceAlert;
			if (!short.TryParse(parsedSentence[_POS_GFA], out gForceAlert))
				throw new SseInvalidGFAException(parsedSentence[_POS_GFA], "ClientResponseSIR, ParseSentence method.");

			AccountID = accountId;
			IMEI = parsedSentence[_POS_IMEI];
			SIM = parsedSentence[_POS_SIM];
			PPM = ppm;
			LowBatteryAlert = lowBatteryAlert;
			SpeedAlert = speedAlert;
			GForceAlert = gForceAlert;
			ExecuteDateTime = DateTimeConversions.GetDateTimeFromData(parsedSentence[_POS_DATE], parsedSentence[_POS_TIME]);


		}

		private void ValidateProperties()
		{
			/** AccountID */
			if (AccountID == 0) throw new ClientResponseMissingProperty("AccountID");
			if (AccountID < _MIN_ACCOUNT_ID) throw new ClientResponseLengthProperty("AccountID", _ACCOUNTID_LENGTH, AccountID.ToString(CultureInfo.InvariantCulture).Length);

			/** IMEI. */
			if (string.IsNullOrEmpty(IMEI)) throw new ClientResponseMissingProperty("IMEI");

			// ** SIM
			if (string.IsNullOrEmpty(SIM)) throw new ClientResponseMissingProperty("SIM");
			// ** PPM 
			if (PPM == 0) throw new ClientResponseMissingProperty("PPM");
			// ** LowBatteryAlert
			if (LowBatteryAlert.IsZero()) throw new ClientResponseMissingProperty("LowBatteryAlert");
			// ** SPA
			if (SpeedAlert.IsZero()) throw new ClientResponseMissingProperty("SPA");
			// ** GForceAlert
			if (GForceAlert == 0) throw new ClientResponseMissingProperty("GForceAlert");


		}

		public string GetSentenceRaw()
		{
			// ** Validate properties
			ValidateProperties();

			// ** Inititalize. 
			string result = _SENTENCE_RAW.Replace("{AccountID}", AccountID.ToString(CultureInfo.InvariantCulture));
			result = result.Replace("{IMEI}", IMEI);
			result = result.Replace("{SIM}", SIM);
			result = result.Replace("{PPM}", PPM.ToString(CultureInfo.InvariantCulture));
			result = result.Replace("{LBA}", LowBatteryAlert.ToString(CultureInfo.InvariantCulture));
			result = result.Replace("{SPA}", SpeedAlert.ToString(CultureInfo.InvariantCulture));
			result = result.Replace("{GFA}", GForceAlert.ToString(CultureInfo.InvariantCulture));
			result = result.Replace("{UTCDate}", DateTimeConversions.GetDateDeivceFormat(DateTime.UtcNow));
			result = result.Replace("{UTCTime}", DateTimeConversions.GetTimeDeivceFormat(DateTime.UtcNow));

			// ** Return result.
			return result;
		}

		public string GetSentence()
		{
			/** Build result. */
			var rawSentence = _SENTENCE.Replace("[DATA_ENVELOP]", GetSentenceRaw());

			CheckSum = SentenceParser.GetCheckSum(rawSentence);

			/** Return result. */
			return string.Format("${0}*{1}", rawSentence, CheckSum);
		}

		public void SaveInfo(EndPoint remoteEndPoint, SS_CommandMessage commandMessage)
		{
			// ** Initialize
			SetCommandMessage(commandMessage);
		}

		#endregion Member Functions
	}

	#region Exceptions 

	public class ClientResponseBaseException : BaseException
	{
		public ClientResponseBaseException(string propertyName, string message)
			: base(message)
		{
			PropertyName = propertyName;
		}
		public string PropertyName { get; private set; }
	}

	public class ClientResponseMissingProperty : ClientResponseBaseException
	{
		public ClientResponseMissingProperty(string propertyName) : base(propertyName, string.Format("The property '{0}' did not have a valid value,", propertyName))
		{
		}
	}


	public class ClientResponseLengthProperty : ClientResponseBaseException
	{
		public ClientResponseLengthProperty(string propertyName, int requiredLength, int actualLength)
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