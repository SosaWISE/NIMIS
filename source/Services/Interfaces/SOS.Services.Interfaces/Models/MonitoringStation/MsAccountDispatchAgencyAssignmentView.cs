using System;

namespace SOS.Services.Interfaces.Models.MonitoringStation
{
	public class MsAccountDispatchAgencyAssignmentView : IMsAccountDispatchAgencyAssignmentView
	{
		#region Properties
		public long DispatchAgencyAssignmentID { get; set; }
		public int DispatchAgencyId { get; set; }
		public int DispatchAgencyOsId { get; set; }
		public short DispatchAgencyTypeId { get; set; }
		public string DispatchAgencyTypeName { get; set; }
		public long AccountId { get; set; }
		public long IndustryAccountID { get; set; }
		public string CsNo { get; set; }
		public string DispatchAgencyName { get; set; }
		public string Phone1 { get; set; }
		public string CityName { get; set; }
		public string StateAB { get; set; }
		public string ZipCode { get; set; }
		public string PermitNumber { get; set; }
		public DateTime? PermitEffectiveDate { get; set; }
		public DateTime? PermitExpireDate { get; set; }
		public bool IsVerified { get; set; }
		public bool IsActive { get; set; }
		#endregion Properties
	}

	public interface IMsAccountDispatchAgencyAssignmentView
	{
		#region Properties

		long DispatchAgencyAssignmentID { get; set; }
		int DispatchAgencyId { get; set; }
		int DispatchAgencyOsId { get; set; }
		short DispatchAgencyTypeId { get; set; }
		string DispatchAgencyTypeName { get; set; }
		long AccountId { get; set; }
		long IndustryAccountID { get; set; }
		string CsNo { get; set; }
		string DispatchAgencyName { get; set; }
		string Phone1 { get; set; }
		string CityName { get; set; }
		string StateAB { get; set; }
		string ZipCode { get; set; }
		string PermitNumber { get; set; }
		DateTime? PermitEffectiveDate { get; set; }
		DateTime? PermitExpireDate { get; set; }
		bool IsVerified { get; set; }
		bool IsActive { get; set; }

		#endregion Properties
	}

}
