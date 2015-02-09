using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOS.Services.Interfaces.Models.CmsModels
{
	#region QlLeadBasicView

	public interface IQlLeadBasicInfo
	{
		long LeadID { get; set; }
		long AddressId { get; set; }
		string CustomerTypeId { get; set; }
		long CustomerMasterFileId { get; set; }
		int DealerId { get; set; }
		string LocalizationId { get; set; }
		int TeamLocationId { get; set; }
		int SeasonId { get; set; }
		string SalesRepId { get; set; }
		string Salutation { get; set; }
		string FirstName { get; set; }
		string MiddleName { get; set; }
		string LastName { get; set; }
		string Suffix { get; set; }
		string SSN { get; set; }
		DateTime? DOB { get; set; }
		string DL { get; set; }
		string DLStateID { get; set; }
		string Email { get; set; }
		string PhoneHome { get; set; }
		string PhoneWork { get; set; }
		string PhoneMobile { get; set; }
		string PremisePhone { get; set; }
		string StreetAddress { get; set; }
		string City { get; set; }
		string StateId { get; set; }
		string Postal { get; set; }
		bool IsActive { get; set; }
	}
	public class QlLeadBasicView : IQlLeadBasicInfo
	{
		#region .ctor
		#endregion .ctor

		#region Implementation of IQlLeadBasicInfo

		public long LeadID { get; set; }
		public long AddressId { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public string LocalizationId { get; set; }
		public int TeamLocationId { get; set; }
		public int SeasonId { get; set; }
		public string SalesRepId { get; set; }
		public string Salutation { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Suffix { get; set; }
		public string SSN { get; set; }
		public DateTime? DOB { get; set; }
		public string DL { get; set; }
		public string DLStateID { get; set; }
		public string Email { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string PremisePhone { get; set; }
		public string StreetAddress { get; set; }
		public string City { get; set; }
		public string StateId { get; set; }
		public string Postal { get; set; }
		public bool IsActive { get; set; }

		#endregion Implementation of IQlLeadBasicInfo
	}

	#endregion QlLeadBasicView
}
