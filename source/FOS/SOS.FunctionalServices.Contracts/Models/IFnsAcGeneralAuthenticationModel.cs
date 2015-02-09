/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 09/15/2012
 * Time: 06:52 am
 * 
 * Description:  Entity used to acquired authentication information.
 *********************************************************************************************************************/

using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsAcGeneralAuthenticationModel
	{
		#region Properties
		[DataMember]
		long? AuthenticationID { get; set; }

		[DataMember]
		long SessionID { get; set; }

		[DataMember]
		string AuthenticationToken { get; set; }

		[DataMember]
		string Url { get; set; }

		[DataMember]
		string ApplicationID { get; set; }

		[DataMember]
		string IPAddress { get; set; }

		[DataMember]
		long ID { get; set; }

		#endregion Properties
	}
}