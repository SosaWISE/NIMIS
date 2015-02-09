using System;

namespace SOS.Lib.Util.Extensions
{
	public static class DecimalExtensions
	{
		public static bool IsZero(this decimal value)
		{
			return (Math.Abs(value -  (decimal) 1E-10) <= 0);
		}
	}
}
