using NXS.Data.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class SlContactCategory
	{
		public int ID { get; set; }
		public string RepCompanyID { get; set; }
		public string Name { get; set; }
		public short Sequence { get; set; }
		public string Filename { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		internal static SlContactCategory FromDb(SL_ContactCategory item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("ContactCategory is null");
			}

			var result = new SlContactCategory();
			//@NOTE: if not null RealID effectively become the PK
			result.ID = item.RealID.HasValue ? item.RealID.Value : item.ID;
			result.RepCompanyID = item.RepCompanyID;
			result.Name = item.Name;
			result.Sequence = item.Sequence;
			result.Filename = item.Filename;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			return result;
		}
	}
}
