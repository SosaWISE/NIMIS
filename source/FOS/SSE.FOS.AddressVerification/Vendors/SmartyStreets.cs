using Newtonsoft.Json;
using NXS.Lib;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SSE.FOS.AddressVerification.Interfaces;
using SSE.FOS.AddressVerification.Models;
using SSE.Lib.Interfaces.FOS;
using SSE.Lib.Interfaces.Helpers;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Web;

namespace SSE.FOS.AddressVerification.Vendors
{
	public class SmartyStreets : IVendor
	{
		#region .ctor

		public SmartyStreets()
		{
			ApiUrl = WebConfig.Instance.GetConfig("AddressVerification_SmartyStreetApiUrl");
			AuthenticationID = WebConfig.Instance.GetConfig("AddressVerification_SmartySAuthID");
			AuthenticationToken = WebConfig.Instance.GetConfig("AddressVerification_SmartySAuthToken");
		}

		#endregion .ctor

		#region Supporting Classes

		[DataContract]
		public class CandidateAddress
		{
			[DataMember(Name = "input_index")]
			public int InputIndex { get; set; }

			[DataMember(Name = "candidate_index")]
			public int CandidateIndex { get; set; }

			[DataMember(Name = "delivery_line_1")]
			public string DeliveryLine1 { get; set; }

			[DataMember(Name = "delivery_line_2")]
			public string DeliveryLine2 { get; set; }

			[DataMember(Name = "last_line")]
			public string LastLine { get; set; }

			[DataMember(Name = "delivery_point_barcode")]
			public string DeliveryPointBarcode { get; set; }

			[DataMember(Name = "components")]
			public Components Components { get; set; }

			[DataMember(Name = "metadata")]
			public Metadata Metadata { get; set; }

			[DataMember(Name = "analysis")]
			public Analysis Analysis { get; set; }
		}

		[DataContract]
		public class Components
		{
			[DataMember(Name = "primary_number")]
			public string PrimaryNumber { get; set; }

			[DataMember(Name = "street_name")]
			public string StreetName { get; set; }

			[DataMember(Name = "street_predirection")]
			public string StreetPredirection { get; set; }

			[DataMember(Name = "street_postdirection")]
			public string StreetPostdirection { get; set; }

			[DataMember(Name = "street_suffix")]
			public string StreetSuffix { get; set; }

			[DataMember(Name = "secondary_number")]
			public string SecondaryNumber { get; set; }

			[DataMember(Name = "secondary_designator")]
			public string SecondaryDesignator { get; set; }

			[DataMember(Name = "extra_secondary_designator")]
			public string ExtraSecondaryDesignator { get; set; }

			[DataMember(Name = "extra_secondary_number")]
			public string ExtraSecondaryNumber { get; set; }

			[DataMember(Name = "pmb_number")]
			public string PmbNumber { get; set; }

			[DataMember(Name = "pmb_designator")]
			public string PmbDesignator { get; set; }

			[DataMember(Name = "city_name")]
			public string CityName { get; set; }

			[DataMember(Name = "state_abbreviation")]
			public string StateAbbreviation { get; set; }

			[DataMember(Name = "zipcode")]
			public string Zipcode { get; set; }

			[DataMember(Name = "plus4_code")]
			public string Plus4Code { get; set; }

			[DataMember(Name = "delivery_point")]
			public string DeliveryPoint { get; set; }

			[DataMember(Name = "delivery_point_check_digit")]
			public string DeliveryPointCheckDigit { get; set; }

			[DataMember(Name = "urbanization")]
			public string Urbanization { get; set; }
		}

		[DataContract]
		public class Metadata
		{
			[DataMember(Name = "record_type")]
			public string RecordType { get; set; }

			[DataMember(Name = "county_fips")]
			public string CountyFips { get; set; }

			[DataMember(Name = "county_name")]
			public string CountyName { get; set; }

			[DataMember(Name = "carrier_route")]
			public string CarrierRoute { get; set; }

			[DataMember(Name = "congressional_district")]
			public string CongressionalDistrict { get; set; }

			[DataMember(Name = "building_default_indicator")]
			public string BuildingDefaultIndicator { get; set; }

			[DataMember(Name = "rdi")]
			public string RedidentialDeliveryIndicator { get; set; }

			[DataMember(Name = "latitude")]
			public string Latitude { get; set; }

			[DataMember(Name = "longitude")]
			public string Longitude { get; set; }

			[DataMember(Name = "precision")]
			public string Precision { get; set; }
		}

		[DataContract]
		public class Analysis
		{
			[DataMember(Name = "dpv_match_code")]
			public string DpvMatchCode { get; set; }

			[DataMember(Name = "dpv_footnotes")]
			public string DpvFootnotes { get; set; }

			[DataMember(Name = "dpv_cmra")]
			public string DpvCmraCode { get; set; }

			[DataMember(Name = "dpv_vacant")]
			public string DpvVacantCode { get; set; }

			[DataMember(Name = "active")]
			public string Active { get; set; }

			[DataMember(Name = "ews_match")]
			public bool EwsMatch { get; set; }

			[DataMember(Name = "footnotes")]
			public string Footnotes { get; set; }

			[DataMember(Name = "lacslink_code")]
			public string LacsLinkCode { get; set; }

			[DataMember(Name = "lacslink_indicator")]
			public string LacsLinkIndicator { get; set; }
		}

		#endregion Supporting Classes

		#region Properties

		public readonly string ApiUrl;
		public readonly string AuthenticationID;
		public readonly string AuthenticationToken;

		#endregion Properties

		#region Methods

		public IFosAVResult<IFosAddressVerified> VerifyAddress(IFosQlAddress address, int nAreaCode, int dealerId, int seasonId, int teamLocationId, string salesRepId, string userId)
		{
			#region Initialize
	
			// ** Initialized.
			var caServices = new WebClient();
			var addressData = new QL_Address
			{
				AddressTypeId = MC_AddressType.MetaData.Non_StandardID,
				AddressValidationStateId = MC_AddressValidationState.MetaData.UnverifiedID,
				ValidationVendorId = MC_AddressValidationVendor.MetaData.Smarty_StreetID,
				SeasonId = seasonId,
				TeamLocationId = teamLocationId,
				SalesRepId = salesRepId,
				DealerId = dealerId,
				StreetAddress = address.AddressLine1,
				StreetAddress2 = address.AddressLine2,
				City = !string.IsNullOrEmpty(address.City)
					? address.City
					: string.Empty,
				StateId = !string.IsNullOrEmpty(address.StateId)
					? address.StateId
					: "00",
				PostalCode = !string.IsNullOrEmpty(address.PostalCode)
					? address.PostalCode
					: string.Empty,
				County = !string.IsNullOrEmpty(address.County)
					? address.County
					: string.Empty,
				StreetNumber = !string.IsNullOrEmpty(address.StreetNumber)
					? address.StreetNumber
					: null,
				StreetName = !string.IsNullOrEmpty(address.StreetName)
					? address.StreetName
					: null,
				PreDirectional = address.PreDirectional,
				PostDirectional = address.PostDirectional,
				StreetType = address.StreetType,
				Extension = address.Extension,
				ExtensionNumber = address.ExtensionNumber,
				CarrierRoute = address.CarrierRoute,
				DPVResponse = address.DPVResponse,
				CountryId = MC_PoliticalCountry.MetaData.No_CountryID
			};
			addressData.Save(userId);

			#endregion Initialize

			#region Parameter Validation

			// ** Parameter validation
			var argList = new List<ArgValidation>
				{
					new ArgValidation(address.AddressLine1, (string.IsNullOrEmpty(address.AddressLine1))
						, "<li>'AddressLine1' is empty and should have a street address.</li>"),
					new ArgValidation(address.PostalCode, (string.IsNullOrEmpty(address.PostalCode))
						, "<li>'PostalCode is a required field'</li>")
				};
			IFosResult<IFosAddressVerified> tempResult;
			if (!ArgValidation.ValidateArguments(argList, out tempResult)) { return FosAVResult<IFosAddressVerified>.GetFromBase(tempResult); }

			#endregion Parameter Validation

			#region Execute 
			var url = ApiUrl +
				"?auth-id=" + AuthenticationID +
				"&auth-token=" + AuthenticationToken +
				"&street=" + HttpUtility.UrlEncode(address.AddressLine1) +
				"&city=" + HttpUtility.UrlEncode(address.City) +
				"&state=" + HttpUtility.UrlEncode(address.StateId) +
				"&zipCode=" + HttpUtility.UrlEncode(address.PostalCode);

			var restResponse = caServices.DownloadString(url);
			var candidates = JsonConvert.DeserializeObject<CandidateAddress[]>(restResponse);

			#endregion Execute 

			/** Check that there is a valid address returned. */
			IFosAddressVerified resultValue = null;
			if (candidates.Length > 0)
			{
				#region Bind Data

				BindData(addressData, address.Phone, candidates, userId);

				#endregion Bind Data

				#region Bind TimeZone by State

				resultValue = Main.BindTimeZone(addressData, nAreaCode, userId);

				#endregion Bind TimeZone by State
			}

			// ** Get results
			FosAVResult<IFosAddressVerified> result = addressData.AddressTypeId.Equals(MC_AddressType.MetaData.Standard_AddressID) 
				                     ? new FosAVResult<IFosAddressVerified>(ResultCodes.Success, "Success") {Value = resultValue} 
				                     : new FosAVResult<IFosAddressVerified>(ResultCodes.AddressValidationError, "Non standard address");

			// ** Return result.
			return result;

		}

		private static void BindData(QL_Address addressData, string phone, CandidateAddress[] candidates, string userId)
		{
			// ** Initialize 
			var response = candidates[0];

			addressData.StreetNumber = response.Components.PrimaryNumber;
			addressData.StreetName = response.Components.StreetName;
			if (!string.IsNullOrEmpty(response.Components.StreetSuffix))
			{
				#region StreetType
				switch (response.Components.StreetSuffix.ToUpper())
				{
					//case "AVE":
					//    addressData.StreetType = "AV";
					//    break;
					//case "BLVD":
					//    addressData.StreetType = "BV";
					//    break;
					//case "CIR":
					//    addressData.StreetType = "CI";
					//    break;
					//case "LOOP":
					//    addressData.StreetType = "LP";
					//    break;
					//case "PKWY":
					//    addressData.StreetType = "PY";
					//    break;
					//case "TRL":
					//    addressData.StreetType = "TR";
					//    break;
					//case "WAY":
					//    addressData.StreetType = "WY";
					//    break;
					case "ALLEE":
					case "ALLEY":
					case "ALLY":
					case "ALY":
					case "AL":
						addressData.StreetType = "AL";
						break;
					case "AV":
					case "AVE":
					case "AVEN":
					case "AVENU":
					case "AVENUE":
					case "AVN":
					case "AVNUE":
						addressData.StreetType = "AV";
						break;
					case "BLVD":
					case "BOUL":
					case "BOULEVARD":
					case "BOULV":
					case "BV":
						addressData.StreetType = "BV";
						break;

					case "BD":
					case "BUILD":
					case "BUILDING":
						addressData.StreetType = "BD";
						break;

					case "CEN":
					case "CENT":
					case "CENTER":
					case "CENTR":
					case "CENTRE":
					case "CNTER":
					case "CNTR":
					case "CTR":
					case "CENTERS":
					case "CN":
						addressData.StreetType = "CN";
						break;

					case "CI":
					case "CIR":
					case "CIRC":
					case "CIRCL":
					case "CIRCLE":
					case "CRCL":
					case "CRCLE":
					case "CIRCLES":
						addressData.StreetType = "CI";
						break;

					case "COURT":
					case "CRT":
					case "CT":
					case "COURTS":
						addressData.StreetType = "CT";
						break;

					case "CS":
					case "CRECENT":
					case "CRES":
					case "CRESCENT":
					case "CRESENT":
					case "CRSCNT":
					case "CRSENT":
					case "CRSNT":
						addressData.StreetType = "CS";
						break;

					case "DALE":
					case "DA":
						addressData.StreetType = "DA";
						break;

					case "DR":
					case "DRIV":
					case "DRIVE":
					case "DRV":
						addressData.StreetType = "DR";
						break;

					case "EX":
					case "EXP":
					case "EXPR":
					case "EXPRESS":
					case "EXPY":
					case "EXPW":
						addressData.StreetType = "EX";
						break;

					case "FREEWAY":
					case "FREEWY":
					case "FRWAY":
					case "FRWY":
					case "FWY":
					case "FY":
						addressData.StreetType = "FY";
						break;

					case "GARDEN":
					case "GDN":
					case "GARDN":
					case "GRDEN":
					case "GRDN":
					case "GDNS":
					case "GRDNS":
					case "GA":
						addressData.StreetType = "GA";
						break;

					case "GROVE":
					case "GROV":
					case "GRV":
					case "GROVES":
					case "GR":
						addressData.StreetType = "GR";
						break;

					case "HEIGHTS":
					case "HEIGHT":
					case "HTS":
					case "HGTS":
					case "HT":
						addressData.StreetType = "HT";
						break;

					case "HIGHWY":
					case "HIWAY":
					case "HIWY":
					case "HWAY":
					case "HWY":
					case "HY":
					case "HIGHWAY":
						addressData.StreetType = "HY";
						break;

					case "HILL":
					case "HL":
					case "HI":
						addressData.StreetType = "HI";
						break;

					case "KNOLL":
					case "KNL":
					case "KNOL":
					case "KNLS":
					case "KNOLLS":
					case "KN":
						addressData.StreetType = "KN";
						break;

					case "LANE":
					case "LA":
					case "LN":
					case "LANES":
						addressData.StreetType = "LN";
						break;

					case "LOOP":
					case "LOOPS":
					case "LP":
						addressData.StreetType = "LP";
						break;

					case "MALL":
					case "MA":
						addressData.StreetType = "MA";
						break;

					case "OVAL":
					case "OVL":
					case "OV":
						addressData.StreetType = "OV";
						break;

					case "PARK":
					case "PARKS":
					case "PRK":
					case "PK":
						addressData.StreetType = "PK";
						break;

					case "PARKWAY":
					case "PARKWY":
					case "PKWAY":
					case "PKWY":
					case "PKY":
					case "PY":
					case "PARKWAYS":
					case "PKWYS":
						addressData.StreetType = "PY";
						break;

					case "PATH":
					case "PATHS":
					case "PA":
						addressData.StreetType = "PA";
						break;

					case "PIKE":
					case "PIKES":
					case "PI":
						addressData.StreetType = "PI";
						break;

					case "PLACE":
					case "PL":
						addressData.StreetType = "PL";
						break;

					case "PLAZA":
					case "PLZA":
					case "PLZ":
					case "PZ":
						addressData.StreetType = "PZ";
						break;

					case "POINT":
					case "PT":
					case "POINTS":
					case "PTS":
						addressData.StreetType = "PT";
						break;

					case "RD":
					case "ROAD":
					case "RDS":
					case "ROADS":
						addressData.StreetType = "RD";
						break;

					case "ROUTE":
					case "RT":
						addressData.StreetType = "RT";
						break;

					case "ROW":
					case "RO":
						addressData.StreetType = "RO";
						break;

					case "RUN":
					case "RN":
						addressData.StreetType = "RN";
						break;

					case "RURALROUTE":
					case "RR":
						addressData.StreetType = "RR";
						break;

					case "SQUARE":
					case "SQ":
					case "SQR":
					case "SQRE":
					case "SQU":
					case "SQRS":
					case "SQUARES":
						addressData.StreetType = "SQ";
						break;

					case "STREET":
					case "STR":
					case "ST":
						addressData.StreetType = "ST";
						break;

					case "TC":
					case "TER":
					case "TERR":
					case "TERRACE":
						addressData.StreetType = "TC";
						break;

					case "THRUWAY":
					case "THROUGHWAY":
					case "TRWY":
					case "TY":
						addressData.StreetType = "TY";
						break;

					case "TRAIL":
					case "TR":
					case "TRL":
					case "TRAILS":
					case "TRLS":
						addressData.StreetType = "TR";
						break;

					case "TURNPIKE":
					case "TPK":
					case "TPKE":
					case "TRNPK":
					case "TRPK":
					case "TURNPK":
					case "TP":
						addressData.StreetType = "TP";
						break;

					case "VIADUCT":
					case "VDCT":
					case "VIA":
					case "VIADCT":
					case "VI":
						addressData.StreetType = "VI";
						break;

					case "VIEW":
					case "VW":
					case "VIEWS":
					case "VWS":
						addressData.StreetType = "VW";
						break;

					case "WALK":
					case "WALKS":
					case "WK":
						addressData.StreetType = "WK";
						break;

					case "WAY":
					case "WY":
						addressData.StreetType = "WY";
						break;
					default:
						addressData.StreetType = null;
						break;
				}
				#endregion StreetType
			}
			else
				addressData.StreetType = null;
			
			Console.WriteLine("This is a break point.");
			addressData.PreDirectional = !string.IsNullOrEmpty(response.Components.StreetPredirection)
				? response.Components.StreetPredirection
				: null;
			addressData.PostDirectional = !string.IsNullOrEmpty(response.Components.StreetPostdirection)
				? response.Components.StreetPostdirection
				: null;
			addressData.DPV = response.Analysis.DpvCmraCode != null && !response.Analysis.DpvCmraCode.Equals("N");
			addressData.DPVResponse = response.Analysis == null ? null : response.Analysis.DpvMatchCode;
			addressData.DPV = response.Analysis != null && response.Analysis.DpvMatchCode.Equals("Y");
			addressData.DPVFootnote = response.Analysis == null ? null : response.Analysis.DpvFootnotes;
			addressData.Extension = !string.IsNullOrEmpty(response.Components.ExtraSecondaryDesignator) ? response.Components.ExtraSecondaryDesignator : null;
			addressData.ExtensionNumber = !string.IsNullOrEmpty(response.Components.ExtraSecondaryNumber) ? response.Components.ExtraSecondaryNumber : null;
			addressData.CarrierRoute = response.Metadata.CarrierRoute;
			addressData.City = response.Components.CityName;
			addressData.StateId = SosCrmDataContext.Instance.MC_PoliticalStates.GetByStateAB(response.Components.StateAbbreviation).StateID;
			addressData.County = response.Metadata.CountyName;
			addressData.CountyCode = response.Metadata.CountyFips;
			addressData.CountryId = MC_PoliticalCountry.MetaData.United_States_Of_AmericaID;

			addressData.PostDirectional = !string.IsNullOrEmpty(response.Components.StreetPostdirection)
				? response.Components.StreetPostdirection
				: null;

			if (!string.IsNullOrEmpty(response.Components.Urbanization))
			{
				addressData.Urbanization = response.Components.Urbanization;
			}
			addressData.PostalCode = response.Components.Zipcode;
			addressData.PlusFour = response.Components.Plus4Code;
			addressData.PostalCodeFull = response.DeliveryPointBarcode;
			addressData.Phone = phone;
			addressData.DeliveryPoint = response.Components.DeliveryPoint;
			double lattitude, longitude;
			if (double.TryParse(response.Metadata.Latitude, out lattitude))
				addressData.Latitude = lattitude;
			if (double.TryParse(response.Metadata.Longitude, out longitude))
				addressData.Longitude = longitude;
			if (!string.IsNullOrEmpty(response.Metadata.CongressionalDistrict))
					addressData.CongressionalDistric = int.Parse(response.Metadata.CongressionalDistrict);
			addressData.StreetAddress = response.DeliveryLine1;
			if (!string.IsNullOrEmpty(response.DeliveryLine2))
				addressData.StreetAddress2 = response.DeliveryLine2;

			// ** Figure out AddressTypeID
			addressData.AddressTypeId = !string.IsNullOrEmpty(response.Metadata.RecordType)
				? MC_AddressType.MetaData.Standard_AddressID 
				: MC_AddressType.MetaData.Non_StandardID;

			// ** Save
			addressData.IsActive = true;
			addressData.CreatedOn = DateTime.UtcNow;
			addressData.CreatedBy = userId;
			addressData.Save(userId);

		}

		#endregion Methods

	}
} 