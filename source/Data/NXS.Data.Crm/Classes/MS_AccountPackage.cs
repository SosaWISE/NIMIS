using System;
using System.Collections.Generic;

namespace NXS.Data.Crm
{
	public partial class MS_AccountPackage
	{
		// one to many
		[IgnorePropertyAttribute(true)]
		public IEnumerable<MS_AccountPackageItem> PackageItems { get; set; }
	}
}
