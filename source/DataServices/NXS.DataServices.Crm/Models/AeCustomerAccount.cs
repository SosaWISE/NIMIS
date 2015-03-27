using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class AeCustomerAccount
	{
		public string CustomerTypeId { get; set; }
		public AeCustomer Customer { get; set; }
		public McAddress Address { get; set; }

		internal static AeCustomerAccount FromDb(AE_CustomerAccount item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("customerAccount is null");
			}

			var result = new AeCustomerAccount();
			result.CustomerTypeId = item.CustomerTypeId;
			result.Customer = AeCustomer.FromDb(item.Customer);
			result.Address = McAddress.FromDb(item.Address);

			return result;
		}

	}
}
