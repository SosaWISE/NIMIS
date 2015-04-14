using NXS.Data.Crm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.Crm.Models
{
	public class MsHold
	{
		public long AccountHoldID { get; set; }
		public long AccountId { get; set; }
		public int Catg2Id { get; set; }
		public string HoldDescription { get; set; }
		public string FixedNote { get; set; }
		public string FixedBy { get; set; }
		public DateTime? FixedOn { get; set; }
		public bool IsActive { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime ModifiedOn { get; set; }

		internal static MsHold FromDb(MS_AccountHold item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("hold is null");
			}

			var result = new MsHold();
			result.AccountHoldID = item.AccountHoldID;
			result.AccountId = item.AccountId;
			result.Catg2Id = item.Catg2Id;
			result.HoldDescription = item.HoldDescription;
			result.FixedNote = item.FixedNote;
			result.FixedBy = item.FixedBy;
			result.FixedOn = item.FixedOn;
			result.IsActive = item.IsActive;
			result.CreatedBy = item.CreatedBy;
			result.CreatedOn = item.CreatedOn;
			result.ModifiedBy = item.ModifiedBy;
			result.ModifiedOn = item.ModifiedOn;
			return result;
		}
	}
}
