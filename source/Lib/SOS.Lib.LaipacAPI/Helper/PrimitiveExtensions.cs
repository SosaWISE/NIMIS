namespace SOS.Lib.LaipacAPI.Helper
{
	internal static class PrimitiveExtensions
	{
		public static bool? TryParseWithNull(this bool? cntrl, string value)
		{
			/** Initialize. */
			value = value.ToLower();

			if (value.Equals("0")) return false;
			if (value.Equals("1")) return true;
			if (value.Equals("false")) return false;
			if (value.Equals("true")) return true;

			/** Return default. */
			return null;
		}

		public static int? TryParseWithNull (this int cntlr, string value)
		{
			/** Initialize. */
			int result;
			if (int.TryParse(value, out result))
				return result;

			/** Default path of execution. */
			return null;
		}

		public static decimal? TryParseWithNull(this decimal cntlr, string value)
		{
			/** Initialize. */
			decimal result;
			if (decimal.TryParse(value, out result))
				return result;

			/** Default path of execution. */
			return null;
		}

		public static double? TryParseWithNull(this double cntlr, string value)
		{
			/** Initialize. */
			double result;
			if (double.TryParse(value, out result))
				return result;

			/** Default path of execution. */
			return null;
		}
	}
}
