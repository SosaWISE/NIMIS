using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models
{
	[DataContract]
	public class CmsAddress
	{
		#region Properties

		[DataMember]
		public long AddressId { get; set; }
		[DataMember]
		public int DealerId { get; set; }
		[DataMember]
		public string StreetAddress { get; set; }
		[DataMember]
		public string StreetAddress2 { get; set; }
		[DataMember]
		public string City { get; set; }
		[DataMember]
		public string State { get; set; }
		[DataMember]
		public string PostalCode { get; set; }
		[DataMember]
		public string PhoneNumber { get; set; }
		[DataMember]
		public bool IsActive { get; set; }
		[DataMember]
		public DateTime ModifiedOn { get; set; }
		[DataMember]
		public string ModifiedBy { get; set; }
		[DataMember]
		public DateTime CreatedOn { get; set; }
		[DataMember]
		public string CreatedBy { get; set; }

		public object SessionID { get; set; }

		#endregion Properties
	}
}
