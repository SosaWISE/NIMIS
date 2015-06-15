using NXS.Data.Sales;
using System;

namespace NXS.DataServices.Sales.Models
{
	public class ContactInput
	{
		public int ID { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		//public string RepName { get; set; }

		// Note
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int CategoryId { get; set; }
		public int SystemId { get; set; }
		public string Note { get; set; }

		// Address
		public string Address { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

		// Followup
		public DateTime? FollowupOn { get; set; }

		// Tracking
		public decimal SalesRepLatitude { get; set; }
		public decimal SalesRepLongitude { get; set; }

		internal void ToDb(SL_Contact item)
		{
			//if (item.ID != this.ID) throw new Exception("IDs don't match");
			item.Latitude = this.Latitude;
			item.Longitude = this.Longitude;
		}
		internal void ToDb(SL_ContactNote item)
		{
			//if (item.ID != this.ID) throw new Exception("IDs don't match");
			item.FirstName = this.FirstName;
			item.LastName = this.LastName;
			item.CategoryId = this.CategoryId;
			item.SystemId = this.SystemId;
			item.Note = this.Note;
		}
		internal void ToDb(SL_ContactAddress item)
		{
			//if (item.ID != this.ID) throw new Exception("IDs don't match");
			item.Address = this.Address;
			item.Address2 = this.Address2;
			item.State = this.State;
			item.City = this.City;
			item.Zip = this.Zip;
		}
		internal void ToDb(SL_ContactFollowup item)
		{
			//if (item.ID != this.ID) throw new Exception("IDs don't match");
			item.FollowupOn = this.FollowupOn.HasValue ? this.FollowupOn.Value : DateTime.UtcNow;
		}
		//internal void ToDb(SL_Tracking item)
		//{
		//	item.Latitude = this.SalesRepLatitude;
		//	item.Longitude = this.SalesRepLongitude;
		//}
	}
}
