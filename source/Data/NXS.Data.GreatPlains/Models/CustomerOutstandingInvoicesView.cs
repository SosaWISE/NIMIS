using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NXS.Data.GreatPlains
{
	public partial class CustomerOutstandingInvoicesView
	{
		public decimal BalanceRemaining { get; set; }

		protected override void Loaded()
		{
			base.Loaded();

			this.BalanceRemaining = InvoiceBalance ?? 0M;
		}
	}
}