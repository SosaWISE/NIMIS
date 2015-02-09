/**********************************************************************************************************************
 * User: Andres Sosa
 * Date: 07/08/13
 * Time: 12:21 PM
 * 
 * Description:  Describes a GPS Client information.
 *********************************************************************************************************************/

using System;

namespace SOS.FunctionalServices.Contracts.Models
{
	public interface IFnsAeGpsClient
	{
		#region Properties

		long CustomerID { get; }
		bool? IsCurrent { get; }
		long CustomerMasterFileId { get; }
		string CustomerTypeId { get; }
		string CustomerTypeUi { get; }
		int DealerId { get; }
		string DealerName { get; }
		long AddressId { get; }
		long LeadId { get; }
		string LocalizationId { get; }
		string LocalizationName { get; }
		string Prefix { get; }
		string FirstName { get; }
		string MiddleName { get; }
		string LastName { get; }
		string Postfix { get; }
		string Gender { get; }
		string PhoneHome { get; }
		string PhoneWork { get; }
		string PhoneMobile { get; }
		string Email { get; }
		DateTime? DOB { get; }
		string SSN { get; }
		string Username { get; }
		string Password { get; }
		DateTime? LastLoginOn { get; }
		bool IsActive { get; }
		bool IsDeleted { get; }
		DateTime ModifiedOn { get; }
		string ModifiedBy { get; }
		DateTime CreatedOn { get; }
		string CreatedBy { get; }
		DateTime DexRowTs { get; }


		#endregion Properties
	}
}