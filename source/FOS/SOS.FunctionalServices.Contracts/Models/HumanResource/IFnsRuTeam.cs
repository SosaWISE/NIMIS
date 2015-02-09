using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsRuTeam
	{
		int TeamID { get; set; }
		string Description { get; set; }
		int? CreatedFromTeamId { get; set; }
		int TeamLocationId { get; set; }
		int? RoleLocationId { get; set; }
		int? RegionalManagerRecruitId { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		string CreatedBy { get; set; }
		DateTime? CreatedOn { get; set; }
		string ModifiedBy { get; set; }
		DateTime? ModifiedOn { get; set; }
	}
}
