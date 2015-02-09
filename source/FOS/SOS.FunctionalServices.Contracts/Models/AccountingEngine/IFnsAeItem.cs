using System;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeItem
	{
		[DataMember]
		string ItemID { get; set; }
		[DataMember]
		string ItemTypeId { get; set; }
		[DataMember]
		string TaxOptionId { get; set; }
		[DataMember]
		string VerticalId { get; set; }
		[DataMember]
		string ModelNumber { get; set; }
		[DataMember]
		string ItemSKU { get; set; }
		[DataMember]
		string ItemDesc { get; set; }
		[DataMember]
		decimal Price { get; set; }
		[DataMember]
		decimal Cost { get; set; }
		[DataMember]
		decimal SystemPoints { get; set; }
		[DataMember]
		bool IsCatalogItem { get; set; }
		[DataMember]
		bool IsActive { get; set; }
		[DataMember]
		bool IsDeleted { get; set; }
		[DataMember]
		DateTime ModifiedOn { get; set; }		 
		[DataMember]
		string ModifiedBy { get; set; }
		[DataMember]
		DateTime CreatedOn { get; set; }
		[DataMember]
		string CreatedBy { get; set; }
	}
}