using SOS.FOS.CellStation.AlarmComWebServiceValidate;

namespace SOS.FOS.CellStation.AlarmCom
{
	public class AlarmComDeviceStatus
	{
		#region .ctor

		public AlarmComDeviceStatus(AlarmComAccount account)
		{
			Account = account;
		}

		#endregion .ctor

		#region Properties
		public AlarmComAccount Account { get; set; }

		public bool Valid { get; set; }

		public string ErrorDescription { get; set; }

		public bool IsReferral { get; set; }

		public int FirmwareVersion { get; set; }

		public string ModemSerial { get; set; }

		public NetworkEnum Network { get; set; }

		public RadioNetworkTypeEnum RadioNetworkType { get; set; }

		public bool TwoWayVoiceCapable { get; set; }

		public int ReferralPackageId { get; set; }

		public int ReferralServicePlanId { get; set; }

		#endregion Properties

		#region Methods

		public void RetrieveDeviceStatus(AlarmComAccount account)
		{
			#region Initialization
			// ** Initialize
			var client = new ValidateSoapClient();

			#endregion Initialization

			#region Call ADC to get information

			SerialNumberValidationResult result = client.ValidateSerialNumber(GetAuth(), Account.SerialNumber);


			if (!result.Valid)
			{
				Valid = result.Valid;
				return;
			}

			Valid = result.Valid;
			ErrorDescription = result.ErrorDescription;
			IsReferral = result.IsReferral;
			FirmwareVersion = result.ModemInfo.FirmwareVersion;
			ModemSerial = result.ModemInfo.ModemSerial;
			Network = result.ModemInfo.Network;
			RadioNetworkType = result.ModemInfo.RadioNetworkType;
			TwoWayVoiceCapable = result.ModemInfo.TwoWayVoiceCapable;
			ReferralPackageId = result.ReferralPackageId;
			ReferralServicePlanId = result.ReferralServicePlanId;

			#endregion Call ADC to get information
		}

		private Authentication GetAuth()
		{
			return new Authentication { Password = Account.Password, User = Account.Username };
		}

		#endregion Methods
	}
}
