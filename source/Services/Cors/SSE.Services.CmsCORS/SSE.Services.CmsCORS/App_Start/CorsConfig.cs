using System.Web.Http;

// ReSharper disable once CheckNamespace
namespace SSE.Services.CmsCORS.App_Start
{
	public class CorsConfig
	{
		public static void RegisterCors(HttpConfiguration httpConfig)
		{
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