using NXS.Data.Sales;
using System;

namespace NXS.DataServices.Sales.Models
{
	public class CategoryInput
	{
		public int id { get; set; }
		public int userId { get; set; }
		public string name { get; set; }
		public string filename { get; set; }

		public void ToDb(SalesContactCategory item)
		{
			//item.id = item.id;
			item.userId = item.userId;
			item.name = item.name;
			item.filename = item.filename;
			item.sequence = 100; //???
			item.status = "A";
		}
	}
}
