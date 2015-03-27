using NXS.Data.Crm;
using System;

namespace NXS.DataServices.Crm.Models
{
	public class RuTeam
	{
		public int TeamID { get; set; }
		public string Description { get; set; }

		internal static RuTeam FromDb(RU_Team item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("team is null");
			}

			var result = new RuTeam();
			result.TeamID = item.TeamID;
			result.Description = item.Description;
			return result;
		}
	}
}
