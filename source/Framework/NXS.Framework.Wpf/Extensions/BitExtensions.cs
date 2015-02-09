namespace NXS.Framework.Wpf.Extensions
{
	public static class BitExtensions
	{
		public static int BooleanSet(this int value, int flag)
		{
			return BitExtensions.BooleanSet(value, flag, true);
		}
		public static int BooleanReset(this int value, int flag, bool state)
		{
			return BitExtensions.BooleanSet(value, flag, false);
		}
		public static int BooleanSet(this int value, int flag, bool state)
		{
			if (state)
				return value | flag;

			return value & (~flag);
		}
	}
}
