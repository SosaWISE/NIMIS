using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SOS.Lib.RestCake.Attributes;
using SOS.Lib.RestCake.Clients;
using SOS.Lib.RestCake.Metadata;
using SOS.Lib.RestCake.Routing;
using SOS.Lib.RestCake.Util;


namespace SOS.Lib.RestCake
{
	public abstract class RestHttpHandler : RoutedHttpHandler
	{
		private static readonly string VERSION = "JakeTakesTheCake v1.3.0";

		/// <summary>
		/// All of the dictionaries in this class that have "Type" as the first type parameter store information for the class that inherits from this class.
		/// These dictionaries create a sort of "static inherited" data storage.
		/// The value of the "Type" in the outer dictionary is the type of derived class, so that the derived class has it's own Dictionary of storage (the inner dictionary).
		/// 
		/// The inner dictionaries are mappings: A UriTemplate that maps to a specific rest service method in the service class.
		/// </summary>
		private static readonly Dictionary<Type, bool> s_isMetadataInitialized = new Dictionary<Type, bool>();

		private static readonly Dictionary<Type, ServiceMetadata> s_services = new Dictionary<Type, ServiceMetadata>();

		private static readonly Dictionary<Type, Dictionary<string, string>> s_jsProxies = new Dictionary<Type, Dictionary<string, string>>();

		/// <summary>
		/// Serializer settings for individual methods.  If there's no entry for a MethodMetadata key, we fall back to the setting for the service (see below dictionary)
		/// </summary>
		private static readonly Dictionary<MethodMetadata, JsonSerializerSettings> s_methodSerializerSettings = new Dictionary<MethodMetadata, JsonSerializerSettings>();

		/// <summary>
		/// Serializer settings for the whole service class.  If this is null, we'll use whatever we get from GetSerializer().
		/// </summary>
		private static readonly Dictionary<Type, JsonSerializerSettings> s_serviceSerializerSettings = new Dictionary<Type, JsonSerializerSettings>();

		/// <summary>
		/// In ProcessRequest, if the incoming url matches a regex in this dictionary, the normal processing will not occur.  Instead,
		/// the action (the value in the dictionary) will be invoked.
		/// </summary>
		private static readonly Dictionary<Type, Dictionary<Regex, Action<Match, Type, string, HttpContext>>> s_regexOverrides = new Dictionary<Type, Dictionary<Regex, Action<Match, Type, string, HttpContext>>>();


		/// <summary>
		/// Used for locking for thread safety when I first initialize a sub class's metadata.
		/// I don't know the specifics of the ASP.NET runtime, but there was a race condition previously in initializeMetadata(),
		/// that was causing either NullReferenceExceptions or "The given key is not present in the dictionary" exceptions. Hard to track
		/// down, but the lock fixes it.
		/// </summary>
		private static readonly object s_objSync = new object();


		/// <summary>
		/// sendErrorToClientAndCloseResponse() will set this value to true.  This value is specific to each request, as it uses
		/// HttpContext.Current.Items for storage.  This value can be useful in your hosting applications Application_Error handler (in Global.asax).
		/// If it's true, you may want to go ahead and call Server.ClearError(), since the error has been sent to the client and the response stream has already
		/// been closed anyway (if you DON'T clear the error, the asp.net runtime will just try to write a yellow screen of death to the closed reponse stream, though
		/// it doesn't seem to cause any noticeable errors...).  If it's not true, you might want to avoid calling Server.ClearError(), since no error was sent to the client
		/// (this usually means there was an error in RestCake -- possibly (probably?) a bug.  Report it!), and by not clearing the error you'll at least get a yellow
		/// screen of death that hopefully points you in the right direction.
		/// </summary>
		public static bool IsJsonErrorSent
		{
			get
			{
				if (HttpContext.Current.Items["RestCake_IsJsonErrorSent"] == null)
					return false;
				return (bool) HttpContext.Current.Items["RestCake_IsJsonErrorSent"];
			}

			private set { HttpContext.Current.Items["RestCake_IsJsonErrorSent"] = value; }
		}


		private JsonSerializerSettings m_jsonSettings;

		private JsonSerializer __serializer;
		protected JsonSerializer Serializer
		{
			get { return __serializer ?? (__serializer = JsonSerializer.Create(m_jsonSettings)); }
		}

		private HttpResponse m_response;
		public HttpResponse Response
		{
			get { return m_response; }
		}

		private HttpRequest m_request;
		public HttpRequest Request
		{
			get { return m_request; }
		}

		private HttpContext m_context;
		public HttpContext Context
		{
			get { return m_context; }
		}


		private const string JSON_CALLBACK_PARAM_NAME = "jsoncallback";

		private bool? __isJsonp;
		public bool IsJsonp
		{
			// TODO: A plain old contains could cause false positives.  Need to be more specific about this.
			get
			{
				// This just caches the result of the expression, so it's only evaluated once.
				// Subsequent calls will just return __isJsonp, since it'll have a value.
				//return (bool)(__isJsonp ?? (__isJsonp = EverythingAfterRouteUrl.Contains(JSON_CALLBACK_PARAM_NAME)));
				return (bool) (__isJsonp ?? (__isJsonp = !String.IsNullOrWhiteSpace(m_request.QueryString[JSON_CALLBACK_PARAM_NAME])));
			}
		}


		/*
		// TODO: Implement some kind of formatting option in the future.
		public Formatting Formatting { get; set; }

		protected RestHttpHandler()
		{
			Formatting = Formatting.None;
		}
		*/


		// ********************************************************************************
		// *** Private methods ************************************************************
		// ********************************************************************************
		private string getStartJsonp()
		{
			if (!IsJsonp)
				return String.Empty;

			return string.Format("{0}(", m_request.QueryString[JSON_CALLBACK_PARAM_NAME]);
		}

		private string getEndJsonp()
		{
			if (!IsJsonp)
				return String.Empty;

			return ")";
		}

		private void writeJsonResponse(string json)
		{
			m_response.Write(getStartJsonp());
			m_response.ContentType = Constants.ContentTypeJson;
			m_response.Write(json);
			m_response.Write(getEndJsonp());
		}

		private void writeJsonResponse(object obj)
		{
			m_response.Write(getStartJsonp());
			m_response.ContentType = Constants.ContentTypeJson;
			StringWriter stringWriter = new StringWriter();
			Serializer.Serialize(stringWriter, obj);
			m_response.Write(stringWriter.ToString());
			m_response.Write(getEndJsonp());
		}

		private void writeBareResponse(object obj)
		{
			m_response.Write(getStartJsonp());
			m_response.Write(obj);
			m_response.Write(getEndJsonp());
		}


		private void initializeMetadata(Type type)
		{
			// Dirty check (not thread-safe)
			if (s_isMetadataInitialized.ContainsKey(type) && s_isMetadataInitialized[type])
				return;

			lock (s_objSync)
			{
				// Redundant check; thread-safe this time
				if (s_isMetadataInitialized.ContainsKey(type) && s_isMetadataInitialized[type])
					return;

				s_services[type] = new ServiceMetadata(type);

				// Set up a "regex backdoor" for the js and clr clients, and the help page
				s_regexOverrides[type] = new Dictionary<Regex, Action<Match, Type, string, HttpContext>>();
				s_regexOverrides[type][new Regex(@"^/_js\?(?<type>\w+).*$")] = returnJsClientDefinition;
				s_regexOverrides[type][new Regex(@"^/_cs\?type=(?<type>\w+)&namespace=(?<namespace>\w+)$")] = returnClrClientDefinition;
				s_regexOverrides[type][new Regex(@"^/_help")] = returnHelpPage;

				// Set up the js proxies cache for this type
				s_jsProxies[type] = new Dictionary<string, string>();

				// Get serialization settings from [JsonNetSettings] attributes, on both service classes and methods (the attribute is valid on both)
				JsonNetSettings serviceJsonSettings = ReflectionHelper.GetAttribute<JsonNetSettings>(type);
				if (serviceJsonSettings != null)
				{
					JsonSerializerSettings settings = new JsonSerializerSettings()
					{
						ConstructorHandling = serviceJsonSettings.ConstructorHandling,
						DefaultValueHandling = serviceJsonSettings.DefaultValueHandling,
						MissingMemberHandling = serviceJsonSettings.MissingMemberHandling,
						NullValueHandling = serviceJsonSettings.NullValueHandling,
						ObjectCreationHandling = serviceJsonSettings.ObjectCreationHandling,
						PreserveReferencesHandling = serviceJsonSettings.PreserveReferencesHandling,
						ReferenceLoopHandling = serviceJsonSettings.ReferenceLoopHandling,
						Converters = serviceJsonSettings.Converters.ToList()
					};

					s_serviceSerializerSettings[type] = settings;
				}

				foreach(MethodMetadata methodMeta in s_services[type].Methods)
				{
					JsonNetSettings methodJsonSettings = ReflectionHelper.GetAttribute<JsonNetSettings>(methodMeta.Method);
					if (methodJsonSettings != null)
					{
						JsonSerializerSettings settings = new JsonSerializerSettings()
						{
							ConstructorHandling = methodJsonSettings.ConstructorHandling,
							DefaultValueHandling = methodJsonSettings.DefaultValueHandling,
							MissingMemberHandling = methodJsonSettings.MissingMemberHandling,
							NullValueHandling = methodJsonSettings.NullValueHandling,
							ObjectCreationHandling = methodJsonSettings.ObjectCreationHandling,
							PreserveReferencesHandling = methodJsonSettings.PreserveReferencesHandling,
							ReferenceLoopHandling = methodJsonSettings.ReferenceLoopHandling,
							Converters = methodJsonSettings.Converters.ToList()
						};

						s_methodSerializerSettings[methodMeta] = settings;
					}
				}

				s_isMetadataInitialized.Add(type, true);
			}
		}


		/// <summary>
		/// You will notice that every single thrown exception in this class uses this method, including exceptions caught in catch blocks (they are
		/// passed to this method, where they are wrapped in a RestException, with the original exception becoming the inner exception, and then rethrown).
		/// This method not only wraps all of the exceptions in RestExceptions, but it writes the RestException object to the response stream (json serialized),
		/// and then CLOSES the response stream.  By closing the response stream AND rethrowing, we accomplish 2 things.  1, We send the client an appropriately formatted error
		/// (remember that this is a service call.  The client is not expecting a yellow-screen-of-death (YSOD), they are expecting a json string),
		/// and 2, we let the exception bubble up to the hosting application (where it will go to Application_Error and hopefully be logged).
		/// If Application_Error doesn't happen to call Server.ClearError(), the asp.net runtime would attempt to write a YSOD to the response stream, and the response would contain
		/// the serialized RestException (json), with the YSOD tacked on to the end, and it would cause an error in the client.  That's why we close the stream, so that ONLY the
		/// json is written.  It seems hackish, but this is a very important part of RestCake!: Send back only json errors to clients AND let the hosting applications error handling
		/// execute at the same time.
		/// </summary>
		/// <param name="innerException"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		private RestException sendErrorToClientAndCloseResponse(Exception innerException, string message = null)
		{
			RestException restEx = innerException as RestException;
			RestException exceptionToThrow = restEx ?? new RestException(message ?? innerException.Message, innerException);

			// clear anything we may have written to the output stream already
			m_response.Clear();
			m_response.StatusCode = (int)exceptionToThrow.ResponseStatusCode;
			m_response.ContentType = Constants.ContentTypeJson;

			StringBuilder sbuilder = new StringBuilder();
			TextWriter twriter = new StringWriter(sbuilder);
			twriter.Write(getStartJsonp());
			Serializer.Serialize(twriter, exceptionToThrow);
			twriter.Write(getEndJsonp());
			twriter.Close();

			// By flushing and closing the response stream, the content length cannot be properly set by the asp.net runtime, so we have to set it ourselves.
			// That's why we write the json for the exception to a StringBuilder, so we can get the length.
			// We set the content-length, and then write the StringBuidler's contents to the output stream, and then flush it and close it.
			m_response.AddHeader("Content-Length", sbuilder.Length.ToString());

			// By flushing and closing the output stream, it makes it so that a YSOD cannot be sent under any conditions.  Normally, you'd
			// want to Server.Clear() in an error handler to prevent the YSOD, but if the developer doesn't do that, then this will still prevent it.
			m_response.Write(sbuilder.ToString());
			m_response.Flush();
			m_response.Close();
			
			IsJsonErrorSent = true;
			return exceptionToThrow;
		}

		/// <summary>
		/// Overload of above method
		/// </summary>
		/// <param name="message"></param>
		/// <returns></returns>
		private RestException sendErrorToClientAndCloseResponse(string message)
		{
			RestException restEx = new RestException(message);
			return sendErrorToClientAndCloseResponse(restEx);
		}


		private Dictionary<string, string> getRequestParamsIntoDictionary(UriTemplateMatch match, MethodMetadata methodMetadata)
		{
			Dictionary<string, string> prms = new Dictionary<string, string>();

			// TODO: Should these be in ProcessRequest()?  Have them set some private member vars, so the info is available everywhere?

			// Whether or not the posted data is in query string format (if not, we're assuming JSON.  In the future we could support "text/xml" as well)
			string contentType = m_request.ContentType.ToLower();
			bool isFormUrlEncoded = contentType.Contains("application/x-www-form-urlencoded");
			bool isXml = contentType.Contains("text/xml");
			bool isJson = contentType.Contains("application/json");
			var isOctetStream = contentType.Contains("application/octet-stream");

			string postedJson = null;
			JObject postedObj = null;
			bool processedFirstJsonArg = false;

			foreach (ParameterInfo param in methodMetadata.Parameters)
			{
				string sVal;
				// See if the method param is part of the headers (this is a code contribution from "thecutter" (Gerrit) on CodePlex: http://rest.codeplex.com/Thread/View.aspx?ThreadId=234885)
				if (m_request.Headers.AllKeys.Contains(param.Name, StringComparer.CurrentCultureIgnoreCase))
				{
					// Param found in headers, so ASP.NET will have already put the name/value pairs in the Request.Headers bag for us.
					sVal = m_request.Headers[param.Name];
				}
				// See if the method param is part of the UriTemplate or query string
				else if (match.BoundVariables.AllKeys.Contains(param.Name.ToUpper()))
				{
					// Value was part of the uri or query string: content type is irrelevant for this parameter
					sVal = match.BoundVariables[param.Name.ToUpper()];
				}
				else if (isFormUrlEncoded)
				{
					// "x-www-form-urlencoded" means the data was posted as a query string, so ASP.NET will have already put the name/value pairs in the Request.Form bag for us.
					sVal = m_request.Form[param.Name];
				}
				else if (isXml)
				{
					throw sendErrorToClientAndCloseResponse(new NotSupportedException("text/xml is not currently supported by the RestHttpHandler"));
				}
				else if (isJson)
				{
					//
					// This is where it gets tricky.  How we parse the json depends on the number of parameters, their types, and whether the request is wrapped or bare.
					//

					// TODO: Can posted args go before uri and query string args?

					// TODO: need to test how this reacts when nothing is posted.  Will postedJson be null or ""?  What happens when we try to deserialize it?
					if (postedJson == null)
						postedJson = new StreamReader(m_request.InputStream).ReadToEnd();

					// Is the request bare?
					if (methodMetadata.BodyStyle == BodyStyle.Bare || methodMetadata.BodyStyle == BodyStyle.WrappedResponse)
					{
						// If we've already processed a json argument (the string data sent with the PUT, POST or DELETE request), then we need to tell
						// the user that their method needs a wrapped request.
						if (processedFirstJsonArg)
							throw sendErrorToClientAndCloseResponse("If the service method has more than a single argument, the body style must be WebMessageBodyStyle.Wrapped or WebMessageBodyStyle.WrappedRequest");
						processedFirstJsonArg = true;
						sVal = postedJson;
					}
					else
					{
						if (postedObj == null)
						{
							try
							{
								postedObj = JObject.Parse(postedJson);
							}
							catch (JsonSerializationException ex)
							{
								// Potentially a missing default constructor...
								string message = "There was an error deserializing the " + param.Name
									+ " object.  Make sure that the " + param.ParameterType.FullName +
									" class has a default parameterless constructor.";
								throw sendErrorToClientAndCloseResponse(ex, message);
							}
						}

						JProperty property = postedObj.Property(param.Name);
						// We only want the value part of the json.  So if the json was {"name": "Sam"}, we just want "Sam".
						string valueJson = property.Value.ToString(Formatting.None);
						sVal = valueJson;
						// If we're dealing with a string type, let's get rid of the wrapping "s.
						if (param.ParameterType == typeof(String) && sVal == "null")
							sVal = null;
						else if (param.ParameterType == typeof(String))
							sVal = sVal.Substring(1, sVal.Length - 2);
					}
				}
				else
				{
					string message = "RestHttpHandler does not currently support the content type \"" + contentType + "\"";
					throw sendErrorToClientAndCloseResponse(new NotSupportedException(message));
				}
				prms.Add(param.Name, sVal);
			}
			return prms;
		}


		/// <summary>
		/// This method is one of the core pieces of RestCake.
		/// Inputs are the UriTemplateMatch and the service method's metadata.  But what does that mean?
		/// Think about it from the start.  The client made a request with a url.  The ASP.NET routing matched that url to an IHttpHandler,
		/// which happens to be a RestHttpHandler.  So in the first place, we know what class will be handler the request.
		/// Once in the RestHttpHandler, ProcessRequest() determines which specific service method matches the UriTemplate by getting a match (UriTemplateMatch).
		/// From the match, we can get the values of the bound variables (the {} placeholders in the uri template, so something like "{id}", we get the string value
		/// for id in the url).
		/// The second arg is the MethodMetadata.  So now we know WHAT service method we need to call, and we have the argument values we need to pass that method,
		/// but we only have those values AS STRINGS, since this came in via http.
		/// What THIS method does is, based on the types of params of the service method we're calling, it tries to convert the string values to their
		/// correct target types.
		/// This method returns an object[], which should have the correct values (and correctly typed) for the target service method being called.
		/// </summary>
		private object[] getMethodArgs(UriTemplateMatch match, MethodMetadata methodMetadata)
		{
			Dictionary<string, string> prms = getRequestParamsIntoDictionary(match, methodMetadata);

			string[] stringValues = new string[methodMetadata.Parameters.Length];

			for (int i = 0; i < methodMetadata.Parameters.Length; i++)
			{
				ParameterInfo param = methodMetadata.Parameters[i];
				stringValues[i] = prms[param.Name];
			}

			Type[] targetTypes = methodMetadata.Parameters.Select(p => p.ParameterType).ToArray();

			object[] args;
			try
			{
				args = ReflectionHelper.ConvertStringValuesToObjects(stringValues, targetTypes, Serializer);
			}
			catch (JsonSerializationException ex)
			{
				// Potentially a missing default constructor...
				string message = "There was an error converting one of the input values to the service method's argument's target type";
				throw sendErrorToClientAndCloseResponse(ex, message);
			}

			return args;
		}


		/// <summary>
		/// In your rest service, you can override this method and it will be called just before your actual service method is called.
		/// This can be useful if you have some kind of custom security (such as checking an api key in the query string, etc) that you want to enforce.
		/// </summary>
		public virtual bool BeforeServiceMethod(MethodMetadata method, object[] args)
		{
			return true;
		}


		/// <summary>
		/// This is called from <see cref="ProcessRequest" /> once it's determined which method in the derived service class we need to call.
		/// This method uses the metadata we collected about the derived class to (via reflection) call the correct method in the service class,
		/// with the proper values for each of the arguments.
		/// </summary>
		/// <param name="args"></param>
		/// <param name="methodMetadata"></param>
		private void callMethod(object[] args, MethodMetadata methodMetadata)
		{
			if (!BeforeServiceMethod(methodMetadata, args))
				return;

			// The service method itself might throw an exception, so we wrap the Invoke() call to the service method in a try block.
			// This way, with any thrown exception, we can send back a RestException, so that in any client API, error handling is much easier.
			// Also, in the service methods, you can simply throw an exception for any kind of validation error, etc.  Easy on both ends.
			object result;
			try
			{
				// If the service method is static, there's no instance object that we're calling the service method on.
				// If the method is non-static, we use the current IHttpHandler as the instance object, since the current handler is our service class.
				IHttpHandler handler = null;
				if (!methodMetadata.Method.IsStatic)
					handler = m_context.CurrentHandler;
				result = methodMetadata.Method.Invoke(handler, args);
			}
			// Here are all the exceptions that MethodInfo.Invoke() can throw: http://msdn.microsoft.com/en-us/library/a89hcwhh.aspx
			// If certain of those are thrown, it just means there is a bug in RestCake.  Currently I'm trapping for the ones that I would expect
			// (like the user deliberately throws a RestException in a service method because validation failed)
			catch (TargetInvocationException ex)
			{
				// If an exception is thrown in the service method, the call to Invoke() will wrap that exception in a TargetInvocationException.
				// We simply want to unwrap that and throw the original exception.  It's up to the developer to handle it in the Application_Error method (Global.asax)
				Exception actualEx = ex.InnerException;
				
				// This usage is a little bit different.  We want to preserve the stack trace of the thrown exception, because it makes more sense to the hosting
				// application, so we have to be careful to rethrow the ORIGINAL exception, or the stack trace is destroyed (lost).  So we write the actual exception
				// to the client (cause they don't care about reflection), but rethrow the original reflection exception.
				sendErrorToClientAndCloseResponse(actualEx);
				// We don't throw validation exceptions, because we don't want to log them on the server side.  We just return.
				if (!(actualEx is RestValidationException))
					throw;
				return;

				// This might feel more natural:
				// throw sendErrorToClientAndCloseResponse(actualEx);
				// But it's very very bad!!  More so because it LOOKS right.  If we used the above line, we'd see the same result on the client, but the hosting app
				// would see a stack trace that ends in this call to callMethod(), and we'd have no idea what line in the actual service method caused the problem, or
				// even what kind of exception it was.  We'd essentially have no data.
			}
			catch (Exception ex)
			{
				// If an exception is thrown in the target method, that results in a TargetInvocationException (handled above).  Anything else (this catch block)
				// is probably a bug in RestCake (it doesn't handle a certain situation yet), so we'll report it as such.

				throw sendErrorToClientAndCloseResponse(ex, "There was an error in callMethod, probably due to reflection.  This is likely a bug in RestCake, please report it!");
			}

			if (methodMetadata.Method.ReturnType != typeof(void))
			{
				result = BeforeSerializeResult(result);
				if (methodMetadata.BodyStyle == BodyStyle.Wrapped
					|| methodMetadata.BodyStyle == BodyStyle.WrappedResponse)
				{
					// Wrapped response
					StringWriter sw = new StringWriter();
					JsonTextWriter writer = new JsonTextWriter(sw);
					writer.WriteStartObject();
					writer.WritePropertyName(methodMetadata.Method.Name + "Result");

					StringWriter swriter = new StringWriter();
					Serializer.Serialize(swriter, result);
					writer.WriteRaw(swriter.ToString());

					writer.WriteEndObject();
					writeJsonResponse(sw.ToString());
				}
				else
				{
					// Bare response
					string contentType = m_response.ContentType;
					if (String.IsNullOrEmpty(contentType) || contentType.ToLower().Contains("json"))
						writeJsonResponse(result);
					else
						writeBareResponse(result);
				}
			}
		}

		protected virtual object BeforeSerializeResult(object result)
		{
			return result;
		}


		// This static method just calls the instance method
		private static void returnHelpPage(Match match, Type type, string everythingAfterRouteUrl, HttpContext context)
		{
			((RestHttpHandler)context.CurrentHandler).returnHelpPage2();
		}


		private void returnHelpPage2()
		{
			m_response.ContentType = "text/html";
			ServiceMetadata service = new ServiceMetadata(GetType());

			string htmlTemplate = ReflectionHelper.GetTemplateContents("Js.service-help-page.html");

			StringBuilder sbMethods = new StringBuilder();

			foreach(MethodMetadata method in service.Methods)
			{
				string clrArgsList = String.Join(", ", method.Parameters.Select(
					p => ReflectionHelper.GetTypeAsHtml(p.ParameterType) + " " + p.Name)
					.ToArray());

				string jsArgsList = JsClientWriter.getArgsListAsString(method) + "callback, callbackScope";

				Regex rxColorTemplateParams = new Regex(@"{\w+}");
				string uriTemplateHtml = rxColorTemplateParams.Replace(method.UriTemplate, "<span class='param'>$0</span>");

				sbMethods.AppendLine("<tr>")
					.Append("<td>")
					.Append(method.Name)
					.AppendLine("</td>")

					.Append("<td>")
					.Append("(" + jsArgsList + ")")
					.AppendLine("</td>")

					.Append("<td>")
					.Append(ReflectionHelper.GetTypeAsHtml(method.Method.ReturnType))
					.AppendLine("</td>")

					.Append("<td>")
					.Append(method.Verb.ToString("g"))
					.AppendLine("</td>")


					.Append("<td>")
					.Append("(" + clrArgsList + ")")
					.AppendLine("</td>")

					.Append("<td>")
					.Append(uriTemplateHtml)
					.AppendLine("</td>")

					.AppendLine("</tr>");
			}

			StringBuilder sbOtherServices = new StringBuilder();
			sbOtherServices.AppendLine("<strong>Services with active routes hosted in the same process</strong><br />");

			string appPath = m_request.ApplicationPath;
			if (!appPath.EndsWith("/"))
				appPath += "/";

			// We know that the Route is a GenericHandlerRoute<T>, so let's get the RouteUrl string
			string currentServiceRouteUrl = ((dynamic) Route).RouteUrl;

			Assembly serviceAssembly = Assembly.GetAssembly(GetType());
			AssemblyMetadata assemblyMeta = new AssemblyMetadata(serviceAssembly);
			List<ServiceMetadata> routedServices = new List<ServiceMetadata>();

			// Go through each Route in the RouteTable, and see what other RestCake services are hosted...
			foreach(RouteBase route in RouteTable.Routes)
			{
				Type routeType = route.GetType();
				if (routeType.IsGenericType)
				{
					Type[] genArgs = routeType.GetGenericArguments();
					Type serviceType = genArgs[0];

					if (serviceType.BaseType != null && serviceType.BaseType.IsAssignableFrom(typeof(RestHttpHandler)))
					{
						// We use dynamic because it's a generic type.  We don't want to have to create a generic type, with the correct type param...yuck
						// At this point we know it's a GenericHandlerRoute<T>.  Who cares what T is?  I just want the RouteUrl.
						string routeUrl = ((dynamic) route).RouteUrl;

						ServiceMetadata svcMeta = assemblyMeta.Services.Where(svc => svc.Type == serviceType).FirstOrDefault()
							?? new ServiceMetadata(serviceType);
						routedServices.Add(svcMeta);

						// Make sure it's not the current service (don't need to link to the current page from the current page)
						if (routeUrl == currentServiceRouteUrl)
							continue;

						sbOtherServices
							.Append("<tr>")

							.Append("<td>")
							.Append("<a href=\"" + appPath + routeUrl + "/_help\">")
							.Append(appPath + routeUrl)
							.Append("</a>")
							.AppendLine("</td>")

							.Append("<td>")
							.Append(serviceType.FullName)
							.Append("</td>")

							.Append("<td>")
							.Append(svcMeta.ServiceNamespace + "." + svcMeta.ServiceName)
							.Append("</td>")
							.AppendLine("</li>")

							.AppendLine("</tr>");
					}
				}
			}

			if (routedServices.Count == 0)
			{
				sbOtherServices.AppendLine("No other services could be found (at least not with with active routes pointing to them)");
			}
			else
			{
				sbOtherServices.Insert(0, 
					"<table cellpadding='5' cellspacing='0' border='1'>"
					+ "<thead><th>Url</th><th>Service Class</th><th>Service Name</th></thead><tbody>");
				sbOtherServices.AppendLine("</tbody></table>");
			}

			sbOtherServices.AppendLine("<br /><strong>Services found in the current assembly that have no active routes</strong><br />");

			// Find unrouted services in the current assembly
			List<ServiceMetadata> unroutedServices = assemblyMeta.Services.Where(svc => !routedServices.Contains(svc)).ToList();
			if (unroutedServices.Count == 0)
			{
				sbOtherServices.AppendLine("No services were found without routes.");
			}
			else
			{
				sbOtherServices.AppendLine("<table cellpadding='5' cellspacing='0' border='1'>"
					+ "<thead><th>Service Class</th><th>Service Name</th></thead><tbody>");

				foreach (ServiceMetadata unroutedService in unroutedServices)
				{
						sbOtherServices
							.Append("<tr>")

							.Append("<td>")
							.Append(unroutedService.Type.FullName)
							.Append("</td>")

							.Append("<td>")
							.Append(unroutedService.ServiceNamespace + "." + unroutedService.ServiceName)
							.Append("</td>")
							.AppendLine("</li>")

							.AppendLine("</tr>");

				}
			}

			string baseUrl = BaseUrl;
			if (!baseUrl.EndsWith("/"))
				baseUrl += "/";
			htmlTemplate = htmlTemplate
				.Replace("<#= ServiceName #>", service.ServiceName)
				.Replace("<#= ServiceNamespace #>", service.ServiceNamespace)
				.Replace("<#= ServiceBaseUrl #>", baseUrl)
				.Replace("<#= RestCakeVersion #>", VERSION)
				.Replace("<#= MethodRows #>", sbMethods.ToString());
				//.Replace("<#= OtherServices #>", sbOtherServices.ToString())
				//.Replace("<#= AdditionalContent #>", AdditionalHelpPageContent());

			m_response.Write(htmlTemplate);
		}


		/// <summary>
		/// This can be overridden in derived classes to put additional content in the /_help page for a service,
		/// so the service can have custom content.
		/// </summary>
		/// <returns></returns>
		public virtual string AdditionalHelpPageContent()
		{
			return "";
		}


		/// <summary>
		/// This is used for an automatically setup regex override, so it matches the signature for that.
		/// This was set up in <see cref="initializeMetadata" />.
		/// </summary>
		private static void returnClrClientDefinition(Match match, Type type, string everythingAfterRouteUrl, HttpContext context)
		{
			context.Response.ContentType = "text/plain";
			//string query = context.Request.Url.Query.ToLower();
			string clientType = context.Request.QueryString[0];
			string serviceNamespace = context.Request.QueryString[1];

			Assembly serviceAssembly = Assembly.GetAssembly(type);
			AssemblyMetadata assemblyMeta = new AssemblyMetadata(serviceAssembly);
			StringWriter stringWriter = new StringWriter();
			ClientWriterBase clientWriter;

			if (clientType == "plain")
				clientWriter = new ClrClientWriter(stringWriter, serviceNamespace);
			else if (clientType == "restsharp")
				clientWriter = new RestSharpClientWriter(stringWriter);
			else
				throw new ArgumentException("Unsupported csharp client type provided (try \"plain\")");

			clientWriter.WriteClientHeaders();
			foreach (ServiceMetadata cls in assemblyMeta.Services)
				clientWriter.WriteServiceClient(cls);

			context.Response.Write(stringWriter.ToString());
		}


		/// <summary>
		/// This is used for an automatically setup regex override, so it matches the signature for that.
		/// This was set up in <see cref="initializeMetadata" />.
		/// </summary>
		private void returnJsClientDefinition(Match match, Type type, string everythingAfterRouteUrl, HttpContext context)
		{
			context.Response.ContentType = "text/javascript";

			string query = context.Request.Url.Query.ToLower();
			string rawUrl = context.Request.RawUrl.ToLower();

			string proxy;
			if (!s_jsProxies[type].TryGetValue(query, out proxy))
			{
				var serviceAssembly = Assembly.GetAssembly(type);
				var assemblyMeta = new AssemblyMetadata(serviceAssembly);

				var stringWriter = new StringWriter();
				var clientWriter = new JsClientWriter(stringWriter);

				// See if we're supposed to write the base javascript client class, that the client classes derive from
				if (rawUrl.Contains("base=true") || rawUrl.Contains("baseonly=true"))
				{
					clientWriter.WriteJsClientBase();
				}

				// See if we're supposed to write the clients or not (if they specified baseonly, then we don't write the specific client defs, just the base client class)
				if (!rawUrl.Contains("baseonly=true"))
				{
					foreach (
						ServiceMetadata cls in
							rawUrl.Contains("allclasses=true")
								? assemblyMeta.Services
								: assemblyMeta.Services.Where(cls => cls.Type.FullName == type.FullName))
					{
						clientWriter.WriteServiceClient(cls);
					}
				}

				proxy = stringWriter.ToString();
				s_jsProxies[type][query] = proxy;
			}

			proxy = proxy.Replace("$$serviceUrl$$", BaseUrl);
			context.Response.Write(proxy);
		}



		// ********************************************************************************
		// *** Public methods *************************************************************
		// ********************************************************************************

		public string EverythingAfterRouteUrl
		{
			get
			{
				string rawUrl = m_request.RawUrl;
				int ix = rawUrl.IndexOf(BaseUrl);
				string everythingAfter = rawUrl.Substring(ix + BaseUrl.Length);
				return everythingAfter;
			}
		}


		/// <summary>
		/// Defaults to true.  If your REST service uses any state (session ,etc), you should override this in derived classes,
		/// and have it return false.
		/// </summary>
		public override bool IsReusable
		{
			get { return true; }
		}


		/// <summary>
		/// This can be called from an implementing class's static constructor (or anywhere else really, but it
		/// should only be called once per type/regex combo) to register a regex override for that service class.
		/// When a request comes in, if the url of the request matches the regex, the action provided will be called,
		/// instead of the regular processing of ProcessRequest().  (This is actually how the dynamic client definition is
		/// served up)
		/// </summary>
		/// <param name="type"></param>
		/// <param name="regex"></param>
		/// <param name="action"></param>
		public void AddRegexOverride(Type type, Regex regex, Action<Match, Type, string, HttpContext> action)
		{
			initializeMetadata(type);
			s_regexOverrides[type][regex] = action;
		}


		/// <summary>
		/// The priority order for getting the Json.NET serialization settings is:
		/// 1. A [JsonNetSettings] attribute on the service method
		/// 2. A [JsonNetSettings] attribute on the service class
		/// 3. An overridden implementation of GetSerializerSettings() in a service class
		/// 4. The default RestCake settings
		/// </summary>
		/// <returns></returns>
		private void setSerializerSettings()
		{
			Type thisType = GetType();

			// Priority 1. look for a [JsonNetSettings] attribute on the service method.
			// We don't actually do this part here.  There are some scenarios where the serializer is needed before we
			// determine the service method to be called (such as returning an error that the request url doesn't match any
			// of the service methods' UriTemplates, or a bad HTTP verb was used -- that error needs to be serialized).
			// Once we determine the service method to be called, in ProcessRequest(), we'll look for the attribute on the service method.

			// Priority 2. A [JsonNetSettings] attribute on the service class
			if (s_serviceSerializerSettings.ContainsKey(thisType))
			{
				m_jsonSettings = s_serviceSerializerSettings[thisType];
			}
			// Priority 3 & 4. An overridden implementation of GetSerializer() in a service class, or the default implementation (whichever is called via polymorphism)
			else
			{
				m_jsonSettings = GetSerializerSettings();
			}
		}


		/// <summary>
		/// Can be overridden in an implementing class to specify specific JsonSerializer settings to be used.
		/// Note the priority used to determine serialization settings:
		/// 1. A [JsonNetSettings] attribute on the service method
		/// 2. A [JsonNetSettings] attribute on the service class
		/// 3. An overridden implementation of GetSerializerSettings() in a service class
		/// 4. The default RestCake settings (default implementation of GetSerializerSettings())
		/// 
		/// So even if you implement this method, a [JsonNetSettings] attribute on the service class or method will override the results of this method.
		/// </summary>
		public virtual JsonSerializerSettings GetSerializerSettings()
		{
			JsonSerializerSettings settings = new JsonSerializerSettings
			{
				ConstructorHandling = ConstructorHandling.Default,
				ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
				NullValueHandling = NullValueHandling.Include,
				MissingMemberHandling = MissingMemberHandling.Ignore,
				DefaultValueHandling = DefaultValueHandling.Include
			};
			settings.Converters.Add(new IsoDateTimeConverter());
			return settings;
		}


		/// <summary>
		/// This tries to match the Uri of the current request to one of the templates in the service class (the current IHttpHandler).
		/// If it finds a match, it will call that method, with the appropriate arguments.  If it doesn't find a match on the Uri, an error
		/// is returned to the client.
		/// </summary>
		/// <param name="context"></param>
		public override void ProcessRequest(HttpContext context)
		{
			m_request = context.Request;
			m_response = context.Response;
			m_context = context;

			// This can be overridden in individual service methods
			m_response.ContentType = Constants.ContentTypeJson;

			Type thisType = GetType();

			// Ensures that the metadata for all of the service methods in the derived type is populated.
			// We run this for each ProcessRequest, because IIS might have discarded the old AppDomain, etc, meaning all
			// static data is reinitialized, which would cause all of the static storage dictionaries in this class to be emptied.
			initializeMetadata(thisType);

			// Get the JsonSerializerSettings initially.  This might change once we determine a specific service method to call, because that method might
			// have a [JsonNetSettings] attribute on it.
			setSerializerSettings();

			// This is the "regex backdoor" that allows us to have custom actions when a certain url is accessed.
			// If one of the registered regexes matches, the corresponding Action will be invoked, and then this method will return early,
			// and no service methods will attempt to be called.
			foreach(KeyValuePair<Regex, Action<Match, Type, string, HttpContext>> regexOverride in s_regexOverrides[thisType])
			{
				if (regexOverride.Key.IsMatch(EverythingAfterRouteUrl))
				{
					Match m = regexOverride.Key.Match(EverythingAfterRouteUrl);
					regexOverride.Value(m, thisType, EverythingAfterRouteUrl, context);
					return;
				}
			}

			string[] supportedVerbs = Enum.GetNames(typeof (HttpVerb)).Select(v => v.ToUpper()).ToArray();
			string verb = context.Request.HttpMethod.ToUpper();
			if (!supportedVerbs.Contains(verb))
				throw sendErrorToClientAndCloseResponse("Verb \"" + verb + "\" not supported");

			// Filter the list of the service's methods to ones that match the current request's HTTP verb.
			IEnumerable<MethodMetadata> methods = s_services[thisType].Methods.Where(m => m.Verb.ToString("g").ToUpper() == context.Request.HttpMethod.ToUpper());

			string absBaseUrl = RestCakeUtil.ResolveAbsoluteUrl(BaseUrl ?? context.Request.CurrentExecutionFilePath);
			Uri baseUri = new Uri(absBaseUrl);
			UriTemplateMatch match = null;

			// Further filter the list of methods to ones where the UriTemplate is a match
			// (We should actually only match 1.  If we match multiple, that's a logic error, and we'll report that error back to the client).
			methods = methods.Where(m =>
			{
				UriTemplate template = new UriTemplate(m.UriTemplate);
				match = template.Match(baseUri, context.Request.Url);
				return match != null;
			});

			if (methods.Count() == 0)
				// The url of the request did not match any of the service methods' UriTemplates.
				throw sendErrorToClientAndCloseResponse("Bad service path '" + context.Request.PathInfo + "'");
			
			if (methods.Count() > 1)
			{
				// Ambiguous UriTemplates, more than 1 matches.
				string errorMessage = "Multiple UriTemplates match the request's url.  The UriTemplates cannot be ambiguous.  The methods that matched are: ";
				foreach (MethodMetadata m in methods)
					errorMessage += m.Name + ", ";
				throw sendErrorToClientAndCloseResponse(errorMessage);
			}

			MethodMetadata method = methods.First();

			// Let's see if they pass any auth rules
			if ((method.AuthRules != null && !method.AuthRules.DoesPass(context))
				|| (method.Service.AuthRules != null && !method.Service.AuthRules.DoesPass(context)))
			{
				// send a 403 Forbidden
				m_response.StatusCode = (int)HttpStatusCode.Forbidden;
				return;
			}

			// Set the response content type to whatever the method produces
			m_response.ContentType = method.ContentTypeProduces;

			// Now that we know the method, we have to see if it has specific serializer settings.
			// If it does, the attribute on the method always has the highest priority.)
			if (s_methodSerializerSettings.ContainsKey(method))
				m_jsonSettings = s_methodSerializerSettings[method];

			object[] args = getMethodArgs(match, method);
			callMethod(args, method);
		}


		// ********************************************************************************
		// *** Public static methods ******************************************************
		// *** These methods don't neccessarily have anything to do with the specific processing in the RestHttpHandler,
		// *** but this class servers as the best entry point to access these static utility methods, API-wise.
		// ********************************************************************************

		public static bool FormsAuthOrRedirectMessage(HttpContext context, bool checkUrlAccessForPrincipal = true)
		{
			// If something else in the pipeline has already indicated that we should ignore auth altogether, then respect it.
			// See the SkipAuthorizationRulesModule for examples.
			if (context.SkipAuthorization)
				return true;

			if (checkUrlAccessForPrincipal)
			{
				// Make sure the url they're trying to access is actually protected under forms auth.
				// The context.User might be null if the user is not logged in, hence the coalesce with a generic principal.
				IPrincipal genericPrincipal = new GenericPrincipal(new GenericIdentity(""), null);
				if (UrlAuthorizationModule.CheckUrlAccessForPrincipal(context.Request.Path, context.User ?? genericPrincipal,
				                                                      context.Request.HttpMethod))
					// The url is accessible without forms auth, so return true
					return true;
			}

			// The url requires forms auth, so let's see if they have a valid cookie
			FormsAuthenticationTicket ticket = GetFormsAuthTicket(context);
			if (ticket == null || ticket.Expired)
			{
				// They are not logged in via forms auth
				var redirectMessage = new
				                      	{
				                      		_redirect = true,
				                      		_url = FormsAuthentication.LoginUrl
				                      	};
				// Create an HTTP 407 response.
				// Using things like jQuery.ajax(), this will cause the error callback to fire, instead of the success callback,
				// and the error number of very specific so it's easy to trap for.
				context.Response.StatusCode = (int)HttpStatusCode.ProxyAuthenticationRequired;
				context.Response.ContentType = Constants.ContentTypeJson;
				string json = JsonConvert.SerializeObject(redirectMessage);
				context.Response.Write(json);
				context.Response.End();
				return false;
			}
			return true;
		}

		/// <summary>
		/// This will return the decrypted Forms Authentication ticket, or null if it doesn't exist, is invalid, or has expired.
		/// </summary>
		/// <param name="context"></param>
		/// <returns></returns>
		public static FormsAuthenticationTicket GetFormsAuthTicket(HttpContext context)
		{
			HttpCookie formsAuthCookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
			FormsAuthenticationTicket ticket = null;
			if (formsAuthCookie != null)
			{
				try
				{
					ticket = FormsAuthentication.Decrypt(formsAuthCookie.Value);
				}
				catch (Exception)
				{
					ticket = null;
				}
			}
			return ticket;
		}
	}
}
