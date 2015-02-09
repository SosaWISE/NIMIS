using System;
using System.Globalization;
using SOS.Data.SosCrm.ControllerExtensions;
using SOS.Lib.LaipacAPI.ExceptionHandling;
using SOS.Lib.Util.Extensions;

namespace SOS.Lib.LaipacAPI.Helper
{
	public static class GPSUnit
	{
		#region Member Functions
		
		public static string GetPassword(string sUnitID)
		{
			/** Initialize. */
			Data.SosCrm.GS_Account gsAccounts =  GetMsAccount(sUnitID);

			/** Check Password. */
			if (string.IsNullOrWhiteSpace(gsAccounts.GpsWatchPassword))
				throw new LaipacDeviceMissingPasswordInCrm(
					string.Format("The UnitID '{0}' was found in the CRM, but the password was not set.", sUnitID), sUnitID);

			/** Return password. */
				return gsAccounts.GpsWatchPassword;
		}

		public static Data.SosCrm.GS_Account GetMsAccount(string sUnitID)
		{
			/** Initialize. */
			Data.SosCrm.GS_Account gsAccounts = Data.SosCrm.SosCrmDataContext.Instance.GS_Accounts.GetByLaipacUnitID(sUnitID);

			/** Check Account was found. */
			if (gsAccounts == null)
				throw new LaipacDeviceNotFoundInCrm(
					string.Format("The UnitID '{0}' was not found in the CRM, unable to retieve password.", sUnitID), sUnitID);

			/** Return MsAccount. */
			return gsAccounts;
		}

		public static decimal GetLatitudeFromLapacDevice(string sLatitude)
		{
			/** Figure out if this is a negative number or not. */
			sLatitude = sLatitude.Trim();
			string sDirectionalNS = (sLatitude.Substring(0, 1).Equals("-"))
			                        	? "S"
			                        	: "N";
			if (sDirectionalNS.Equals("S")) sLatitude = sLatitude.Substring(1);  // Removes the negative sign.
			
			return GetLatitudeFromLapacDevice(sLatitude, sDirectionalNS);
		}

		public static decimal GetLatitudeFromLapacDevice(string sLatitude, string directionalNS)
		{
			/** Initialize. */
			double value;
			if (double.TryParse(sLatitude, out value))
			{
				if (value.IsZero()) return 0m;
			}

			/** Parse Latitude. */
			int decimalPointIndex = sLatitude.IndexOf('.') - 2; // move carrot two places to the left.
			string mmMMM = sLatitude.Substring(decimalPointIndex);
			string dd = sLatitude.Substring(0, sLatitude.IndexOf(mmMMM, StringComparison.Ordinal));


			/** Get the minutes. */
			decimal minutes;
			if (!decimal.TryParse(mmMMM, out minutes))
				throw new LaipacConversionToLatitude(string.Format("Failed to convert minutes from '{0}'.", mmMMM), sLatitude);

			// ** Convert to minutes
			minutes = minutes/60;

			/** Convert to decimal */
			decimal result;
			if (!decimal.TryParse(dd, out result))
				throw new LaipacConversionToLatitude(string.Format("Failed to conert to decimal from '{0}'.", dd), sLatitude);

			/** Return result. */
			return (result + minutes) * (directionalNS.Equals("S") ? -1 : 1);
		}

		public static decimal GetLongitudeFromLaipacDevice(string sLongitude)
		{
			/** Figure out if this is a negative number or not. */
			sLongitude = sLongitude.Trim();
			string sDirectionalWE = (sLongitude.Substring(0, 1).Equals("-"))
										? "W"
										: "E";
			if (sDirectionalWE.Equals("W")) sLongitude = sLongitude.Substring(1); // Removes the leading negative sign. 

			/** Return result. */
			return GetLongitudeFromLaipacDevice(sLongitude, sDirectionalWE);
		}

		public static decimal GetLongitudeFromLaipacDevice(string sLongitude, string directionalWE)
		{
			/** Initialize. */
			double value;
			if (double.TryParse(sLongitude, out value))
			{
				if (value.IsZero()) return 0m;
			}

			/** Parse Latitude. */
			int decimalPointIndex = sLongitude.IndexOf('.') - 2; // move carrot two places to the left.
			string mmMMM = sLongitude.Substring(decimalPointIndex);
			string ddd = sLongitude.Substring(0, sLongitude.IndexOf(mmMMM, StringComparison.Ordinal));

			/** Get the minutes. */
			decimal minutes;
			if (!decimal.TryParse(mmMMM, out minutes))
				throw new LaipacConversionToLatitude(string.Format("Failed to convert minutes from '{0}'.", mmMMM), sLongitude);

			// ** Convert to minutes
			minutes = minutes / 60;

			/** Convert to decimal */
			decimal result;
			if (!decimal.TryParse(ddd, out result))
				throw new LaipacConversionToLatitude(string.Format("Failed to conert to decimal from '{0}'.", ddd), sLongitude);

			/** Return result. */
			return (result + minutes) * (directionalWE.Equals("W") ? -1 : 1);
		}

		/// <summary>
		/// Convert to this format ddmm.mmmm
		/// </summary>
		/// <param name="fLatitude"></param>
		/// <returns></returns>
		public static string ConvertFromGoogleMapsToLaipacLatitude(double fLatitude)
		{
			/** Initialize. */
			bool isNegative = fLatitude < 0;

			// ** separate dd from mm.mmmm
			string sDD = (fLatitude).ToString(CultureInfo.InvariantCulture).Replace("-",string.Empty);
			sDD = sDD.Substring(0, sDD.IndexOf('.'));
			double fDD = Convert.ToDouble(sDD);
			string sMMmm = fLatitude.ToString(CultureInfo.InvariantCulture);
			sMMmm = sMMmm.Substring(sMMmm.IndexOf('.'));
			double fMMmm = Convert.ToDouble(sMMmm);
			fMMmm = fMMmm*60;

			// ** Add the DD with the MMmm.
			double result = (fDD * 100) + fMMmm;
			if (isNegative) result = result * (-1);

			/** Return result. */
			return result.ToString("0000.0000");
		}

		public static string ConvertFromGoogleMapsToLaipacLongitude(double fLongitude)
		{
			/** Initialize. */
			bool isNegative = fLongitude < 0;

			// ** separate dd from mm.mmmm
			string sDD = (fLongitude).ToString(CultureInfo.InvariantCulture).Replace("-", string.Empty);
			sDD = sDD.Substring(0, sDD.IndexOf('.'));
			double fDD = Convert.ToDouble(sDD);
			string sMMmm = fLongitude.ToString(CultureInfo.InvariantCulture);
			sMMmm = sMMmm.Substring(sMMmm.IndexOf('.'));
			double fMMmm = Convert.ToDouble(sMMmm);
			fMMmm = fMMmm*60;

			// ** Add the DD with the MMmm.
			double result = (fDD * 100) + fMMmm;
			if (isNegative) result = result*(-1);

			/** Return result. */
			return result.ToString("00000.0000");
		}

		#endregion Member Functions
	}
}
