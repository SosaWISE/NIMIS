using System;
using SOS.Data.HumanResource;
using SOS.FunctionalServices.Contracts.Models.HumanResource;

namespace SOS.FunctionalServices.Models.HumanResource
{
	public class FnsSeason : IFnsSeason
	{
		#region .ctor

		public FnsSeason(RU_Season season)
		{
			SeasonID = season.SeasonID;
			DealerId = season.DealerId;
			PreSeasonID = season.PreSeasonID;
			SeasonName = season.SeasonName;
			StartDate = season.StartDate;
			EndDate = season.EndDate;
			ShowInHiringManager = season.ShowInHiringManager;
			IsCurrent = season.IsCurrent;
			IsVisibleToRecruits = season.IsVisibleToRecruits;
			IsInsideSales = season.IsInsideSales;
			IsPreseason = season.IsPreseason;
			IsSummer = season.IsSummer;
			IsExtended = season.IsExtended;
			IsYearRound = season.IsYearRound;
			ExcellentCreditScoreThreshold = season.ExcellentCreditScoreThreshold;
			PassCreditScoreThreshold = season.PassCreditScoreThreshold;
			SubCreditScoreThreshold = season.SubCreditScoreThreshold;
			IsActive = season.IsActive;
			IsDeleted = season.IsDeleted;
			CreatedByID = season.CreatedByID;
			CreatedDate = season.CreatedDate;
			ModifiedByID = season.ModifiedByID;
			ModifiedDate = season.ModifiedDate;
		}
		#endregion .ctor

		#region Properties
		
		public int SeasonID { get; set; }
		public int DealerId { get; set; }
		public int? PreSeasonID { get; set; }
		public string SeasonName { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
		public bool ShowInHiringManager { get; set; }
		public bool IsCurrent { get; set; }
		public bool IsVisibleToRecruits { get; set; }
		public bool IsInsideSales { get; set; }
		public bool IsPreseason { get; set; }
		public bool IsSummer { get; set; }
		public bool IsExtended { get; set; }
		public bool IsYearRound { get; set; }
		public int ExcellentCreditScoreThreshold { get; set; }
		public int PassCreditScoreThreshold { get; set; }
		public int SubCreditScoreThreshold { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public int CreatedByID { get; set; }
		public DateTime CreatedDate { get; set; }
		public int ModifiedByID { get; set; }
		public DateTime ModifiedDate { get; set; }

		#endregion Properties
	}
}
