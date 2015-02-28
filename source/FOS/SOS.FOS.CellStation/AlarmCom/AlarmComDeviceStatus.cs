using System.Globalization;
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
		public AlarmComAccount Account { get; private set; }

		public bool IsVisibleToNexsense { get; private set; }

		public bool IsRegistered { get; private set; }
		
		public bool Valid { get; private set; }

		public string ErrorDescription { get; private set; }

		public bool IsReferral { get; private set; }

		public int FirmwareVersion { get; private set; }

		public string ModemSerial { get; private set; }

		public NetworkEnum Network { get; private set; }

		public RadioNetworkTypeEnum RadioNetworkType { get; private set; }

		public bool TwoWayVoiceCapable { get; private set; }

		public int ReferralPackageId { get; private set; }

		public int ReferralServicePlanId { get; private set; }

		#endregion Properties

		#region Methods

		public void RetrieveDeviceStatus(AlarmComAccount account)
		{
			#region Initialization
			// ** Initialize
			var client = new ValidateSoapClient();

			#endregion Initialization

			#region Call ADC to get information
			var clientCustomer = new AlarmComWebService.CustomerManagementSoapClient();
			int customerId = clientCustomer.LookupCustomerIdFromDealerCustomerId(GetAuthWebService(), account.AccountID.ToString(CultureInfo.InvariantCulture));
			if (customerId == 0)
			{
				IsVisibleToNexsense = false;
			}
			else
			{
				var customerInfo = clientCustomer.GetCustomerInfo(GetAuthWebService(), customerId);
			}

			var serialNumber = account.SerialNumber;
			SerialNumberValidationResult result = client.ValidateSerialNumber(GetAuth(), serialNumber);

			IsRegistered = result.ErrorDescription.Equals("SerialNumber is already in use.");

			// ** This condition says that we do not have access to this device.
			if (!result.Valid && result.ModemInfo == null)
			{
				Valid = result.Valid;
				ErrorDescription = result.ErrorDescription;
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

		private AlarmComWebService.Authentication GetAuthWebService()
		{
			return new AlarmComWebService.Authentication { Password = Account.Password, User = Account.Username };
		}

		#endregion Methods
	}
}
