using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace System.Web // include it whenever using System.Web even though it is in System.Security.Principal
{
	public static class IIdentityExtensions
	{
		private static readonly Regex domainUsernameRegx = new Regex(@"^.*\\(?<username>\w+)$");
		public static string GetUsername(this System.Security.Principal.IIdentity identity)
		{
			var match = domainUsernameRegx.Match(identity.Name);
			var username = (match != null) ? match.Groups["username"].Value : identity.Name;
			return username;
		}
	}
}
