using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel.Web;
using System.Text;
using SOS.Lib.RestCake.Attributes;
using SOS.Lib.RestCake.Util;


namespace SOS.Lib.RestCake.Metadata
{
	/// <summary>
	/// This class helps me group together a few things that are (a), related and (b), expensive to fetch.
	/// The related objects are:
	///		- The MethodInfo that represents the service method
	///		- The BodyStyle of that method
	///		- The UriTemplate of that method
	///		
	///	Because this info is fetched using reflection, I only want to do it once, and I want to do it right away.
	///	See the lookup dictionaries below to see where this data is kept.
	/// </summary>
	public class MethodMetadata
	{
		public ServiceMetadata Service { get; private set; }
		public MethodInfo Method { get; private set; }
		public ParameterInfo[] Parameters { get; private set; }

		public string UriTemplate { get; private set; }

		/// <summary>
		/// Defaults to Bare if not specified (just like WCF)
		/// </summary>
		public BodyStyle BodyStyle { get; private set; }

		public HttpVerb Verb { get; private set; }

		/// <summary>
		/// The content-type that the service method returns.  If not specified, defaults to "application/json; charset=utf-8" (in Util.Constants)
		/// </summary>
		public string ContentTypeProduces { get; private set; }

		/// <summary>
		/// Default to UriSegments if not specified.
		/// </summary>
		public UrlStyle UrlStyle { get; private set; }

		public AuthorizeAttribute AuthRules { get; private set; }

		
		public MethodMetadata(ServiceMetadata service, MethodInfo methodInfo)
		{
			Service = service;
			Method = methodInfo;
			Parameters = Method.GetParameters();

			// Determine the HTTP verb, the BodyStyle, and UriTemplate, depending on which attribute is on the class, and what property values the attribute has
			dynamic serviceAttribute = ReflectionHelper.GetAttribute<GetAttribute>(Method)
				?? ReflectionHelper.GetAttribute<PutAttribute>(Method)
				?? ReflectionHelper.GetAttribute<PostAttribute>(Method)
				?? ReflectionHelper.GetAttribute<DeleteAttribute>(Method)
				?? ReflectionHelper.GetAttribute<WebGetAttribute>(Method)
				?? (dynamic)ReflectionHelper.GetAttribute<WebInvokeAttribute>(Method);

			// The order of some of these are dependent on one another! (UriTemplate depends on UrlStyle, etc)
			Verb = determineVerb(serviceAttribute);
			UrlStyle = determineUrlStyle(serviceAttribute);
			UriTemplate = determineUriTemplateString(serviceAttribute);
			BodyStyle = (BodyStyle)(int)serviceAttribute.BodyStyle;

			ProducesAttribute produces = ReflectionHelper.GetAttribute<ProducesAttribute>(Method);
			ContentTypeProduces = produces == null ? Constants.ContentTypeJson : produces.ContentType;

			AuthRules = ReflectionHelper.GetAttribute<AuthorizeAttribute>(Method);
		}


		private static UrlStyle determineUrlStyle(dynamic serviceAttribute)
		{
			// WCF defaults to query string style url if no uri template is specified.  Let's respect that default for methods using the WcfAttributes.
			if (serviceAttribute is WebGetAttribute || serviceAttribute is WebInvokeAttribute)
				return UrlStyle.QueryString;

			// If it's not a WCF attribute, it's a RestCake attribute, which has a UrlStyle property (defaults to UriSegments).
			return serviceAttribute.UrlStyle;
		}


		private static HttpVerb determineVerb(dynamic serviceAttribute)
		{
			// Determine the HTTP verb by figuring out which attribute is on the method
			HttpVerb verb;
			if (serviceAttribute is GetAttribute || serviceAttribute is WebGetAttribute)
				verb = HttpVerb.Get;
			else if (serviceAttribute is PutAttribute)
				verb = HttpVerb.Put;
			else if (serviceAttribute is PostAttribute)
				verb = HttpVerb.Post;
			else if (serviceAttribute is DeleteAttribute)
				verb = HttpVerb.Delete;
			else if (serviceAttribute is WebInvokeAttribute)
			{
				// A [WebInvoke] with no Method property defaults to POST
				if (String.IsNullOrEmpty(serviceAttribute.Method))
				{
					verb = HttpVerb.Post;
				}
				else
				{
					if (!HttpVerb.TryParse(serviceAttribute.Method, true, out verb))
						throw new ArgumentException("Unknown http verb " + serviceAttribute.Method + " in [WebInvoke] attribute");
				}
			}
			else
				throw new ArgumentException("A MethodMetadata instance can only be created for a method that has a valid [WebGet], [WebInvoke] (WCF), or [Get], [Put], [Post] or [Delete] (RestCake) attribute");
			return verb;
		}


		private string determineUriTemplateString(dynamic serviceAttribute)
		{
			// For a uri template, try to get a MethodName value.  If there isn't one, default to the method's plain name, with no arguments
			string methodName = null;
			if (serviceAttribute is VerbAttributeBase)
				methodName = ((VerbAttributeBase) serviceAttribute).MethodName;
			if (String.IsNullOrWhiteSpace(methodName))
				methodName = Method.Name;
			string uriTemplate = methodName;

			// If a specific uri template is provided, just use it
			if (!String.IsNullOrWhiteSpace(serviceAttribute.UriTemplate))
			{
				uriTemplate = serviceAttribute.UriTemplate;
			}
			// If it's a GET method, and there's no provided uri template, it defaults to the method name with named params that follow the UrlStyle (segments or query string)
			else if (Verb == HttpVerb.Get && String.IsNullOrWhiteSpace(serviceAttribute.UriTemplate))
			{
				if (UrlStyle == UrlStyle.QueryString)
				{
					// Query string style (old hat)
					StringBuilder queryString = new StringBuilder();
					foreach (ParameterInfo param in Method.GetParameters())
						queryString.Append(String.Format("&{0}={{{0}}}", param.Name));
					if (queryString.Length > 0)
						queryString[0] = '?';
					uriTemplate = methodName + queryString;
				}
				else if (UrlStyle == UrlStyle.UriSegments)
				{
					// Uri segment style
					StringBuilder segments = new StringBuilder(methodName);
					foreach (ParameterInfo param in Method.GetParameters())
						segments.Append("/{").Append(param.Name).Append("}");
					uriTemplate = segments.ToString();
				}
				else
				{
					throw new Exception("Unknown UrlStyle value");
				}
			}
			return uriTemplate;
		}


		/// <summary>
		/// All method params.  Both url and data params.
		/// </summary>
		/// <returns></returns>
		public string[] GetParamNames()
		{
			return Parameters.Select(param => param.Name).ToArray();
		}


		/// <summary>
		/// Just params that are part of the UriTemplate
		/// </summary>
		/// <returns></returns>
		public string[] GetUrlParamNames()
		{
			return GetUrlParams().Select(param => param.Name).ToArray();

		}


		public ParameterInfo[] GetUrlParams()
		{
			return Parameters
				.Where(param => UriTemplate.Contains("{" + param.Name + "}")).ToArray();
		}


		/// <summary>
		/// Params that are in the UriTemplate are part of the "MethodUrl".
		/// Other params are sent as data (content). 
		/// </summary>
		/// <returns></returns>
		public string[] GetDataParamNames()
		{
			return Parameters.Select(param => param.Name)
				.Where(param => !UriTemplate.Contains("{" + param + "}")).ToArray();
		}


		public string Name
		{
			get { return Method.Name;  }
		}


		public bool IsWrappedRequest
		{
			get
			{
				return BodyStyle == BodyStyle.Wrapped
					|| BodyStyle == BodyStyle.WrappedRequest;
			}
		}

		public bool IsWrappedResponse
		{
			get
			{
				return BodyStyle == BodyStyle.Wrapped
					|| BodyStyle == BodyStyle.WrappedResponse;
			}
		}
	}
}