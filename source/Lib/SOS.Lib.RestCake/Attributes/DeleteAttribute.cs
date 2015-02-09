namespace SOS.Lib.RestCake.Attributes
{
	/// <summary>
	/// Applying this attribute on a method in a class with the [ServiceContract] (WCF) or [RestService] (RestCake) attribute will expose
	/// that method to access via the HTTP DELETE verb (as long as a route is set up that will direct a request to that service class)
	/// </summary>
	public class DeleteAttribute : VerbAttributeBase
	{ }
}
