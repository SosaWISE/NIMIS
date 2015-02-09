/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 09/18/12
 * Time: 06:30 am
 * 
 * Description:  Describes a general authentication token
 *********************************************************************************************************************/

using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.Authentication
{
	[DataContract]
	public class GeneralAuthenticationModel
	{
		[DataMember]
		public long? AuthenticationID { get; set; }

		[DataMember]
		public long? SessionID { get; set; }

		[DataMember]
		public string AuthenticationToken { get; set; }

		[DataMember]
		public string Url { get; set; }

		[DataMember]
		public string ApplicationID { get; set; }

		[DataMember]
		public string IPAddress { get; set; }

		[DataMember]
		public long ID { get; set; }
	}
}
