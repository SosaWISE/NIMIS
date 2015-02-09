using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class AeCustomerMasterFileGeneral : IAeCustomerMasterFileGeneral
	{
		#region .ctor
		#endregion .ctor

		#region Properties
		public long CustomerMasterFileID { get;  set; }
		public long? FkId { get; set; }
		public string ResultType { get; set; }
		public string Fullname { get; set; }
		public string City { get;  set; }
		public string Phone { get;  set; }
		public string Email { get;  set; }
		public List<string> AccountTypes { get; set; }
		#endregion Properties
	}

	public interface IAeCustomerMasterFileGeneral
	{
		#region Properties
		[DataMember]
		long CustomerMasterFileID { get; set; }
		[DataMember]
		long? FkId { get; set; }
		[DataMember]
		string ResultType { get; set; }
		[DataMember]
		string Fullname { get; set; }
		[DataMember]
		string City { get; set; }
		[DataMember]
		string Phone { get; set; }
		[DataMember]
		string Email { get; set; }
		[DataMember]
		List<string> AccountTypes { get; set; }
		#endregion Properties
	}
}
