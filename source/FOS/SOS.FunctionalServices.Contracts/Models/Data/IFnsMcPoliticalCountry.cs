namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsMcPoliticalCountry
	{
		string CountryID { get; set; }
		string CountryName { get; set; }
		string CountryAB { get; set; }
		bool IsActive { get; set; }
	}
}