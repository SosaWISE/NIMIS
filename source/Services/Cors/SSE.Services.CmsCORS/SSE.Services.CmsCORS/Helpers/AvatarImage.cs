using NXS.Lib;
using System.Globalization;
using System.Web;

namespace SSE.Services.CmsCORS.Helpers
{
	public static class AvatarImage
	{

		#region Properties

		private const string _DEF_IMAGE_PATH = "//{DOMAIN}{PORT}/images/Avatar/{UserID}_{WIDTH}x{HEIGHT}.png";
		private const string _DEF_IMAGELP_PATH = "//lorempixel.com/{WIDTH}/{HEIGHT}/people/";
		public static string ImagePath
		{
			get
			{
				string result = WebConfig.Instance.GetConfig("ImagePath_Avatars");
				if (string.IsNullOrEmpty(result))
					return _DEF_IMAGE_PATH;
				return result;
			}
		}

		public static string ImagePathLP
		{
			get
			{
				string result = WebConfig.Instance.GetConfig("ImagePath_LoremPixelURL_People");
				if (string.IsNullOrEmpty(result))
					return _DEF_IMAGELP_PATH;
				return result;
			}
		}

		public static bool UseLoremPixel
		{
			get
			{
				/** Initialize. */
				bool useLoremPixel;
				if (bool.TryParse(WebConfig.Instance.GetConfig("ImagePath_LoremPixelUse"),
					out useLoremPixel)) return useLoremPixel;
				return false;
			}
		}
		#endregion Properties

		#region Methods

		private static string GetBoundData(string formatStr, int userId, int width = 100, int height = 100)
		{
			/** Initialize. */
			string domainName = HttpContext.Current.Request.Url.Host;
			int port = HttpContext.Current.Request.Url.Port;
			string portStr = port == 80 ? string.Empty : string.Format(":{0}", port);

			/** Bind data. */
			var result = formatStr.Replace("{DOMAIN}", domainName);
			result = result.Replace("{PORT}", portStr);
			result = result.Replace("{UserID}", userId.ToString(CultureInfo.InvariantCulture));
			result = result.Replace("{WIDTH}", width.ToString(CultureInfo.InvariantCulture));
			result = result.Replace("{HEIGHT}", height.ToString(CultureInfo.InvariantCulture));

			// ** Return result
			return result;
		}

		public static string GetImagePath(int userId, int width = 100, int height = 100)
		{
			/** Check that we are in test mode. */
			if (UseLoremPixel)
			{
				return GetBoundData(ImagePathLP, userId, width, height);
			}

			// ** Default path of execution
			return GetBoundData(ImagePath, userId, width, height);
		}
		#endregion Methods
	}
}