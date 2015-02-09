namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsMcPoliticalTimeZone
	{
		int TimeZoneID { get; set; }
		string TimeZoneName { get; set; }
		string TimeZoneAB { get; set; }
		string CentralTime { get; set; }
		int HourDifference { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
	}
}