using System;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.FOS.CellStation.AlarmComWebService;
using SOS.Lib.Core;
using SOS.Lib.Core.ErrorHandling;
using SOS.Lib.Core.ExceptionHandling;

namespace SOS.FOS.CellStation.AlarmCom
{
	public class AlarmComAccount
	{
		#region .ctor
		public AlarmComAccount(MS_IndustryAccount aMsIndAcct, AE_Customer customer, MS_VendorAlarmComAccount vendorAccount, MS_IndustryAccount industryAccount)
			: this(aMsIndAcct.Account, customer, vendorAccount, industryAccount)
		{
		}
		public AlarmComAccount(MS_Account msAccount, AE_Customer customer, MS_VendorAlarmComAccount vendorAccount, MS_IndustryAccount industryAccount)
		{
			MsAccount = msAccount;
			AccountID = msAccount.AccountID;
			McPremise = msAccount.PremiseAddress;
			Customer = customer;
			Username = vendorAccount.Username;
			Password = vendorAccount.Password;
			IndustryAccount = industryAccount;
			if (IndustryAccount != null)
			{
				CsId = IndustryAccount.Csid;
				IndustryAccountID = IndustryAccount.IndustryAccountID;
				ReceiverLineID = IndustryAccount.ReceiverLineId;
				ReceiverLine = IndustryAccount.ReceiverLine;
			}
			// Set important values
			SetCustomerProperties();
		}
		#endregion .ctor

		#region Properties

		public enum EnumServicePackage
		{
			WSF = 1, //Wireless Signal Forwarding (4697)
			BI = 2, //Platinum Interactive (4698)
			AdvancedInteractive = 9,
			CommercialBasicInteractive = 41,
			CommercialAdvancedInteractive = 42,
			HomeCenter = 183,
			ProDealerResi = 184,
			ProDealerCommerical = 185,
			InteractiveGold = 193,
			NotSet
		}

		public string CsId { get; private set; }
		public MS_Account MsAccount { get; private set; }
		public MC_Address McPremise { get; private set; }
		public long AccountID { get; private set; }
		public long IndustryAccountID { get; private set; }
		public MS_IndustryAccount IndustryAccount { get; private set; }
		public string ReceiverLineID { get; private set; }
		public MS_ReceiverLine ReceiverLine { get; private set; }
		public AE_Customer Customer { get; private set; }

		public string Username { get; private set; }
		public string Password { get; private set; }
		public int? CustomerID { get; private set; }
		public string SerialNumber { get; private set; }
		public bool EnableTwoWay { get; set; }
		public string CellPackageItemId { get; set; }
		public EnumServicePackage ServicePackageID { get; set; }
		public PropertyTypeEnum PropertyTypeID { get; set; }

		public string CustomerAccountEmail { get; private set; }
		public string CustomerAccountPhone { get; private set; }

		#endregion // Properties

		#region Public Methods

		public void SetCustomerProperties()
		{
			// Set Customer Phone
			if (string.IsNullOrEmpty(McPremise.Phone) && string.IsNullOrEmpty(Customer.PhoneHome) && string.IsNullOrEmpty(Customer.PhoneMobile) && string.IsNullOrEmpty(Customer.PhoneWork))
				throw new Exception("Alarm.com requires a phone number.  This customer does not have a phone number set.  Please set a number for this customer before registering Unit.");

			CustomerAccountPhone = string.IsNullOrEmpty(Customer.PhoneHome)
				? !string.IsNullOrEmpty(Customer.PhoneMobile)
					? Customer.PhoneMobile
					: string.IsNullOrEmpty(Customer.PhoneWork)
						? McPremise.Phone
						: Customer.PhoneWork
				: Customer.PhoneHome;

			var salesInformationView = SosCrmDataContext.Instance.MS_AccountSalesInformationsViews.Read(AccountID);
			if (salesInformationView == null)
			{
				CustomerAccountEmail = null;
				//ServicePackageID = EnumServicePackage.NotSet;
			}
			else
			{
				// Set CUstomer Email;
				if (string.IsNullOrEmpty(salesInformationView.Email))
				{
					var nxsResult = new Result<bool>
					{
						Code = BaseErrorCodes.ErrorCodes.MSAccountAlarmComMissingEmailAddress.Code(),
						Message = BaseErrorCodes.ErrorCodes.MSAccountAlarmComMissingEmailAddress.Message(),
						Value = false
					};
					throw new NXSResultException<bool>(nxsResult);
				}
				CustomerAccountEmail = salesInformationView.Email;
						ServicePackageID = EnumServicePackage.InteractiveGold;
			}
			PropertyTypeID = PropertyTypeEnum.SingleFamilyHouse;

			var acct = SosCrmDataContext.Instance.MS_Accounts.LoadByPrimaryKey(AccountID);
			CellPackageItemId = acct.CellPackageItemId;
			ServicePackageID = ToEnumServicePackage(acct.CellPackageItemId);

			var lineBlock = SosCrmDataContext.Instance.MS_ReceiverLineBlockAlarmComs.LoadByPrimaryKey(IndustryAccount.ReceiverLineBlockId);
			if (lineBlock == null)
			{
				CustomerID = null;
				SerialNumber = null;
				EnableTwoWay = false;
			}
			else
			{
				CustomerID = lineBlock.CustomerId;
				SerialNumber = lineBlock.SerialNumber;
				EnableTwoWay = lineBlock.IsTwoWay;
			}
		}

		#endregion // Public Methods

		#region Private Methods

		public bool ValidateIndustryNumber(MS_IndustryAccount oCellIndAccount)
		{
			//@TODO: ReceiverLine VendorName
			return false;
			//// Check that this Industry Account supports Alarm.com
			//return oCellIndAccount.ReceiverLine.OSCellVendor.MCellVendor.VendorName.Equals("Alarm.Com");
		}

		#endregion // Private Methods

		public static EnumServicePackage ToEnumServicePackage(string cellPackageItemId)
		{
			EnumServicePackage result = EnumServicePackage.NotSet;
			switch (cellPackageItemId)
			{
				case "CELL_SRV_AC_BI":
					result = EnumServicePackage.CommercialBasicInteractive;
					break;
				case "CELL_SRV_AC_WSF":
					result = EnumServicePackage.WSF; //Wireless Signal Forwarding (4697)
					break;
				//case "":
				//	result = EnumServicePackage.BI; //Platinum Interactive (4698)
				//	break;
				case "CELL_SRV_AC_AI":
					result = EnumServicePackage.AdvancedInteractive;
					break;
				//case "":
				//	result = EnumServicePackage.CommercialBasicInteractive;
				//	break;
				case "CELL_SRV_AC_IG":
					result = EnumServicePackage.InteractiveGold;
					break;
			}
			return result;
		}
		public static string FromEnumServicePackage(EnumServicePackage servicePackage)
		{
			string result = string.Empty;
			switch (servicePackage)
			{
				case EnumServicePackage.CommercialBasicInteractive:
					result = "CELL_SRV_AC_BI";
					break;
				case EnumServicePackage.WSF: //Wireless Signal Forwarding (4697)
					result = "CELL_SRV_AC_WSF";
					break;
				//case EnumServicePackage.BI: //Platinum Interactive (4698)
				//  result = "";
				//  break;
				case EnumServicePackage.AdvancedInteractive:
					result = "CELL_SRV_AC_AI";
					break;
				//case EnumServicePackage.CommercialBasicInteractive:
				//  result = "";
				//  break;
				case EnumServicePackage.InteractiveGold:
					result = "CELL_SRV_AC_IG";
					break;
			}
			return result;
		}

	}
}
