using System;

namespace SOS.Lib.Util.Extensions
{
	public static class FloatExtensions
	{
		public static bool IsZero (this float fValue)
		{
			return (Math.Abs(fValue - 0) < 1E-10);
		}
	}
}
