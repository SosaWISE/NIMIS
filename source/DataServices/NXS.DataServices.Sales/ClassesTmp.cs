using NXS.Data;
using System;

namespace NXS.Data.Sales
{
	public class SalesAreaAssignment
	{
		public int ID { get; set; }
		public int SalesAreaId { get; set; }
		public int OfficeId { get; set; }
		public string RepCompanyID { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public bool IsActive { get; set; }
	}
	public class SalesArea
	{
		public int ID { get; set; }
		public string AreaName { get; set; }
		public double MinLatitude { get; set; }
		public double MaxLatitude { get; set; }
		public double MinLongitude { get; set; }
		public double MaxLongitude { get; set; }
		public string PointData { get; set; }
		public bool IsActive { get; set; }
	}
	public class SalesContactAddress
	{
		public int ID { get; set; }
		public int SalesContactId { get; set; }
		public string Address { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }

		internal void ToDb(SL_ContactAddress item)
		{
			//if (item.ID != this.ID) throw new Exception("IDs don't match");

			//item.salesContactId = contact.id;
			item.Address = DatabaseHelper.db_null_or_string(this.Address);
			item.Address2 = DatabaseHelper.db_null_or_string(this.Address2);
			item.City = DatabaseHelper.db_null_or_string(this.City);
			item.State = DatabaseHelper.db_null_or_string(this.State);
			item.Zip = DatabaseHelper.db_null_or_string(this.Zip);
		}
	}
	public class SalesContactCategory
	{
		public int ID { get; set; }
		public string RepCompanyID { get; set; }
		public string Name { get; set; }
		public short Sequence { get; set; }
		public string Filename { get; set; }
		public bool IsActive { get; set; }
	}
	//public class SalesContactCategoriesBlacklist
	//{
	//	public int categoryId { get; set; }
	//	public int userId { get; set; }
	//}
	public class SalesContactFollowup
	{
		public int ID { get; set; }
		//public int contactId { get; set; }
		public DateTime FollowupOn { get; set; }
		public bool IsActive { get; set; }

		internal void ToDb(SL_ContactFollowup item)
		{
			//if (item.ID != this.ID) throw new Exception("IDs don't match");

			//item.contactId = contact.id;
			item.FollowupOn = this.FollowupOn;
			item.IsActive = this.IsActive;
		}
	}
	public class SalesContactNote
	{
		public int ID { get; set; }
		public int ContactId { get; set; }
		//public int salesRepId { get; set; }
		//public DateTime noteTimestamp { get; set; }
		//public decimal SalesRepLatitude { get; set; }
		//public decimal SalesRepLongitude { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int CategoryId { get; set; }
		public int SystemId { get; set; }
		public string Note { get; set; }

		internal void ToDb(SL_ContactNote item)
		{
			//if (item.ID != this.ID) throw new Exception("IDs don't match");

			//item.contactId = contact.id;
			//item.noteTimestamp = DateTime.UtcNow;
			//item.salesRepId = this.salesRepId;
			//item.salesRepLatitude = this.SalesRepLatitude;
			//item.salesRepLongitude = this.salesRepLongitude;
			item.FirstName = DatabaseHelper.db_null_or_string(this.FirstName);
			item.LastName = DatabaseHelper.db_null_or_string(this.LastName);
			item.CategoryId = this.CategoryId;
			item.SystemId = this.SystemId;
			item.Note = DatabaseHelper.db_null_or_string(this.Note);
		}
	}
	//public class SalesOffice
	//{
	//	public int id { get; set; }
	//	public string officeCity { get; set; }
	//	public string officeState { get; set; }
	//	public string address { get; set; }
	//	public string status { get; set; }
	//}
	//public class SalesRep
	//{
	//	public int userId { get; set; }
	//	public int officeId { get; set; }
	//	public string status { get; set; }
	//}
	public class SalesTracking
	{
		//public int id { get; set; }
		//public string RepCompanyID { get; set; }
		//public DateTime trackingTimestamp { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }

		internal void ToDb(SL_Tracking item)
		{
			//item.RepCompanyID = this.RepCompanyID;
			item.Latitude = this.Latitude;
			item.Longitude = this.Longitude;
		}
	}
	public class SystemType
	{
		public int ID { get; set; }
		public int OfficeId { get; set; }
		public string CompanyName { get; set; }
		public short Sequence { get; set; }
		public string Filename { get; set; }
	}
	//public class UserPermission
	//{
	//	public int userId { get; set; }
	//	public string permissionId { get; set; }
	//}
	//public partial class User
	//{
	//	public int ID { get; set; }
	//	public string FirstName { get; set; }
	//	public string LastName { get; set; }
	//	public string Email { get; set; }
	//	public string Password { get; set; }
	//	public string PIN { get; set; }
	//	public DateTime dateJoined { get; set; }
	//	public string GPID { get; set; }
	//	public string status { get; set; }
	//}
}