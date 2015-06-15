using System;

namespace NXS.Data.Sales
{
	public class SL_ContactView
	{
		public int ID { get; set; }
		public string RepCompanyID { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		public string RepName { get; set; }

		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? CategoryId { get; set; }
		public int? SystemId { get; set; }
		public string Note { get; set; }

		public string Address { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

		public DateTime FollowupOn { get; set; }
	}
}
