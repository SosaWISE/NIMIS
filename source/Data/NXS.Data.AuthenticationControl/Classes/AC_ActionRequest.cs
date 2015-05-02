using System;
using System.Collections.Generic;

namespace NXS.Data.AuthenticationControl
{
	public partial class AC_ActionRequest
	{
		[IgnorePropertyAttribute(true)]
		public string Username { get; set; }
	}
}
