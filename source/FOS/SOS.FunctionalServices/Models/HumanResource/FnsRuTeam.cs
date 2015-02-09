using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;
using System;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsRuTeam : IFnsRuTeam
	{
		public int TeamID { get; set; }
		public string Description { get; set; }
		public int? CreatedFromTeamId { get; set; }
		public int TeamLocationId { get; set; }
		public int? RoleLocationId { get; set; }
		public int? RegionalManagerRecruitId { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreatedOn { get; set; }
		public string ModifiedBy { get; set; }
		public DateTime? ModifiedOn { get; set; }

		public FnsRuTeam() { }

		public FnsRuTeam(RU_Team item)
		{
			TeamID = item.TeamID;
			Description = item.Description;
			CreatedFromTeamId = item.CreatedFromTeamId;
			TeamLocationId = item.TeamLocationId;
			RoleLocationId = item.RoleLocationId;
			RegionalManagerRecruitId = item.RegionalManagerRecruitId;
			IsActive = item.IsActive;
			IsDeleted = item.IsDeleted;
			CreatedBy = item.CreatedBy;
			CreatedOn = item.CreatedOn;
			ModifiedBy = item.ModifiedBy;
			ModifiedOn = item.ModifiedOn;
		}
	}
}
