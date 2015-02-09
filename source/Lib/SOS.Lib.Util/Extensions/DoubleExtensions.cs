using System;

namespace SOS.Lib.Util.Extensions
{
	public static class DoubleExtensions
	{
		public static bool IsZero(this double fValue)
		{
			return (Math.Abs(fValue - 0) < 1E-10);
		}
	}
}
