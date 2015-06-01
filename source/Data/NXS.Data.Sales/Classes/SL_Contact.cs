using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Data.Sales
{
	public partial class SL_Contact // SL_Contacts
	{
		[IgnorePropertyAttribute(true)]
		public SL_ContactNote Note { get; set; }
		[IgnorePropertyAttribute(true)]
		public SL_ContactAddress Address { get; set; }
		[IgnorePropertyAttribute(true)]
		public SL_ContactFollowup Followup { get; set; }
	}
}
