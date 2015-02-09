using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsCustomerMasterFileGeneral
	{
		[DataMember]
		long CustomerMasterFileID { get; }
		[DataMember]
		long? FkId { get; }
		[DataMember]
		string ResultType { get; }
		[DataMember]
		string Fullname { get; }
		[DataMember]
		string City { get; }
		[DataMember]
		string Phone { get; }
		[DataMember]
		string Email { get; }
		[DataMember]
		List<string> AccountTypes { get; } 
	}
}