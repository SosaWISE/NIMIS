namespace SOS.FunctionalServices.Contracts.Models.DoNotCall
{
	public interface IFnsDncPhoneNumber
	{
		string PhoneNumberID { get; set; }
		string AreaCodeId { get; set; }
		string PhoneNumber { get; set; }
	}
}