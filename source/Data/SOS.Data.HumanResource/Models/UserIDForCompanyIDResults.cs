using System;
using System.Runtime.Serialization;

namespace SOS.Data.HumanResource.Models
{
	[DataContract]
	public class UserIDForCompanyIDResults
	{
		[DataMember]
		public Guid QueryKey { get; set; }
		[DataMember]
		public string CompanyID { get; set; }
		[DataMember]
		public int? UserID { get; set; }
	}
}
