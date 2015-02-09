﻿using SSE.Lib.SseGpsDeviceAPI.Commands.Interface;
using SSE.Lib.SseGpsDeviceAPI.Helper;

namespace SSE.Lib.SseGpsDeviceAPI.Commands
{
	public class ClientResponseFDA : ClientResponsePDE
	{
		#region .ctor

		public ClientResponseFDA() : base(string.Empty, CommandDef.FDA)
		{
		}

		#endregion .ctor

		#region Contants

		private const string _SENTENCE = "FDA,[DATA_ENVELOP]";

		#endregion Contants

		#region Methods

		private void ValidateProperties()
		{
			if (string.IsNullOrEmpty(DeviceID)) throw new ResponsePDEMissingProperty("DeviceID");
			if (DeviceID.Length != 8) throw new ClientRequestLengthProperty("DeviceID", DEVICEID_LENGTH, DeviceID.Length);

			if (string.IsNullOrEmpty(Lattitude)) throw new ResponsePDEMissingProperty("Lattitude");
			if (!GPSValidator.Lattitude(Lattitude)) throw new GPSValidator.GPSValidationException(Lattitude, NSIndicator.Value);

			if (string.IsNullOrEmpty(Longitude)) throw new ResponsePDEMissingProperty("Longitude");
			if (!GPSValidator.Longitude(Longitude)) throw new GPSValidator.GPSValidationException(Longitude, EWIndicator.Value);
		}

		public new string GetSentence()
		{
			/** Validate properties. */
			ValidateProperties();

			/** Build Result. */
			var rawSentence = _SENTENCE.Replace("[DATA_ENVELOP]", GetSentenceRaw());
			var chkSum = SentenceParser.GetCheckSum(rawSentence);
			SetCheckSum(chkSum);

			/** Return result. */
			return string.Format("${0}*{1}", rawSentence, CheckSum);
		}

		#endregion Methods
	}
}
