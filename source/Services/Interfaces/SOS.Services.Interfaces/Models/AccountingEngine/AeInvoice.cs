using System.Collections.Generic;
using System.Runtime.Serialization;
using SOS.Services.Interfaces.Models.MonitoringStation;

namespace SOS.Services.Interfaces.Models.AccountingEngine
{
	public class AeInvoice : IAeInvoice
	{
		#region .ctor

		public AeInvoice(AeInvoiceHeader header) : this(header, new List<AeInvoiceItem>(), new MsAccountSalesInformation())
		{
		}

		public AeInvoice(AeInvoiceHeader header, IEnumerable<AeInvoiceItem> items, IMsAccountSalesInformation salesInformation )
		{
			Header = header;
			Items = new List<IAeInvoiceItem>(items);
			SalesInformation = salesInformation;
		}

		public AeInvoice()
		{
			Header = new AeInvoiceHeader();
			Items = new List<IAeInvoiceItem>();
			SalesInformation = new MsAccountSalesInformation();
		}

		#endregion .ctor

		#region Properties
		public IAeInvoiceHeader Header { get; private set; }
		public List<IAeInvoiceItem> Items { get; private set; }
		public IMsAccountSalesInformation SalesInformation { get; private set; }
		#endregion Properties
	}

	public interface IAeInvoice
	{
		[DataMember]
		IAeInvoiceHeader Header { get; }

		[DataMember]
		List<IAeInvoiceItem> Items { get; } 

		[DataMember]
		IMsAccountSalesInformation SalesInformation { get; }
	}
}