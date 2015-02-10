/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 11:43
 * 
 * Description:  Describes the entitty for all authenticated users in the system.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace SOS.Services.Wcf.Crm.Model
{
	[DataContract]
	public class SosUser
	{
		#region Properties

		[DataMember]
		public long UserId { get; set; }

		[DataMember]
		public Guid SosUid { get; set; }

		[DataMember]
		public string FirstName { get; set; }

		[DataMember]
		public string LastName { get; set; }
		#endregion Properties
	}
}