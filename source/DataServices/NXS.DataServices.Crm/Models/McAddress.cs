using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class McAddress
	{
		public long AddressID { get; set; }
		public long? QlAddressId { get; set; }
		public int DealerId { get; set; }
		public string ValidationVendorId { get; set; }
		public string AddressValidationStateId { get; set; }
		public string StateId { get; set; }
		public string CountryId { get; set; }
		public int TimeZoneId { get; set; }
		public string AddressTypeId { get; set; }
		public string StreetAddress { get; set; }
		public string StreetAddress2 { get; set; }
		public string StreetNumber { get; set; }
		public string StreetName { get; set; }
		public string StreetType { get; set; }
		public string PreDirectional { get; set; }
		public string PostDirectional { get; set; }
		public string Extension { get; set; }
		public string ExtensionNumber { get; set; }
		public string County { get; set; }
		public string CountyCode { get; set; }
		public string Urbanization { get; set; }
		public string UrbanizationCode { get; set; }
		public string City { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string Phone { get; set; }
		public string DeliveryPoint { get; set; }
		public string CrossStreet { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public int? CongressionalDistric { get; set; }
		public bool DPV { get; set; }
		public string DPVResponse { get; set; }
		public string DPVFootnote { get; set; } // public string DPVFootNote { get; set; }
		public string CarrierRoute { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }

		internal static McAddress FromDb(MC_Address item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("address is null");
			}

			var result = new McAddress();
			result.AddressID = item.AddressID;
			result.QlAddressId = item.QlAddressId;
			result.DealerId = item.DealerId;
			result.ValidationVendorId = item.ValidationVendorId;
			result.AddressValidationStateId = item.AddressValidationStateId;
			result.StateId = item.StateId;
			result.CountryId = item.CountryId;
			result.TimeZoneId = item.TimeZoneId;
			result.AddressTypeId = item.AddressTypeId;
			result.StreetAddress = item.StreetAddress;
			result.StreetAddress2 = item.StreetAddress2;
			result.StreetNumber = item.StreetNumber;
			result.StreetName = item.StreetName;
			result.StreetType = item.StreetType;
			result.PreDirectional = item.PreDirectional;
			result.PostDirectional = item.PostDirectional;
			result.Extension = item.Extension;
			result.ExtensionNumber = item.ExtensionNumber;
			result.County = item.County;
			result.CountyCode = item.CountyCode;
			result.Urbanization = item.Urbanization;
			result.UrbanizationCode = item.UrbanizationCode;
			result.City = item.City;
			result.PostalCode = item.PostalCode;
			result.PlusFour = item.PlusFour;
			result.Phone = item.Phone;
			result.DeliveryPoint = item.DeliveryPoint;
			result.CrossStreet = item.CrossStreet;
			result.Latitude = item.Latitude;
			result.Longitude = item.Longitude;
			result.CongressionalDistric = item.CongressionalDistric;
			result.DPV = item.DPV;
			result.DPVResponse = item.DPVResponse;
			result.DPVFootnote = item.DPVFootNote;
			result.CarrierRoute = item.CarrierRoute;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.CreatedBy = item.CreatedBy;
			result.CreatedOn = item.CreatedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.ModifiedOn = item.ModifiedOn;

			return result;
		}
	}
}
