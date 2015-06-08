using NXS.Data.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class SlSystemType
	{
		public int ID { get; set; }
		//public int TeamId { get; set; }
		public string CompanyName { get; set; }
		//public short Sequence { get; set; }
		public string Filename { get; set; }

		internal static SlSystemType FromDb(SL_SystemType item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("SystemType is null");
			}

			var result = new SlSystemType();
			result.ID = item.ID;
			result.CompanyName = item.CompanyName;
			result.Filename = item.Filename;
			return result;
		}
	}
}
