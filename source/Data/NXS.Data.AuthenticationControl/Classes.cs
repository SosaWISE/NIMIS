using System;
using NXS.Data;

namespace NXS.Data.AuthenticationControl
{
	public partial class AC_KeyValue // AC_KeyValues
	{
		public int ID { get; set; }
		public string KeyValue { get; set; }
		public DateTime CreatedOn { get; set; }
	}
}
