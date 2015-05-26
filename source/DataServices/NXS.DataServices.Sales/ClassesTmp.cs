using NXS.Data;
using System;

namespace NXS.Data.Sales
{
	public class SalesAreaAssignment
	{
		public int id { get; set; }
		public int salesAreaId { get; set; }
		public int officeId { get; set; }
		public int salesRepId { get; set; }
		public DateTime startTimestamp { get; set; }
		public DateTime? endTimestamp { get; set; }
		public string status { get; set; }
	}
	public class SalesArea
	{
		public int id { get; set; }
		public string areaName { get; set; }
		public double minLatitude { get; set; }
		public double maxLatitude { get; set; }
		public double minLongitude { get; set; }
		public double maxLongitude { get; set; }
		public string pointData { get; set; }
		public string status { get; set; }
	}
	public class SalesContactAddress
	{
		public int id { get; set; }
		public int salesContactId { get; set; }
		public string address { get; set; }
		public string address2 { get; set; }
		public string city { get; set; }
		public string state { get; set; }
		public string zip { get; set; }
	}
	public class SalesContactCategory
	{
		public int id { get; set; }
		public int userId { get; set; }
		public string name { get; set; }
		public short sequence { get; set; }
		public string filename { get; set; }
		public string status { get; set; }
	}
	public class SalesContactCategoriesBlacklist
	{
		public int categoryId { get; set; }
		public int userId { get; set; }
	}
	public class SalesContactFollowup
	{
		public int id { get; set; }
		public int contactId { get; set; }
		public int salesRepId { get; set; }
		public DateTime followupTimestamp { get; set; }
		public string status { get; set; }
	}
	public class SalesContactNote
	{
		public int id { get; set; }
		public int contactId { get; set; }
		public int salesRepId { get; set; }
		public DateTime noteTimestamp { get; set; }
		public double salesRepLatitude { get; set; }
		public double salesRepLongitude { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public int categoryId { get; set; }
		public int systemId { get; set; }
		public string note { get; set; }
	}
	public class SalesContact
	{
		public int id { get; set; }
		public DateTime contactTimestamp { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
	}
	public class SalesOffice
	{
		public int id { get; set; }
		public string officeCity { get; set; }
		public string officeState { get; set; }
		public string address { get; set; }
		public string status { get; set; }
	}
	public class SalesRep
	{
		public int userId { get; set; }
		public int officeId { get; set; }
		public string status { get; set; }
	}
	public class SalesTracking
	{
		public int id { get; set; }
		public int salesRepId { get; set; }
		public DateTime trackingTimestamp { get; set; }
		public double latitude { get; set; }
		public double longitude { get; set; }
	}
	public class SystemType
	{
		public int id { get; set; }
		public int officeId { get; set; }
		public string companyName { get; set; }
		public short sequence { get; set; }
		public string filename { get; set; }
	}
	public class UserPermission
	{
		public int userId { get; set; }
		public string permissionId { get; set; }
	}
	public partial class User
	{
		public int id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public string PIN { get; set; }
		public DateTime dateJoined { get; set; }
		public string GPID { get; set; }
		public string status { get; set; }
	}
}