namespace SOS.Lib.RestCake
{
	/// <summary>
	/// This is essentially a replacement for System.ServiceModel.Web.WebMessageBodyStyle.
	/// The name has been shortened because the usage of RestCake is very specific, and I don't need a huge redundant name.  Also, I don't want a naming conflict.
	/// RestCake *can* use the WCF attributes for a service class, which makes it easier for people to transition existing services.  However, it also provides its own
	/// attributes for those that want to completely embrace RestCake and remove dependencies on System.ServiceModel and all things WCF.
	/// Like WCF, unless specified otherwise on a method, BodyStyle defaults to Bare.
	/// </summary>
	public enum BodyStyle
	{
		Bare = 0,
		Wrapped,
		WrappedRequest,
		WrappedResponse
	}
}

