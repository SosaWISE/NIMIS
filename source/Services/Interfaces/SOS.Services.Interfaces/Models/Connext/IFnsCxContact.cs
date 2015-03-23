using System;

namespace SOS.FunctionalServices.Contracts.Models.Connext
{
	public interface IFnsCxContact
	{
		long ContactID { get; }
		string ContactTypeId { get; }
		long AddressId { get; }
		int DealerId { get; }
		string LocalizationId { get; }
		int TeamLocationId { get; }
		int SeasonId { get; }
		string SalesRepId { get; }
		string Salutation { get; }
		string FirstName { get; }
		string MiddleName { get; }
		string LastName { get; }
		string Suffix { get; }
		string Gender { get; }
		string SSN { get; }
		DateTime? DOB { get; }
		string Email { get; }
		string PhoneHome { get; }
		string PhoneWork { get; }
		string PhoneMobile { get; }
		string CurrentSystem { get; }
		bool IsActive { get; }
		bool IsDeleted { get; }
		DateTime CreatedOn { get; }
		string CreatedBy { get; }
	}
}