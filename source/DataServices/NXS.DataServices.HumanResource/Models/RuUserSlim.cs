using NXS.Data.HumanResource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NXS.DataServices.HumanResource.Models
{
	public class RuUserSlim
	{
		public int ID { get; set; }
		public string FullName { get; set; }
		public string CompanyID { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		public List<RuRecruitSlim> Recruits { get; set; }

		internal static RuUserSlim FromDb(RU_User item, bool nullable = false)
		{
			if (item == null)
			{
				if (nullable)
					return null;
				else
					throw new Exception("user is null");
			}

			var result = new RuUserSlim();
			result.ID = item.ID;
			result.FullName = item.FullName;
			result.CompanyID = item.GPEmployeeId;
			result.IsActive = item.IsActive;
			result.IsDeleted = item.IsDeleted;

			result.Recruits = item.Recruits.ConvertAll(a => RuRecruitSlim.FromDb(a));

			return result;
		}
	}
}
