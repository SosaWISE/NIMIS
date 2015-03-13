using NXS.Data.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm.Models
{
	public class AeCustomer
	{
		public static AeCustomer FromDb(AE_Customer item, bool nullable = false)
		{
			if (nullable && item == null)
				return null;

			return new AeCustomer()
			{
			};
		}
	}
}
