using NXS.Data;
using System;

namespace NXS.Data.Sales
{
	public partial class SL_AreaAssignment // SL_AreaAssignments
	{
		public int ID { get; set; }
		public int AreaId { get; set; }
		public int OfficeId { get; set; }
		public string RepCompanyID { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	public partial class SL_Area // SL_Areas
	{
		public int ID { get; set; }
		public string AreaName { get; set; }
		public decimal MinLatitude { get; set; }
		public decimal MinLongitude { get; set; }
		public decimal MaxLatitude { get; set; }
		public decimal MaxLongitude { get; set; }
		public string PointData { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	public partial class SL_ContactAddress // SL_ContactAddresses
	{
		public int ID { get; set; }
		public int ContactId { get; set; }
		public string Address { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Zip { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	public partial class SL_ContactCategory // SL_ContactCategories
	{
		public int ID { get; set; }
		public string RepCompanyID { get; set; }
		public string Name { get; set; }
		public short Sequence { get; set; }
		public string Filename { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
	}
	public partial class SL_ContactCategoriesBlacklist // SL_ContactCategoriesBlacklist
	{
		[IgnorePropertyAttribute(true)] public int ID { get { return CategoryId; } set { CategoryId = value; } }
		public int CategoryId { get; set; }
		public string RepCompanyID { get; set; }
	}
	public partial class SL_ContactFollowup // SL_ContactFollowups
	{
		public int ID { get; set; }
		public int ContactId { get; set; }
		public DateTime FollowupOn { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	public partial class SL_ContactNote // SL_ContactNotes
	{
		public int ID { get; set; }
		public int ContactId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public int? CategoryId { get; set; }
		public int? SystemId { get; set; }
		public string Note { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
	public partial class SL_Contact // SL_Contacts
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
	}
	public partial class SL_SalesRepStatistic // SL_SalesRepStatistics
	{
		[IgnorePropertyAttribute(true)] public long ID { get { return SalesRepStatisticsID; } set { SalesRepStatisticsID = value; } }
		public long SalesRepStatisticsID { get; set; }
		public string SalesRepID { get; set; }
		public DateTime StatisticsDate { get; set; }
		public int? SeasonID { get; set; }
		public int? SalesTeamID { get; set; }
		public int? DealerID { get; set; }
		public int? OfficeID { get; set; }
		public int? NumberExcellentCreditScores { get; set; }
		public int? NumberGoodCreditScores { get; set; }
		public int? NumberSubCreditScores { get; set; }
		public int? NumberUnapprovedCreditScores { get; set; }
		public int? NumberCreditReportsPulled { get; set; }
		public int? NumberCreditsPassed { get; set; }
		public int? TotalCreditScore { get; set; }
		public int? CreditPassPercentage { get; set; }
		public int? PassAndInstallPercentage { get; set; }
		public int? NumberCancels { get; set; }
		public int? NumberNetSales { get; set; }
		public int? NumberPresurveys { get; set; }
		public int? NumberPostsurveys { get; set; }
		public int? NumberInstallations { get; set; }
		public int? NumberSameDayInstallations { get; set; }
		public int? SameDayInstallationPercentage { get; set; }
		public int? NumberActivationsWaived { get; set; }
		public int? ActivationsWaivedPercentage { get; set; }
		public int? NumberCCPayments { get; set; }
		public int? NumberACHPayments { get; set; }
		public int? NumberInvoicePayments { get; set; }
		public int? NumberSystemsOver8Points { get; set; }
		public decimal? NumberFreePointsGivenBySalesRep { get; set; }
		public decimal? NumberFreePointsGivenByTech { get; set; }
		public int? NumberFriendsAndFamily { get; set; }
	}
	public partial class SL_SystemType // SL_SystemTypes
	{
		public int ID { get; set; }
		public int OfficeId { get; set; }
		public string CompanyName { get; set; }
		public short Sequence { get; set; }
		public string Filename { get; set; }
	}
	public partial class SL_Tracking // SL_Tracking
	{
		public int ID { get; set; }
		public string RepCompanyID { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
	}
}
