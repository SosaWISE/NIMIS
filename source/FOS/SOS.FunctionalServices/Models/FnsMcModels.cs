using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.Data;

namespace SOS.FunctionalServices.Models
{
	public class FnsMcLocalization : IFnsMcLocalization
	{
		#region .ctor

		public FnsMcLocalization() {}
		public FnsMcLocalization (MC_Localization oLocalization)
		{
			LocalizationID = oLocalization.LocalizationID;
			MSLocalId = oLocalization.MSLocalId;
			LocalizationName = oLocalization.LocalizationName;
			IsActive = oLocalization.IsActive;
			IsDeleted = oLocalization.IsDeleted;
		}


		#endregion .ctor

		#region Implementation of IFnsMcLocalization

		public string LocalizationID { get; set; }
		public int MSLocalId { get; set; }
		public string LocalizationName { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		#endregion Implementation of IFnsMcLocalization
	}

	public class FnsMcPoliticalState : IFnsMcPoliticalState
	{
		#region .ctor
		public FnsMcPoliticalState () {}
		public FnsMcPoliticalState(MC_PoliticalState oPolState)
		{
			StateID = oPolState.StateID;
			CountryId = oPolState.CountryId;
			StateName = oPolState.StateName;
			StateAB = oPolState.StateAB;
			IsActive = oPolState.IsActive;
			IsDeleted = oPolState.IsDeleted;
		}

		#endregion .ctor

		#region Implementation of IFnsMcPoliticalState

		public string StateID { get; set; }
		public string CountryId { get; set; }
		public string StateName { get; set; }
		public string StateAB { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		#endregion Implementation of IFnsMcPoliticalState
	}

	public class FnsMcPoliticalCountry : IFnsMcPoliticalCountry
	{
		#region .ctor
		public FnsMcPoliticalCountry() { }
		public FnsMcPoliticalCountry(MC_PoliticalCountry oPolCntry)
		{
			CountryID = oPolCntry.CountryID;
			CountryName = oPolCntry.CountryName;
			CountryAB = oPolCntry.CountryAB;
			IsActive = oPolCntry.IsActive;
		}

		#endregion .ctor

		#region Implementation of IFnsMcPoliticalCountry

		public string CountryID { get; set; }
		public string CountryName { get; set; }
		public string CountryAB { get; set; }
		public bool IsActive { get; set; }

		#endregion Implementation of IFnsMcPoliticalCountry
	}

	public class FnsMcPoliticalTimeZone : IFnsMcPoliticalTimeZone
	{
		#region .ctor

		public FnsMcPoliticalTimeZone(MC_PoliticalTimeZone oPolTz)
		{
			TimeZoneID = oPolTz.TimeZoneID;
			TimeZoneName = oPolTz.TimeZoneName;
			TimeZoneAB = oPolTz.TimeZoneAB;
			CentralTime = oPolTz.CentralTime;
			HourDifference = oPolTz.HourDifference;
			IsActive = oPolTz.IsActive;
			IsDeleted = oPolTz.IsDeleted;
		}

		public FnsMcPoliticalTimeZone() { }

		#endregion .ctor

		#region Implementation of IFnsMcPoliticalTimeZone

		public int TimeZoneID { get; set; }
		public string TimeZoneName { get; set; }
		public string TimeZoneAB { get; set; }
		public string CentralTime { get; set; }
		public int HourDifference { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		#endregion  Implementation of IFnsMcPoliticalTimeZone
	}
}