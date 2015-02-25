using System;
using SOS.Data.SosCrm;
using SOS.FOS.CellStation.AlarmComWebService;

namespace SOS.FOS.CellStation.AlarmCom
{
	public class AlarmComAccountStatus
	{

		#region Properties
		public string CellPackageItemId { get; set; }
		// other properties
		public int CustomerId { get; set; }
		public bool EnableTwoWay { get; set; }
		public string FirstName { get; private set; }
		public string LastName { get; private set; }
		public string ServicePlanType { get; private set; }
		public decimal? ServicePlanTotalPrice { get; private set; }
		public int? ServicePlanPackageId { get; private set; }
		public int? ModemFirmwareVersion { get; private set; }
		public string ModemSerial { get; private set; }
		public string CompanyName { get; private set; }
		public string PanelNotRespondingStatus { get; private set; }
		public string PrimaryPhoneNumber { get; private set; }
		public string PrimaryEmail { get; private set; }
		public DateTime JoinDate { get; private set; }
		public bool IsTerminated { get; private set; }
		public bool IsDemo { get; private set; }
		public string InstallAddress { get; private set; }
		public CellularAccountStatuses RegistrationStatus { get; set; }
		public string Message { get; set; }
		#endregion Properties

		#region Methods

		/// <summary>
		/// Given a CustomerInfo object it will populate the properties of the class.
		/// </summary>
		/// <param name="info">CustomerInfo</param>
		/// <param name="alarmComAccount"></param>
		public void SetCustInfo(CustomerInfo info, AlarmComAccount alarmComAccount)
		{
			CellPackageItemId = alarmComAccount.MsAccount.CellPackageItemId;

			CustomerId = info.CustomerId;
			FirstName = info.FirstName;
			LastName = info.LastName;
			InstallAddress = info.InstallAddress.Street1;
			IsDemo = info.IsDemo;
			IsTerminated = info.IsTerminated;
			RegistrationStatus = info.IsTerminated ? CellularAccountStatuses.Unregistered : CellularAccountStatuses.Registered;
			JoinDate = info.JoinDate;
			PrimaryEmail = info.PrimaryEmail;
			PrimaryPhoneNumber = info.PrimaryPhoneNumber;
			PanelNotRespondingStatus = info.PanelNotRespondingStatus.ToString();
			CompanyName = info.CompanyName;

			if (info.ModemInfo == null)
			{
				ModemSerial = alarmComAccount.SerialNumber;
				//ModemSerial = null; //???
				ModemFirmwareVersion = null;
			}
			else
			{
				ModemSerial = info.ModemInfo.ModemSerial;
				ModemFirmwareVersion = info.ModemInfo.FirmwareVersion == 0 ? (int?)null : info.ModemInfo.FirmwareVersion;
			}

			if (info.ServicePlanInfo == null)
			{
				ServicePlanPackageId = null;
				ServicePlanType = null;
				ServicePlanTotalPrice = null;
			}
			else
			{
				ServicePlanPackageId = info.ServicePlanInfo.PackageId == 0 ? (int?)null : info.ServicePlanInfo.PackageId;
				ServicePlanType = info.ServicePlanInfo.PlanType.ToString();
				ServicePlanTotalPrice = info.ServicePlanInfo.TotalServicePrice == 0 ? (decimal?)null : info.ServicePlanInfo.TotalServicePrice;
			}

			EnableTwoWay = alarmComAccount.EnableTwoWay;
		}

		public void SaveAccountInfo(CustomerInfo info, AlarmComAccount alarmComAccount)
		{
			var lineBlock = SosCrmDataContext.Instance.MS_ReceiverLineBlockAlarmComs.LoadByPrimaryKey(alarmComAccount.IndustryAccount.ReceiverLineBlockId);

			// Check if it is loaded
			if (lineBlock != null && !lineBlock.IsLoaded)
			{
				lineBlock = new MS_ReceiverLineBlockAlarmCom { ReceiverLineBlockID = alarmComAccount.IndustryAccount.ReceiverLineBlockId };
			}

			// Save Changes
			lineBlock.CustomerId = info.CustomerId;
			if (info.ModemInfo != null)
			{
				lineBlock.SerialNumber = info.ModemInfo.ModemSerial;
			}

			DateTime? tempDate = info.JoinDate;

			if (info.JoinDate != null && info.JoinDate.Year < 2005)
			{
				tempDate = null;
			}

			lineBlock.RegisteredDate = tempDate;
			if (info.IsTerminated)
			{
				if (lineBlock.UnRegisteredDate == null)
				{
					lineBlock.UnRegisteredDate = DateTime.Now;
				}
			}
			else
			{
				lineBlock.UnRegisteredDate = null;
			}
			lineBlock.Save();
		}

		#endregion Methods
	}
}