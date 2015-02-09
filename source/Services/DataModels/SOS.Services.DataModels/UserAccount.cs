/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/16/11
 * Time: 08:28
 * 
 * Description:  Describes how the result of service calls interfaces will be.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace SOS.Services.DataModels
{
	[Serializable]
	[DataContract]
	public class UserAccount
	{
		[DataMember(IsRequired = true)]
		public Guid UserAccountId { get; set; }
	}
}
