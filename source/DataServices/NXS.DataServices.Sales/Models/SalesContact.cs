using NXS.Data.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class SalesContact
	{
		public int ID { get; set; }
		//public DateTime contactTimestamp { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }

		internal void ToDb(SL_Contact item)
		{
			if (item.ID != this.ID) throw new Exception("IDs don't match");
			//item.contactTimestamp = DateTime.UtcNow;
			item.Latitude = this.Latitude;
			item.Longitude = this.Longitude;
		}
	}
}
