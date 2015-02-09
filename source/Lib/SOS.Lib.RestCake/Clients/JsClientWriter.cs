using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using SOS.Lib.RestCake.Metadata;
using SOS.Lib.RestCake.Util;

namespace SOS.Lib.RestCake.Clients
{
	public class JsClientWriter
	{
		private readonly TextWriter m_textWriter;

		public JsClientWriter(TextWriter textWriter)
		{
			m_textWriter = textWriter;
		}


		public void WriteJsClientBase()
		{
			// Write out the WcfClient.js contents
			m_textWriter.WriteLine("// WcfClient.js");
			m_textWriter.WriteLine(ReflectionHelper.GetTemplateContents("Js.WcfClient.js"));
		}


		public void WriteServiceClient(ServiceMetadata service)
		{
			// Get the service client template, and setup the namespace, and the name of the js class
			string clientTemplate = ReflectionHelper.GetTemplateContents("Js.ServiceClient.js.txt");
			clientTemplate = clientTemplate
				.Replace("<#= Namespace #>", service.ServiceNamespace)
				.Replace("<#= JsClassName #>", service.ServiceName + "Client");

			// Go through each method in the service class, creating the js body for each one.
			StringBuilder sbServiceMethods = new StringBuilder();
			foreach (MethodMetadata method in service.Methods)
				sbServiceMethods.AppendLine(getMethodBody(method));

			// Add the service method bodies (js) that we just created to the client template
			clientTemplate = clientTemplate.Replace("<#= ServiceMethods #>", sbServiceMethods.ToString());

			// Write out the client template
			m_textWriter.WriteLine("// ServiceClient.js template");
			m_textWriter.WriteLine(clientTemplate); 
		}


		private static string getMethodBody(MethodMetadata method)
		{
			string serviceMethodTemplate = ReflectionHelper.GetTemplateContents("Js.ServiceMethod.txt");
			string dataArg = "null";

			// The template works "as-is" with HTTP GET.  For the other verbs (PUT, POST, DELETE), we have to modify the dataArg value.
			if (method.Verb != HttpVerb.Get)
			{
				string[] dataParams = method.GetDataParamNames();

				// Set up the dataArg variable
				StringBuilder sbDataArg = new StringBuilder();
				if (method.IsWrappedRequest)
				{
					// Start the wrapped json object
					sbDataArg.Append("{");

					foreach (string param in dataParams)
					{
						sbDataArg.Append("\"").Append(param).Append("\": ");
						if (param.EndsWith("Json"))
							sbDataArg.Append("JSON.stringify(").Append(param.Substring(0, param.Length - "Json".Length)).Append("), ");
						else
							sbDataArg.Append(param).Append(", ");
					}
					// Get rid of the trailing ", "
					if (sbDataArg.Length > 1)
						sbDataArg.Remove(sbDataArg.Length - 2, 2);

					// End the json wrapped object
					sbDataArg.Append("}");
				}
				else // Not wrapped
				{
					if (dataParams.Length > 1)
						throw new NotSupportedException("Error with service method " + method.Name + " in the " + method.Service.ServiceName
							+ " service. Passing in multiple data arguments requires that the service have a wrapped request "
							+ "(WebMessageBodyStyle.Wrapped or WebMessageBodyStyle.WrappedRequest)");

					if (dataParams.Length == 0)
					{
						sbDataArg.Append("null");
					}
					else
					{
						if (dataParams[0].EndsWith("Json"))
							sbDataArg.Append("JSON.stringify(").Append(dataParams[0].Substring(0, dataParams[0].Length - "Json".Length)).
								Append(")");
						else
							sbDataArg.Append(dataParams[0]);
					}
				}
				dataArg = sbDataArg.ToString();
			}

			var isStreamArg = method.Verb == HttpVerb.Post &&
				method.Parameters.Length > 0 &&
				method.Parameters[0].ParameterType == typeof(Stream);

			string methodBody = serviceMethodTemplate
				.Replace("<#= MethodName #>", method.Name)
				.Replace("<#= jsonp #>", "")
				.Replace("<#= isJsonp #>", "false")
				.Replace("<#= MethodArgs #>", getArgsListAsString(method))
				.Replace("<#= DefaultErrorMessage #>", "Error calling " + method.Service.ServiceNamespace + "." + method.Name)
				.Replace("<#= MethodUrl #>", getMethodUrl(method))
				.Replace("<#= HttpVerb #>", method.Verb.ToString("g").ToUpper())
				.Replace("<#= DataArg #>", dataArg)
				.Replace("<#= IsWrappedResponse #>", method.IsWrappedResponse.ToString().ToLower())
				.Replace("<#= responseMimeType #>", "application/json");
//				.Replace("<#= responseMimeType #>", isStreamArg ? "application/octet-stream" : "application/json");

			//if (method.Verb == HttpVerb.Get)
			//{
			//    string jsonpMethodBody = serviceMethodTemplate
			//        .Replace("<#= MethodName #>", method.Name)
			//        .Replace("<#= jsonp #>", "_jsonp")
			//        .Replace("<#= isJsonp #>", "true")
			//        .Replace("<#= MethodArgs #>", getArgsListAsString(method))
			//        .Replace("<#= DefaultErrorMessage #>", "Error calling " + method.Service.ServiceNamespace + "." + method.Name)
			//        .Replace("<#= MethodUrl #>", getMethodUrl(method))
			//        .Replace("<#= HttpVerb #>", method.Verb.ToString("g").ToUpper())
			//        .Replace("<#= DataArg #>", dataArg)
			//        .Replace("<#= IsWrappedResponse #>", method.IsWrappedResponse.ToString().ToLower());
			//    methodBody += Environment.NewLine + jsonpMethodBody;
			//}

			return methodBody;
		}


		/// <summary>
		/// Returns a string like "arg1, arg2, arg3", etc (joins all params with a comma)
		/// TODO: Currently, any arg that ends with the string "Json" (such as personJson), the "Json" will be chopped off.
		/// This was for a specific feature (need to look into it and see if it's still used)
		/// </summary>
		/// <returns></returns>
		internal static string getArgsListAsString(MethodMetadata method)
		{
			// For args that end with "Json", we want to strip that off the arg name.
			// WCF doesn't seem to let us pass raw json in as a param.  They want to use their own deserialization, which fails for many things.
			// We are using Json.NET, and so we pass in doubly quoted json strings, so the arg going into the service method is not an object, but a string.
			// But, we want our params named sanely.
			Regex rxNoJson = new Regex(@"Json$");
			string argsList = String.Join(", ", method.GetParamNames().Select(p => rxNoJson.Replace(p, "")).ToArray());
			if (argsList.Length > 0)
				argsList += ", ";
			return argsList;
		}


		private static string getMethodUrl(MethodMetadata method)
		{
			string methodUrl = "'" + method.UriTemplate + "'";
			ParameterInfo[] urlParams = method.GetUrlParams();

			// Query string params
			foreach (ParameterInfo param in urlParams)
			{
				string search = String.Format("{0}={{{0}}}", param.Name);
				string replace = String.Format("' + (({0} == null || {0} === 'undefined') ? '' : '{0}=' + {1}) + '",
				                               param.Name,
				                               getParamUrlString(param));
				methodUrl = methodUrl.Replace(search, replace);
			}

			// Get rid of useless empty string concats
			methodUrl = methodUrl.Replace(" + ''", "");

			// Uri segment params
			foreach (var param in urlParams)
				methodUrl = methodUrl.Replace("{" + param.Name + "}", "' + " + getParamUrlString(param) + " + '");

			// Get rid of weird [ + ''] and ['' + ] instances at the end or beginning (respecitvely) of the string
			Regex rxBeg = new Regex(@"^'' \+ ");
			Regex rxEnd = new Regex(@" \+ ''$");
			methodUrl = rxBeg.Replace(methodUrl, "");
			methodUrl = rxEnd.Replace(methodUrl, "");
			return methodUrl;
		}


		private static string getParamUrlString(ParameterInfo param)
		{
			string value = param.Name;
			// Do we need to JSON.stringify() the arg?
			if ((!typeof(IEnumerable).IsAssignableFrom(param.ParameterType) || typeof(IDictionary).IsAssignableFrom(param.ParameterType)) // Not an IEnumerable or IS a Dictionary.
					&& !typeof(string).IsAssignableFrom(param.ParameterType)
					&& !param.ParameterType.IsPrimitive
					&& !param.ParameterType.IsEnum)
			{
				value = "JSON.stringify(" + param.Name + ")";
			}
			// True, RestCake on the server handles it when it's NOT that way, but it won't handle commas in the strings, so it's an easy point of failure.
			else if (param.ParameterType == typeof(string[]) || typeof(IList<string>).IsAssignableFrom(param.ParameterType))
			{
				// For arrays or lists of strings (in a GET url, remember), use an empty string for null, [] for an empty array, and JSON.stringify() for a populated array.
				value = "(" + param.Name + " == null ? '' : (" + param.Name + ".length == 0 ? '[]' : JSON.stringify(" + param.Name + ")))";
			}

			return value;
		}

	}
}
