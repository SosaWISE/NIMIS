using NXS.Data;
using System;

namespace NXS.Data.Sales
{
	public partial class DBase : Database<DBase>
	{
		public static readonly string Database = "NXSE_Sales";
		public readonly Sprocs Sprocs;
		public DBase()
		{
			Sprocs = new Sprocs(this);
		}

		public SL_AreaTable SL_Areas { get; set; }
		public SL_ContactAddressTable SL_ContactAddresses { get; set; }
		public SL_ContactCategoryTable SL_ContactCategories { get; set; }
		public SL_ContactCategoriesBlacklistTable SL_ContactCategoriesBlacklists { get; set; }
		public SL_ContactFollowupTable SL_ContactFollowups { get; set; }
		public SL_ContactNoteTable SL_ContactNotes { get; set; }
		public SL_ContactTable SL_Contacts { get; set; }
		public SL_SalesRepStatisticTable SL_SalesRepStatistics { get; set; }
		public SL_SystemTypeTable SL_SystemTypes { get; set; }
		public SL_TrackingTable SL_Trackings { get; set; }

		public partial class SL_AreaTable : Table<SL_Area, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_AreaTable(DBase db) : base(db, "SlA", "[NXSE_Sales].[dbo].[SL_Areas]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string Name { get { return _alias + "[Name]"; } }
			public string MinLatitude { get { return _alias + "[MinLatitude]"; } }
			public string MinLongitude { get { return _alias + "[MinLongitude]"; } }
			public string MaxLatitude { get { return _alias + "[MaxLatitude]"; } }
			public string MaxLongitude { get { return _alias + "[MaxLongitude]"; } }
			public string PointData { get { return _alias + "[PointData]"; } }
			public string TeamId { get { return _alias + "[TeamId]"; } }
			public string RepCompanyID { get { return _alias + "[RepCompanyID]"; } }
			public string StartTime { get { return _alias + "[StartTime]"; } }
			public string EndTime { get { return _alias + "[EndTime]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class SL_ContactAddressTable : Table<SL_ContactAddress, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_ContactAddressTable(DBase db) : base(db, "SlCA", "[NXSE_Sales].[dbo].[SL_ContactAddresses]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string ContactId { get { return _alias + "[ContactId]"; } }
			public string Address { get { return _alias + "[Address]"; } }
			public string Address2 { get { return _alias + "[Address2]"; } }
			public string City { get { return _alias + "[City]"; } }
			public string State { get { return _alias + "[State]"; } }
			public string Zip { get { return _alias + "[Zip]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class SL_ContactCategoryTable : Table<SL_ContactCategory, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_ContactCategoryTable(DBase db) : base(db, "SlCC", "[NXSE_Sales].[dbo].[SL_ContactCategories]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string RealID { get { return _alias + "[RealID]"; } }
			public string RepCompanyID { get { return _alias + "[RepCompanyID]"; } }
			public string Name { get { return _alias + "[Name]"; } }
			public string Sequence { get { return _alias + "[Sequence]"; } }
			public string Filename { get { return _alias + "[Filename]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
		}
		public partial class SL_ContactCategoriesBlacklistTable : Table<SL_ContactCategoriesBlacklist, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_ContactCategoriesBlacklistTable(DBase db) : base(db, "SlCCB", "[NXSE_Sales].[dbo].[SL_ContactCategoriesBlacklist]", "CategoryId", "int", false) { }
			public string CategoryId { get { return _alias + "[CategoryId]"; } }
			public string RepCompanyID { get { return _alias + "[RepCompanyID]"; } }
		}
		public partial class SL_ContactFollowupTable : Table<SL_ContactFollowup, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_ContactFollowupTable(DBase db) : base(db, "SlCF", "[NXSE_Sales].[dbo].[SL_ContactFollowups]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string ContactId { get { return _alias + "[ContactId]"; } }
			public string FollowupOn { get { return _alias + "[FollowupOn]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class SL_ContactNoteTable : Table<SL_ContactNote, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_ContactNoteTable(DBase db) : base(db, "SlCN", "[NXSE_Sales].[dbo].[SL_ContactNotes]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string ContactId { get { return _alias + "[ContactId]"; } }
			public string FirstName { get { return _alias + "[FirstName]"; } }
			public string LastName { get { return _alias + "[LastName]"; } }
			public string CategoryId { get { return _alias + "[CategoryId]"; } }
			public string SystemId { get { return _alias + "[SystemId]"; } }
			public string Note { get { return _alias + "[Note]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class SL_ContactTable : Table<SL_Contact, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_ContactTable(DBase db) : base(db, "SlC", "[NXSE_Sales].[dbo].[SL_Contacts]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string RepCompanyID { get { return _alias + "[RepCompanyID]"; } }
			public string Latitude { get { return _alias + "[Latitude]"; } }
			public string Longitude { get { return _alias + "[Longitude]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string ModifiedOn { get { return _alias + "[ModifiedOn]"; } }
			public string ModifiedBy { get { return _alias + "[ModifiedBy]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}
		public partial class SL_SalesRepStatisticTable : Table<SL_SalesRepStatistic, long>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_SalesRepStatisticTable(DBase db) : base(db, "SlSRS", "[NXSE_Sales].[dbo].[SL_SalesRepStatistics]", "SalesRepStatisticsID", "bigint", true) { }
			public string SalesRepStatisticsID { get { return _alias + "[SalesRepStatisticsID]"; } }
			public string SalesRepID { get { return _alias + "[SalesRepID]"; } }
			public string StatisticsDate { get { return _alias + "[StatisticsDate]"; } }
			public string SeasonID { get { return _alias + "[SeasonID]"; } }
			public string SalesTeamID { get { return _alias + "[SalesTeamID]"; } }
			public string DealerID { get { return _alias + "[DealerID]"; } }
			public string OfficeID { get { return _alias + "[OfficeID]"; } }
			public string NumberExcellentCreditScores { get { return _alias + "[NumberExcellentCreditScores]"; } }
			public string NumberGoodCreditScores { get { return _alias + "[NumberGoodCreditScores]"; } }
			public string NumberSubCreditScores { get { return _alias + "[NumberSubCreditScores]"; } }
			public string NumberUnapprovedCreditScores { get { return _alias + "[NumberUnapprovedCreditScores]"; } }
			public string NumberCreditReportsPulled { get { return _alias + "[NumberCreditReportsPulled]"; } }
			public string NumberCreditsPassed { get { return _alias + "[NumberCreditsPassed]"; } }
			public string TotalCreditScore { get { return _alias + "[TotalCreditScore]"; } }
			public string CreditPassPercentage { get { return _alias + "[CreditPassPercentage]"; } }
			public string PassAndInstallPercentage { get { return _alias + "[PassAndInstallPercentage]"; } }
			public string NumberCancels { get { return _alias + "[NumberCancels]"; } }
			public string NumberNetSales { get { return _alias + "[NumberNetSales]"; } }
			public string NumberPresurveys { get { return _alias + "[NumberPresurveys]"; } }
			public string NumberPostsurveys { get { return _alias + "[NumberPostsurveys]"; } }
			public string NumberInstallations { get { return _alias + "[NumberInstallations]"; } }
			public string NumberSameDayInstallations { get { return _alias + "[NumberSameDayInstallations]"; } }
			public string SameDayInstallationPercentage { get { return _alias + "[SameDayInstallationPercentage]"; } }
			public string NumberActivationsWaived { get { return _alias + "[NumberActivationsWaived]"; } }
			public string ActivationsWaivedPercentage { get { return _alias + "[ActivationsWaivedPercentage]"; } }
			public string NumberCCPayments { get { return _alias + "[NumberCCPayments]"; } }
			public string NumberACHPayments { get { return _alias + "[NumberACHPayments]"; } }
			public string NumberInvoicePayments { get { return _alias + "[NumberInvoicePayments]"; } }
			public string NumberSystemsOver8Points { get { return _alias + "[NumberSystemsOver8Points]"; } }
			public string NumberFreePointsGivenBySalesRep { get { return _alias + "[NumberFreePointsGivenBySalesRep]"; } }
			public string NumberFreePointsGivenByTech { get { return _alias + "[NumberFreePointsGivenByTech]"; } }
			public string NumberFriendsAndFamily { get { return _alias + "[NumberFriendsAndFamily]"; } }
		}
		public partial class SL_SystemTypeTable : Table<SL_SystemType, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_SystemTypeTable(DBase db) : base(db, "SlST", "[NXSE_Sales].[dbo].[SL_SystemTypes]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string TeamId { get { return _alias + "[TeamId]"; } }
			public string CompanyName { get { return _alias + "[CompanyName]"; } }
			public string Sequence { get { return _alias + "[Sequence]"; } }
			public string Filename { get { return _alias + "[Filename]"; } }
		}
		public partial class SL_TrackingTable : Table<SL_Tracking, int>
		{
			public DBase Db { get { return (DBase)_database; } }
			public SL_TrackingTable(DBase db) : base(db, "SlT", "[NXSE_Sales].[dbo].[SL_Tracking]", "ID", "int", true) { }
			public string ID { get { return _alias + "[ID]"; } }
			public string RepCompanyID { get { return _alias + "[RepCompanyID]"; } }
			public string Latitude { get { return _alias + "[Latitude]"; } }
			public string Longitude { get { return _alias + "[Longitude]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
		}

	}
}
