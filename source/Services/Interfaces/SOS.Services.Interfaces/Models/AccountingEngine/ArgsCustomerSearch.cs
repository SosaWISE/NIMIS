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
		public bool ExcludeLeads { get; set; }
		public int PageSize { get; set; }
		public int PageNumber { get; set; }
		#endregion Properties
	}

	public interface IArgsCustomerSearch
	{
		string City { get; set; }
		string StateId { get; set; }
		string PostalCode { get; set; }
		string Email { get; set; }
		string FirstName { get; set; }
		string LastName { get; set; }
		string PhoneNumber { get; set; }
		bool ExcludeLeads { get; set; }
		int PageSize { get; set; }
		int PageNumber { get; set; }
	}
}
