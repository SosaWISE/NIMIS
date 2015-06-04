using NXS.Data.Sales;
using System;

namespace NXS.DataServices.Sales.Models
{
	public class CategoryInput
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Filename { get; set; }

		public void ToDb(SL_ContactCategory item)
		{
			if (item.ID != this.ID) throw new Exception("IDs don't match");
			//item.ID = this.ID;
			item.Name = this.Name;
			item.Filename = this.Filename;
			item.Sequence = 100; //???
			item.IsActive = true;
		}
	}
}
