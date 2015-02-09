using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NXS.Lib.Web
{
	public class HttpCorsModule : IHttpModule
	{
		public void Init(HttpApplication app)
		{
			app.BeginRequest += new EventHandler(this.app_BeginRequest);
		}
		private void app_BeginRequest(object sender, EventArgs e)
		{
			this.PerformCorsCheck();
		}
		private void PerformCorsCheck()
		{
			HttpContext ctx = HttpContext.Current;
			CorsAccessRequest accessRequest = new CorsAccessRequest(new HttpRequestWrapper(ctx.Request));
			if (accessRequest.IsCors)
			{
				var accessResponse = CorsEngine.CheckAccess(accessRequest);
				if (accessResponse != null)
				{
					HttpResponse response = ctx.Response;
					accessResponse.WriteResponse(new HttpResponseWrapper(response));
				}
				if (accessRequest.IsCorsPreflight)
				{
					ctx.Response.StatusCode = 200;
					//ctx.Response.End(); // throws “Thread was being aborted” Exception
					//http://stackoverflow.com/a/22363396
					ctx.Response.Flush(); // Sends all currently buffered output to the client.
					ctx.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
					ctx.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
				}
			}
		}
		public void Dispose()
		{
		}
	}

	internal static class CorsEngine
	{
		static readonly CorsConfigurationEntry _configEntry;
		static CorsEngine()
		{
			_configEntry = new CorsConfigurationEntry();

			_configEntry.AllowCookies = ("true" == SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig(CorsConstants.Access_Control_Allow_Credentials));

			AddFromList(_configEntry.OriginsDict, GetConfigList(CorsConstants.Access_Control_Allow_Origin));

			AddFromList(_configEntry.MethodsDict, CorsConstants.SimpleMethods);
			AddFromList(_configEntry.MethodsDict, GetConfigList(CorsConstants.Access_Control_Allow_Methods));

			AddFromList(_configEntry.HeadersDict, CorsConstants.SimpleHeaders);
			AddFromList(_configEntry.HeadersDict, GetConfigList(CorsConstants.Access_Control_Allow_Headers));

			var exposedHeadersDict = new Dictionary<string, bool>();
			AddFromList(exposedHeadersDict, GetConfigList(CorsConstants.Access_Control_Expose_Headers));
			//AddFromList(responseHeadersDict, CorsConstants.SimpleResponseHeaders);
			foreach (var key in exposedHeadersDict.Keys)
			{
				_configEntry.ExposedHeaders.Add(key);
			}
			_configEntry.ExposedHeaders.RemoveSimpleResponseHeaders();

			//@TODO: read CacheDuration from web.config ((cacheDuration > 0) ? cacheDuration : null)
			_configEntry.PreflightCacheDuration = null;
		}
		private static IEnumerable<string> GetConfigList(string key)
		{
			var val = SOS.Lib.Util.Configuration.ConfigurationSettings.Current.GetConfig(key);
			return val.SplitComma();
		}
		private static void AddFromList(IDictionary<string, bool> dict, IEnumerable<string> list)
		{
			foreach (var key in list)
			{
				if (!dict.ContainsKey(key))
				{
					dict.Add(key, true);
				}
			}
		}

		public static CorsAccessResponse CheckAccess(CorsAccessRequest accessRequest)
		{
			var configEntry = _configEntry;
			if (accessRequest.IsCors && configEntry.OriginsDict.ContainsKey(accessRequest.Origin))
			{
				if (accessRequest.IsCorsPreflight)
				{
					var requestedHeaders = accessRequest.RequestedHeaders.RemoveSimpleHeaders().ToList();
					if (
						// check that method is in dictionary
						configEntry.MethodsDict.ContainsKey(accessRequest.RequestedMethod) &&
						// all requested headers must be in dictionary
						requestedHeaders.All(a => configEntry.HeadersDict.ContainsKey(a))
						)
					{
						// it is a valid preflight request
						return new CorsAccessResponse
						{
							Origin = accessRequest.Origin,
							AllowCookies = configEntry.AllowCookies,
							PreflightCacheDurationSeconds = configEntry.PreflightCacheDuration,
							AllowedMethod = accessRequest.RequestedMethod,
							AllowedRequestHeaders = requestedHeaders,
						};
					}
				}
				else
				{
					// it is a valid cors request
					return new CorsAccessResponse
					{
						Origin = accessRequest.Origin,
						AllowCookies = configEntry.AllowCookies,
						ExposedHeaders = configEntry.ExposedHeaders,
					};
				}
			}
			return null;
		}
	}

	internal class CorsConstants
	{
		public const string OPTIONS = "OPTIONS";
		public const string Origin = "Origin";

		public const string Access_Control_Request_Method = "Access-Control-Request-Method";
		public const string Access_Control_Request_Headers = "Access-Control-Request-Headers";
		public const string Access_Control_Allow_Origin = "Access-Control-Allow-Origin";
		public const string Access_Control_Allow_Headers = "Access-Control-Allow-Headers";
		public const string Access_Control_Expose_Headers = "Access-Control-Expose-Headers";
		public const string Access_Control_Allow_Methods = "Access-Control-Allow-Methods";
		public const string Access_Control_Allow_Credentials = "Access-Control-Allow-Credentials";
		public const string Access_Control_Max_Age = "Access-Control-Max-Age";

		//public const string Content_Type = "Content-Type";
		public static int? DefaultCacheDurationInSeconds = null;

		// http://www.w3.org/TR/cors/#resource-preflight-requests
		// "In response to a preflight request the resource indicates which
		// methods and headers (other than simple methods and simple headers)
		// it is willing to handle and whether it supports credentials."
		public static readonly string[] SimpleHeaders = new string[]
		{
			"Origin",
			"Accept",
			"Accept-Language",
			"Content-Language",
		};
		public static readonly string[] SimpleMethods = new string[]
		{
			"GET",
			"HEAD",
			"POST",
		};
		//public static readonly string[] NotSimpleMethods = new string[]
		//{
		//	"PUT",
		//	"DELETE",
		//};

		public static readonly string[] SimpleResponseHeaders = new string[]
		{
			"Cache-Control",
			"Content-Language",
			//Content_Type,
			"Expires",
			"Last-Modified",
			"Pragma",
		};
	}

	internal class CorsConfigurationEntry
	{
		public bool AllowCookies { get; set; }
		public int? PreflightCacheDuration { get; set; }

		public IDictionary<string, bool> OriginsDict { get; private set; }
		public IDictionary<string, bool> MethodsDict { get; private set; }
		public IDictionary<string, bool> HeadersDict { get; private set; }

		public IList<string> ExposedHeaders { get; private set; }

		public CorsConfigurationEntry()
		{
			// http://www.w3.org/TR/cors/#resource-preflight-requests
			// "2. If the value of the Origin header is not a case-sensitive match for any of the values in list of origins do not set any additional headers and terminate this set of steps."
			this.OriginsDict = new Dictionary<string, bool>(StringComparer.Ordinal);
			// "5. If method is not a case-sensitive match for any of the values in list of methods do not set any additional headers and terminate this set of steps."
			this.MethodsDict = new Dictionary<string, bool>(StringComparer.Ordinal);
			// "6. If any of the header field-names is not a ASCII case-insensitive match for any of the values in list of headers do not set any additional headers and terminate this set of steps."
			this.HeadersDict = new Dictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

			// http://www.w3.org/TR/cors/#handling-a-response-to-a-cross-origin-request
			// "User agents must filter out all response headers other than those that are a simple response header or of which the field name is an
			//  ASCII case-insensitive match for one of the values of the Access-Control-Expose-Headers headers (if any), before exposing response
			//  headers to APIs defined in CORS API specifications.
			//      Note: The getResponseHeader() method of XMLHttpRequest will therefore not expose any header not indicated above."
			this.ExposedHeaders = new List<string>();
		}
	}

	internal static class ExtensionMethods
	{
		public static IEnumerable<string> SplitComma(this string val)
		{
			return val.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());
		}
		public static IEnumerable<string> RemoveSimpleHeaders(this IEnumerable<string> headers)
		{
			return headers.RemoveList(CorsConstants.SimpleHeaders);
		}
		public static IEnumerable<string> RemoveSimpleResponseHeaders(this IEnumerable<string> headers)
		{
			return headers.RemoveList(CorsConstants.SimpleResponseHeaders);
		}

		private static IEnumerable<string> RemoveList(this IEnumerable<string> headers, IEnumerable<string> list)
		{
			if (headers == null)
			{
				return Enumerable.Empty<string>();
			}
			return headers.Where(item => !list.Contains(item, StringComparer.OrdinalIgnoreCase));
		}
	}

	internal class CorsAccessRequest
	{
		private HttpRequestWrapper httpRequest;

		public string Origin
		{
			get { return this.httpRequest.Headers[CorsConstants.Origin]; }
		}
		public bool IsCors
		{
			get { return this.Origin != null; }
		}
		public bool IsCorsPreflight
		{
			get { return this.httpRequest.HttpMethod == CorsConstants.OPTIONS && this.IsCors && this.RequestedMethod != null; }
		}
		public string RequestedMethod
		{
			get { return this.httpRequest.Headers[CorsConstants.Access_Control_Request_Method]; }
		}
		public IEnumerable<string> RequestedHeaders
		{
			get
			{
				string val = this.httpRequest.Headers[CorsConstants.Access_Control_Request_Headers];
				if (val != null)
				{
					return (val.SplitComma()).Distinct<string>();
				}
				return Enumerable.Empty<string>();
			}
		}
		public CorsAccessRequest(HttpRequestWrapper httpRequest)
		{
			this.httpRequest = httpRequest;
		}
	}

	internal class CorsAccessResponse
	{
		public string Origin { get; internal set; }
		public bool AllowCookies { get; internal set; }
		public string AllowedMethod { get; internal set; }
		public IEnumerable<string> AllowedRequestHeaders { get; internal set; }
		public IEnumerable<string> ExposedHeaders { get; internal set; }
		public int? PreflightCacheDurationSeconds { get; internal set; }

		public void WriteResponse(HttpResponseWrapper response)
		{
			if (this.Origin != null)
			{
				response.AddHeader(CorsConstants.Access_Control_Allow_Origin, this.Origin);
			}
			if (this.AllowCookies)
			{
				response.AddHeader(CorsConstants.Access_Control_Allow_Credentials, "true");
			}
			if (this.AllowedMethod != null)
			{
				response.AddHeader(CorsConstants.Access_Control_Allow_Methods, this.AllowedMethod);
			}
			if (this.AllowedRequestHeaders != null && this.AllowedRequestHeaders.Any<string>())
			{
				string headers = this.AllowedRequestHeaders.Aggregate((string x, string y) => x + "," + y);
				response.AddHeader(CorsConstants.Access_Control_Allow_Headers, headers);
			}
			if (this.ExposedHeaders != null && this.ExposedHeaders.Any<string>())
			{
				string headers = this.ExposedHeaders.Aggregate((string x, string y) => x + "," + y);
				response.AddHeader(CorsConstants.Access_Control_Expose_Headers, headers);
			}
			if (this.PreflightCacheDurationSeconds.HasValue)
			{
				response.AddHeader(CorsConstants.Access_Control_Max_Age, this.PreflightCacheDurationSeconds.Value.ToString());
			}
		}
	}
}
