using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;
using SOS.Lib.Util;
using SOS.Data.HumanResource;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeCustomerCardInfo : IFnsAeCustomerCardInfo
	{
		#region .ctor

		public FnsAeCustomerCardInfo(AE_Customer item, QL_CreditReport creditReport, RU_Season season)
		{
			CustomerID = item.CustomerID;
			CustomerTypeId = item.CustomerTypeId;
			CustomerMasterFileID = item.CustomerMasterFileId;
			Prefix = item.Prefix;
			FirstName = item.FirstName;
			MiddleName = item.MiddleName;
			LastName = item.LastName;
			PostFix = item.Postfix;
			FullName = StringUtility.JoinIfNotEmpty(" ", item.Prefix, item.FirstName, item.MiddleName, item.LastName, item.Postfix);
			Gender = item.Gender;
			PhoneHome = item.PhoneHome;
			PhoneWork = item.PhoneWork;
			PhoneMobile = item.PhoneMobile;
			Email = item.Email;
			DOB = item.DOB;
			if (!string.IsNullOrEmpty(item.SSN))
			{
				string ssnRaw;
				if (Lib.Util.Cryptography.TripleDES.DecryptStringTry(item.SSN, out ssnRaw))
				{
					SSN = StringUtility.FormatSsnWithBlanks(ssnRaw);
				}
			}
			Username = item.Username;
			Password = item.Password;

			var address = item.Address;
			AddressID = address.AddressID;
			StreetAddress = address.StreetAddress;
			StreetAddress2 = address.StreetAddress2;
			City = address.City;
			StateId = address.StateId;
			PostalCode = address.PostalCode;
			PlusFour = address.PlusFour;
			//CityStateZip = address.CityStateZip;

			if (creditReport != null && season != null)
			{
				CreditGroup = SOS.Lib.Core.CreditReportService.CreditHelper.GetCreditScoreGroup(creditReport.Score, creditReport.IsHit,
					excellentCreditScore: season.ExcellentCreditScoreThreshold,
					passCreditScore: season.PassCreditScoreThreshold,
					subCreditScore: season.SubCreditScoreThreshold).ToString();
			}

			Latitude = address.Latitude;
			Longitude = address.Longitude;
		}

		#endregion .ctor


		#region Properties
		public long CustomerID { get; private set; }
		public string CustomerTypeId { get; private set; }
		public long CustomerMasterFileID { get; private set; }
		public string Prefix { get; private set; }
		public string FirstName { get; private set; }
		public string MiddleName { get; private set; }
		public string LastName { get; private set; }
		public string PostFix { get; private set; }
		public string FullName { get; private set; }
		public string Gender { get; private set; }
		public string PhoneHome { get; private set; }
		public string PhoneWork { get; private set; }
		public string PhoneMobile { get; private set; }
		public string Email { get; private set; }
		public DateTime? DOB { get; private set; }
		public string SSN { get; private set; }
		public string Username { get; private set; }
		public string Password { get; private set; }
		public long AddressID { get; private set; }
		public string StreetAddress { get; private set; }
		public string StreetAddress2 { get; private set; }
		public string City { get; private set; }
		public string StateId { get; private set; }
		public string PostalCode { get; private set; }
		public string PlusFour { get; private set; }
		public string CityStateZip { get; private set; }
		public string CreditGroup { get; private set; }
		public double Latitude { get; private set; }
		public double Longitude { get; private set; }
		#endregion Properties
	}
}
