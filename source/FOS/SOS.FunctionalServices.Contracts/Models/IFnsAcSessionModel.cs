/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 17:52
 * 
 * Description:  Session Model information returned for a created instance of a handshake.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsAcSessionModel
	{

		#region Properties
		[DataMember]
		long SessionID { get; }

		[DataMember]
		string ApplicationId { get; }

		[DataMember]
		int? UserId { get; }

		[DataMember]
		string IPAddress { get; }

		[DataMember]
		DateTime LastAccessedOn { get; }

		[DataMember]
		bool SessionTerminated { get; }

		[DataMember]
		DateTime CreatedOn { get; }

		#endregion Properties

	}
}