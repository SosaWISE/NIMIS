using SOS.FunctionalServices.Contracts.Models;

namespace SOS.FunctionalServices.Models
{
	public class FnsAcGeneralAuthenticationModel : IFnsAcGeneralAuthenticationModel
	{
		public long? AuthenticationID { get; set; }
		public long SessionID { get; set; }
		public string AuthenticationToken { get; set; }
		public string Url { get; set; }
		public string ApplicationID { get; set; }
		public string IPAddress { get; set; }
		public long ID { get; set; }
	}
}
