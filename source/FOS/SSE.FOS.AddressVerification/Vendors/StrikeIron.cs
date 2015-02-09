using System.Collections.Generic;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Lib.Util.Configuration;
using SSE.FOS.AddressVerification.Interfaces;
using SSE.FOS.AddressVerification.Models;
using SSE.FOS.AddressVerification.SI_NorthAmericanAddressVerification;
using SSE.Lib.Interfaces.FOS;
using SSE.Lib.Interfaces.Helpers;

namespace SSE.FOS.AddressVerification.Vendors
{
	public class StrikeIron : IVendor 
	{
		#region Properties
// ReSharper disable once UnusedMember.Local
		private readonly string _username = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("AddressVerification_StrikeIronUID"), null);
		private readonly string _password = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("AddressVerification_StrikeIronPWD"), null);
		private readonly string _prodkey1 = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("AddressVerification_StrikeIronKEY"), null);

		private const int _SUCCESSFUL_INVOCATION_MIN = 200;
		private const int _SUCCESSFUL_INVOCATION_MAX = 299;
		private const int _NONFATAL_ERROR_MIN = 300;
		private const int _NONFATAL_ERROR_MAX = 399;
		private const int _ERROR_INVALID_INPUT_MIN = 400;
		private const int _ERROR_INVALID_INPUT_MAX = 499;
		private const int _UNEXPECTED_ERROR_MIN = 500;

		#endregion Properties

		#region Methods

		/// <summary>
		/// This will verify the address with a vendor and return the information of the address decomposed.
		/// </summary>
		/// <param name="address"></param>
		/// <param name="nAreaCode"></param>
		/// <param name="dealerId"></param>
		/// <param name="seasonId"></param>
		/// <param name="teamLocationId"></param>
		/// <param name="salesRepId"></param>
		/// <param name="userId"></param>
		/// <returns></returns>
		public IFosAVResult<IFosAddressVerified> VerifyAddress(IFosQlAddress address, int nAreaCode, int dealerId, int seasonId, int teamLocationId, string salesRepId, string userId)
		{
			#region Initialize
			// ** Initialized.
			var result = new FosAVResult<IFosAddressVerified>(ResultCodes.Initializing, "Initializing 'VerifyAddress'.");
			var siService = new NorthAmericanAddressVerificationServiceSoapClient();
			var addressData = new QL_Address
				{
					AddressTypeId = MC_AddressType.MetaData.Non_StandardID,
					AddressValidationStateId = MC_AddressValidationState.MetaData.UnverifiedID,
					ValidationVendorId = MC_AddressValidationVendor.MetaData.Strike_IronID,
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
			var licenseInfo = new LicenseInfo();
			//var registeredUser = new RegisteredUser {UserID = _username, Password = _password}; /** Using Key instead of UID. */
			var registeredUser = new RegisteredUser {UserID = _prodkey1, Password = _password};
			licenseInfo.RegisteredUser = registeredUser;
			

			// ** Execute verification
			SIWsOutputOfNorthAmericanAddress output;
			var subInfo = siService.NorthAmericanAddressVerification(
				licenseInfo
				, address.AddressLine1
				, address.AddressLine2
				, string.Format("{0} {1} {2}", address.City, address.StateId, address.PostalCode)
				, CountryCode.US
				, null, null, CasingEnum.UPPER, out output);

			// ** Get remaining hits.
			result.RemainingHits = subInfo.RemainingHits;

			// ** Check for results
			if (_SUCCESSFUL_INVOCATION_MIN <= output.ServiceStatus.StatusNbr &&
			    output.ServiceStatus.StatusNbr <= _SUCCESSFUL_INVOCATION_MAX)
			{
				result = new FosAVResult<IFosAddressVerified>(ResultCodes.Success, "Success");
				addressData.AddressValidationStateId = MC_AddressValidationState.MetaData.VerifiedID;
				addressData.AddressTypeId = MC_AddressType.MetaData.Standard_AddressID;

			}
			if (_NONFATAL_ERROR_MIN <= output.ServiceStatus.StatusNbr &&
				output.ServiceStatus.StatusNbr <= _NONFATAL_ERROR_MAX)
			{
				result = new FosAVResult<IFosAddressVerified>(ResultCodes.AddressValidationError, output.ServiceStatus.StatusDescription);
				addressData.AddressValidationStateId = MC_AddressValidationState.MetaData.Failed_VerificationID;
				addressData.AddressTypeId = MC_AddressType.MetaData.Non_StandardID;
			}
			if (_ERROR_INVALID_INPUT_MIN <= output.ServiceStatus.StatusNbr &&
				output.ServiceStatus.StatusNbr <= _ERROR_INVALID_INPUT_MAX)
			{
				result = new FosAVResult<IFosAddressVerified>(ResultCodes.AddressValidationInuptError, output.ServiceStatus.StatusDescription);
				addressData.AddressValidationStateId = MC_AddressValidationState.MetaData.Failed_VerificationID;
				addressData.AddressTypeId = MC_AddressType.MetaData.Non_StandardID;
			}
			if (_UNEXPECTED_ERROR_MIN <= output.ServiceStatus.StatusNbr)
			{
				result = new FosAVResult<IFosAddressVerified>(ResultCodes.AddressValidationUnexpectedError, output.ServiceStatus.StatusDescription);
				addressData.AddressValidationStateId = MC_AddressValidationState.MetaData.Failed_VerificationID;
				addressData.AddressTypeId = MC_AddressType.MetaData.Non_StandardID;
			}

			// ** Check to see if we found the address
			if (addressData.AddressValidationStateId == MC_AddressValidationState.MetaData.VerifiedID)
			{
				// ** Save data
				BindData(addressData, address.Phone, output.ServiceResult.USAddress, seasonId, teamLocationId, salesRepId, userId);
				IFosAddressVerified resultValue = BindTimeZone(addressData, nAreaCode, userId);

				result.Value = resultValue;
			}

			#endregion Execute

			// ** Return result
			return result;
		}

		private static void BindData(QL_Address address, string phone, USAddress oResponse, int seasonId, int teamLocationId, string salesRepId, string userId)
		{
			address.SeasonId = seasonId;
			address.TeamLocationId = teamLocationId;
			address.SalesRepId = salesRepId;
			address.StreetNumber = oResponse.StreetNumber;
			address.StreetName = oResponse.StreetName;
			if (!oResponse.StreetType.Equals(string.Empty))
			{
				#region StreetType
				switch (oResponse.StreetType)
				{
					case "ALLEE":
					case "ALLEY":
					case "ALLY":
					case "ALY":
					case "AL":
						address.StreetType = "AL";
						break;
					case "AV":
					case "AVE":
					case "AVEN":
					case "AVENU":
					case "AVENUE":
					case "AVN":
					case "AVNUE":
						address.StreetType = "AV";
						break;
					case "BLVD":
					case "BOUL":
					case "BOULEVARD":
					case "BOULV":
					case "BV":
						address.StreetType = "BV";
						break;

					case "BD":
					case "BUILD":
					case "BUILDING":
						address.StreetType = "BD";
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
						address.StreetType = "CN";
						break;

					case "CI":
					case "CIR":
					case "CIRC":
					case "CIRCL":
					case "CIRCLE":
					case "CRCL":
					case "CRCLE":
					case "CIRCLES":
						address.StreetType = "CI";
						break;

					case "COURT":
					case "CRT":
					case "CT":
					case "COURTS":
						address.StreetType = "CT";
						break;

					case "CS":
					case "CRECENT":
					case "CRES":
					case "CRESCENT":
					case "CRESENT":
					case "CRSCNT":
					case "CRSENT":
					case "CRSNT":
						address.StreetType = "CS";
						break;

					case "DALE":
					case "DA":
						address.StreetType = "DA";
						break;

					case "DR":
					case "DRIV":
					case "DRIVE":
					case "DRV":
						address.StreetType = "DR";
						break;

					case "EX":
					case "EXP":
					case "EXPR":
					case "EXPRESS":
					case "EXPY":
					case "EXPW":
						address.StreetType = "EX";
						break;

					case "FREEWAY":
					case "FREEWY":
					case "FRWAY":
					case "FRWY":
					case "FWY":
					case "FY":
						address.StreetType = "FY";
						break;

					case "GARDEN":
					case "GDN":
					case "GARDN":
					case "GRDEN":
					case "GRDN":
					case "GDNS":
					case "GRDNS":
					case "GA":
						address.StreetType = "GA";
						break;

					case "GROVE":
					case "GROV":
					case "GRV":
					case "GROVES":
					case "GR":
						address.StreetType = "GR";
						break;

					case "HEIGHTS":
					case "HEIGHT":
					case "HTS":
					case "HGTS":
					case "HT":
						address.StreetType = "HT";
						break;

					case "HIGHWY":
					case "HIWAY":
					case "HIWY":
					case "HWAY":
					case "HWY":
					case "HY":
					case "HIGHWAY":
						address.StreetType = "HY";
						break;

					case "HILL":
					case "HL":
					case "HI":
						address.StreetType = "HI";
						break;

					case "KNOLL":
					case "KNL":
					case "KNOL":
					case "KNLS":
					case "KNOLLS":
					case "KN":
						address.StreetType = "KN";
						break;

					case "LANE":
					case "LA":
					case "LN":
					case "LANES":
						address.StreetType = "LN";
						break;

					case "LOOP":
					case "LOOPS":
					case "LP":
						address.StreetType = "LP";
						break;

					case "MALL":
					case "MA":
						address.StreetType = "MA";
						break;

					case "OVAL":
					case "OVL":
					case "OV":
						address.StreetType = "OV";
						break;

					case "PARK":
					case "PARKS":
					case "PRK":
					case "PK":
						address.StreetType = "PK";
						break;

					case "PARKWAY":
					case "PARKWY":
					case "PKWAY":
					case "PKWY":
					case "PKY":
					case "PY":
					case "PARKWAYS":
					case "PKWYS":
						address.StreetType = "PY";
						break;

					case "PATH":
					case "PATHS":
					case "PA":
						address.StreetType = "PA";
						break;

					case "PIKE":
					case "PIKES":
					case "PI":
						address.StreetType = "PI";
						break;

					case "PLACE":
					case "PL":
						address.StreetType = "PL";
						break;

					case "PLAZA":
					case "PLZA":
					case "PLZ":
					case "PZ":
						address.StreetType = "PZ";
						break;

					case "POINT":
					case "PT":
					case "POINTS":
					case "PTS":
						address.StreetType = "PT";
						break;

					case "RD":
					case "ROAD":
					case "RDS":
					case "ROADS":
						address.StreetType = "RD";
						break;

					case "ROUTE":
					case "RT":
						address.StreetType = "RT";
						break;

					case "ROW":
					case "RO":
						address.StreetType = "RO";
						break;

					case "RUN":
					case "RN":
						address.StreetType = "RN";
						break;

					case "RURALROUTE":
					case "RR":
						address.StreetType = "RR";
						break;

					case "SQUARE":
					case "SQ":
					case "SQR":
					case "SQRE":
					case "SQU":
					case "SQRS":
					case "SQUARES":
						address.StreetType = "SQ";
						break;

					case "STREET":
					case "STR":
					case "ST":
						address.StreetType = "ST";
						break;

					case "TC":
					case "TER":
					case "TERR":
					case "TERRACE":
						address.StreetType = "TC";
						break;

					case "THRUWAY":
					case "THROUGHWAY":
					case "TRWY":
					case "TY":
						address.StreetType = "TY";
						break;

					case "TRAIL":
					case "TR":
					case "TRL":
					case "TRAILS":
					case "TRLS":
						address.StreetType = "TR";
						break;

					case "TURNPIKE":
					case "TPK":
					case "TPKE":
					case "TRNPK":
					case "TRPK":
					case "TURNPK":
					case "TP":
						address.StreetType = "TP";
						break;

					case "VIADUCT":
					case "VDCT":
					case "VIA":
					case "VIADCT":
					case "VI":
						address.StreetType = "VI";
						break;

					case "VIEW":
					case "VW":
					case "VIEWS":
					case "VWS":
						address.StreetType = "VW";
						break;

					case "WALK":
					case "WALKS":
					case "WK":
						address.StreetType = "WK";
						break;

					case "WAY":
					case "WY":
						address.StreetType = "WY";
						break;
					default:
						address.StreetType = null;
						break;
				}
				#endregion StreetType

			}
			else
				address.StreetType = null;
			address.PreDirectional = !oResponse.PreDirection.Equals(string.Empty) 
				? oResponse.PreDirection 
				: null;
			address.PostDirectional = !oResponse.PostDirection.Equals(string.Empty) 
				? oResponse.PostDirection 
				: null;
			address.DPV = !oResponse.DPV.Equals("N");
			address.DPVResponse = oResponse.DPV;
			address.DPVFootnote = oResponse.DPVFootnote;
			address.Extension = !string.IsNullOrEmpty(oResponse.Extension) ? oResponse.Extension : null;
			address.ExtensionNumber = !string.IsNullOrEmpty(oResponse.ExtensionNumber) ? oResponse.ExtensionNumber : null;
			address.CarrierRoute = oResponse.CarrierRoute;
			address.City = oResponse.City;
			address.StateId = SosCrmDataContext.Instance.MC_PoliticalStates.GetByStateAB(oResponse.State).StateID;
			address.County = oResponse.County;
			address.CountyCode = oResponse.CountyNumber;
			address.CountryId = MC_PoliticalCountry.MetaData.United_States_Of_AmericaID;

			if (!string.IsNullOrEmpty(oResponse.Urbanization))
			{
				address.Urbanization = oResponse.Urbanization;
			}
			address.PostalCode = oResponse.ZIPCode;
			address.PlusFour = oResponse.ZIPAddOn;
			address.PostalCodeFull = oResponse.ZIPPlus4;
			address.Phone = phone;
			address.DeliveryPoint = oResponse.DeliveryPoint;
			address.Latitude = oResponse.GeoCode.Latitude;
			address.Longitude = oResponse.GeoCode.Longitude;
			if (!string.IsNullOrEmpty(oResponse.CongressDistrict))
				address.CongressionalDistric = int.Parse(oResponse.CongressDistrict);
			address.StreetAddress = oResponse.AddressLine1;
			if (!string.IsNullOrEmpty(oResponse.AddressLine2))
				address.StreetAddress2 = oResponse.AddressLine2;

			// ** Save
			address.Save(userId);

			// ** Create response object
			//var result = new FosAddressVerified(address);

			// ** Return result.
			//return result;
		}

		private static IFosAddressVerified BindTimeZone(QL_Address address, int nAreaCode, string userId)
		{
			// ** Calculate the timezone if it is missing
			if (address.TimeZoneId == (int) MC_PoliticalTimeZone.TimeZoneEnum.No_Zone)
			{
				#region Lookup TimeZone by State
				if (address.State != null && !string.IsNullOrEmpty(address.State.StateID))
				{
					MS_TimeZoneLookupCollection oTzLookUp = SosCrmDataContext.Instance.MS_TimeZoneLookups.GetByStateAB(address.State.StateAB);
					if (oTzLookUp.Count > 0)
					{
						MS_TimeZoneLookup oRw;
						if (oTzLookUp.Count == 1)
						{
							oRw = oTzLookUp[0];
						}
						else
						{
							oRw = oTzLookUp[0];
							foreach (MS_TimeZoneLookup oR in oTzLookUp)
							{
								if (int.Parse(oR.AreaCode) == nAreaCode)
								{
									oRw = oR;
								}
							}
						}

						// Assigne the correct timezone
						switch (oRw.GreenwichOffset)
						{
							case 0:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.No_Zone;
								break;
							case -10:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.HawaiiAleutian_Standard_Time;
								break;
							case -9:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.Alaska_Standard_Time;
								break;
							case -8:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.Pacific_Standard_Time;
								break;
							case -7:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.Mountain_Standard_Time;
								break;
							case -6:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.Central_Standard_Time;
								break;
							case -5:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.Eastern_Standard_Time;
								break;
							case -4:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.Atlantic_Standard_Time;
								break;
							default:
								address.TimeZoneId = (int)MC_PoliticalTimeZone.TimeZoneEnum.No_Zone;
								break;
						}
					}
				}
				#endregion Lookup TimeZone by State
			}

			// ** Save address
			address.Save(userId);

			// ** Create response object
			var result = new FosAddressVerified(address);

			// ** Return result.
			return result;
			
		}

		#endregion Methods
	}
}
