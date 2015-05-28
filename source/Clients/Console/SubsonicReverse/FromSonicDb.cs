using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubsonicReverse
{
	public class ForeignKeyRow
	{
		public string ThisTable { get; set; }
		public string ThisColumn { get; set; }

		public string OtherTable { get; set; }
		public string OtherColumn { get; set; }

		public string ConstraintName { get; set; }
		public string Owner { get; set; }
	}
}
