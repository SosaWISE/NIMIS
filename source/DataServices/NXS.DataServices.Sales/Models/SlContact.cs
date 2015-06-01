using NXS.Data.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Sales.Models
{
	public class SlContact
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
		public DateTime FollowupOn { get; set; }

		private static bool NullCheck(string name, object item, bool nullable)
		{
			if (item == null)
			{
				if (nullable)
					return false;
				else
					throw new Exception(name + " is null");
			}
			return true;
		}

		//internal static SlContact FromDb(SL_Contact item, SL_ContactNote note, SL_ContactAddress address, SL_ContactFollowup followup)//, bool nullable = false)
		internal static SlContact FromDb(SL_Contact item, bool nullable = false)
		{
			if (!NullCheck("Contact", item, nullable))
				return null;

			var result = new SlContact();
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

			// Note
			var note = item.Note;
			if (!NullCheck("Note", item, nullable))
			{
				result.FirstName = note.FirstName;
				result.LastName = note.LastName;
				result.CategoryId = note.CategoryId;
				result.SystemId = note.SystemId;
				result.Note = note.Note;
			}

			// Address
			var address = item.Address;
			if (!NullCheck("Address", address, nullable))
			{
				result.Address = address.Address;
				result.Address2 = address.Address2;
				result.State = address.State;
				result.City = address.City;
				result.Zip = address.Zip;
			}

			// Followup
			var followup = item.Followup;
			if (!NullCheck("Followup", followup, nullable))
			{
				result.FollowupOn = followup.FollowupOn;
			}

			return result;
		}
	}
}
