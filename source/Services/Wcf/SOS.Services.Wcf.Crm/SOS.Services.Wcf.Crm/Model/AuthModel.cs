/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 11:48
 * 
 * Description:  Entity used to acquired authentication information.
 *********************************************************************************************************************/

using System.Runtime.Serialization;

namespace SOS.Services.Wcf.Crm.Model
{
	[DataContract]
	public class AuthModel
	{
		#region Properties

		[DataMember]
		public long SessionId { get; set; }

		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public string Password { get; set; }

		#endregion Properties
	
	}
}