


using System;
using System.Data;
using SubSonic;
using SubSonic.Utilities;

namespace NXS.Data.Licensing {
	public partial class LicensingDataStoredProcedureManager {
		public static StoredProcedure GetMasterLicensingDataSet(int? LocationID) {
			StoredProcedure sp = new StoredProcedure("custGetMasterLicensingDataSet" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@LocationID", LocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure GetRequirementsMetAndNeeded(string GPEmployeeID,int? RequirementTypeID,string LocationName) {
			StoredProcedure sp = new StoredProcedure("custGetRequirementsMetAndNeeded" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			sp.Command.AddParameter("@RequirementTypeID", RequirementTypeID, DbType.Int32);
			sp.Command.AddParameter("@LocationName", LocationName, DbType.String);
			return sp;
		}
		public static StoredProcedure LM_AgencyGetAgenciesByLocationID(int? LocationID) {
			StoredProcedure sp = new StoredProcedure("custLM_AgencyGetAgenciesByLocationID" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@LocationID", LocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_GetPertainingCustomerLicenses(string CountryName,string StateName,string CountyName,string CityName,string TownshipName,string GPRepID,string GPTechID,int? AccountID) {
			StoredProcedure sp = new StoredProcedure("custLM_GetPertainingCustomerLicenses" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CountryName", CountryName, DbType.String);
			sp.Command.AddParameter("@StateName", StateName, DbType.String);
			sp.Command.AddParameter("@CountyName", CountyName, DbType.String);
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			sp.Command.AddParameter("@TownshipName", TownshipName, DbType.String);
			sp.Command.AddParameter("@GPRepID", GPRepID, DbType.String);
			sp.Command.AddParameter("@GPTechID", GPTechID, DbType.String);
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_LicenseGetCompanyLicensesByLocationID(int? LocationID) {
			StoredProcedure sp = new StoredProcedure("custLM_LicenseGetCompanyLicensesByLocationID" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@LocationID", LocationID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_LicenseGetExpiringLicenses() {
			StoredProcedure sp = new StoredProcedure("custLM_LicenseGetExpiringLicenses" ,DataService.GetInstance("NxsLicensingProvider"));
			return sp;
		}
		public static StoredProcedure LM_LicenseGetLicensesByLocationIDAndRequirementTypeID(int? LocationID,int? RequirementTypeID) {
			StoredProcedure sp = new StoredProcedure("custLM_LicenseGetLicensesByLocationIDAndRequirementTypeID" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@LocationID", LocationID, DbType.Int32);
			sp.Command.AddParameter("@RequirementTypeID", RequirementTypeID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_LicensesGetTechCertifiedCount(int? ServiceTechID,int? RequirementID) {
			StoredProcedure sp = new StoredProcedure("custLM_LicensesGetTechCertifiedCount" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@ServiceTechID", ServiceTechID, DbType.Int32);
			sp.Command.AddParameter("@RequirementID", RequirementID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_LocationsLoadActiveLocations() {
			StoredProcedure sp = new StoredProcedure("custLM_LocationsLoadActiveLocations" ,DataService.GetInstance("NxsLicensingProvider"));
			return sp;
		}
		public static StoredProcedure LM_RequirementGetIncompleteCompanyLicensing() {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementGetIncompleteCompanyLicensing" ,DataService.GetInstance("NxsLicensingProvider"));
			return sp;
		}
		public static StoredProcedure LM_RequirementGetIncompleteCustomerLicensing() {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementGetIncompleteCustomerLicensing" ,DataService.GetInstance("NxsLicensingProvider"));
			return sp;
		}
		public static StoredProcedure LM_RequirementGetIncompleteSalesRepLicensing(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementGetIncompleteSalesRepLicensing" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_RequirementGetIncompleteTechLicensing(int? SeasonID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementGetIncompleteTechLicensing" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@SeasonID", SeasonID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_RequirementGetRequirementByLocationIDAndRequirementTypeID(int? LocationID,int? RequirementTypeID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementGetRequirementByLocationIDAndRequirementTypeID" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@LocationID", LocationID, DbType.Int32);
			sp.Command.AddParameter("@RequirementTypeID", RequirementTypeID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetCompanyLicenseByCityName(string CityName) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetCompanyLicenseByCityName" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetCompanyLicenseByLocation(string CountryName,string StateName,string CountyName,string CityName,string TownshipName) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetCompanyLicenseByLocation" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CountryName", CountryName, DbType.String);
			sp.Command.AddParameter("@StateName", StateName, DbType.String);
			sp.Command.AddParameter("@CountyName", CountyName, DbType.String);
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			sp.Command.AddParameter("@TownshipName", TownshipName, DbType.String);
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetCustomerLicenseByCityName(string CityName,int? AccountID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetCustomerLicenseByCityName" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetCustomerLicenseByLocation(string CountryName,string StateName,string CountyName,string CityName,string TownshipName,int? AccountID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetCustomerLicenseByLocation" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CountryName", CountryName, DbType.String);
			sp.Command.AddParameter("@StateName", StateName, DbType.String);
			sp.Command.AddParameter("@CountyName", CountyName, DbType.String);
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			sp.Command.AddParameter("@TownshipName", TownshipName, DbType.String);
			sp.Command.AddParameter("@AccountID", AccountID, DbType.Int32);
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetCustomerPermitLetterData() {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetCustomerPermitLetterData" ,DataService.GetInstance("NxsLicensingProvider"));
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetRepLicenseByCityName(string CityName,string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetRepLicenseByCityName" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetRepLicenseByLocation(string CountryName,string StateName,string CountyName,string CityName,string TownshipName,string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetRepLicenseByLocation" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CountryName", CountryName, DbType.String);
			sp.Command.AddParameter("@StateName", StateName, DbType.String);
			sp.Command.AddParameter("@CountyName", CountyName, DbType.String);
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			sp.Command.AddParameter("@TownshipName", TownshipName, DbType.String);
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetTechLicenseByCityName(string CityName,string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetTechLicenseByCityName" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure LM_RequirementsGetTechLicenseByLocation(string CountryName,string StateName,string CountyName,string CityName,string TownshipName,string GPEmployeeID) {
			StoredProcedure sp = new StoredProcedure("custLM_RequirementsGetTechLicenseByLocation" ,DataService.GetInstance("NxsLicensingProvider"));
			sp.Command.AddParameter("@CountryName", CountryName, DbType.String);
			sp.Command.AddParameter("@StateName", StateName, DbType.String);
			sp.Command.AddParameter("@CountyName", CountyName, DbType.String);
			sp.Command.AddParameter("@CityName", CityName, DbType.String);
			sp.Command.AddParameter("@TownshipName", TownshipName, DbType.String);
			sp.Command.AddParameter("@GPEmployeeID", GPEmployeeID, DbType.String);
			return sp;
		}
		public static StoredProcedure LM_RequirmentsGetCustomerPermitLetterData() {
			StoredProcedure sp = new StoredProcedure("custLM_RequirmentsGetCustomerPermitLetterData" ,DataService.GetInstance("NxsLicensingProvider"));
			return sp;
		}
	}
}
 
