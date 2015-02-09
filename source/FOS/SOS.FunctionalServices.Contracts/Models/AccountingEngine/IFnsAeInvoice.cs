using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SOS.FunctionalServices.Contracts.Models.AccountingEngine
{
	public interface IFnsAeInvoice
	{
		[DataMember]
		IFnsAeInvoiceHeader Header { get; }

		[DataMember]
		List<IFnsAeInvoiceItemView> Items { get; }
	}
}