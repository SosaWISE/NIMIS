using System;

namespace NXS.DataServices.Sales.Models
{
	public class ContactInput
	{
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public int CategoryId { get; set; }
		public int SystemId { get; set; }
		public string Note { get; set; }
		public DateTime? Followup { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public decimal SalesRepLatitude { get; set; }
		public decimal SalesRepLongitude { get; set; }
	}
}
