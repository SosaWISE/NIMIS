
namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	#region AeCustomerType
	public interface IAeCustomerType
	{
		string CustomerTypeID { get; set; }
		string CustomerType { get; set; }
	}

	public class AeCustomerType : IAeCustomerType
	{
		#region Implementation of IAeCustomerType

		public string CustomerTypeID { get; set; }
		public string CustomerType { get; set; }

		#endregion Implementation of IAeCustomerType
	}

	#endregion AeCustomerType
}
