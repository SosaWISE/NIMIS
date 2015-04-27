using System;

namespace NXS.Data.Crm
{
	public partial class MS_AccountSalesInformation
	{
		public bool IsNew()
		{
			return this.CreatedOn == this.ModifiedOn;
		}
	}
}
