namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class AeItem : IAeItem
	{
		#region Properties
		public string ItemID { get; set; }
		public string ItemTypeId { get; set; }
		public string TaxOptionId { get; set; }
		public string VerticalId { get; set; }
		public string ModelNumber { get; set; }
		public string ItemSKU { get; set; }
		public string ItemDesc { get; set; }
		public decimal Price { get; set; }
		public decimal Cost { get; set; }
		public decimal SystemPoints { get; set; }
		public bool IsCatalogItem { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		#endregion Properties
	}

	#region Interface

	public interface IAeItem
	{
		string ItemID { get; set; }
		string ItemTypeId { get; set; }
		string TaxOptionId { get; set; }
		string VerticalId { get; set; }
		string ModelNumber { get; set; }
		string ItemSKU { get; set; }
		string ItemDesc { get; set; }
		decimal Price { get; set; }
		decimal Cost { get; set; }
		decimal SystemPoints { get; set; }
		bool IsCatalogItem { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
	}

	#endregion Interface
}
