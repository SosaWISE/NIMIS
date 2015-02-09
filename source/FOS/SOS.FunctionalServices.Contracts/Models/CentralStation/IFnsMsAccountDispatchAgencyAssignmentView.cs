using System;

namespace SOS.FunctionalServices.Contracts.Models.CentralStation
{
	public interface IFnsMsAccountDispatchAgencyAssignmentView
	{
		#region Properties

		long DispatchAgencyAssignmentID { get; }
		int DispatchAgencyId { get; }
		short DispatchAgencyTypeId { get; }
		string DispatchAgencyTypeName { get; }
		long AccountId { get; }
		long IndustryAccountID { get; }
		string CsNo { get; }
		string DispatchAgencyName { get; }
		string Phone1 { get; }
		string CityName { get; set; }
		string StateAB { get; set; }
		string ZipCode { get; set; }
		string PermitNumber { get; }
		DateTime? PermitEffectiveDate { get; }
		DateTime? PermitExpireDate { get; }
		bool IsVerified { get; }
		bool IsActive { get; }

		#endregion Properties
	}
}