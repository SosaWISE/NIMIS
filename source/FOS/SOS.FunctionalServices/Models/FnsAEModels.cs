using System;
using System.Collections.Generic;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models
{
	public class FnsAeCustomerType : IFnsAeCustomerType
	{
		#region .ctor

		public FnsAeCustomerType (){}
		public FnsAeCustomerType (AE_CustomerType oCustomerType)
		{
			CustomerTypeID = oCustomerType.CustomerTypeID;
			CustomerType = oCustomerType.CustomerType;
		}

		#endregion .ctor

		#region Implementation of IFnsAeCustomerType

		public string CustomerTypeID { get; set; }
		public string CustomerType { get; set; }

		#endregion Implementation of IFnsAeCustomerType
	}

	public class FnsAeDealer : IFnsAeDealer
	{
		#region .ctor

		public FnsAeDealer() { }
		public FnsAeDealer(AE_Dealer oDealer)
		{
			DealerID = oDealer.DealerID;
			DealerName = oDealer.DealerName;
			ContactFirstName = oDealer.ContactFirstName;
			ContactLastName = oDealer.ContactLastName;
			ContactEmail = oDealer.ContactEmail;
			PhoneWork = oDealer.PhoneWork;
			PhoneMobile = oDealer.PhoneMobile;
			PhoneFax = oDealer.PhoneFax;
			Address = oDealer.Address;
			Address2 = oDealer.Address2;
			City = oDealer.City;
			StateAB = oDealer.StateAB;
			PostalCode = oDealer.PostalCode;
			PlusFour = oDealer.PlusFour;
			Username = oDealer.Username;
			Password = oDealer.Password;
			IsActive = oDealer.IsActive;
			IsDeleted = oDealer.IsDeleted;
			ModifiedOn = oDealer.ModifiedOn;
			ModifiedBy = oDealer.ModifiedBy;
			CreatedOn = oDealer.CreatedOn;
			CreatedBy = oDealer.CreatedBy;
			DEX_ROW_TS = oDealer.DEX_ROW_TS;
		}

		#endregion .ctor

		#region Implementation of IFnsAeDealer

		public int DealerID { get; set; }
		public string DealerName { get; set; }
		public string ContactFirstName { get; set; }
		public string ContactLastName { get; set; }
		public string ContactEmail { get; set; }
		public string PhoneWork { get; set; }
		public string PhoneMobile { get; set; }
		public string PhoneFax { get; set; }
		public string Address { get; set; }
		public string Address2 { get; set; }
		public string City { get; set; }
		public string StateAB { get; set; }
		public string PostalCode { get; set; }
		public string PlusFour { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime ModifiedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string CreatedBy { get; set; }
		public DateTime DEX_ROW_TS { get; set; }

		#endregion
	}

	public class FnsAePaymentFull : IFnsAePaymentFull
	{
		#region Old Interface Items

		public decimal Amount { get; set; }
		public bool Approved { get; set; }
		public string AuthorizationCode { get; set; }
		public string CardNumber { get; set; }
		public string InvoiceNumber { get; set; }
		public long InvoiceID { get; set; }
		public long PaymentID { get; set; }
		public string PaymentMethod { get; set; }
		public string PurchaseMessageDescription { get; set; }
		public string ResponseCode { get; set; }
		public string TransactionID { get; set; }
		public decimal OriginalTransactionAmount { get; set; }
		public decimal? CurrentTransactionAmount { get; set; }
		public decimal TotalSum { get; set; }
		public decimal TotalDiscount { get; set; }
		public decimal TotalSub { get; set; }
		public decimal SalesAmount { get; set; }
		public decimal TaxAmount { get; set; }
		public long InvoicePaymentJoinID { get; set; }
		public long AccountId { get; set; }
		public bool? TransactionSuccess { get; set; }
		public DateTime DocDate { get; set; }
		public DateTime? PostedDate { get; set; }
		public decimal? ActualTransactionAmount { get; set; }
		public List<IFnsAePaymentItem> ItemsList { get; set; }
		public IFnsAeCustomerInfo BillTo { get; set; }
		public IFnsAeCustomerInfo SoldTo { get; set; }

		#endregion Old Interface Items
	}

	public class FnsAePaymentItem : IFnsAePaymentItem
	{
		#region Implementation of IFnsAePaymentItem

		public int Qty { get; set; }
		public string Skw { get; set; }
		public string LineDescription { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal DiscountPrice { get; set; }
		public decimal TotalLine { get; set; }

		#endregion Implementation of IFnsAePaymentItem
	}

	public class FnsAeCustomerInfo : IFnsAeCustomerInfo
	{
		#region Implementation of IFnsAeCustomerInfo

		public long CustomerID { get; set; }
		public string CustomerName { get; set; }
		public string AddressLine1 { get; set; }
		public string AddressLine2 { get; set; }
		public string Phone { get; set; }

		#endregion Implementation of IFnsAeCustomerInfo
	}
}