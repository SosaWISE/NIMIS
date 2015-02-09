/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 11:48
 * 
 * Description:  Entity used to acquired authentication information.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsAcAuthenticationModel : IFnsAcSessionModel
	{
		#region Properties

		[DataMember]
		long AuthenticationID { get; }

		[DataMember]
		new int? UserId { get; }

		[DataMember]
		string Username { get; }

		[DataMember]
		string Password { get; }

		[DataMember]
		DateTime CreatedDate { get; }

		#endregion Properties
	}
}
