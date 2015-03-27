using Nancy;
using Nancy.Responses;
using Nancy.Responses.Negotiation;
using Nancy.Serialization.JsonNet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules
{
	public class FormattedJsonProcessor : IResponseProcessor// Nancy.Responses.Negotiation.JsonProcessor
	{
		private static readonly ISerializer formattedSerializer = new JsonNetSerializer(new JsonSerializer()
			{
				Formatting = Formatting.Indented,
			});

		private static readonly IEnumerable<Tuple<string, MediaRange>> extensionMappings =
			new[] { new Tuple<string, MediaRange>("fjson", new MediaRange("application/json")) };

		public IEnumerable<Tuple<string, MediaRange>> ExtensionMappings
		{
			get { return extensionMappings; }
		}

		public ProcessorMatch CanProcess(MediaRange requestedMediaRange, dynamic model, NancyContext context)
		{
			var originalRequestPath = context.Request.Path;
			if (IsExactJsonContentType(requestedMediaRange))
			{
				return new ProcessorMatch
					{
						ModelResult = MatchResult.DontCare,
						RequestedContentTypeResult = MatchResult.ExactMatch
					};
			}

			if (IsWildcardJsonContentType(requestedMediaRange))
			{
				return new ProcessorMatch
				{
					ModelResult = MatchResult.DontCare,
					RequestedContentTypeResult = MatchResult.NonExactMatch
				};
			}

			return new ProcessorMatch
			{
				ModelResult = MatchResult.DontCare,
				RequestedContentTypeResult = MatchResult.NoMatch
			};
		}

		public Response Process(MediaRange requestedMediaRange, dynamic model, NancyContext context)
		{
			return new JsonResponse(model, formattedSerializer);
		}

		private static bool IsExactJsonContentType(MediaRange requestedContentType)
		{
			if (requestedContentType.Type.IsWildcard && requestedContentType.Subtype.IsWildcard)
			{
				return true;
			}

			return requestedContentType.Matches("application/json") || requestedContentType.Matches("text/json");
		}

		private static bool IsWildcardJsonContentType(MediaRange requestedContentType)
		{
			if (!requestedContentType.Type.IsWildcard && !string.Equals("application", requestedContentType.Type, StringComparison.InvariantCultureIgnoreCase))
			{
				return false;
			}

			if (requestedContentType.Subtype.IsWildcard)
			{
				return true;
			}

			var subtypeString = requestedContentType.Subtype.ToString();

			return (subtypeString.StartsWith("vnd", StringComparison.InvariantCultureIgnoreCase) &&
					subtypeString.EndsWith("+json", StringComparison.InvariantCultureIgnoreCase));
		}
	}

}
