using SOS.Lib.Util;

namespace System.Web
{
	public static class HttpContextExtensions
	{
		public static string ClientIPAddress(this HttpContext context)
		{
			// ** CHeck for unit testing
			if (UnitTestingHelper.CmsCORS.IsActive)
				return UnitTestingHelper.CmsCORS.IPAddress;

			return context.Request.ServerVariables["REMOTE_ADDR"];
		}
	}
}
