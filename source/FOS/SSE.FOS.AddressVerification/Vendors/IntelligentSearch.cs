using System;
using System.Collections.Generic;
using System.IO;
using SOS.Data.SosCrm;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Lib.Util.Configuration;
using SSE.FOS.AddressVerification.IS_CorrectAddress;
using SSE.FOS.AddressVerification.Interfaces;
using SSE.FOS.AddressVerification.Models;
using SSE.Lib.Interfaces.FOS;
using SSE.Lib.Interfaces.Helpers;

namespace SSE.FOS.AddressVerification.Vendors
{
	public class IntelligentSearch : IVendor
	{
		#region Properties

		private readonly string _username = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("AddressVerification_IntelligentSearchUID"), null);
		private readonly string _password = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("AddressVerification_IntelligentSearchPWD"), null);
		private readonly string _filePath = SOS.Lib.Util.Cryptography.TripleDES.DecryptString(ConfigurationSettings.Current.GetConfig("AddressVerification_IntelligentSearchFilePath"), null);

		#endregion Properties

		#region Methods

		public IFosAVResult<IFosAddressVerified> VerifyAddress(IFosQlAddress address, int nAreaCode, int dealerId, int seasonId, int teamLocationId, string salesRepId, string userId)
		{
			#region Initialize
			// ** Initialized.
			FosAVResult<IFosAddressVerified> result;
			var caServices = new CorrectAddressWebServiceSoapClient();
			var addressData = new QL_Address
				{
					SeasonId = seasonId,
					TeamLocationId = teamLocationId,
					SalesRepId = salesRepId,
					AddressValidationStateId = MC_AddressValidationState.MetaData.UnverifiedID,
					ValidationVendorId = MC_AddressValidationVendor.MetaData.Intelligent_SearchID,
					CountryId = MC_PoliticalCountry.MetaData.United_States_Of_AmericaID,
					DealerId = dealerId
				};
//			CorrectAddressServices caServices = new CorrectAddressServices();
			string szFiller = string.Format("{0}{1}", "135".PadLeft(18), "139".PadLeft(12)); // ca_filler
			bool bIsDPV = false;
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

			// Build the szCityStPostal
			string szCityStPostal = !string.IsNullOrEmpty(address.City) 
				                        ? string.Format("{0} {1} {2}", address.City, address.StateId, address.PostalCode) 
				                        : address.PostalCode;

			var oNode = caServices.wsTigerCA(_username, _password
			, string.Empty // This was for Firm ??
			, string.Empty // Urbanization
			, address.AddressLine1
			, address.AddressLine2
			, szCityStPostal
			, szFiller // ca_codes
			, string.Empty // ca_filler
			, string.Empty // batchname
			);


			// ** Check for errors
			if (!string.IsNullOrEmpty(oNode[0].ErrorCodes))
			{
				return new FosAVResult<IFosAddressVerified>(ResultCodes.AddressValidationError, oNode[0].ErrorDesc);
			}

			// Check for result
			WsCorrectAddress output = oNode[0];
			addressData.StreetNumber = output.StreetNumber;
			addressData.PreDirectional = output.PreDirectional;
			addressData.StreetName = output.StreetName;
			#region AddressType
			if (!output.StreetSuffix.Equals(string.Empty))
			{
				switch (output.StreetSuffix.ToUpper())
				{
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
				#endregion AddressType
			}
			else
			{
				addressData.StreetType = null;
			}
			addressData.PostDirectional = !string.IsNullOrEmpty(output.PostDirectional) 
				? output.PostDirectional 
				: null;
			addressData.Extension = !string.IsNullOrEmpty(output.SecondaryDesignation) 
				? output.SecondaryDesignation 
				: null;

			addressData.ExtensionNumber = !string.IsNullOrEmpty(output.SecondaryNumber)
				? output.SecondaryNumber 
				: null;

			addressData.StreetAddress = output.DeliveryLine1;
			if (!output.DeliveryLine2.Equals(string.Empty))
				addressData.StreetAddress2 = output.DeliveryLine2;
			addressData.City = output.City;

			if (!string.IsNullOrEmpty(output.State))
			{
				addressData.StateId = SosCrmDataContext.Instance.MC_PoliticalStates.GetByStateAB(output.State).StateID;
			}
			addressData.PostalCode = output.Zip;
			addressData.PlusFour = output.Addon;
			addressData.PostalCodeFull = output.ZipAddon;
			addressData.Phone = address.Phone;

			addressData.AddressTypeId = string.IsNullOrEmpty(output.RecordType) 
				? MC_AddressType.MetaData.Non_StandardID
				: MC_AddressType.MetaData.Standard_AddressID;
	
			addressData.CarrierRoute = output.CarrierRoute;

			addressData.Urbanization = !string.IsNullOrEmpty(output.Urbanization)
				? output.Urbanization
				: null;
			addressData.County = output.CountyName;
			addressData.CountyCode = output.CountyNumber;
			Console.Write("See Filler: {0}", output.Filler);
			if (!output.Filler.Trim().Equals(string.Empty))
			{
				// Locals 
				string szFillerOutput = output.Filler;
				addressData.DPV = szFillerOutput.Substring(0, 1).Equals("Y");
				bIsDPV = szFillerOutput.Substring(0, 1).Equals("Y");
				addressData.DPVResponse = szFillerOutput.Substring(0, 1);
				addressData.DPVFootnote = szFillerOutput.Substring(1, 4);
			}

			_setCoordinates(addressData, output);
			WriteGEOToFile(addressData, output);
	
			// Check to see if the addressData verified
			if (output.ReturnCodes != null && (output.ReturnCodes.Equals("1") && bIsDPV))
			{
				addressData.AddressValidationStateId = MC_AddressValidationState.MetaData.VerifiedID;
				result = new FosAVResult<IFosAddressVerified>(ResultCodes.Success, "Success");
			}
			else
			{
				result = new FosAVResult<IFosAddressVerified>(ResultCodes.AddressValidationError, string.Format("{0}", output.ReturnCodes));
			}

			#region Bind TimeZone by State

			IFosAddressVerified resultValue = Main.BindTimeZone(addressData, nAreaCode, userId);

			#endregion Bind TimeZone by State

			// ** Return result.
			result.Value = resultValue;
			return result;
		}

		private static void _setCoordinates(QL_Address address, WsCorrectAddress output)
		{
			// Locals
			string[] geofLat = output.GeofLat.Trim().Split(' ');
			string[] geotLat = output.GeotLat.Trim().Split(' ');
			string[] geofLong = output.GeofLong.Trim().Split(' ');
			string[] geotLong = output.GeotLong.Trim().Split(' ');
			double dLat = 0, dLog = 0;

			#region Convert Lattitude
			// Take the average of all the data.
			int nCount = 0;
			foreach (string szValue in geofLat)
			{
				double dTmp;
				if (double.TryParse(szValue, out dTmp))
				{
					nCount++;
					dLat += dTmp;
				}
			}
			foreach (string szValue in geotLat)
			{
				double dTmp;
				if (double.TryParse(szValue, out dTmp))
				{
					nCount++;
					dLat += dTmp;
				}
			}
			if (nCount > 0)
			{
				dLat = dLat / nCount;
			}
			#endregion Convert Lattitude

			#region Convert Longitude
			// Take the average of all the data
			nCount = 0;
			foreach (string szValue in geofLong)
			{
				double dTmp;
				if (double.TryParse(szValue, out dTmp))
				{
					nCount++;
					dLog += dTmp;
				}
			}
			foreach (string szValue in geotLong)
			{
				double dTmp;
				if (double.TryParse(szValue, out dTmp))
				{
					nCount++;
					dLog += dTmp;
				}
			}
			if (nCount > 0)
			{
				dLog = dLog / nCount;
			}
			#endregion Convert Longitude

			// Save Results
			address.Latitude = dLat;
			address.Longitude = dLog;
		}

		private void WriteGEOToFile(QL_Address address, WsCorrectAddress oRow)
		{
			// Locals
			var oRand = new Random();
			int nRandom = oRand.Next(100, 999);

			// Init values
			string szFileName = string.Format(_filePath, address.StreetAddress.Replace(@"\", "").Replace("/", ""), nRandom);

			// Check that the first folder exists
			//szPath += nAccountID.ToString();
			//if (!Directory.Exists(szPath))
			//    Directory.CreateDirectory(szPath);
			//szPath += "\\" + szRequestNumber;
			//if (!Directory.Exists(szPath))
			//    Directory.CreateDirectory(szPath);

			// Get the entire file path name
			//szFileName = string.Format("{0}\\{1}", szPath, szFileName);

			string szText = string.Format("Add Lattitude: {0}", address.Latitude);
			szText += string.Format("\r\nAdd Longitude: {0}", address.Longitude);
			szText += string.Format("\r\nRow Lattitude From: {0}", oRow.GeofLat);
			szText += string.Format("\r\nRow Lattitude To: {0}", oRow.GeotLat);
			szText += string.Format("\r\nRow Longitude From: {0}", oRow.GeofLong);
			szText += string.Format("\r\nRow Longitude To: {0}", oRow.GeotLong);

			// Save to file
			var writer = new StreamWriter(szFileName);
			writer.Write(szText);
			writer.Close();
		}

		#endregion Methods
	}
}
