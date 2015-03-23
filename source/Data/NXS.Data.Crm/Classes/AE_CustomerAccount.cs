using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.Data.Crm
{
	public partial class AE_CustomerAccount
	{
		public AE_Customer Customer { get; set; }
		public MC_Address Address { get; set; }
	}
}
