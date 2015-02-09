using System.Collections.Generic;
using SOS.FunctionalServices.Contracts.Models.AccountingEngine;

namespace SOS.FunctionalServices.Models.AccountingEngine
{
	public class FnsAeInvoice : IFnsAeInvoice
	{
		#region .ctor

		public FnsAeInvoice(IFnsAeInvoiceHeader header) : this(header, new List<IFnsAeInvoiceItemView>()) { }

		public FnsAeInvoice(IFnsAeInvoiceHeader header, List<IFnsAeInvoiceItemView> items)
		{
			Header = header;
			Items = items;
		}
		#endregion .ctor

		#region Properties
		public IFnsAeInvoiceHeader Header { get; private set; }
		public List<IFnsAeInvoiceItemView> Items { get; private set; }

		#endregion Properties
	}
}
