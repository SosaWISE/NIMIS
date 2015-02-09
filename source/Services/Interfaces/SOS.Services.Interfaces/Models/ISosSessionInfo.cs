/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 17:23
 * 
 * Description:  Entity that contains session information.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	public interface ISosSessionInfo
	{
		#region Properties

		[DataMember]
		long SessionId { get; set; }

		[DataMember]
		string ApplicationId { get; set; }

		[DataMember]
		int? UserId { get; set; }

		[DataMember]
		string IPAddress { get; set; }

		[DataMember]
		DateTime LastAccessedOn { get; set; }

		[DataMember]
		bool SessionTerminated { get; set; }

		[DataMember]
		SosCustomer AuthCustomer { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		#endregion Properties

	}

	public interface ISosSessionInfo<T>
	{
		#region Properties

		[DataMember]
		long SessionId { get; set; }

		[DataMember]
		string ApplicationId { get; set; }

		[DataMember]
		int? UserId { get; set; }

		[DataMember]
		string IPAddress { get; set; }

		[DataMember]
		DateTime LastAccessedOn { get; set; }

		[DataMember]
		bool SessionTerminated { get; set; }

		[DataMember]
		T AuthUser { get; set; }

		[DataMember]
		DateTime CreatedOn { get; set; }

		#endregion Properties

	}
}