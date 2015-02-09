namespace SOS.FunctionalServices.Contracts.Models.Data
{
	public interface IFnsMcLocalization
	{
		string LocalizationID { get; set; }
		int MSLocalId { get; set; }
		string LocalizationName { get; set; }
		bool IsActive { get; set; }
		bool IsDeleted { get; set; }
	}
}