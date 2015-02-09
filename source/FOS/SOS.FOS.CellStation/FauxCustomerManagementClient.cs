using SOS.FOS.CellStation.AlarmComWebService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.FOS.CellStation
{
	public class FauxCustomerManagementClient : ICustomerManagementSoapClient
	{
		public ActivateModemOutput ActivateModem(Authentication Authentication, string modemSerial)
		{
			throw new NotImplementedException();
		}

		public AddCustomerLoginOutput AddCustomerLogin(Authentication Authentication, AddCustomerLoginInput input)
		{
			throw new NotImplementedException();
		}

		public AddDefaultNotificationsOutput AddDefaultNotifications(Authentication Authentication, AddDefaultNotificationsInput input)
		{
			throw new NotImplementedException();
		}

		public AddExistingLoginOutput AddExistingLogins(Authentication Authentication, AddExistingLoginInput input)
		{
			throw new NotImplementedException();
		}

		public AddSensorDeviceOutput AddImageSensorDevice(Authentication Authentication, ImageSensorInfo input)
		{
			throw new NotImplementedException();
		}

		public AddSensorDeviceOutput AddSensorDevice(Authentication Authentication, AddSensorDeviceInput input)
		{
			throw new NotImplementedException();
		}

		public AddSensorDeviceFor2GigOutput AddSensorDeviceFor2Gig(Authentication Authentication, AddSensorDeviceFor2GigInput input)
		{
			throw new NotImplementedException();
		}

		public AddSensorGetSensorTypesOutput AddSensorGetSensorTypes(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public AddSensorGetSensorVoiceDescriptorsOutput AddSensorGetSensorVoiceDescriptors(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public AddUserCodeOutput AddUserCode(Authentication Authentication, AddUserCodeInput input)
		{
			throw new NotImplementedException();
		}

		public BackupPanelSettingsOutput BackupPanelSettings(Authentication Authentication, BackupPanelSettingsInput input)
		{
			throw new NotImplementedException();
		}

		public ChangeServicePlanOutput ChangeServicePlan(Authentication Authentication, int customerId, int newPackageId, AddOnFeatureEnum[] addOnFeatures)
		{
			throw new NotImplementedException();
		}

		public CheckPanelSettingsBackupOutput CheckPanelSettingsBackup(Authentication Authentication, CheckPanelSettingsBackupInput input)
		{
			throw new NotImplementedException();
		}

		public ConfirmPrimaryEmailOutput ConfirmPrimaryEmail(Authentication Authentication, int customerId, string emailAddress)
		{
			throw new NotImplementedException();
		}

		public CreateCustomerOutput CreateCustomer(Authentication Authentication, CreateCustomerInput input)
		{
			throw new NotImplementedException();
		}

		public CreateCustomerOutput CreateCustomerForDealer(Authentication Authentication, CreateCustomerInput input, int? dealerId)
		{
			throw new NotImplementedException();
		}

		public CreateImageSensorAutoUploadRuleOutput CreateImageSensorAutoUploadRule(Authentication Authentication, int customerId, ImageSensorAutoUploadRule[] rules)
		{
			throw new NotImplementedException();
		}

		public CreateNewCustomerWebsiteMessageOutput CreateNewCustomerWebsiteMessage(Authentication Authentication, CreateNewCustomerWebsiteMessageInput input)
		{
			throw new NotImplementedException();
		}

		public DeleteCustomerLoginOutput DeleteCustomerLogin(Authentication Authentication, DeleteCustomerLoginInput input)
		{
			throw new NotImplementedException();
		}

		public DeleteCustomerWebsiteMessageOutput DeleteCustomerWebsiteMessage(Authentication Authentication, DeleteCustomerWebsiteMessageInput input)
		{
			throw new NotImplementedException();
		}

		public DownloadToPanelOutput DownloadToPanel(Authentication Authentication, int customerId, PanelSettingInput[] panelSettings)
		{
			throw new NotImplementedException();
		}

		public EditCustomerMasterCodeOutput EditMasterCode(Authentication Authentication, EditCustomerMasterCodeInput input)
		{
			throw new NotImplementedException();
		}

		public EditUserCodeOutput EditUserCode(Authentication Authentication, EditUserCodeInput input)
		{
			throw new NotImplementedException();
		}

		public TransferAccountOutput[] EmulateTransferAccounts(Authentication Authentication, TransferAccountInput[] inputs)
		{
			throw new NotImplementedException();
		}

		public GenerateCustomerLoginTokenOutput GenerateCustomerLoginToken(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public GenerateCustomerLoginTokenOutput GenerateLoginToken(Authentication Authentication, string loginName)
		{
			throw new NotImplementedException();
		}

		public GetAllLoginsOutput GetAllLogins(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public GetCustomerEnergyProgramEligibilityOutput GetCustomerEnergyProgramEligibility(Authentication Authentication, EnergyProgramEnum program)
		{
			throw new NotImplementedException();
		}

		public CustomerInfo GetCustomerInfo(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public int[] GetCustomerList(Authentication Authentication, bool includeTermed)
		{
			throw new NotImplementedException();
		}

		public int[] GetCustomerListByBranchId(Authentication Authentication, int branchId, bool includeTermed)
		{
			throw new NotImplementedException();
		}

		public CustomerListWithTroubleConditions[] GetCustomerListWithTroubleConditions(Authentication Authentication, TroubleConditionTypeEnum[] conditions)
		{
			throw new NotImplementedException();
		}

		public string GetCustomerListWithTroubleConditionsCsv(Authentication Authentication, TroubleConditionTypeEnum[] conditions)
		{
			throw new NotImplementedException();
		}

		public TroubleConditionOutput[] GetCustomerTroubleConditions(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public PanelDevice[] GetDeviceList(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public GetDownloadableSettingsOutput GetDownloadableSettings(Authentication Authentication, int customerId, int deviceId)
		{
			throw new NotImplementedException();
		}

		public GetEligibleSensorGroupsOutput GetEligibleSensorGroups(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public GetEligibleSensorNamesOutput GetEligibleSensorNames(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public PanelDevice[] GetFullEquipmentList(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public GetImageSensorInfoOutput GetImageSensorInfo(Authentication Authentication, int customerId, int deviceId)
		{
			throw new NotImplementedException();
		}

		public GetLastCsEventDateOutput GetLastCsEventDate(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public GetLastMessageDateOutput GetLastMessageDate(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public GetSecurityQuestionsOutput GetSecurityQuestions(Authentication Authentication, GetSecurityQuestionsInput input)
		{
			throw new NotImplementedException();
		}

		public GetSignalStrengthHistoryOutput GetSignalStrengthHistory(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public GetUploadedPanelSettingsOutput GetUploadedPanelSettings(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public InvalidateLoginTokensOutput InvalidateLoginTokens(Authentication Authentication, int customerId, string loginName, string password)
		{
			throw new NotImplementedException();
		}

		public bool IsModemActivated(Authentication Authentication, string modemSerial)
		{
			throw new NotImplementedException();
		}

		public int LookupCustomerId(Authentication Authentication, string receiverPhoneNumber, string acctNumber)
		{
			throw new NotImplementedException();
		}

		public int LookupCustomerIdForDealer(Authentication Authentication, string receiverPhoneNumber, string acctNumber, int dealerId)
		{
			throw new NotImplementedException();
		}

		public int LookupCustomerIdFromDealerCustomerId(Authentication Authentication, string dealerCustomerId)
		{
			throw new NotImplementedException();
		}

		public int LookupCustomerIdFromModemSerial(Authentication Authentication, string modemSerial)
		{
			throw new NotImplementedException();
		}

		public MergeLoginsOutput MergeLogins(Authentication Authentication, MergeLoginsInput input)
		{
			throw new NotImplementedException();
		}

		public MergeLoginsByLoginNameOutput MergeLoginsByLoginName(Authentication Authentication, MergeLoginsByLoginNameInput input)
		{
			throw new NotImplementedException();
		}

		public RemoveNotificationsOutput[] RemoveNotification(Authentication Authentication, int[] customerId, NotificationRemoval input)
		{
			throw new NotImplementedException();
		}

		public RequestFirmwareUpgradeOutput RequestFirmwareUpgrade(Authentication Authentication, int customerId, FirmwareUpgradeEnum newVersion)
		{
			throw new NotImplementedException();
		}

		public RequestRoundTripCsTestOutput RequestRoundTripCsTest(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public bool RequestSensorNames(Authentication Authentication, int customerId, bool waitUntilPanelConnects)
		{
			throw new NotImplementedException();
		}

		public RequestSignalStrengthOutput RequestSignalStrength(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public RequestUpdatedEquipmentListOutput RequestUpdatedEquipmentList(Authentication Authentication, int customerId, int maxZones)
		{
			throw new NotImplementedException();
		}

		public RequestUploadOfPanelSettingsOutput RequestUploadOfPanelSettings(Authentication Authentication, int customerId, int deviceId)
		{
			throw new NotImplementedException();
		}

		public RequestZWaveEquipmentListOutput RequestZWaveEquipmentList(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public ResetCustomerPasswordOutput ResetCustomerPassword(Authentication Authentication, ResetCustomerPasswordInput input)
		{
			throw new NotImplementedException();
		}

		public RestoreBackedUpPanelSettingsOutput RestoreBackedUpPanelSettings(Authentication Authentication, RestoreBackedUpPanelSettingsInput input)
		{
			throw new NotImplementedException();
		}

		public SendEnterpriseNoticeOutput SendEnterpriseNotice(Authentication Authentication, int customerId, int messageId, string[] messageParameters)
		{
			throw new NotImplementedException();
		}

		public SetAddonQuantityOutput SetAddOnQuantity(Authentication Authentication, int customerId, AddOnFeatureEnum addOn, int quantity)
		{
			throw new NotImplementedException();
		}

		public SetAutoPhoneTestSettingsOutput SetAutoPhoneTestSettings(Authentication Authentication, SetAutoPhoneTestSettingsInput input)
		{
			throw new NotImplementedException();
		}

		public SwapModemOutput SwapModem(Authentication Authentication, SwapModemInput input)
		{
			throw new NotImplementedException();
		}

		public bool TerminateCustomer(Authentication Authentication, int customerId)
		{
			throw new NotImplementedException();
		}

		public TerminateModemOutput TerminateModem(Authentication Authentication, string modemSerial)
		{
			throw new NotImplementedException();
		}

		public TransferAccountOutput[] TransferAccounts(Authentication Authentication, TransferAccountInput[] inputs)
		{
			throw new NotImplementedException();
		}

		public TurnOffAccessCodeLockOutput TurnOffAccessCodeLock(Authentication Authentication, int customerId, string installerCode)
		{
			throw new NotImplementedException();
		}

		public UpdateCameraSettingsOutput UpdateCameraSettings(Authentication Authentication, int customerId, string mac, CameraSettings settings)
		{
			throw new NotImplementedException();
		}

		public UpdateCentralStationInfoOutput UpdateCentralStationInfo(Authentication Authentication, int customerId, CentralStationForwardingOptionEnum forwardingOption, bool phoneLinePresent, CentralStationEventGroupEnum[] eventGroupsToForward, string accountNumber, string receiverNumber)
		{
			throw new NotImplementedException();
		}

		public UpdateCustomDevicesOutput UpdateCustomDevices(Authentication Authentication, UpdateCustomDevicesInput input)
		{
			throw new NotImplementedException();
		}

		public UpdateCustomerAddressOutput UpdateCustomerAddress(Authentication Authentication, UpdateCustomerAddressInput input)
		{
			throw new NotImplementedException();
		}

		public UpdateCustomerBranchOutput UpdateCustomerBranch(Authentication Authentication, int customerId, int? destinationBranchId)
		{
			throw new NotImplementedException();
		}

		public UpdateCustomerEnergyProgramEnrollmentOutput UpdateCustomerEnergyProgramEnrollment(Authentication Authentication, UpdateCustomerEnergyProgramEnrollmentInput input)
		{
			throw new NotImplementedException();
		}

		public UpdateCustomerLoginOutput UpdateCustomerLogin(Authentication Authentication, int customerId, string newLogin)
		{
			throw new NotImplementedException();
		}

		public UpdateCustomerPasswordOutput UpdateCustomerPassword(Authentication Authentication, int customerId, string newPassword)
		{
			throw new NotImplementedException();
		}

		public UpdateCustomerSecurityQuestionOutput UpdateCustomerSecurityQuestion(Authentication Authentication, UpdateCustomerSecurityQuestionInput input)
		{
			throw new NotImplementedException();
		}

		public UpdateDealerCustomerIdOutput UpdateDealerCustomerId(Authentication Authentication, int customerId, string dealerCustomerId)
		{
			throw new NotImplementedException();
		}

		public UpdateLoginPasswordOutput UpdateLoginPassword(Authentication Authentication, int customerId, string loginName, string oldPassword, string newPassword)
		{
			throw new NotImplementedException();
		}

		public UpdateNotificationsOutput UpdateNotifications(Authentication Authentication, int customerId, NotificationSubscription[] subscriptions)
		{
			throw new NotImplementedException();
		}

		public UpdatePrimaryEmailOutput UpdatePrimaryEmail(Authentication Authentication, int customerId, string newEmailAddress)
		{
			throw new NotImplementedException();
		}

		public UpdatePrimaryPhoneOutput UpdatePrimaryPhone(Authentication Authentication, int customerId, string newPhone)
		{
			throw new NotImplementedException();
		}

		public UpdateRolesOutput UpdateRoles(Authentication Authentication, UpdateRolesInput input)
		{
			throw new NotImplementedException();
		}

		public UpdateSensorDeviceOutput UpdateSensorDevice(Authentication Authentication, UpdateSensorDeviceInput input)
		{
			throw new NotImplementedException();
		}

		public UpdateUnitDescriptionOutput UpdateUnitDescription(Authentication Authentication, UpdateUnitDescriptionInput input)
		{
			throw new NotImplementedException();
		}

		public UpgradeSAVToSecurityAccountOutput UpgradeSAVToSecurityAccount(Authentication Authentication, UpgradeSAVToSecurityAccountInput input)
		{
			throw new NotImplementedException();
		}

		public ValidateLoginPasswordOutput ValidateLoginPassword(Authentication Authentication, string loginName, string password)
		{
			throw new NotImplementedException();
		}
	}
}
