using NXS.Data.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class SlContactView
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

		internal static SlContactView FromDb(SL_ContactView item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("Contact is null");
			}

			var result = new SlContactView();

			result.ID = item.ID;
			result.RepCompanyID = item.RepCompanyID;
			result.Latitude = item.Latitude;
			result.Longitude = item.Longitude;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;

			result.RepName = item.RepName;

			result.FirstName = item.FirstName;
			result.LastName = item.LastName;
			result.CategoryId = item.CategoryId;
			result.SystemId = item.SystemId;
			result.Note = item.Note;

			result.Address = item.Address;
			result.Address2 = item.Address2;
			result.City = item.City;
			result.State = item.State;
			result.Zip = item.Zip;

			result.FollowupOn = item.FollowupOn;

			return result;
		}
	}
}
