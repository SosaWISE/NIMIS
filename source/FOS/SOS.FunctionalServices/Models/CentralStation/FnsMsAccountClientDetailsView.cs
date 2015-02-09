using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountClientDetailsView : IFnsMsAccountClientDetailsView
	{
		public FnsMsAccountClientDetailsView(MS_AccountClientDetailsView oResulItem)
		{
			AccountCustomerID = oResulItem.AccountCustomerID;
			CustomerId = oResulItem.CustomerId;
			AccountId = oResulItem.AccountId;
			CustomerTypeId = oResulItem.CustomerTypeId;
			CustomerMasterFileId = oResulItem.CustomerMasterFileId;
			DealerId = oResulItem.DealerId;
			AddressId = oResulItem.AddressId;
			StreetAddress = oResulItem.StreetAddress;
			StreetAddress2 = oResulItem.StreetAddress2;
			City = oResulItem.City;
			StateId = oResulItem.StateId;
			PostalCode = oResulItem.PostalCode;
			PlusFour = oResulItem.PlusFour;
			County = oResulItem.County;
			CountryId = oResulItem.CountryId;
			LeadId = oResulItem.LeadId;
			LocalizationId = oResulItem.LocalizationId;
			Prefix = oResulItem.Prefix;
			FirstName = oResulItem.FirstName;
			MiddleName = oResulItem.MiddleName;
			LastName = oResulItem.LastName;
			Postfix = oResulItem.Postfix;
			Gender = oResulItem.Gender;
			PhoneHome = oResulItem.PhoneHome;
			PhoneWork = oResulItem.PhoneWork;
			PhoneMobile = oResulItem.PhoneMobile;
			Email = oResulItem.Email;
			DOB = oResulItem.DOB;
			SSN = oResulItem.SSN;
			Username = oResulItem.Username;
			Password = oResulItem.Password;
			CustomerIsActive = oResulItem.CustomerIsActive;
			IndustryAccountId = oResulItem.IndustryAccountId;
			SystemTypeId = oResulItem.SystemTypeId;
			CellularTypeId = oResulItem.CellularTypeId;
			PanelTypeId = oResulItem.PanelTypeId;
			SimProductBarcodeId = oResulItem.SimProductBarcodeId;
			//GpsWatchProductBarcodeId = oResulItem.GpsWatchProductBarcodeId;
			//GpsWatchPhoneNumber = oResulItem.GpsWatchPhoneNumber;
			//GpsWatchUnitID = oResulItem.GpsWatchUnitID;
			MsAccountIsActive = oResulItem.MsAccountIsActive;
			IndustryNumber = oResulItem.IndustryAccount;
			Designator = oResulItem.Designator;
			SubscriberNumber = oResulItem.SubscriberNumber;
		}

		public long AccountCustomerID { get; set; }
		public long CustomerId { get; set; }
		public long AccountId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public long AddressId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string County { get; set; }
		public string CountryId { get; set; }
		public long LeadId { get; set; }
		public string LocalizationId { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Postfix { get; set; }
		public string Gender { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string Email { get; set; }
		public DateTime? DOB { get; set; }
		public string SSN { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool CustomerIsActive { get; set; }
		public long? IndustryAccountId { get; set; }
		public string SystemTypeId { get; set; }
		public string CellularTypeId { get; set; }
		public string PanelTypeId { get; set; }
		public string SimProductBarcodeId { get; set; }
		//public string GpsWatchProductBarcodeId { get; set; }
		//public string GpsWatchPhoneNumber { get; set; }
		//public string GpsWatchUnitID { get; set; }
		public bool MsAccountIsActive { get; set; }
		public string IndustryNumber { get; set; }
		public string Designator { get; set; }
		public string SubscriberNumber { get; set; }
	}
}
