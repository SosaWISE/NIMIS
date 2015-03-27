using System;
using SOS.FunctionalServices.Contracts.Models.QualifyLead;
using SOS.Data.SosCrm;

namespace SOS.FunctionalServices.Models
{
	public class FnsQlLead : IFnsQlLead
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
		public string DLStateId { get; set; }
		public string Email { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneMobile { get; set; }
		public string ProductSkwId { get; set; }

		public static FnsQlLead FromQL_Lead(QL_Lead lead)
		{
			return new FnsQlLead
			{
				LeadID = lead.LeadID,
				AddressId = lead.AddressId,
				CustomerTypeId = lead.CustomerTypeId,
				CustomerMasterFileId = lead.CustomerMasterFileId,
				DealerId = lead.DealerId,
				LocalizationId = lead.LocalizationId,
				TeamLocationId = lead.TeamLocationId,
				SeasonId = lead.SeasonId,
				SalesRepId = lead.SalesRepId,
				LeadSourceId = lead.LeadSourceId,
				LeadDispositionId = lead.LeadDispositionId,
				LeadDispositionDateChange = lead.LeadDispositionDateChange,
				Salutation = lead.Salutation,
				FirstName = lead.FirstName,
				MiddleName = lead.MiddleName,
				LastName = lead.LastName,
				Suffix = lead.Suffix,
				Gender = lead.Gender,
				SSN = string.IsNullOrEmpty(lead.SSN) ? null : Lib.Util.Cryptography.TripleDES.DecryptString(lead.SSN, null),
				//SSN = lead.SSN,
				DOB = lead.DOB,
				DL = lead.DL,
				DLStateId = lead.DLStateID,
				Email = lead.Email,
				PhoneWork = lead.PhoneWork,
				PhoneMobile = lead.PhoneMobile,
				PhoneHome = lead.PhoneHome,
				ProductSkwId = null,
			};
		}
	}
}
