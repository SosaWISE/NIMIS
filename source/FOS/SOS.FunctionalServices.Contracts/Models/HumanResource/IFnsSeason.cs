using System;

namespace SOS.FunctionalServices.Contracts.Models.HumanResource
{
	public interface IFnsSeason
	{
		int SeasonID { get; set; }
		int DealerId { get; set; }
		int? PreSeasonID { get; set; }
		string SeasonName { get; set; }
		DateTime? StartDate { get; set; }
		DateTime? EndDate { get; set; }
		bool ShowInHiringManager { get; set; }
		bool IsCurrent { get; set; }
		bool IsVisibleToRecruits { get; set; }
		bool IsInsideSales { get; set; }
		bool IsPreseason { get; set; }
		bool IsSummer { get; set; }
		bool IsExtended { get; set; }
		bool IsYearRound { get; set; }
		int ExcellentCreditScoreThreshold { get; set; }
		int PassCreditScoreThreshold { get; set; }
		int SubCreditScoreThreshold { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
		int CreatedByID { get; set; }
		DateTime CreatedDate { get; set; }
		int ModifiedByID { get; set; }
		DateTime ModifiedDate { get; set; }

	}
}