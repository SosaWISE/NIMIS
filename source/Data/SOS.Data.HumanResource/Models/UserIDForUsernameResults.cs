using System;
using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class UserIDForUsernameResults
	{
		[DataMember]
		public Guid QueryKey { get; set; }
		[DataMember]
		public string Username { get; set; }
		[DataMember]
		public int? UserID { get; set; }
	}
}
