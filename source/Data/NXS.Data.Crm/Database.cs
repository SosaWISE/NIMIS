using System;
using NXS.Data;

namespace NXS.Data.Crm
{
	public partial class CrmDb : Database<CrmDb>
	{
		public QL_CustomerMasterLeadTable QL_CustomerMasterLeads { get; set; }
		public QL_LeadTable QL_Leads { get; set; }

		public partial class QL_CustomerMasterLeadTable : Table<QL_CustomerMasterLead, Guid>
		{
			public QL_CustomerMasterLeadTable(Database<CrmDb> db) : base(db, "CML", "[dbo].[QL_CustomerMasterLeads]") { }
			public string CustomerMasterLeadID { get { return _alias + "[CustomerMasterLeadID]"; } }
			public string CustomerMasterFileId { get { return _alias + "[CustomerMasterFileId]"; } }
			public string LeadId { get { return _alias + "[LeadId]"; } }
			public string CustomerTypeId { get { return _alias + "[CustomerTypeId]"; } }
		}
		public partial class QL_LeadTable : Table<QL_Lead, long>
		{
			public QL_LeadTable(Database<CrmDb> db) : base(db, "Le", "[dbo].[QL_Leads]") { }
			public string LeadID { get { return _alias + "[LeadID]"; } }
			public string AddressId { get { return _alias + "[AddressId]"; } }
			public string CustomerTypeId { get { return _alias + "[CustomerTypeId]"; } }
			public string CustomerMasterFileId { get { return _alias + "[CustomerMasterFileId]"; } }
			public string DealerId { get { return _alias + "[DealerId]"; } }
			public string LocalizationId { get { return _alias + "[LocalizationId]"; } }
			public string TeamLocationId { get { return _alias + "[TeamLocationId]"; } }
			public string SeasonId { get { return _alias + "[SeasonId]"; } }
			public string SalesRepId { get { return _alias + "[SalesRepId]"; } }
			public string LeadSourceId { get { return _alias + "[LeadSourceId]"; } }
			public string LeadDispositionId { get { return _alias + "[LeadDispositionId]"; } }
			public string LeadDispositionDateChange { get { return _alias + "[LeadDispositionDateChange]"; } }
			public string Salutation { get { return _alias + "[Salutation]"; } }
			public string FirstName { get { return _alias + "[FirstName]"; } }
			public string MiddleName { get { return _alias + "[MiddleName]"; } }
			public string LastName { get { return _alias + "[LastName]"; } }
			public string Suffix { get { return _alias + "[Suffix]"; } }
			public string Gender { get { return _alias + "[Gender]"; } }
			public string SSN { get { return _alias + "[SSN]"; } }
			public string DOB { get { return _alias + "[DOB]"; } }
			public string DL { get { return _alias + "[DL]"; } }
			public string DLStateID { get { return _alias + "[DLStateID]"; } }
			public string Email { get { return _alias + "[Email]"; } }
			public string PhoneHome { get { return _alias + "[PhoneHome]"; } }
			public string PhoneWork { get { return _alias + "[PhoneWork]"; } }
			public string PhoneMobile { get { return _alias + "[PhoneMobile]"; } }
			public string InsideSalesId { get { return _alias + "[InsideSalesId]"; } }
			public string IsActive { get { return _alias + "[IsActive]"; } }
			public string IsDeleted { get { return _alias + "[IsDeleted]"; } }
			public string CreatedOn { get { return _alias + "[CreatedOn]"; } }
			public string CreatedBy { get { return _alias + "[CreatedBy]"; } }
			public string DEX_ROW_TS { get { return _alias + "[DEX_ROW_TS]"; } }
		}

	}
}
