using System;
using NXS.Data.Funding;
using SOS.FunctionalServices.Contracts.Models.Funding;

namespace SOS.FunctionalServices.Models.Funding
{
	public class FnsFeCriteria : IFnsFeCriteria
	{
		#region .ctor

		public FnsFeCriteria(FE_CriteriasView item)
		{
			CriteriaID = item.CriteriaID;
			PurchaserId = item.PurchaserId;
			PurchaserName = item.PurchaserName;
			CriteriaName = item.CriteriaName;
			Description = item.DESCRIPTION;
			FilterString = item.FilterString;
			CreatedBy = item.CreatedBy;
			CreatedOn = item.CreatedOn;
		}

		#endregion .ctor

		#region Properties
		public int CriteriaID { get; set; }
		public string PurchaserId { get; set; }
		public string PurchaserName { get; set; }
		public string CriteriaName { get; set; }
		public string Description { get; set; }
		public string FilterString { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedOn { get; set; }
		#endregion Properties
	}
}
