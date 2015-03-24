using NXS.Data.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm.Models
{
	public class AeCustomer
	{
		public long CustomerID { get; set; }
		public string CustomerTypeId { get; set; }
		public long CustomerMasterFileId { get; set; }
		public int DealerId { get; set; }
		public long AddressId { get; set; }
		public long LeadId { get; set; }
		public string LocalizationId { get; set; }
		public string Prefix { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Postfix { get; set; }
		public string BusinessName { get; set; }
		public string Gender { get; set; }
		public string PhoneHome { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string Email { get; set; }
		public DateTime? DOB { get; set; }
		public string SSN { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		internal static AeCustomer FromDb(AE_Customer item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("customer is null");
			}

			var result = new AeCustomer();
			result.CustomerID = item.CustomerID;
			result.CustomerTypeId = item.CustomerTypeId;
			result.CustomerMasterFileId = item.CustomerMasterFileId;
			result.DealerId = item.DealerId;
			result.AddressId = item.AddressId;
			result.LeadId = item.LeadId;
			result.LocalizationId = item.LocalizationId;
			result.Prefix = item.Prefix;
			result.FirstName = item.FirstName;
			result.MiddleName = item.MiddleName;
			result.LastName = item.LastName;
			result.Postfix = item.Postfix;
			result.BusinessName = item.BusinessName;
			result.Gender = item.Gender;
			result.PhoneHome = item.PhoneHome;
			result.PhoneWork = item.PhoneWork;
			result.PhoneMobile = item.PhoneMobile;
			result.Email = item.Email;
			result.DOB = item.DOB;
			result.SSN = string.IsNullOrEmpty(item.SSN) ? null : SOS.Lib.Util.Cryptography.TripleDES.DecryptString(item.SSN, null); //result.SSN = item.SSN;
			result.Username = item.Username;
			result.Password = item.Password;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;

			return result;
		}
	}
}
