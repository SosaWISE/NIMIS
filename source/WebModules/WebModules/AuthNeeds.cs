using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModules
{
	public class AuthNeeds
	{
		public string ApplicationId { get; set; }
		public string ActionId { get; set; }

		public AuthNeeds(IEnumerable<string> apps, IEnumerable<string> actions)
		{
			this.ApplicationId = FirstOrDefault(apps);
			this.ActionId = FirstOrDefault(actions);
		}

		private static string FirstOrDefault(IEnumerable<string> list)
		{
			return list == null ? null : list.FirstOrDefault();
		}
	}
}
