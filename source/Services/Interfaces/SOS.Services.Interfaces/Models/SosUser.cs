/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 12/17/11
 * Time: 11:43
 * 
 * Description:  Describes the entitty for all authenticated users in the system.
 *********************************************************************************************************************/

using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	[DataContract]
	public class SosUser
	{
		#region Properties

		[DataMember]
		public long UserId { get; set; }

		[DataMember]
		public int DealerUserId { get; set; }

		[DataMember]
		public Guid SosUid { get; set; }

		[DataMember]
		public string SalesRepId { get; set; }

		[DataMember]
		public string Fullname { get; set; }

		[DataMember]
		public string Firstname { get; set; }

		[DataMember]
		public string Lastname { get; set; }

		[DataMember]
		public string Email { get; set; }

		[DataMember]
		public string PhoneWork { get; set; }

		[DataMember]
		public string PhoneCell { get; set; }

		[DataMember]
		public string Username { get; set; }

		[DataMember]
		public DateTime? LastLogin { get; set; }

		[DataMember]
		public long SessionID { get; set; }

		[DataMember]
		public int DealerId { get; set; }

		[DataMember]
		public string DealerName { get; set; }

		[DataMember]
		public int SeasonId { get; set; }

		[DataMember]
		public int TeamLocationId { get; set; }

		#endregion Properties
	}
}