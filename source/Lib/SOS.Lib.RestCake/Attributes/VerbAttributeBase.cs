using System;

namespace SOS.Lib.RestCake.Attributes
{
	/// <summary>
	/// Derivitives of thsi class are essentially a replacement for the WCF [WebGet] and [WebInvoke(Method="(verb)")] attributes.
	/// You can use either on a service method to expose that method through the service.
	/// RestCake *can* use the WCF attributes for a service class, which makes it easier for people to transition existing services.  However, it also provides its own
	/// attributes for those that want to completely embrace RestCake and remove dependencies on System.ServiceModel and all things WCF.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public abstract class VerbAttributeBase : Attribute
	{
		/// <summary>
		/// You may want to specify a specific method name, without having to declare the whole UriTemplate.  This is handy if
		/// you want the default parameter convention (either UrlStyle.Segments or UrlSegment.QueryString), but you don't want to default
		/// to the name of the method (you want to specify a specific method name).
		/// Note that if a UriTemplate is declared, this value is completely ignored.
		/// </summary>
		public string MethodName { get; set; }


		public string UriTemplate { get; set; }

		/// <summary>
		/// Defaults to Bare, just like WCF.
		/// </summary>
		public BodyStyle BodyStyle { get; set; }

		/// <summary>
		/// Default to UrlSegment.
		/// </summary>
		public UrlStyle UrlStyle { get; set; }


		protected VerbAttributeBase()
		{
			// default values
			UriTemplate = null;
			BodyStyle = BodyStyle.Bare;
			UrlStyle = UrlStyle.UriSegments;
		}

	}
}
