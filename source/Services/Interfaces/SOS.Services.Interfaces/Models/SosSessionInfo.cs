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
	[DataContract]
	public class SosSessionInfo : ISosSessionInfo
	{
		#region .ctor
		#endregion .ctor

		#region Properties

		public long SessionId { get; set; }

		public string ApplicationId { get; set; }

		public int? UserId { get; set; }

		public string IPAddress { get; set; }

		public DateTime LastAccessedOn { get; set; }

		public bool SessionTerminated { get; set; }

		public SosCustomer AuthCustomer { get; set; }

		public DateTime CreatedOn { get; set; }

		#endregion Properties
	}

	public class SosSessionInfo<T> : ISosSessionInfo<T>
	{
		#region Properties
		public long SessionId { get; set; }
		public string ApplicationId { get; set; }
		public int? UserId { get; set; }
		public string IPAddress { get; set; }
		public DateTime LastAccessedOn { get; set; }
		public bool SessionTerminated { get; set; }
		public T AuthUser { get; set; }
		public DateTime CreatedOn { get; set; }
		#endregion Properties
	}
}