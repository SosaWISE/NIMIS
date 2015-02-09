namespace SOS.Lib.Core.ErrorHandling
{
	public enum ErrorMessageType
	{
		Normal = 0,
		Warning = 1,
		Critical = 2,
		Success = 3,
		Licensing = 4,
		CustomerPermit = 5,
		Exception = 6
	}

	public static class ErrorMessageTypeReadable
	{
		public static string Get(ErrorMessageType eType)
		{
			switch (eType)
			{
				case ErrorMessageType.Normal:
					return "Normal";
				case ErrorMessageType.Warning:
					return "Warning";
				case ErrorMessageType.Critical:
					return "Critical";
				case ErrorMessageType.Success:
					return "Success";
				case ErrorMessageType.Licensing:
					return ErrorMessageType.Licensing.ToString();
				case ErrorMessageType.CustomerPermit:
					return ErrorMessageType.CustomerPermit.ToString();
				case ErrorMessageType.Exception:
					return ErrorMessageType.Exception.ToString();
				default:
					return "NOT DEFINED";
			}
		}
	}
}
