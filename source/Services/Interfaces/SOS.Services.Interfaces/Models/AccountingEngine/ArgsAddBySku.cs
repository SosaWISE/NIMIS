using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class ArgsAddBySku : IArgsAddBySku
	{
		#region .ctor

		public ArgsAddBySku()
		{
		}

		public ArgsAddBySku(long invoiceID, long accountId, string itemSku, int qty, string salesmanID, string technicianID)
		{
			InvoiceID = invoiceID;
			AccountId = accountId;
			ItemSku = itemSku;
			Qty = qty;
			SalesmanID = salesmanID;
			TechnicianID = technicianID;
		}
		#endregion .ctor

		#region Properteis
		public long InvoiceID { get; set; }
		public long AccountId { get; set; }
		public string ItemSku { get; set; }
		public int Qty { get; set; }
		public string SalesmanID { get; set; }
		public string TechnicianID { get; set; }
	    public string BarcodeId { get; set; }

	    #endregion Properteis
	}


	public interface IArgsAddBySku
	{
		[DataMember]
		long InvoiceID { get; set; }

		[DataMember]
		long AccountId { get; set; }

		[DataMember]
		string ItemSku { get; set; }

		[DataMember]
		int Qty { get; set; }

		[DataMember]
		string SalesmanID { get; set; }

		[DataMember]
		string TechnicianID { get; set; }
	}
}
