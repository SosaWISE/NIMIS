using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class HoldFix
	{
		public int AccountHoldID { get; set; }
		public string FixedNote { get; set; }
		public DateTime ModifiedOn { get; set; }

		internal void ToDb(MS_AccountHold item)
		{
			if (item.AccountHoldID != this.AccountHoldID)
				throw new Exception("IDs don't match");
			NXS.Data.VersionException.CheckModifiedOn(item.ModifiedOn, this.ModifiedOn);

			item.FixedNote = this.FixedNote;
		}
	}
}
