namespace NXS.Framework.Wpf.Converters
{
	public static class ConverterHelper
	{
		public static bool AsBoolean(object parameter)
		{
			bool match;
			if (parameter is bool)
			{
				match = (bool) parameter;
			}
			else if (!bool.TryParse(parameter as string, out match))
			{
				match = true;
			}
			return match;
		}
	}
}