using System;

namespace NXS.DataServices.Sales.Models
{
	public class ContactInput
	{
		public int id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string address { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
		public int categoryId { get; set; }
		public int systemId { get; set; }
		public string note { get; set; }
		public DateTime? followup { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
		public double salesRepLatitude { get; set; }
		public double salesRepLongitude { get; set; }
	}
}
