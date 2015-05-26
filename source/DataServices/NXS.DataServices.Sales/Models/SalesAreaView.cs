using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class SalesAreaView
	{
		public int id { get; set; }
		//public string areaName { get; set; }
		public string pointData { get; set; }
		public double minLatitude { get; set; }
		public double maxLatitude { get; set; }
		public double minLongitude { get; set; }
		public double maxLongitude { get; set; }
		//public string status { get; set; }
		//}

		//public class SalesAreaAssignment
		//{
		//public int id { get; set; }
		//public int salesAreaId { get; set; }
		public int officeId { get; set; }
		public int salesRepId { get; set; }
		public DateTime startTimestamp { get; set; }
		public DateTime? endTimestamp { get; set; }
		//public string status { get; set; }

		public string salesRepName { get; set; }
	}
}
