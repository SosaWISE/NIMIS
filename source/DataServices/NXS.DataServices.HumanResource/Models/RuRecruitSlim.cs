using NXS.Data.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.HumanResource.Models
{
	public class RuRecruitSlim
	{
		public int ID { get; set; }
		public short UserTypeId { get; set; }
		public int? ReportsToId { get; set; }
		public int SeasonId { get; set; }
		public int? TeamId { get; set; }

		internal static RuRecruitSlim FromDb(RU_Recruit item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("user is null");
			}

			var result = new RuRecruitSlim();
			result.ID = item.ID;
			result.UserTypeId = item.UserTypeId;
			result.ReportsToId = item.ReportsToId;
			result.SeasonId = item.SeasonId;
			result.TeamId = item.TeamId;
			return result;
		}
	}
}
