using System;
using SOS.Data.SosCrm;
using SOS.FunctionalServices.Contracts.Models.CentralStation;

namespace SOS.FunctionalServices.Models.CentralStation
{
	public class FnsMsAccountDispatchAgencyAssignmentView : IFnsMsAccountDispatchAgencyAssignmentView
	{
		#region .ctor

		public FnsMsAccountDispatchAgencyAssignmentView(MS_AccountDispatchAgencyAssignmentView view)
		{
			DispatchAgencyAssignmentID = view.DispatchAgencyAssignmentID;
			DispatchAgencyId = view.DispatchAgencyId;
			DispatchAgencyTypeId = view.DispatchAgencyTypeId;
			DispatchAgencyTypeName = view.DispatchAgencyTypeName;
			AccountId = view.AccountId;
			IndustryAccountID = view.IndustryAccountID;
			CsNo = view.CsNo;
			DispatchAgencyName = view.DispatchAgencyName;
			Phone1 = view.Phone1;
			PermitNumber = view.PermitNumber;
			PermitEffectiveDate = view.PermitEffectiveDate;
			PermitExpireDate = view.PermitExpireDate;
			IsVerified = view.IsVerified;
			IsActive = view.IsActive;
		}

		public FnsMsAccountDispatchAgencyAssignmentView() {}

		#endregion .ctor

		public long DispatchAgencyAssignmentID { get; set; }
		public int DispatchAgencyId { get; set; }
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
	}
}
