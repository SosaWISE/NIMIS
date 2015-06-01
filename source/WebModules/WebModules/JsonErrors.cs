using Nancy;
using Nancy.Bootstrapper;
using Nancy.Responses;
using SOS.Lib.Core;
using System;

namespace WebModules
{
	public class ErrorMsg
	{
		public string Message { get; set; }
		public string FullException { get; set; }
	}
	public class JsonErrors
	{
		public static void Enable(IPipelines pipelines, ISerializer serializer)
		{
			if (pipelines == null) throw new ArgumentNullException("pipelines");
			pipelines.OnError.AddItemToEndOfPipeline(GetErrorHandler(serializer));
		}

		private static Func<NancyContext, Exception, Response> GetErrorHandler(ISerializer serializer)
		{
			return (context, ex) => HandleError(context, ex, serializer);
		}

		private static Response HandleError(NancyContext context, Exception ex, ISerializer serializer)
		{
			Result<object> result;
			if (ex is ResultException)
				result = new Result<object>(((ResultException)ex).Code, ex.Message);
			else
			{
				result = new Result<object>
				{
					Code = (int)HttpStatusCode.InternalServerError,
					Message = StaticConfiguration.DisableErrorTraces ? ex.Message : ex.ToString(),
				};
			}
			return new JsonResponse(result, serializer);
		}
	}
}
