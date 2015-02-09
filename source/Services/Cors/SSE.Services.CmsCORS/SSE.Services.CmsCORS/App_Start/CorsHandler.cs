using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable once CheckNamespace
namespace SSE.Services.CmsCORS.App_Start
{
	public class CorsHandler : DelegatingHandler
	{
		const string _ORIGIN = "Origin";
		const string _ACCESS_CONTROL_REQUEST_METHOD = "Access-Control-Request-Method";
		const string _ACCESS_CONTROL_REQUEST_HEADERS = "Access-Control-Request-Headers";
		const string _ACCESS_CONTROL_ALLOW_ORIGIN = "Access-Control-Allow-Origin";
		const string _ACCESS_CONTROL_ALLOW_METHODS = "Access-Control-Allow-Methods";
		const string _ACCESS_CONTROL_ALLOW_HEADERS = "Access-Control-Allow-Headers";

		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			bool isCorsRequest = request.Headers.Contains(_ORIGIN);
			bool isPreflightRequest = request.Method == HttpMethod.Options;
			if (isCorsRequest)
			{
				if (isPreflightRequest)
				{
					return Task.Factory.StartNew(() =>
					{
						var response = new HttpResponseMessage(HttpStatusCode.OK);
						response.Headers.Add(_ACCESS_CONTROL_ALLOW_ORIGIN, request.Headers.GetValues(_ORIGIN).First());

						string accessControlRequestMethod = request.Headers.GetValues(_ACCESS_CONTROL_REQUEST_METHOD).FirstOrDefault();
						if (accessControlRequestMethod != null)
						{
							response.Headers.Add(_ACCESS_CONTROL_ALLOW_METHODS, accessControlRequestMethod);
						}

						string requestedHeaders = string.Join(", ", request.Headers.GetValues(_ACCESS_CONTROL_REQUEST_HEADERS));
						if (!string.IsNullOrEmpty(requestedHeaders))
						{
							response.Headers.Add(_ACCESS_CONTROL_ALLOW_HEADERS, requestedHeaders);
						}

						return response;
					}, cancellationToken);
				}

				// Default path of execution
				return base.SendAsync(request, cancellationToken).ContinueWith(t =>
				{
					HttpResponseMessage resp = t.Result;
					resp.Headers.Add(_ACCESS_CONTROL_ALLOW_ORIGIN, request.Headers.GetValues(_ORIGIN).First());
					return resp;
				});
			}

			// Default path of execution
			return base.SendAsync(request, cancellationToken);
		}
	}
}