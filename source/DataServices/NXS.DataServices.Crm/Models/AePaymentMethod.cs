using NXS.Data.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm.Models
{
	public class AePaymentMethod
	{
		public int ID { get; set; }
		public string PaymentTypeId { get; set; }
		public int? CardTypeId { get; set; }
		public string CardNumber { get; set; }
		public string VerificationValue { get; set; }
		public int? ExpirationMonth { get; set; }
		public int? ExpirationYear { get; set; }
		public string NameOnCard { get; set; }
		public int? AccountTypeId { get; set; }
		public string AccountNumber { get; set; }
		public string RoutingNumber { get; set; }
		public string NameOnAccount { get; set; }
		public string CheckNumber { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }

		internal static AePaymentMethod FromDb(AE_PaymentMethod item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("payment method is null");
			}

			var result = new AePaymentMethod();
			result.ID = item.ID;
			result.PaymentTypeId = item.PaymentTypeId;
			result.CardTypeId = item.CardTypeId;
			result.CardNumber = item.CardNumber;
			result.VerificationValue = item.VerificationValue;
			result.ExpirationMonth = item.ExpirationMonth;
			result.ExpirationYear = item.ExpirationYear;
			result.NameOnCard = item.NameOnCard;
			result.AccountTypeId = item.AccountTypeId;
			result.AccountNumber = item.AccountNumber;
			result.RoutingNumber = item.RoutingNumber;
			result.NameOnAccount = item.NameOnAccount;
			result.CheckNumber = item.CheckNumber;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;
			result.ModifiedOn = item.ModifiedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.CreatedOn = item.CreatedOn;
			result.CreatedBy = item.CreatedBy;
			return result;
		}

		internal void ToDb(AE_PaymentMethod item)
		{
			if (item.ID != this.ID)
				throw new Exception("IDs don't match");
			item.ID = this.ID;
			item.PaymentTypeId = this.PaymentTypeId;
			item.CardTypeId = this.CardTypeId;
			item.CardNumber = this.CardNumber;
			item.VerificationValue = this.VerificationValue;
			item.ExpirationMonth = this.ExpirationMonth;
			item.ExpirationYear = this.ExpirationYear;
			item.NameOnCard = this.NameOnCard;
			item.AccountTypeId = this.AccountTypeId;
			item.AccountNumber = this.AccountNumber;
			item.RoutingNumber = this.RoutingNumber;
			item.NameOnAccount = this.NameOnAccount;
			item.CheckNumber = this.CheckNumber;
			item.IsActive = this.IsActive;
			item.IsDeleted = this.IsDeleted;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
		}
	}
}
