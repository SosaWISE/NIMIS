namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsMcPoliticalState
	{
		string StateID { get; set; }
		string CountryId { get; set; }
		string StateName { get; set; }
		string StateAB { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
	}
}