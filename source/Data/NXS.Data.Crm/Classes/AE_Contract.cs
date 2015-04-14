using System;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	public partial class AE_Contract
	{
		// Fix Modified/Created Field Names
		[IgnorePropertyAttribute(true)]
		public DateTime ModifiedOn { get { return ModifiedDate; } set { ModifiedDate = value; } }
		[IgnorePropertyAttribute(true)]
		public string ModifiedBy { get { return ModifiedById; } set { ModifiedById = value; } }
		[IgnorePropertyAttribute(true)]
		public DateTime CreatedOn { get { return CreatedDate; } set { CreatedDate = value; } }
		[IgnorePropertyAttribute(true)]
		public string CreatedBy { get { return CreatedById; } set { CreatedById = value; } }
	}
}
