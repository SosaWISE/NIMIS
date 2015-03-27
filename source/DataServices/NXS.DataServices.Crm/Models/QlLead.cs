using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class QlLead
	{
		public long LeadID { get; set; }
		public long AddressId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public int TeamLocationId { get; set; }
		public int SeasonId { get; set; }
		public string SalesRepId { get; set; }
		public int LeadSourceId { get; set; }
		public int LeadDispositionId { get; set; }
		public DateTime? LeadDispositionDateChange { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string Gender { get; set; }
		public string SSN { get; set; }
		public DateTime? DOB { get; set; }
		public string DL { get; set; }
		public string DLStateID { get; set; }
		public string Email { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public int? InsideSalesId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }

		internal static QlLead FromDb(QL_Lead item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("lead is null");
			}

			var result = new QlLead();
			result.LeadID = item.LeadID;
			result.AddressId = item.AddressId;
			result.CustomerTypeId = item.CustomerTypeId;
			result.CustomerMasterFileId = item.CustomerMasterFileId;
			result.DealerId = item.DealerId;
			result.LocalizationId = item.LocalizationId;
			result.TeamLocationId = item.TeamLocationId;
			result.SeasonId = item.SeasonId;
			result.SalesRepId = item.SalesRepId;
			result.LeadSourceId = item.LeadSourceId;
			result.LeadDispositionId = item.LeadDispositionId;
			result.LeadDispositionDateChange = item.LeadDispositionDateChange;
			result.Salutation = item.Salutation;
			result.FirstName = item.FirstName;
			result.MiddleName = item.MiddleName;
			result.LastName = item.LastName;
			result.Suffix = item.Suffix;
			result.Gender = item.Gender;
			result.SSN = string.IsNullOrEmpty(item.SSN) ? null : SOS.Lib.Util.Cryptography.TripleDES.DecryptString(item.SSN, null); //result.SSN = item.SSN;
			result.DOB = item.DOB;
			result.DL = item.DL;
			result.DLStateID = item.DLStateID;
			result.Email = item.Email;
			result.PhoneHome = item.PhoneHome;
			result.PhoneWork = item.PhoneWork;
			result.PhoneMobile = item.PhoneMobile;
			result.InsideSalesId = item.InsideSalesId;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;
			return result;
		}
	}
}
