using NXS.Data.Crm;
using System;

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
			if (item.ID != ID)
				throw new Exception("IDs don't match");
			item.ID = ID;
			item.PaymentTypeId = PaymentTypeId;
			item.CardTypeId = CardTypeId;
			item.CardNumber = CardNumber;
			item.VerificationValue = VerificationValue;
			item.ExpirationMonth = ExpirationMonth;
			item.ExpirationYear = ExpirationYear;
			item.NameOnCard = NameOnCard;
			item.AccountTypeId = AccountTypeId;
			item.AccountNumber = AccountNumber;
			item.RoutingNumber = RoutingNumber;
			item.NameOnAccount = NameOnAccount;
			item.CheckNumber = CheckNumber;
			item.IsActive = IsActive;
			item.IsDeleted = IsDeleted;
			//item.ModifiedOn = this.ModifiedOn;
			//item.ModifiedBy = this.ModifiedBy;
			//item.CreatedOn = this.CreatedOn;
			//item.CreatedBy = this.CreatedBy;
		}
	}
}
