using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class ArgsCustomerSearch : IArgsCustomerSearch
	{
		#region Properties
		public string City { get; set; }
		public string StateId { get; set; }
		public string PostalCode { get; set; }
		public string Email { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string PhoneNumber { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
		#endregion Properties
	}

	public interface IArgsCustomerSearch
	{
		[DataMember]
		string City { get; set; }
		[DataMember]
		string StateId { get; set; }
		[DataMember]
		string PostalCode { get; set; }
		[DataMember]
		string Email { get; set; }
		[DataMember]
		string FirstName { get; set; }
		[DataMember]
		string LastName { get; set; }
		[DataMember]
		string PhoneNumber { get; set; }
		[DataMember]
		int PageSize { get; set; }
		[DataMember]
		int PageNumber { get; set; }
	}
}
