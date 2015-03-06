using System.Linq;
using Nancy;
using Nancy.ErrorHandling;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using Nancy.ViewEngines;
using Newtonsoft.Json;
using Nancy.Serialization.JsonNet;
using System.IO;
using SOS.Lib.Core;

namespace WebModules
{
	public class StatusCodeHandler : /*DefaultViewRenderer, */IStatusCodeHandler
	{
		//private readonly ISerializer _serializer;
		//public Handler(IViewFactory viewFactory, ISerializer serializer)
		//	: base(viewFactory)
		//{
		//	_serializer = serializer;
		//}

		private readonly ISerializer _serializer;
		public StatusCodeHandler(ISerializer serializer)
		{
			_serializer = serializer;
		}

		public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
		{
			return true;
			//return statusCode == HttpStatusCode.NotFound
			//	   || statusCode == HttpStatusCode.InternalServerError
			//	   || statusCode == HttpStatusCode.Forbidden
			//	   || statusCode == HttpStatusCode.Unauthorized;
		}

		public void Handle(HttpStatusCode statusCode, NancyContext context)
		{
			if (!(context.Response is JsonResponse))
			{
				if (context.Request.Url.BasePath != null && !context.Request.Url.BasePath.StartsWith("/_Nancy", System.StringComparison.OrdinalIgnoreCase))
				{
					// don't mess with responses for _Nancy diagnostics
					return;
				}

				if (statusCode == HttpStatusCode.NotFound ||
					statusCode == HttpStatusCode.InternalServerError)
				{
					var result = new Result<object>()
					{
						Code = (int)statusCode,
						Message = statusCode.ToString(),
					};
					context.Response = new JsonResponse(result, _serializer);
					// we want the status code to be 200/OK
					//context.Response.StatusCode = statusCode;
					context.Response.StatusCode = HttpStatusCode.OK;
				}
				//else
				//{
				//	// don't mess with responses for all other status codes
				//}
			}
			else if (ShouldRenderFriendlyErrorPage(context))
			{
				//
				// pretty print json when asking for html?
				// (we have to go this route since adding FormattedJsonProcessor with a .fjson extension never gets
				// called since it has the same contentType as the default JsonProcessor or something like that...
				// should i report this as a bug??)
				//
				var contents = GetResponseContents(context.Response);
				var obj = JsonConvert.DeserializeObject(contents);
				context.Response = new JsonResponse(obj, new JsonNetSerializer(new JsonSerializer()
				{
					Formatting = Formatting.Indented,
				}));
			}
		}

		private static bool ShouldRenderFriendlyErrorPage(NancyContext context)
		{
			var ranges = context.Request.Headers.Accept.OrderByDescending(o => o.Item2)
				.Select(o => new MediaRange(o.Item1))
				.ToList();

			foreach (var range in ranges)
			{
				if (range.Matches("application/json"))
					return false;
				if (range.Matches("text/json"))
					return false;
				if (range.Matches("text/html"))
					return true;
			}
			return true;
		}

		public static string GetResponseContents(Response response)
		{
			using (var ms = new MemoryStream())
			{
				response.Contents(ms);
				using (var sw = new StreamWriter(ms))
				{
					ms.Position = 0;
					return new StreamReader(ms).ReadToEnd();
				}
			}
		}
	}
}
