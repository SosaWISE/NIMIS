using System.Collections.Generic;
using System.Linq;

namespace WebModules
{
	public class AuthNeeds
	{
		public string ApplicationId { get; set; }
		public string ActionId { get; set; }

		public AuthNeeds(IEnumerable<string> apps, IEnumerable<string> actions)
		{
			ApplicationId = FirstOrDefault(apps);
			ActionId = FirstOrDefault(actions);
		}

		private static string FirstOrDefault(IEnumerable<string> list)
		{
			return list == null ? null : list.FirstOrDefault();
		}
	}
}
