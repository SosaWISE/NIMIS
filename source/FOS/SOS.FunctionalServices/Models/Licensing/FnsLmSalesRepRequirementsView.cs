using NXS.Data.Licensing;
using SOS.FunctionalServices.Contracts.Models.Licensing;

namespace SOS.FunctionalServices.Models.Licensing
{
	public class FnsLmSalesRepRequirementsView : IFnsLmSalesRepRequirementsView
	{
		#region .ctor

		public FnsLmSalesRepRequirementsView(LM_SalesRepRequirementsView viewItem)
		{
			RequirementID = viewItem.RequirementID;
			RequirementTypeName = viewItem.RequirementTypeName;
			LocationTypeName = viewItem.LocationTypeName;
			RequirementName = viewItem.RequirementName;
			LockID = viewItem.LockID;
			LockTypeName = viewItem.LockTypeName;
			CallCenterMessage = viewItem.CallCenterMessage;
			Status = viewItem.Status;
			LicenseID = viewItem.LicenseID;
		}

		#endregion .ctor

		#region Properties
		public int RequirementID { get; private set; }
		public string RequirementTypeName { get; private set; }
		public string LocationTypeName { get; private set; }
		public string RequirementName { get; private set; }
		public int LockID { get; private set; }
		public string LockTypeName { get; private set; }
		public string CallCenterMessage { get; private set; }
		public string Status { get; private set; }
		public int? LicenseID { get; private set; }
		#endregion Properties
	}
}
