/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 09/15/12
 * Time: 17:23
 * 
 * Description:  Entity contains general authentication data.
 *********************************************************************************************************************/

using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	[DataContract]
	public class SosGeneralAuthenticationInfo : ISosGeneralAuthenticationInfo
	{
		public string AuthenticationToken { get; set; }
		public string Url { get; set; }
	}
}
