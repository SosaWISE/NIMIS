using System.Web.Http;
using Thinktecture.IdentityModel.Http.Cors.WebApi;

// ReSharper disable once CheckNamespace
namespace Nxs.Services.CorsConnext.App_Start
{
	public class CorsConfig
	{
		public static void RegisterCors(HttpConfiguration httpConfig)
		{
			var corsConfig = new WebApiCorsConfiguration();

			// This adds the CorsMessageHandler to the HttpConfiguration's
			// MessageHandlers collection
			corsConfig.RegisterGlobal(httpConfig);
			corsConfig
				.ForResources("AuthSrv")
				.ForOrigins("http://dev.sscrm.com:8080")
				.AllowAllMethods()
				.AllowRequestHeaders("Content-Type")
				.AllowCookies();

			/**  this allows all CORS request to the AuthSrv controller
			 *	from the http://* or all domains. */
			//corsConfig
			//    .ForResources(new[] { "Values", "Device", "AuthSrv" })
			//    //.ForResources(new[] { "Values", "AuthSrv", "AppSrv" })
			//    .ForOrigins(new[]
			//                    {
			//                        "http://sse.clientgpsv2.local"
			//                        , "http://sse.clientgpsv2.local/"
			//                    })
			//    .AllowMethods(new[]
			//                    {
			//                        "ACCEPT"
			//                        , "PROPFIND"
			//                        , "PROPPATCH"
			//                        , "COPY"
			//                        , "MOVE"
			//                        , "DELETE"
			//                        , "MKCOL"
			//                        , "LOCK"
			//                        , "UNLOCK"
			//                        , "PUT"
			//                        , "GETLIB"
			//                        , "VERSION-CONTROL"
			//                        , "CHECKIN"
			//                        , "CHECKOUT"
			//                        , "UNCHECKOUT"
			//                        , "REPORT"
			//                        , "UPDATE"
			//                        , "CANCELUPLOAD"
			//                        , "HEAD"
			//                        , "OPTIONS"
			//                        , "GET"
			//                        , "POST"				              		
			//                    })
			//    //.AllowRequestHeaders(new[]
			//    //                        {
			//    //                    "Accept"
			//    //                    , "Overwrite"
			//    //                    , "Destination"
			//    //                    , "Content-Type"
			//    //                    , "Depth"
			//    //                    , "User-Agent"
			//    //                    , "X-File-Size"
			//    //                    , "X-Requested-With"
			//    //                    , "If-Modified-Since"
			//    //                    , "X-File-Name"
			//    //                    , "Cache-Control"				                     		
			//    //                        })
			//    //.AllowCookies()
			//    ;
		}
	}
}