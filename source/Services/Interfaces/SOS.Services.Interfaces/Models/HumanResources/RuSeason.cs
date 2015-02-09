using System;
using System.Runtime.Serialization;

namespace SOS.Services.Interfaces.Models.HumanResources
{
	public class RuSeason : IRuSeason
	{
		#region Properties

		public int SeasonID { get; set; }
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

	public interface IRuSeason
	{
		[DataMember]
		int SeasonID { get; set; }
		[DataMember]
		int? PreSeasonID { get; set; }
		[DataMember]
		string SeasonName { get; set; }
		[DataMember]
		DateTime? StartDate { get; set; }
		[DataMember]
		DateTime? EndDate { get; set; }
		[DataMember]
		bool ShowInHiringManager { get; set; }
		[DataMember]
		bool IsCurrent { get; set; }
		[DataMember]
		bool IsVisibleToRecruits { get; set; }
		[DataMember]
		bool IsInsideSales { get; set; }
		[DataMember]
		bool IsPreseason { get; set; }
		[DataMember]
		bool IsSummer { get; set; }
		[DataMember]
		bool IsExtended { get; set; }
		[DataMember]
		bool IsYearRound { get; set; }
		[DataMember]
		int ExcellentCreditScoreThreshold { get; set; }
		[DataMember]
		int PassCreditScoreThreshold { get; set; }
		[DataMember]
		int SubCreditScoreThreshold { get; set; }
		[DataMember]
		bool IsActive { get; set; }
		[DataMember]
		bool IsDeleted { get; set; }
		[DataMember]
		int CreatedByID { get; set; }
		[DataMember]
		DateTime CreatedDate { get; set; }
		[DataMember]
		int ModifiedByID { get; set; }
		[DataMember]
		DateTime ModifiedDate { get; set; }
	}
}
