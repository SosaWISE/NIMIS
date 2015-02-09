using System.Text.RegularExpressions;
using SOS.Lib.Core.ExceptionHandling;

namespace SSE.Lib.SseGpsDeviceAPI.Helper
{
	public static class GPSValidator
	{
		#region Methods

		public static bool Lattitude(string lattitude)
		{
			/** Initialize. */
			var reg = new Regex(@"\d\d\d\d\.\d\d\d\d");
			var match = reg.Match(lattitude);
	
			bool result = (match.Groups.Count == 1);

			if (result)
			{
				/** Check result */
				foreach (Capture group in match.Groups)
				{
					System.Diagnostics.Debug.WriteLine(message: string.Format("Value: {0}", group));
					if (string.IsNullOrEmpty(group.Value)) return false;
				}
			}

			/** Return result. */
			return result;
		}

		public static bool Longitude(string longitude)
		{
			/** Initialize. */
			var match = Regex.Match(longitude, @"\d\d\d\d\d\.\d\d\d\d");
			bool result = (match.Groups.Count == 1);

			/** Return result. */
			return result;
		}

		#endregion Methods

		#region Exceptions

		public class GPSValidationException : BaseException
		{
			public GPSValidationException(string value, string coordType)
				: base(string.Format("The value '{0}' is not a well formed '{1}' coordinate.", value, coordType))
			{
				Value = value;
				CoordType = coordType;
			}

			public string Value { get; private set; }
			public string CoordType { get; private set; }
		}
		#endregion Exceptions
	}
}
