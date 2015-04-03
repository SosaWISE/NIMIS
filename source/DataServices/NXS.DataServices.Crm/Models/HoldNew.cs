using NXS.Data.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm.Models
{
	public class HoldNew
	{
		public long AccountId { get; set; }
		public int Catg2Id { get; set; }
		public string HoldDescription { get; set; }

		internal void ToDb(MS_AccountHold item)
		{
			if (item.AccountHoldID != 0)
				throw new Exception("ID should be 0");

			item.AccountId = this.AccountId;
			item.Catg2Id = this.Catg2Id;
			item.HoldDescription = this.HoldDescription;
		}
	}
}
